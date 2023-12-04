
using API_RecrutateInterna.Models;
using static API_RecrutareInterna.Helpers.ErrorMessageEnum;

namespace API_RecrutareInterna.Repositories.Interfaces
{
    public interface IJobsRepository
    {
        public Task<IEnumerable<JobsItem>> GetJobsAsync();
        Task<JobsItem> GetJobByIdAsync(Guid id);
        Task CreateJobAsync(JobsItem job);

        Task<JobsItem> UpdateJobAsync(Guid id, JobsItem job);

       // Task<JobsItem> UpdatePartiallyJobAsync(Guid id, JobsItemPatch job);

         Task<JobsItem> DeleteJobAsync(Guid id, JobsItem job);

         Task<bool> DeleteJobAsync(Guid id);


    }
}
