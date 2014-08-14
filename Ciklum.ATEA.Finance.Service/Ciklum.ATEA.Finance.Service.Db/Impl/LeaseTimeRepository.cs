using Ciklum.ATEA.Finance.Service.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciklum.ATEA.Finance.Service.Db.Impl
{
    public class LeaseTimeRepository : Ciklum.Atea.Finance.Service.Domain.Model.ILeaseTimeRepository
    {
        #region " Global Variables "
        private readonly IFinanceLogging lgLogging = FinanceLoggingFactory.GetLoggingHandler(FinanceLoggingFactory.LoggingType.NLog);
        #endregion

        #region " GetAllLeaseTime Function "
        public List<Domain.DTO.LeaseTimeDTO> GetAllLeaseTime()
        {
            List<Ciklum.ATEA.Finance.Service.Domain.DTO.LeaseTimeDTO> lstLeaseTime = new List<Domain.DTO.LeaseTimeDTO>();
            try
            {
                using (var context = new Ciklum.Atea.Finance.Service.Domain.EntityModel.FinanceDBEntities())
                {
                    System.Data.Objects.ObjectResult<Ciklum.Atea.Finance.Service.Domain.EntityModel.LeaseTime_GetAll_Result> lst = context.LeaseTime_GetAll();
                    Domain.DTO.LeaseTimeDTO objLeaseTime = null;
                    foreach (var item in lst)
                    {
                        objLeaseTime = new Domain.DTO.LeaseTimeDTO();
                        objLeaseTime.LeaseTimeID = item.ID;
                        objLeaseTime.LeaseTime = item.Months;
                        objLeaseTime.LeaseTimeDescription = item.Description;
                        lstLeaseTime.Add(objLeaseTime);
                        objLeaseTime = null;
                    }
                }
            }
            catch (Exception ex)
            {
                lgLogging.LogFatalException(ex);
            }
            return lstLeaseTime;
        } 
        #endregion
    }
}
