 using API_RecrutareInterna.Models;

namespace API_RecrutareInterna.Repositories.Interfaces
{
    public interface IEchipeRepository
    {
        Task<IEnumerable<EchipeItem>> GetEchipeAsync();
        Task<EchipeItem> GetEchipaByIdAsync (Guid id);
        Task CreateEchipaAsync (EchipeItem echipa);

        Task<EchipeItem> UpdateEchipaAsync(Guid id, EchipeItem echipa);
        Task<EchipeItem> DeleteEchipaAsync(Guid id, EchipeItem echipa);
        Task<bool> DeleteEchipaAsync(Guid id);

    }
}
