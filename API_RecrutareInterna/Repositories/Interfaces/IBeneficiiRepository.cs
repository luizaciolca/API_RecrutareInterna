using API_RecrutateInterna.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_RecrutateInterna.Repositories.Interfaces
{
    public interface IBeneficiiRepository
    {
        Task<IEnumerable<BeneficiiItem>> GetBeneficiiAsync();
        Task<BeneficiiItem> GetBeneficiiByIdAsync(Guid id);
        Task CreateBeneficiiAsync(BeneficiiItem beneficii);
        Task<BeneficiiItem> UpdateBeneficiiAsync(Guid id, BeneficiiItem beneficii);
        Task<bool> DeleteBeneficiiAsync(Guid id);
    }
}
