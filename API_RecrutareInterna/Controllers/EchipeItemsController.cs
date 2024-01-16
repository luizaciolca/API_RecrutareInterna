using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_RecrutareInterna.DataContext;
using API_RecrutareInterna.Models;
using API_RecrutareInterna.Repositories.Interfaces;
using API_RecrutareInterna.Repositories;
using Microsoft.Extensions.Logging;
using API_RecrutareInterna.Helpers;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace API_RecrutareInterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EchipeItemsController : ControllerBase
    {
        private readonly IEchipeRepository _echipeRepository;
        private readonly ILogger<EchipeItemsController> _logger;

        public EchipeItemsController(ILogger<EchipeItemsController> logger, IEchipeRepository echipeRepository)
        {
            _logger = logger;
            _echipeRepository = echipeRepository;
        }

        // GET: api/EchipeItems
        [HttpGet]
        public async Task<IActionResult> GetEchipeAsync()
        {
            try
            {
                _logger.LogWarning($"Functia a inceput");
                var echipe = await _echipeRepository.GetEchipeAsync();

                if (echipe == null || !echipe.Any())
                {
                    _logger.LogInformation("Nu a fost gasit niciun departament");
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessageEnum.Echipa.NotFound);
                }

                var echipa = echipe.OrderBy(c => c.Data_Infiintare).ToList();

                return Ok(echipa);
            }

            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/EchipeItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var echipa = await _echipeRepository.GetEchipaByIdAsync(id);
                if (echipa == null)
                {
                    _logger.LogInformation(ErrorMessageEnum.Echipa.NotFoundById);
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessageEnum.Echipa.NotFoundById);
                }
                return Ok(echipa);
            }
            catch (Exception ex) { return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); }
        }


        // PUT: api/EchipeItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] EchipeItem echipa)
        {
            try
            {
                if (echipa == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessageEnum.Echipa.BadRequest);
                }

                echipa.ID_Echipa = id;
                var updatedEchipa = await _echipeRepository.UpdateEchipaAsync(id, echipa);
                if (updatedEchipa == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, ErrorMessageEnum.Echipa.NotFoundById);
                }
                return StatusCode((int)HttpStatusCode.OK, SuccessMessageEnum.Echipa.EchipaUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create announcement error occurred {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // POST: api/EchipeItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EchipeItem echipa)
        {
            try
            {
                if (echipa == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessageEnum.Echipa.BadRequest);
                }

                await _echipeRepository.CreateEchipaAsync(echipa);
                return Ok(SuccessMessageEnum.Echipa.EchipaAdded);
            }

            catch (Exception ex)
            {
                _logger.LogError($"A aparut o eroare {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE: api/EchipeItems/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var result = await _echipeRepository.DeleteEchipaAsync(id);
                if (result)
                {
                    _logger.LogInformation($"A fost sters departamentul cu id-ul {id}");
                    return Ok(SuccessMessageEnum.Echipa.EchipaDeleted);
                }

                return StatusCode((int)HttpStatusCode.BadRequest, ErrorMessageEnum.Echipa.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteAnnouncement error {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

