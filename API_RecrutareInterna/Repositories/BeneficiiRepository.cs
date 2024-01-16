using API_RecrutateInterna.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using API_RecrutareInterna.DataContext;
using API_RecrutareInterna.Exceptions;
using API_RecrutareInterna.Helpers;
using API_RecrutateInterna.Models;

namespace API_RecrutateInterna.Repositories
{
    public class BeneficiiRepository : IBeneficiiRepository
    {
        private readonly DataRecrutareInternaContext _context;

        public BeneficiiRepository(DataRecrutareInternaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BeneficiiItem>> GetBeneficiiAsync()
        {
            return await _context.Beneficii.ToListAsync();
        }

        public async Task<BeneficiiItem> GetBeneficiiByIdAsync(Guid id)
        {
            return await _context.Beneficii.SingleOrDefaultAsync(b => b.ID_Beneficii == id);
        }

        public async Task CreateBeneficiiAsync(BeneficiiItem beneficii)
        {
            beneficii.ID_Beneficii = Guid.NewGuid();
            bool titluExists = await TitluExists(beneficii.Titlu);

            if (titluExists)
            {
                throw new ModelValidationException(ErrorMessageEnum.Beneficii.TitluExistsError);
            }

            _context.Beneficii.Add(beneficii);
            await _context.SaveChangesAsync();
        }

        public async Task<BeneficiiItem> UpdateBeneficiiAsync(Guid id, BeneficiiItem beneficii)
        {
            if (!await ExistBeneficiiAsync(id))
            {
                return null;
            }

            if (beneficii != null)
            {
                _context.Beneficii.Update(beneficii);
                await _context.SaveChangesAsync();
            }

            return beneficii;
        }

        public async Task<bool> DeleteBeneficiiAsync(Guid id)
        {
            if (!await ExistBeneficiiAsync(id))
            {
                return false;
            }

            _context.Beneficii.Remove(new BeneficiiItem { ID_Beneficii = id });
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ExistBeneficiiAsync(Guid id)
        {
            return await _context.Beneficii.CountAsync(b => b.ID_Beneficii == id) > 0;
        }

        private async Task<bool> TitluExists(string titlu)
        {
            return await _context.Beneficii.CountAsync(b => b.Titlu == titlu) > 0;
        }
    }
}

