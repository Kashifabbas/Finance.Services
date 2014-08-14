using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ciklum.ATEA.Finance.Service.Domain;
using Ciklum.ATEA.Finance.Service.Utilities.Logging;

namespace Ciklum.ATEA.Finance.Service.Db.Impl
{
    public class CountryRepository: Ciklum.Atea.Finance.Service.Domain.Model.ICountryRepository
    {
        #region " Global Variables "
        private readonly IFinanceLogging lgLogging = FinanceLoggingFactory.GetLoggingHandler(FinanceLoggingFactory.LoggingType.NLog);
        #endregion

        #region " Implementing the GetAllCountries Function "
        public List<Ciklum.ATEA.Finance.Service.Domain.DTO.CountryDTO> GetAllCountries()
        {
            List<Ciklum.ATEA.Finance.Service.Domain.DTO.CountryDTO> lstCountry = new List<Domain.DTO.CountryDTO>();
            try
            {
                using (var context = new Ciklum.Atea.Finance.Service.Domain.EntityModel.FinanceDBEntities())
                {
                    System.Data.Objects.ObjectResult<Ciklum.Atea.Finance.Service.Domain.EntityModel.Country_GetAll_Result> lst = context.Country_GetAll();
                    Domain.DTO.CountryDTO objCountryDTO = null;
                    foreach (var item in lst)
                    {
                        objCountryDTO = new Domain.DTO.CountryDTO();
                        objCountryDTO.CountryID = item.ID;
                        objCountryDTO.CountryName = item.Name;
                        objCountryDTO.CountryDescription = item.Description;
                        objCountryDTO.CountryMail = item.Mail;
                        objCountryDTO.CountryEditNote = item.EditNote;
                        objCountryDTO.CountryCurrency = item.Currency;
                        objCountryDTO.RoundCurrency = item.RoundCurrency;
                        lstCountry.Add(objCountryDTO);
                        objCountryDTO = null;
                    }
                }
            }
            catch (Exception ex)
            {
                lgLogging.LogFatalException(ex);
            }
            return lstCountry;
        }
        #endregion
    }
}
