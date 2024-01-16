using API_RecrutareInterna.Helpers;
using API_RecrutareInterna.Models;
using API_RecrutateInterna.Models;
using API_RecrutateInterna.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace API_RecrutateInterna.Repositories.Interfaces
{
    public interface IProcesInterviuRepository
    {
        Task<IEnumerable<ProcesInterviuItem>> GetProceseInterviuAsync();
        Task<ProcesInterviuItem> GetProcesInterviuByIdAsync(Guid id);
        Task CreateProcesInterviuAsync(ProcesInterviuItem procesInterviu);
        Task<ProcesInterviuItem> UpdateProcesInterviuAsync(Guid id, ProcesInterviuItem procesInterviu);
        Task<bool> DeleteProcesInterviuAsync(Guid id);
    }
}

