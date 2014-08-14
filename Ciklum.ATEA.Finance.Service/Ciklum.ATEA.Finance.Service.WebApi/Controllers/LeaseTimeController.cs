using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ciklum.Atea.Finance.Service.Domain.Model;

namespace Ciklum.ATEA.Finance.Service.WebApi.Controllers
{
     [Authorize]
    public class LeaseTimeController : ApiController
    {
        #region " Global Variable "
        private readonly ILeaseTimeRepository _Repository; 
        #endregion

        #region " LeaseTimeController Constructor "
        public LeaseTimeController(ILeaseTimeRepository hostService)
        {
            _Repository = hostService;
        }
        #endregion

        #region " Get Function "
        // GET api/<controller>
        public List<Ciklum.ATEA.Finance.Service.Domain.DTO.LeaseTimeDTO> Get()
        {
            return _Repository.GetAllLeaseTime();
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
            List<Ciklum.ATEA.Finance.Service.Domain.DTO.LeaseTimeDTO> data = _Repository.GetAllLeaseTime();
            //Demo
            var response = Request.CreateResponse<List<Ciklum.ATEA.Finance.Service.Domain.DTO.LeaseTimeDTO>>(HttpStatusCode.Created, data);
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