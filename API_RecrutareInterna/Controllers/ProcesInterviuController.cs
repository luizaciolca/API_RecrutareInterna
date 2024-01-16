using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API_RecrutateInterna.Models;
using API_RecrutateInterna.Repositories.Interfaces;
using API_RecrutareInterna.Helpers;
using API_RecrutareInterna.Models;

namespace API_RecrutateInterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcesInterviuItemsController : ControllerBase
    {
        private readonly IProcesInterviuRepository _procesInterviuRepository;
        private readonly ILogger<ProcesInterviuItemsController> _logger;

        public ProcesInterviuItemsController(IProcesInterviuRepository procesInterviuRepository, ILogger<ProcesInterviuItemsController> logger)
        {
            _procesInterviuRepository = procesInterviuRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProceseInterviuAsync()
        {
            try
            {
                var proceseInterviu = await _procesInterviuRepository.GetProceseInterviuAsync();

                if (!proceseInterviu.Any())
                {
                    return NoContent();
                }

                return Ok(proceseInterviu);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var procesInterviu = await _procesInterviuRepository.GetProcesInterviuByIdAsync(id);

                if (procesInterviu == null)
                {
                    return NotFound();
                }

                return Ok(procesInterviu);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProcesInterviuItem procesInterviu)
        {
            try
            {
                if (procesInterviu == null)
                {
                    //return BadRequest(ErrorMessageEnum.ProcesInterviu.BadRequest);
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessageEnum.ProcesInterviu.BadRequest);
                }

                //await _procesInterviuRepository.CreateProcesInterviuAsync(procesInterviu);
                //return CreatedAtAction(nameof(GetByIdAsync), new { id = procesInterviu.ID_Proces_interviu }, SuccessMessageEnum.ProcesInterviu.ProcesInterviuAdded);

                await _procesInterviuRepository.CreateProcesInterviuAsync(procesInterviu);
                return Ok(SuccessMessageEnum.ProcesInterviu.ProcesInterviuAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create proces intervui error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] ProcesInterviuItem procesInterviu)
        {
            try
            {
                if (procesInterviu == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessageEnum.ProcesInterviu.BadRequest);
                }

                procesInterviu.ID_Proces_interviu = id;
                var updatedProcesInterviu = await _procesInterviuRepository.UpdateProcesInterviuAsync(id, procesInterviu);

                if (updatedProcesInterviu == null)
                {
                    return NotFound(ErrorMessageEnum.ProcesInterviu.NotFoundById);
                }

                return Ok(SuccessMessageEnum.ProcesInterviu.ProcesInterviuUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update proces intervui error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _procesInterviuRepository.DeleteProcesInterviuAsync(id);

                if (result)
                {
                    _logger.LogInformation($"Procesul de interviu cu id-ul {id} a fost șters.");
                    return Ok(SuccessMessageEnum.ProcesInterviu.ProcesInterviuDeleted);
                }

                return NotFound(ErrorMessageEnum.ProcesInterviu.NotFoundById);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete proces intervui error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
