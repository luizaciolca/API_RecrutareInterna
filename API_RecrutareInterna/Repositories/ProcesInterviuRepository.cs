using API_RecrutareInterna.DataContext;
using API_RecrutareInterna.Exceptions;
using API_RecrutareInterna.Helpers;
using API_RecrutareInterna.Models;
using API_RecrutareInterna.Repositories.Interfaces;
using API_RecrutateInterna.Models;
using API_RecrutateInterna.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using static API_RecrutareInterna.Helpers.ErrorMessageEnum;
using System.Threading.Tasks;

namespace API_RecrutateInterna.Repositories
{
    public class ProcesInterviuRepository : IProcesInterviuRepository
    {
        private readonly DataRecrutareInternaContext _context;

        public ProcesInterviuRepository(DataRecrutareInternaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProcesInterviuItem>> GetProceseInterviuAsync()
        {
            return await _context.Proces_Interviu.ToListAsync();
        }

        public async Task<ProcesInterviuItem> GetProcesInterviuByIdAsync(Guid id)
        {
            return await _context.Proces_Interviu.SingleOrDefaultAsync(p => p.ID_Proces_interviu == id);
        }

        public async Task CreateProcesInterviuAsync(ProcesInterviuItem procesInterviu)
        {
            procesInterviu.ID_Proces_interviu = Guid.NewGuid();

            _context.Proces_Interviu.Add(procesInterviu);
            await _context.SaveChangesAsync();
        }

        public async Task<ProcesInterviuItem> UpdateProcesInterviuAsync(Guid id, ProcesInterviuItem procesInterviu)
        {
            if (!await ExistProcesInterviuAsync(id))
            {
                return null;
            }

            if (procesInterviu != null)
            {
                _context.Proces_Interviu.Update(procesInterviu);
                await _context.SaveChangesAsync();
            }

            return procesInterviu;
        }

        public async Task<bool> DeleteProcesInterviuAsync(Guid id)
        {
            if (!await ExistProcesInterviuAsync(id))
            {
                return false;
            }

            _context.Proces_Interviu.Remove(new ProcesInterviuItem { ID_Proces_interviu = id });
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ExistProcesInterviuAsync(Guid id)
        {
            return await _context.Proces_Interviu.CountAsync(p => p.ID_Proces_interviu == id) > 0;
        }
    }
}

