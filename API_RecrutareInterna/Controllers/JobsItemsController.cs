using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_RecrutareInterna.DataContext;
using API_RecrutateInterna.Models;
using API_RecrutareInterna.Repositories;
using Microsoft.Extensions.Logging;
using System.Net;
using API_RecrutareInterna.Repositories.Interfaces;
using static API_RecrutareInterna.Helpers.ErrorMessageEnum;
using API_RecrutareInterna.Helpers;

namespace API_RecrutareInterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsItemsController : ControllerBase
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly ILogger<JobsItemsController> _logger;

        public JobsItemsController(IJobsRepository jobsRepository, ILogger<JobsItemsController> logger)
        {
            _jobsRepository = jobsRepository;
            _logger = logger;

        }

        // GET: api/JobsItems
        [HttpGet]
        public async Task<IActionResult> GetJobsAsync()
        {
            try
            {
                _logger.LogWarning($"GetJob started");
                var jobs = await _jobsRepository.GetJobsAsync();

                if (jobs == null || !jobs.Any())
                {
                    _logger.LogInformation("Nu a fost gasit niciun element");
                    return StatusCode((int)HttpStatusCode.NoContent);

                }

                return Ok(jobs);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        // GET: api/JobsItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var jobs = await _jobsRepository.GetJobByIdAsync(id);


                if (jobs == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }

                return Ok(jobs);
            }

            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        //PUT: api/JobsItems/5
        [HttpPut("{id}")]

        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] JobsItem job)
        //Din ruta se asteapta sa primeasca id-ul, iar din body sa primeasca job-ul
        //binding 
        //datele trimise in body trebuie transformate intr un model corespunzator API

        {
            try
            {
                if (job == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessageEnum.Job.BadRequest);
                }
                job.ID_job = id;
                var updatedJob = await _jobsRepository.UpdateJobAsync(id, job);
                if (updatedJob == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessageEnum.Job.NotFoundById);
                }

                return StatusCode((int)HttpStatusCode.OK, SuccessMessageEnum.Job.JobUpdated);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Create announcement error occurred {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // POST: api/JobsItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobsItem job)
        {
            try
            {
                if (job == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessageEnum.Job.BadRequest);
                }

                await _jobsRepository.CreateJobAsync(job);
                return Ok(SuccessMessageEnum.Job.JobAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create announcement error occurred {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE: api/JobsItems/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromBody] JobsItem job)
        {
            try
            {
                var result = await _jobsRepository.DeleteJobAsync(id);
                if (result)
                {
                    _logger.LogInformation($"A fost sters job-ul cu id-ul {id}");
                    return Ok(SuccessMessageEnum.Job.JobDeleted);
                }

                return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessageEnum.Job.BadRequest);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Eroare {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}





