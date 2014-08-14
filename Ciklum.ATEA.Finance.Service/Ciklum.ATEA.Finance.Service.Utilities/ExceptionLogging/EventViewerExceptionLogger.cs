using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Ciklum.ATEA.Finance.Service.Utilities.ExceptionLogging
{
    public class EventViewerExceptionLogger : IExceptionLogger
    {
        #region " Global Variables "
        private string strSource = "ATEA-VirtualReciption";
        private string strLogType = "ATEA-VirtualReciption";
        private EventLog eLog; 
        #endregion

        #region " EventViewerExceptionLogger Constructor "
        public EventViewerExceptionLogger()
        {
            try
            {
                if (!EventLog.SourceExists(strSource))
                    EventLog.CreateEventSource(strSource, strLogType);
                eLog = new EventLog(strLogType);
                eLog.Source = strSource;
            }
            catch (Exception ex)
            {
                var test = ex.InnerException;
            }
        }
        #endregion

        #region " Log Function "
        public void Log(Exception ex)
        {
            try
            {
                if (ex.InnerException != null)
                {
                    eLog.WriteEntry(ex.InnerException.ToString(), EventLogEntryType.Error);
                }
                else if (ex.Message != null)
                {
                    eLog.WriteEntry(ex.Message.ToString(), EventLogEntryType.Error);
                }
                eLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
            catch (Exception exx)
            {
                var test = exx.InnerException;
            }
        }
        #endregion

        #region " LogInformation Function "
        public void LogInformation(Exception ex)
        {
            try
            {
                if (ex.InnerException != null)
                {
                    eLog.WriteEntry(ex.InnerException.ToString(), EventLogEntryType.Information);
                }
                eLog.WriteEntry(ex.ToString(), EventLogEntryType.Information);
            }
            catch
            {
            }
        }
        #endregion

    }
}
