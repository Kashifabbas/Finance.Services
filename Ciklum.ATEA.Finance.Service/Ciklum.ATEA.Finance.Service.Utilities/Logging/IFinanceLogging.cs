using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciklum.ATEA.Finance.Service.Utilities.Logging
{
    public interface IFinanceLogging
    {
        void LogInformation(string information);
        void LogInformation(string information, string loggerName);
        void LogFatalException(Exception lastException);
    }
}
