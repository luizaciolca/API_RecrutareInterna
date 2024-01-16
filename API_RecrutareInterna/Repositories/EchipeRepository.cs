using API_RecrutareInterna.DataContext;
using API_RecrutareInterna.Exceptions;
using API_RecrutareInterna.Helpers;
using API_RecrutareInterna.Models;
using API_RecrutareInterna.Repositories.Interfaces;
using API_RecrutateInterna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using static API_RecrutareInterna.Helpers.ErrorMessageEnum;

namespace API_RecrutareInterna.Repositories
{
    public class EchipeRepository : IEchipeRepository
    {
        public readonly DataRecrutareInternaContext _context; //accesarea bazei de date de alte clase
        public EchipeRepository(DataRecrutareInternaContext context) //constructor repo
        {
            _context = context;
        } //initializarea bd ca un ob specific pt o noua instanta 

        public async Task<IEnumerable<EchipeItem>> GetEchipeAsync()
        {
            return await _context.Echipe.ToListAsync();
        }

        public async Task<EchipeItem> GetEchipaByIdAsync(Guid id)
        {
            return await _context.Echipe.SingleOrDefaultAsync(a => a.ID_Echipa == id);
        }

        public async Task CreateEchipaAsync(EchipeItem echipa)
        {
            echipa.ID_Echipa = Guid.NewGuid();
            bool departamentExists = await DepartamentExists(echipa.Departament);

            if (departamentExists)
            {
                throw new ModelValidationException(ErrorMessageEnum.Echipa.DepartamentExistsError);
            }

            _context.Echipe.Add(echipa);
            await _context.SaveChangesAsync();
        }

        public async Task<EchipeItem> UpdateEchipaAsync(Guid id, EchipeItem echipa)
        {
            if (!await ResponsabilitatiExistsAsync(id))
            {
                return null;
            }

            if (echipa != null)
            {
                _context.Echipe.Update(echipa);
                await _context.SaveChangesAsync();
            }

            return echipa;
        }

        public async Task<bool> DeleteEchipaAsync(Guid id)
        {
            if (!await ResponsabilitatiExistsAsync(id))
            {
                return false;
            }

            _context.Echipe.Remove(new EchipeItem { ID_Echipa = id });
            await _context.SaveChangesAsync();
            return true;


        }

        private async Task<bool> DepartamentExists(string departament)
        {
            return await _context.Echipe.CountAsync(a => a.Departament == departament) > 0;
        }

        private async Task<bool> ResponsabilitatiExistsAsync(Guid id)
        {
            return await _context.Echipe.CountAsync(a => a.ID_Echipa == id) >0 ;
        }

        public Task<EchipeItem> DeleteEchipaAsync(Guid id, EchipeItem echipa)
        {
            throw new NotImplementedException();
        }
    }
}
