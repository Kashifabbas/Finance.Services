using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciklum.ATEA.Finance.Service.Utilities.ExceptionLogging
{
    public class ExceptionLoggerFactory
    {
        #region " Global Variables "
        private static volatile ExceptionLoggerFactory instance;
        private static object syncRoot = new Object(); 
        #endregion

        #region " ExceptionLoggerFactory Method "
        static ExceptionLoggerFactory()
        {
        } 
        #endregion

        #region " ExceptionLoggerFactory Constructor "
        private ExceptionLoggerFactory()
        {
        } 
        #endregion

        #region " Instance static Method used for Singleton patter implementation "
        public static ExceptionLoggerFactory Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {

                        instance = new ExceptionLoggerFactory();
                    }
                    return instance;
                }
            }
        } 
        #endregion

        #region " GetEventViewerExceptionLogger Function "
        public IExceptionLogger GetEventViewerExceptionLogger()
        {
            return new EventViewerExceptionLogger();
        } 
        #endregion
    }
}
