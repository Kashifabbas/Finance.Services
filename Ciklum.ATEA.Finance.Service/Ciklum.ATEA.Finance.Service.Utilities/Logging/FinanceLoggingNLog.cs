using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Configuration;
using NLog.Targets;
using NLog.Config;

namespace Ciklum.ATEA.Finance.Service.Utilities.Logging
{
    public class FinanceLoggingNLog : IFinanceLogging
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        #region " LogInformation Function "
        public void LogInformation(string information)
        {
            Boolean isLoggingEnabled = true;
            if (System.Web.Configuration.WebConfigurationManager.AppSettings["IsLoggingEnabled"] != null && System.Web.Configuration.WebConfigurationManager.AppSettings["IsLoggingEnabled"].ToString() != String.Empty)
            {
                isLoggingEnabled = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["IsLoggingEnabled"].ToString());
            }
            if (isLoggingEnabled)
            {
                // Step 1. Create configuration object 
                LoggingConfiguration config = new LoggingConfiguration();

                // Step 2. Create targets and add them to the configuration 
                ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
                config.AddTarget("console", consoleTarget);

                FileTarget fileTarget = new FileTarget();
                config.AddTarget("file", fileTarget);

                // Step 3. Set target properties 
                consoleTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
                fileTarget.FileName = "C:\\VirtualReceiptionLogs\\${shortdate}-VirtualReceiptionLogs.txt";
                fileTarget.Layout = "${level:padding=-6}   ${longdate}    ${logger:shortName=true}    ${message}";

                // Step 4. Define rules
                LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
                config.LoggingRules.Add(rule1);

                LoggingRule rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
                config.LoggingRules.Add(rule2);

                // Step 5. Activate the configuration
                LogManager.Configuration = config;

                logger.Log(new LogEventInfo(LogLevel.Info, "LG Information", information));
            }
        }
        #endregion

        #region " LogInformation with LoggerName Function "
        public void LogInformation(string information, string loggerName)
        {
            Boolean isLoggingEnabled = true;
            if (System.Web.Configuration.WebConfigurationManager.AppSettings["IsLoggingEnabled"] != null && System.Web.Configuration.WebConfigurationManager.AppSettings["IsLoggingEnabled"].ToString() != String.Empty)
            {
                isLoggingEnabled = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["IsLoggingEnabled"].ToString());
            }
            if (isLoggingEnabled)
            {
                // Step 1. Create configuration object 
                LoggingConfiguration config = new LoggingConfiguration();

                // Step 2. Create targets and add them to the configuration 
                ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
                FileTarget fileTarget = new FileTarget();
                // Step 3. Set target properties 
                consoleTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";
                fileTarget.FileName = "C:\\VirtualReceiptionLogs\\${shortdate}-VirtualReceiptionLogs.txt";
                fileTarget.Layout = "${level:padding=-6}   ${longdate}    ${logger:shortName=true}    ${message}";


                LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
                config.LoggingRules.Add(rule1);
                logger.Log(new LogEventInfo(LogLevel.Info, loggerName, information));
            }
        }
        #endregion

        #region " LogFatalException Function "
        public void LogFatalException(Exception lastException)
        {
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            if (lastException.InnerException != null)
            {
                logger.Fatal(lastException.InnerException.ToString(), lastException);
            }
            else
            {
                logger.Fatal("Exception occured", lastException);
            }
        }
        #endregion
    }
}
