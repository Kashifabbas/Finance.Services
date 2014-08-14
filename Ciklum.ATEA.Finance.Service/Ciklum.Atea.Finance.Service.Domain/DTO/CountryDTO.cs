using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Ciklum.ATEA.Finance.Service.Domain.DTO
{
    [DataContract]
    public class CountryDTO
    {
        [DataMember]
        public Int32 CountryID { get; set; }

        [DataMember]
        public String CountryName { get; set; }

        [DataMember]
        public String CountryDescription { get; set; }

        [DataMember]
        public String CountryMail { get; set; }

        [DataMember]
        public String CountryEditNote { get; set; }

        [DataMember]
        public String CountryCurrency { get; set; }

        [DataMember]
        public Boolean RoundCurrency { get; set; }

        [DataMember]
        public Boolean isEnabled { get; set; }


    }
}
