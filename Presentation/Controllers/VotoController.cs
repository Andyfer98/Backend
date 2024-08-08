using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
          private readonly IVotoRepository _votoRepository;

        public VotoController(IVotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
        }


        [HttpGet("estudiante/{id}")]
        public async Task<ActionResult<Response<VotoModel>>> GetByIdEstudiante([FromRoute] int id)
        {
            try
            {
                var response = await _votoRepository.GetByIdEstudiante(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("candidata/{id}")]
        public async Task<ActionResult<Response<List<VotoModel>>>> GetByIdCandidata([FromRoute] int id)
        {
            try
            {
                var response = await _votoRepository.GetByIdCandidata(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromBody] VotoModel voto)
        {
            try
            {
                var response = await _votoRepository.Add(voto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> Delete([FromRoute] int id)
        {
            try
            {
                var response = await _votoRepository.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
