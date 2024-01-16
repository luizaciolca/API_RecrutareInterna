using API_RecrutateInterna.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_RecrutateInterna.Repositories.Interfaces
{
    public interface IProiectRepository
    {
        Task<IEnumerable<ProiectItem>> GetProiecteAsync();
        Task<ProiectItem> GetProiectByIdAsync(Guid id);
        Task CreateProiectAsync(ProiectItem proiect);
        Task<ProiectItem> UpdateProiectAsync(Guid id, ProiectItem proiect);
        Task<bool> DeleteProiectAsync(Guid id);
    }
}
