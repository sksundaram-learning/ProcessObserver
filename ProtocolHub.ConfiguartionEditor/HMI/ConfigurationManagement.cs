//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CAS.CommServer.ProtocolHub.ConfigurationEditor.Properties;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using UAOOI.ProcessObserver.Configuration;

namespace CAS.CommServer.ProtocolHub.ConfigurationEditor.HMI
{
  public delegate void ConfigIOHandler(Form form);

  public static class ConfigurationManagement
  {
    #region Properties

    public static string ConfigFileName
    {
      get => m_ProtocolHubConfigurationSIngleton;
      set => m_ProtocolHubConfigurationSIngleton = value;
    }

    #endregion Properties

    #region Constructors

    static ConfigurationManagement()
    {
      ProtocolHubConfiguration = new ComunicationNet
      {
        DataSetName = "ComunicationNet",
        Locale = new CultureInfo("en-US"),
        SchemaSerializationMode = SchemaSerializationMode.IncludeSchema
      };
    }

    #endregion Constructors

    #region public

    public static ComunicationNet ProtocolHubConfiguration { get; private set; }

    public static void ClearConfig(Form form)
    {
      if (MessageBox.Show(form, "Clear all data grids???", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        ClearProtocolHubConfiguration(form);
    }

    internal static void SaveDemoProc(Form form)
    {
      MessageBox.Show(Resources.tx_DemoWriteErr, Resources.tx_licenseCap, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    public static void SaveProc(Form form)
    {
      SaveFileDialog saveXMLFileDialog = new SaveFileDialog
      {
        OverwritePrompt = true,
        Filter = "XML files|*.xml",
        DefaultExt = "xml",
        InitialDirectory = CAS.Lib.CodeProtect.InstallContextNames.ApplicationDataPath
      };
      if (!string.IsNullOrEmpty(ConfigurationManagement.ConfigFileName))
        saveXMLFileDialog.FileName = ConfigurationManagement.ConfigFileName;
      switch (saveXMLFileDialog.ShowDialog())
      {
        case DialogResult.OK:
          try
          {
            XML2DataSetIO.writeXMLFile(ProtocolHubConfiguration, saveXMLFileDialog.FileName);
            UpdateFormName(form, saveXMLFileDialog.FileName);
            ConfigFileName = saveXMLFileDialog.FileName;
            ProtocolHubConfiguration.Channels.AcceptChanges();
            ProtocolHubConfiguration.Protocol.AcceptChanges();
            ProtocolHubConfiguration.SerialSetings.AcceptChanges();
            ProtocolHubConfiguration.Station.AcceptChanges();
            ProtocolHubConfiguration.Segments.AcceptChanges();
            ProtocolHubConfiguration.Interfaces.AcceptChanges();
            ProtocolHubConfiguration.Groups.AcceptChanges();
            ProtocolHubConfiguration.Tags.AcceptChanges();
            ProtocolHubConfiguration.TagBit.AcceptChanges();
            ProtocolHubConfiguration.DataBlocks.AcceptChanges();
          }
          catch (Exception e)
          {
            MessageBox.Show("Error", "I cant save file to this location because: " + e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
          break;

        default:
          break;
      }
    }

    public static void ReadConfiguration(Form form)
    {
      OpenFileDialog openFileDialogXMLFile = new OpenFileDialog();
      if ((ProtocolHubConfiguration.Channels.GetChanges() != null)
        || (ProtocolHubConfiguration.Protocol.GetChanges() != null)
        || (ProtocolHubConfiguration.SerialSetings.GetChanges() != null)
        || (ProtocolHubConfiguration.Segments.GetChanges() != null)
        || (ProtocolHubConfiguration.Station.GetChanges() != null)
        || (ProtocolHubConfiguration.Interfaces.GetChanges() != null)
        || (ProtocolHubConfiguration.Groups.GetChanges() != null)
        || (ProtocolHubConfiguration.Tags.GetChanges() != null)
        || (ProtocolHubConfiguration.TagBit.GetChanges() != null)
        || (ProtocolHubConfiguration.DataBlocks.GetChanges() != null))
      {
        if (MessageBox.Show(form, "Save current data?", "Data changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          SaveProc(form);
      }
      openFileDialogXMLFile.InitialDirectory = CAS.Lib.CodeProtect.InstallContextNames.ApplicationDataPath;
      openFileDialogXMLFile.Filter = "XML files|*.xml";
      openFileDialogXMLFile.DefaultExt = ".XML";
      switch (openFileDialogXMLFile.ShowDialog())
      {
        case DialogResult.OK:
          ClearProtocolHubConfiguration(form);
#if UNDOREDO
          RTLib.DataBase.UndoRedo.UndoRedoMenager.SuspendLogging();
#endif
          try
          {
            XML2DataSetIO.readXMLFile(ProtocolHubConfiguration, openFileDialogXMLFile.FileName);
            //int idx = 0;
            //foreach ( ComunicationNet.DataBlocksRow cr in configDataBase.DataBlocks )
            //{
            //  //cr.DatBlockID = idx++;
            //  foreach ( ComunicationNet.TagsRow ctg in cr.GetTagsRows() )
            //    ctg.DatBlockID = cr.DatBlockID;
            //}
          }
          catch (Exception e)
          {
            MessageBox.Show("Error", "I cant load file from this location because: " + e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
          //((Button)sender).Enabled = false;
          ConfigFileName = openFileDialogXMLFile.FileName;
          UpdateFormName(form, openFileDialogXMLFile.FileName);
          ProtocolHubConfiguration.Channels.AcceptChanges();
          ProtocolHubConfiguration.Protocol.AcceptChanges();
          ProtocolHubConfiguration.SerialSetings.AcceptChanges();
          ProtocolHubConfiguration.Station.AcceptChanges();
          ProtocolHubConfiguration.Segments.AcceptChanges();
          ProtocolHubConfiguration.Interfaces.AcceptChanges();
          ProtocolHubConfiguration.Groups.AcceptChanges();
          ProtocolHubConfiguration.Tags.AcceptChanges();
          ProtocolHubConfiguration.TagBit.AcceptChanges();
          ProtocolHubConfiguration.DataBlocks.AcceptChanges();
#if UNDOREDO
          RTLib.DataBase.UndoRedo.UndoRedoMenager.ClearLog();
#endif
          break;

        default:
          break;
      }
    }

    #endregion public

    #region private

    private static string m_ProtocolHubConfigurationSIngleton;

    private static void UpdateFormName(Form form, string filename)
    {
      form.Text = "Network configuration ";
      if (!string.IsNullOrEmpty(filename))
        form.Text += filename;
    }

    /// <summary>
    /// Clears curent configuration dataset
    /// </summary>
    private static void ClearProtocolHubConfiguration(Form form)
    {
      ProtocolHubConfiguration.Clear();
      UpdateFormName(form, null);
      ConfigFileName = null;
    }

    #endregion private
  }
}