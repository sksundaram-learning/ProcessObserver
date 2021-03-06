//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CAS.CommServer.ProtocolHub.Communication.LicenseControl;
using CAS.CommServer.ProtocolHub.MonitorInterface;
using System;
using System.Collections;
using CommunicationDSC = UAOOI.ProcessObserver.Configuration.ComunicationNet;

namespace CAS.CommServer.ProtocolHub.Communication.BaseStation
{
  internal sealed class Station : Pipe, IStationState
  {
    #region private

    private const string m_Src = "CAS.Lib.CommServer.Station";

    private sealed class Group
    {
      #region private

      private readonly string Name;
      private readonly ushort GroupID;
      private readonly TimeSpan TimeScann;
      private readonly TimeSpan TimeOut;
      private readonly TimeSpan TimeScanFast;
      private readonly TimeSpan TimeOutFast;

      /// <summary>
      /// Summary description for GroupDataDescription.
      /// </summary>
      private class GroupDataDescription : DataQueue.DataDescription
      {
        private Station myStation;
        private Group myGroup;

        internal override TimeSpan TimeScann
        {
          get { if (myStation.highPriority) return myGroup.TimeScanFast; else return myGroup.TimeScann; }
        }

        protected override TimeSpan TimeOut
        {
          get { if (myStation.highPriority) return myGroup.TimeOutFast; else return myGroup.TimeOut; }
        }

        internal GroupDataDescription
          (Group mg, CommunicationDSC.DataBlocksRow myDsc, TimeSpan timeOut, Station myStation, ref int cVConstrain)
          : base(myDsc, timeOut, myStation, myStation, ref cVConstrain)
        {
          myGroup = mg;
          this.myStation = myStation;
        }
      }

      #endregion PRIVATE

      #region PUBLIC

      internal Group(CommunicationDSC.GroupsRow groupsDsc, Station myStation, IList myDataDescriptionsList, ref int cVConstrain)
      {
        ASALicense _ASALicense = new ASALicense();
        Name = groupsDsc.Name;
        GroupID = (ushort)groupsDsc.GroupID;
        TimeOut = new TimeSpan(0, 0, 0, 0, (int)groupsDsc.TimeOut);
        TimeScann = new TimeSpan(0, 0, 0, 0, (int)groupsDsc.TimeScan);
        if (groupsDsc.IsTimeScanFastNull() || !_ASALicense.Licensed)
          TimeScanFast = TimeScann;
        else
          TimeScanFast = new TimeSpan(0, 0, 0, 0, (int)groupsDsc.TimeScanFast);
        if (groupsDsc.IsTimeOutFastNull() || !_ASALicense.Licensed)
          TimeOutFast = TimeOut;
        else
          TimeOutFast = new TimeSpan(0, 0, 0, 0, (int)groupsDsc.TimeOutFast);
        foreach (CommunicationDSC.DataBlocksRow dataBlocksDSC in groupsDsc.GetDataBlocksRows())
        {
          GroupDataDescription group = new GroupDataDescription(this, dataBlocksDSC, TimeOut, myStation, ref cVConstrain);
          group.ResetCounter();
          myDataDescriptionsList.Add(group);
        }
      }//internal Group

      #endregion PUBLIC
    }//class Group

    private ArrayList myGgroupsList = new ArrayList();
    private readonly ArrayList myDataDescriptionsList = new ArrayList();
    private bool highPriority = false;
    private static SortedList createdStations = new SortedList();

    private Station(CommunicationDSC.StationRow currSDsc, ref int cVConstrain)
    {
      createdStations.Add((uint)currSDsc.StationID, this);
      foreach (CommunicationDSC.GroupsRow currGDsc in currSDsc.GetGroupsRows())
        myGgroupsList.Add(new Group(currGDsc, this, myDataDescriptionsList, ref cVConstrain));
      //Statistics
      myStatistics = new Diagnostic.Station(currSDsc)
      {
        priority = highPriority
      };
    }

    #endregion private

    #region IStationState

    void IStationState.ChangeToHighPriority()
    {
      highPriority = true;
      myStatistics.priority = highPriority;
      foreach (DataQueue.DataDescription currDD in myDataDescriptionsList)
        currDD.ChangeTimeout();
      if (NotifyNewTimeScan != null)
        NotifyNewTimeScan(highPriority);
    }

    void IStationState.ChangeToLowPriority()
    {
      highPriority = false;
      myStatistics.priority = highPriority;
      foreach (DataQueue.DataDescription currDD in myDataDescriptionsList)
        currDD.ChangeTimeout();
      if (NotifyNewTimeScan != null)
        NotifyNewTimeScan(highPriority);
    }

    #endregion IStationState

    #region PUBLIC

    protected override IEnumerable GetDataDescriptionList => this.myDataDescriptionsList;

    internal delegate void NotifyProcedure(bool stateOfStation);

    internal event NotifyProcedure NotifyNewTimeScan;

    internal Statistics.StationStatistics getStatistics => myStatistics;

    internal static void SwitchOnDataScanning()
    {
      foreach (DictionaryEntry currStation in createdStations)
        ((Station)currStation.Value).SwitchPipe = true;
    }

    internal static Station FindStation(uint stID)
    {
      return (Station)createdStations[stID];
    }

    internal static void InitStations(CommunicationDSC.StationDataTable myStationDataConfigTab, ref int cVConstrain)
    {
      foreach (CommunicationDSC.StationRow currSDsc in myStationDataConfigTab)
      {
        Station currSt = new Station(currSDsc, ref cVConstrain);
      }
    }

    internal static void InitByStationId
      (CommunicationDSC.StationDataTable myStationDataConfigTab, long StationId, ref int cVConstrain)
    {
      CommunicationDSC.StationRow currSDsc = myStationDataConfigTab.FindByStationID(StationId);
      Station currSt = new Station(currSDsc, ref cVConstrain);
    }

    #endregion PUBLIC
  } //Station
}