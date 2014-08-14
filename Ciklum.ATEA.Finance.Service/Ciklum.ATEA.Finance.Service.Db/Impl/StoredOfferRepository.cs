using Ciklum.ATEA.Finance.Service.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciklum.ATEA.Finance.Service.Db.Impl
{
    public class StoredOfferRepository: Ciklum.Atea.Finance.Service.Domain.Model.IStoredOfferRepository
    {
        #region " Global Variables "
        private readonly IFinanceLogging lgLogging = FinanceLoggingFactory.GetLoggingHandler(FinanceLoggingFactory.LoggingType.NLog);
        #endregion

        #region " CreateStoredOffer Function "
        public String CreateStoredOffer(List<Ciklum.ATEA.Finance.Service.Domain.DTO.StoredOfferDTO> lstStoredOffer)
        {
            String uniqueIdentifier = String.Empty;
            Boolean SendEmail = true;

            if (System.Web.Configuration.WebConfigurationManager.AppSettings["SendEmail"] != null && System.Web.Configuration.WebConfigurationManager.AppSettings["SendEmail"].ToString() != String.Empty)
            {
                SendEmail = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["SendEmail"].ToString());
            }
            try
            {
                using (var context = new Ciklum.Atea.Finance.Service.Domain.EntityModel.FinanceDBEntities())
                {
                    foreach (var storedOfferDTO in lstStoredOffer)
                    {
                        #region " Saving Offer in Database "
                        String Customer = storedOfferDTO.CustomerName == null || storedOfferDTO.CustomerName == String.Empty ? "Customer" : storedOfferDTO.CustomerName;

                        var submitID = context.StoredOffer_Add(storedOfferDTO.Submitter, storedOfferDTO.SubmittedDate, storedOfferDTO.OfferValue, storedOfferDTO.Country, storedOfferDTO.CustomerName, storedOfferDTO.OrganizationNumber, storedOfferDTO.CustomerEmail, storedOfferDTO.CustomerPhone, storedOfferDTO.LeaseTimeMonth, storedOfferDTO.CostOfFund, storedOfferDTO.Margin, storedOfferDTO.FunderMargin, storedOfferDTO.ApplicableStartDate, storedOfferDTO.ApplicableEndDate, storedOfferDTO.EditNote, storedOfferDTO.Monthlypayment).FirstOrDefault().SubmitID;
                        uniqueIdentifier += "'" + storedOfferDTO.UniqueIdentifier + "',";
                        #endregion

                        if (SendEmail)
                        {
                            try
                            {
                                #region " Sending an email to the country email address "
                                if (storedOfferDTO.CountryEmail != null && storedOfferDTO.CountryEmail != String.Empty)
                                {
                                    String body = String.Empty;
                                    //var COF = Convert.ToDouble(storedOfferDTO.CostOfFund).ToString("C3", CultureInfo.CreateSpecificCulture("da-DK"));
                                    //var Margin = Convert.ToDouble(storedOfferDTO.Margin).ToString("C3", CultureInfo.CreateSpecificCulture("da-DK"));
                                    //var FunderMargin = Convert.ToDouble(storedOfferDTO.FunderMargin).ToString("C3", CultureInfo.CreateSpecificCulture("da-DK"));
                                    //var IRR = Convert.ToDouble(storedOfferDTO.IRR).ToString("C3", CultureInfo.CreateSpecificCulture("da-DK"));

                                    body = "<div style='font-family:Arial;font-size:14px;line-height:28px;'>Dear " + Customer +
                                           ",<br/><br/> Your request has been forwarded to ATEA Finance and will be prepared as an offer. " +
                                           "<br/>Here are the details of your request:<br/><br/><table><tr><td><b>Submitter:</b></td><td> " + storedOfferDTO.Submitter +
                                           "</td></tr><tr><td><b>Submit ID:</b></td><td>" + submitID.ToString() +
                                           "</td></tr><tr><td><b>Order value:</b></td><td>" + storedOfferDTO.OfferValue +
                                           "</td></tr><tr><td><b>IRR:</b></td><td> " + storedOfferDTO.IRR +
                                           "</td></tr><tr><td><b>Payment period:</b></td><td> " + storedOfferDTO.LeaseTimeMonth.ToString() +
                                           "</td></tr><tr><td><b>Customer name:</b></td><td> " + storedOfferDTO.CustomerName +
                                           "</td></tr><tr><td><b>Organisation No:</b></td><td> " + storedOfferDTO.OrganizationNumber +
                                           "</td></tr><tr><td><b>Customer email:</b></td><td> " + storedOfferDTO.CustomerEmail +
                                           "</td></tr><tr><td><b>Customer phone:</b></td><td> " + storedOfferDTO.CustomerPhone +
                                           "</td></tr><tr><td><b>Currency:</b></td><td> " + storedOfferDTO.CountryCurrency +
                                           "</td></tr><tr><td><b>Monthly Payment:</b></td><td> " + storedOfferDTO.Monthlypayment +
                                           "</td></tr></table><br/> Thank you</div>";
                                    Utilities.Utilities.Email.SendEmail(storedOfferDTO.CountryEmail, "noreply@atea.dk", "Offer submitted from Finance App", body);
                                    //Utilities.Utilities.Email.SendEmail("kaab@ciklum.com", "noreply@atea.dk", "Offer submitted from Finance App - Admin", body);

                                }
                                #endregion

                                #region " Sending an email to the country email address "
                                if (storedOfferDTO.Submitter != null && storedOfferDTO.Submitter != String.Empty)
                                {
                                    String body = String.Empty;
                                    body = "<div style='font-family:Arial;font-size:14px;line-height:28px;'>Dear " + Customer + 
                                            ",<br/><br/> Your request has been forwarded to ATEA Finance and will be prepared as an offer. "+ 
                                            "<br/>Here are the details of your request:<br/><br/><table><tr><td><b>Submitter:</b></td><td> " + storedOfferDTO.Submitter + 
                                            "</td></tr><tr><td><b>Submit ID:</b></td><td>" + submitID.ToString() + 
                                            "</td></tr><tr><td><b>Order value:</b></td><td>" + storedOfferDTO.OfferValue + 
                                            "</td></tr><tr><td><b>Payment period:</b></td><td> " + storedOfferDTO.LeaseTimeMonth.ToString() + 
                                            "</td></tr><tr><td><b>Customer name:</b></td><td> " + storedOfferDTO.CustomerName + 
                                            "</td></tr><tr><td><b>Organisation No:</b></td><td> " + storedOfferDTO.OrganizationNumber + 
                                            "</td></tr><tr><td><b>Customer email:</b></td><td> " + storedOfferDTO.CustomerEmail + 
                                            "</td></tr><tr><td><b>Customer phone:</b></td><td> " + storedOfferDTO.CustomerPhone + 
                                            "</td></tr><tr><td><b>Currency:</b></td><td> " + storedOfferDTO.CountryCurrency + 
                                            "</td></tr><tr><td><b>Monthly Payment:</b></td><td> " + storedOfferDTO.Monthlypayment + 
                                            "</td></tr></table><br/> Thank you</div>";
                                    Utilities.Utilities.Email.SendEmail(storedOfferDTO.Submitter, "noreply@atea.dk", "Offer submitted from Finance App", body);
                                    //Utilities.Utilities.Email.SendEmail("kaab@ciklum.com", "noreply@atea.dk", "Offer submitted from Finance App - Customer", body);
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lgLogging.LogFatalException(ex);
            }
            return uniqueIdentifier.TrimEnd(',');
        } 
        #endregion
    }
}
