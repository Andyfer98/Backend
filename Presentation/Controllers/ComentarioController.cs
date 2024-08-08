using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioController(IComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }

        [HttpGet("estudiante/{id}")]
        public async Task<ActionResult<Response<List<ComentarioModel>>>> GetByIdEstudiante([FromRoute] int id)
        {
            try
            {
                var response = await _comentarioRepository.GetByIdEstudiante(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("candidata/{id}")]
        public async Task<ActionResult<Response<List<ComentarioModel>>>> GetByIdCandidata([FromRoute] int id)
        {
            try
            {
                var response = await _comentarioRepository.GetByIdCandidata(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromBody] ComentarioModel comentario)
        {
            try
            {
                var response = await _comentarioRepository.Add(comentario);
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
                var response = await _comentarioRepository.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
