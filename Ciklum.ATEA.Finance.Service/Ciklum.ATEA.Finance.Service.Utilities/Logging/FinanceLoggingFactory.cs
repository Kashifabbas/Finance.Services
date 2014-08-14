using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Diagnostics;

namespace Ciklum.ATEA.Finance.Service.Utilities.Logging
{
    public sealed class FinanceLoggingFactory
    {
        private static FinanceLoggingNLog nLogging = null;

        #region " GetLoggingHandler Function "
        public static IFinanceLogging GetLoggingHandler(LoggingType handlerType)
        {
            switch (handlerType)
            {
                case LoggingType.NLog:
                    {
                        if (nLogging == null)
                        {
                            nLogging = new FinanceLoggingNLog();
                            return nLogging;
                        }
                        break;
                    }
                default:
                    {
                        if (nLogging == null)
                        {
                            nLogging = new FinanceLoggingNLog();
                            return nLogging;
                        }
                        break;
                    }
            }
            return nLogging;
        }
        #endregion

        #region " Logging Enumeration "

        public enum LoggingType
        {
            EventLog = 0,
            NLog = 1
        }

        #endregion Logging Enumeration
    }
}
