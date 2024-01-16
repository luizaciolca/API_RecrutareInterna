using API_RecrutareInterna.DataContext;
using API_RecrutateInterna.Models;
using API_RecrutateInterna.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace API_RecrutateInterna.Repositories
{
    public class ProiectRepository : IProiectRepository
    {
        private readonly DataRecrutareInternaContext _context;

        public ProiectRepository(DataRecrutareInternaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProiectItem>> GetProiecteAsync()
        {
            return await _context.Proiecte.ToListAsync();
        }

        public async Task<ProiectItem> GetProiectByIdAsync(Guid id)
        {
            return await _context.Proiecte.SingleOrDefaultAsync(p => p.ID_Proiecte == id);
        }

        public async Task CreateProiectAsync(ProiectItem proiect)
        {
            proiect.ID_Proiecte = Guid.NewGuid();

            _context.Proiecte.Add(proiect);
            await _context.SaveChangesAsync();
        }

        public async Task<ProiectItem> UpdateProiectAsync(Guid id, ProiectItem proiect)
        {
            if (!await ExistProiectAsync(id))
            {
                return null;
            }

            if (proiect != null)
            {
                _context.Proiecte.Update(proiect);
                await _context.SaveChangesAsync();
            }

            return proiect;
        }

        public async Task<bool> DeleteProiectAsync(Guid id)
        {
            if (!await ExistProiectAsync(id))
            {
                return false;
            }

            _context.Proiecte.Remove(new ProiectItem { ID_Proiecte = id });
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ExistProiectAsync(Guid id)
        {
            return await _context.Proiecte.CountAsync(p => p.ID_Proiecte == id) > 0;
        }
    }
}
