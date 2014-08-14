using Ciklum.ATEA.Finance.Service.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciklum.ATEA.Finance.Service.Db.Impl
{
    public class InterestRateRepository: Ciklum.Atea.Finance.Service.Domain.Model.IInterestRateRepository
    {
        #region " Global Variables "
        private readonly IFinanceLogging lgLogging = FinanceLoggingFactory.GetLoggingHandler(FinanceLoggingFactory.LoggingType.NLog);
        #endregion

        #region " GetAllInterestRate Function "
        public List<Domain.DTO.InterestRateDTO> GetAllInterestRate()
        {
            List<Ciklum.ATEA.Finance.Service.Domain.DTO.InterestRateDTO> lstInterestRate = new List<Domain.DTO.InterestRateDTO>();
            try
            {
                using (var context = new Ciklum.Atea.Finance.Service.Domain.EntityModel.FinanceDBEntities())
                {
                    System.Data.Objects.ObjectResult<Ciklum.Atea.Finance.Service.Domain.EntityModel.InterestRate_GetAll_Result> lst = context.InterestRate_GetAll();
                    Domain.DTO.InterestRateDTO objInterestRate = null;
                    foreach (var item in lst)
                    {
                        if (item.IsActive == 0)
                        {
                            continue;
                        }
                        objInterestRate = new Domain.DTO.InterestRateDTO();
                        objInterestRate.CountryLeaseTimeID = item.CountyLeaseTimeID;
                        objInterestRate.CountryID = item.CountryID;
                        objInterestRate.CountryName = item.CountryName;
                        objInterestRate.CountryCurrency = item.CountryCurrency;
                        objInterestRate.RoundCurrency = item.RoundCurrency;
                        objInterestRate.CountryEmail = item.CountryEmail;
                        objInterestRate.LeaseTimeID = item.LeaseTimeID;
                        objInterestRate.LeaseTime = item.LeaseTime.ToString();
                        objInterestRate.CostOfFund = item.CostOfFunds == String.Empty ? "0" : item.CostOfFunds;
                        objInterestRate.Margin = item.Margin == String.Empty ? "0" : item.Margin; ;
                        objInterestRate.FunderMargin = item.FunderMargin == null || item.FunderMargin == String.Empty ? "0" : item.FunderMargin; ;
                        objInterestRate.StartDate = item.StartDate;
                        objInterestRate.IRR = item.IRR;
                        objInterestRate.EndDate = item.EndDate;
                        objInterestRate.IsActive = item.IsActive == 0 ? false : true;
                        lstInterestRate.Add(objInterestRate);
                        objInterestRate = null;
                    }
                }
            }
            catch (Exception ex)
            {
                lgLogging.LogFatalException(ex);
            }
            return lstInterestRate;
        } 
        #endregion
    }
}
