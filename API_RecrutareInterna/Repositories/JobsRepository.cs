using API_RecrutareInterna.DataContext;
using API_RecrutareInterna.Exceptions;
using API_RecrutareInterna.Helpers;
using API_RecrutareInterna.Repositories.Interfaces;
using API_RecrutateInterna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using static API_RecrutareInterna.Helpers.ErrorMessageEnum;

namespace API_RecrutareInterna.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly DataRecrutareInternaContext _context;
        public JobsRepository(DataRecrutareInternaContext context) { _context = context; }


        public async Task<IEnumerable<JobsItem>> GetJobsAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<JobsItem> GetJobByIdAsync(Guid id)
        {
            return await _context.Jobs.SingleOrDefaultAsync(a => a.ID_job == id);
        }

        public async Task CreateJobAsync(JobsItem job)
        {
            job.ID_job = Guid.NewGuid();
            bool pozitieExists = await PozitieExists(job.Pozitie);

            if (pozitieExists)
            {
                throw new ModelValidationException(ErrorMessageEnum.Job.PozitieExistsError);
            }

            ValidationFunctions.ThrowExceptionWhenDateIsNotValid(job.ValidFrom, job.ValidTo);


            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
        }

        public async Task <JobsItem> UpdateJobAsync(Guid id, JobsItem job)
        {
            if (!await ExistJobAsync(id))
            {
                return null;
            }

            if (job != null)
            {
                _context.Jobs.Update(job);
                await _context.SaveChangesAsync();
            }

            return job; 
        }

        public async Task<bool> DeleteJobAsync(Guid id)
        {
            if (!await ExistJobAsync(id)) //verific daca rezultatul met asincrone este fals 
            {
                return false;
            }

            _context.Jobs.Remove(new JobsItem { ID_job = id });
            await _context.SaveChangesAsync();
            return true;
        }


        private async Task<bool> ExistJobAsync(Guid id)

        {

            return await _context.Jobs.CountAsync(a => a.ID_job == id) > 0;
        }

        private async Task<bool> PozitieExists(string pozitie)

        {

            return await _context.Jobs.CountAsync(a => a.Pozitie == pozitie) > 0;
            //daca imi gaseste cel putin un anunt egal cu altul, inseamna ca este true
        }

        public Task<JobsItem> DeleteJobAsync(Guid id, JobsItem job)
        {
            throw new NotImplementedException();
        }


    }
}
