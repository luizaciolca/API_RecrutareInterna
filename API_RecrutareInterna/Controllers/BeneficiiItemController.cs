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
    public class BeneficiiItemsController : ControllerBase
    {
        private readonly IBeneficiiRepository _beneficiiRepository;
        private readonly ILogger<BeneficiiItemsController> _logger;

        public BeneficiiItemsController(IBeneficiiRepository beneficiiRepository, ILogger<BeneficiiItemsController> logger)
        {
            _beneficiiRepository = beneficiiRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetBeneficiiAsync()
        {
            try
            {
                var beneficii = await _beneficiiRepository.GetBeneficiiAsync();

                if (!beneficii.Any())
                {
                    return NoContent();
                }

                return Ok(beneficii);
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
                var beneficii = await _beneficiiRepository.GetBeneficiiByIdAsync(id);

                if (beneficii == null)
                {
                    return NotFound();
                }

                return Ok(beneficii);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BeneficiiItem beneficii)
        {
            try
            {
                if (beneficii == null)
                {
                    return BadRequest(ErrorMessageEnum.Beneficii.BadRequest);
                }

                await _beneficiiRepository.CreateBeneficiiAsync(beneficii);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = beneficii.ID_Beneficii }, SuccessMessageEnum.Beneficii.BeneficiiAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create beneficii error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] BeneficiiItem beneficii)
        {
            try
            {
                if (beneficii == null)
                {
                    return BadRequest(ErrorMessageEnum.Beneficii.BadRequest);
                }

                beneficii.ID_Beneficii = id;
                var updatedBeneficii = await _beneficiiRepository.UpdateBeneficiiAsync(id, beneficii);

                if (updatedBeneficii == null)
                {
                    return NotFound(ErrorMessageEnum.Beneficii.NotFoundById);
                }

                return Ok(SuccessMessageEnum.Beneficii.BeneficiiUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update beneficii error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _beneficiiRepository.DeleteBeneficiiAsync(id);

                if (result)
                {
                    _logger.LogInformation($"Beneficiul cu id-ul {id} a fost șters.");
                    return Ok(SuccessMessageEnum.Beneficii.BeneficiiDeleted);
                }

                return NotFound(ErrorMessageEnum.Beneficii.NotFoundById);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete beneficii error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

