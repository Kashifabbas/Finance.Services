using Ciklum.Atea.Finance.Service.Domain.Model;
using Ciklum.ATEA.Finance.Service.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ciklum.ATEA.Finance.Service.WebApi.Controllers
{
    [Authorize]
    public class CountryController : ApiController
    {

        #region " Global Variable "
        private readonly ICountryRepository _Repository;
       
        #endregion

        #region " CompanyController Constructor "
        public CountryController(ICountryRepository hostService)
        {
            _Repository = hostService;
        }
        #endregion

        #region " Get Function "
        // GET api/<controller>
        [AllowAnonymous]
        public List<Ciklum.ATEA.Finance.Service.Domain.DTO.CountryDTO> Get()
        {
            return _Repository.GetAllCountries();
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
        public HttpResponseMessage Post()
        {
            List<Ciklum.ATEA.Finance.Service.Domain.DTO.CountryDTO> data = _Repository.GetAllCountries();
            //Demo
                var response = Request.CreateResponse<List<Ciklum.ATEA.Finance.Service.Domain.DTO.CountryDTO>>(HttpStatusCode.Created, data);
                string uri = Url.Link("DefaultApi", new { id = data });
                response.Headers.Location = new Uri(uri);
                return response;
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