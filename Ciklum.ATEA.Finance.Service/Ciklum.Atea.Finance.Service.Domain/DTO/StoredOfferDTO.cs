using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Ciklum.ATEA.Finance.Service.Domain.DTO
{
    [DataContract]
    public class StoredOfferDTO
    {
        [DataMember]
        public String UniqueIdentifier { get; set; }

        [DataMember]
        public Int32 ID { get; set; }

        [DataMember]
        public String Submitter { get; set; }

        [DataMember]
        public DateTime SubmittedDate { get; set; }

        [DataMember]
        public String OfferValue { get; set; }

        [DataMember]
        public String Country { get; set; }

        [DataMember]
        public String CustomerName { get; set; }

        [DataMember]
        public String OrganizationNumber { get; set; }

        [DataMember]
        public String CustomerEmail { get; set; }

        [DataMember]
        public String CustomerPhone { get; set; }

        [DataMember]
        public Int32 LeaseTimeMonth { get; set; }

        [DataMember]
        public String CostOfFund { get; set; }

        [DataMember]
        public String Margin { get; set; }

        [DataMember]
        public String FunderMargin { get; set; }

        [DataMember]
        public Double? IRR { get; set; }

        [DataMember]
        public DateTime ApplicableStartDate { get; set; }

        [DataMember]
        public DateTime? ApplicableEndDate { get; set; }

        [DataMember]
        public String EditNote { get; set; }

        [DataMember]
        public String Monthlypayment { get; set; }

        [DataMember]
        public String CountryEmail { get; set; }

        [DataMember]
        public String CountryCurrency { get; set; }


    }
}

