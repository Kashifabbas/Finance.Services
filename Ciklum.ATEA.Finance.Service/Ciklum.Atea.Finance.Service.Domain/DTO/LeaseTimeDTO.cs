using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Ciklum.ATEA.Finance.Service.Domain.DTO
{
    [DataContract]
    public class LeaseTimeDTO
    {
        [DataMember]
        public Int32 LeaseTimeID { get; set; }

        [DataMember]
        public Int32 LeaseTime { get; set; }

        [DataMember]
        public String LeaseTimeDescription { get; set; }
    }
}
