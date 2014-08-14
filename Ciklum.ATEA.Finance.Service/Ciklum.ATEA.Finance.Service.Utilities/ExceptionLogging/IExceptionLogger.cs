using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciklum.ATEA.Finance.Service.Utilities.ExceptionLogging
{
    public interface IExceptionLogger
    {
        void Log(Exception ex);
        void LogInformation(Exception ex);
    }
}
