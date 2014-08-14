using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciklum.Atea.Finance.Service.Domain.Model
{
    public interface IInterestRateRepository
    {
        List<Ciklum.ATEA.Finance.Service.Domain.DTO.InterestRateDTO> GetAllInterestRate();
    }
}
