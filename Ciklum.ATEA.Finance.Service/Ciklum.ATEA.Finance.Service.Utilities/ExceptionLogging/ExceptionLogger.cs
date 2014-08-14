using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Ciklum.ATEA.Finance.Service.Utilities.ExceptionLogging
{
    public static class ExceptionLogger
    {
        #region " Log Function "
        public static void Log(Exception ex)
        {
            try
            {
                EventLog eLog = new EventLog("ATEA-VirtualReciption");
                eLog.Source = "ATEA-VirtualReciption";
                var line = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                if (ex.InnerException != null)
                {
                    eLog.WriteEntry(ex.InnerException.ToString(), EventLogEntryType.Error);
                }
            }
            catch
            {
            }
        }
        #endregion

        #region " LogInformation Function "
        public static void LogInformation(Exception ex)
        {
            try
            {
                EventLog eLog = new EventLog("ATEA-VirtualReciption");
                eLog.Source = "ATEA-VirtualReciption";
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
