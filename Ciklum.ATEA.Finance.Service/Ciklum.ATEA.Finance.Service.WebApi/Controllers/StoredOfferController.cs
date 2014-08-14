using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ciklum.Atea.Finance.Service.Domain.Model;
using Ciklum.ATEA.Finance.Service.Utilities.Logging;
using System.Web.Http.Cors;

namespace Ciklum.ATEA.Finance.Service.WebApi.Controllers
{
    [Authorize]
    public class StoredOfferController : ApiController
    {
         #region " Global Variable "
        private readonly IStoredOfferRepository _Repository;
        private readonly IFinanceLogging lgLogging = FinanceLoggingFactory.GetLoggingHandler(FinanceLoggingFactory.LoggingType.NLog);
        #endregion

        #region " StoredOfferController Constructor "
        public StoredOfferController(IStoredOfferRepository hostService)
        {
            _Repository = hostService;
        }
        #endregion

        #region " Get Function "
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        } 
        #endregion

        #region " Parameterized Get Function "
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        } 
        #endregion

        #region " Post Function "
        // POST api/<controller>
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage Post(List<Ciklum.ATEA.Finance.Service.Domain.DTO.StoredOfferDTO> lstStoredOffer)
        {

            try
            {
                //List<Ciklum.ATEA.Finance.Service.Domain.DTO.StoredOfferDTO> data = 
                String commaSeparatedUniqueKeys = _Repository.CreateStoredOffer(lstStoredOffer);
                var response = Request.CreateResponse<string>(HttpStatusCode.Created, commaSeparatedUniqueKeys);
                string uri = Url.Link("DefaultApi", new { id = "Created Successfully." });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch(Exception ex)
            {
                lgLogging.LogInformation("Error occurred " + ex.ToString());
                var response = Request.CreateResponse<string>(HttpStatusCode.ExpectationFailed, "Error Occurred." + ex.ToString());
                string uri = Url.Link("DefaultApi", new { id = "Error Occurred." });
                response.Headers.Location = new Uri(uri);
                lgLogging.LogFatalException(ex);
                return response;
            }
        } 
        #endregion

        #region " Put Function "
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        } 
        #endregion

        #region " Delete Function "
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        } 
        #endregion
    }
}