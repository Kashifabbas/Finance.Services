using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Ciklum.ATEA.Finance.Service.Domain.DTO
{
    [DataContract]
    public class InterestRateDTO
    {
        [DataMember]
        public Int32 CountryLeaseTimeID { get; set; }

        [DataMember]
        public String CountryName { get; set; }

        [DataMember]
        public Int32 CountryID { get; set; }

        [DataMember]
        public String CountryCurrency { get; set; }

        [DataMember]
        public Boolean RoundCurrency { get; set; }

        [DataMember]
        public String CountryEmail { get; set; }

        [DataMember]
        public String LeaseTime { get; set; }

        [DataMember]
        public Int32 LeaseTimeID { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public String Margin { get; set; }

        [DataMember]
        public String FunderMargin { get; set; }

        [DataMember]
        public String CostOfFund { get; set; }

        [DataMember]
        public Double? IRR { get; set; }

        [DataMember]
        public Boolean IsActive { get; set; }
    }
}
