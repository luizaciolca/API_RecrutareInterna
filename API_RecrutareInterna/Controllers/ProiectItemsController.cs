using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API_RecrutateInterna.Models;
using API_RecrutateInterna.Repositories.Interfaces;
using API_RecrutareInterna.Helpers;

namespace API_RecrutateInterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProiectItemsController : ControllerBase
    {
        private readonly IProiectRepository _proiectRepository;
        private readonly ILogger<ProiectItemsController> _logger;

        public ProiectItemsController(IProiectRepository proiectRepository, ILogger<ProiectItemsController> logger)
        {
            _proiectRepository = proiectRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProiecteAsync()
        {
            try
            {
                var proiecte = await _proiectRepository.GetProiecteAsync();

                if (!proiecte.Any())
                {
                    return NoContent();
                }

                return Ok(proiecte);
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
                var proiect = await _proiectRepository.GetProiectByIdAsync(id);

                if (proiect == null)
                {
                    return NotFound();
                }

                return Ok(proiect);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProiectItem proiect)
        {
            try
            {
                if (proiect == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessageEnum.Proiect.BadRequest);
                }

                await _proiectRepository.CreateProiectAsync(proiect);
                return Ok(SuccessMessageEnum.Proiect.ProiectAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create proiect error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] ProiectItem proiect)
        {
            try
            {
                if (proiect == null)
                {
                    return BadRequest(ErrorMessageEnum.Proiect.BadRequest);
                }

                proiect.ID_Proiecte = id;
                var updatedProiect = await _proiectRepository.UpdateProiectAsync(id, proiect);

                if (updatedProiect == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessageEnum.Proiect.NotFoundById);
                }

                return StatusCode((int)HttpStatusCode.OK, SuccessMessageEnum.Proiect.ProiectUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update proiect error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _proiectRepository.DeleteProiectAsync(id);

                if (result)
                {
                    _logger.LogInformation($"Proiectul cu id-ul {id} a fost șters.");
                    return Ok(SuccessMessageEnum.Proiect.ProiectDeleted);
                }

                return NotFound(ErrorMessageEnum.Proiect.NotFoundById);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete proiect error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
