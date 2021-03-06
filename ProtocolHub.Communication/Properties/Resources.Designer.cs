﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CAS.CommServer.ProtocolHub.Communication.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CAS.CommServer.ProtocolHub.Communication.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to register channel for communication with CommServer Monitor (reason: {0}). 
        ///
        ///This error may occur when multiple CommServer instances are launched (this is not recommended). It is recommended to check DCOM settings to prevent launching many instances of CommServer.
        ///
        ///Please also check &quot;ConsoleRemotingHTTPport&quot; configuration parameter, that indicates TCP port number that is used for communication with CommServer Monitor, maybe another application utilize this port..
        /// </summary>
        public static string ConsoleInterface_ChannelRegistrationError {
            get {
                return ResourceManager.GetString("ConsoleInterface_ChannelRegistrationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to During report creation the following error has occurred: .
        /// </summary>
        public static string ExceptionDuringReportCreation {
            get {
                return ResourceManager.GetString("ExceptionDuringReportCreation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are {0} processes inside Watchdog..
        /// </summary>
        public static string InsideWatchdogMessageFormat {
            get {
                return ResourceManager.GetString("InsideWatchdogMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New maximum delay = {0} mS reached at {1} while calling {2}.
        /// </summary>
        public static string MaxDelayMessageFormat {
            get {
                return ResourceManager.GetString("MaxDelayMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Assertion during read from dataAddress: {0}|{1}|{2} , pipeInterface.address {3}, case {4}..
        /// </summary>
        public static string ReadWriteOperationsReadDataAssertion {
            get {
                return ResourceManager.GetString("ReadWriteOperationsReadDataAssertion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to I had to reboot the system at: {0} while calling {1}.
        /// </summary>
        public static string RestartMessageFormat {
            get {
                return ResourceManager.GetString("RestartMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AL_ReadData_Result.ALRes_DatTransferErrr occurred during myMachine.myProtocol.ReadData in SegmentStateMachine (interface: number {0} address {1}).
        /// </summary>
        public static string TraceALRes_DatTransferEr {
            get {
                return ResourceManager.GetString("TraceALRes_DatTransferEr", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Interface was switched off due to communication error.
        /// </summary>
        public static string TraceCommunicationError {
            get {
                return ResourceManager.GetString("TraceCommunicationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Segment connection failure.
        /// </summary>
        public static string TraceConnectionError {
            get {
                return ResourceManager.GetString("TraceConnectionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception thrown by a DataProvider has beet catch: {0}  at {1} with call stack {2}.
        /// </summary>
        public static string TraceDataProviderError {
            get {
                return ResourceManager.GetString("TraceDataProviderError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to State = State{0} entered..
        /// </summary>
        public static string TraceStateEntered {
            get {
                return ResourceManager.GetString("TraceStateEntered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stopwatch expected value should be 0, but the value is {0} ms..
        /// </summary>
        public static string WatchdogStopwatchIsNotZeroMessageFormat {
            get {
                return ResourceManager.GetString("WatchdogStopwatchIsNotZeroMessageFormat", resourceCulture);
            }
        }
    }
}
