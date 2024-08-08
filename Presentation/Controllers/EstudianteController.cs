using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : Controller
    {

        private readonly IEstudianteRepository _estudianteRespository;

        public EstudianteController(IEstudianteRepository estudianteRespository)
        {
            _estudianteRespository = estudianteRespository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<EstudianteModel>>> GetById([FromRoute] int id)
        {
            try
            {
                var response = await _estudianteRespository.GetById(id);
                return Ok(response);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<Response<string>>> Post([FromBody] AuthenticationRequest authenticationRequest)
        {
            try
            {
                var response = await _estudianteRespository.Authentication(authenticationRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromBody] EstudianteModel estudiante)
        {
            try
            {
                var response = await _estudianteRespository.Add(estudiante);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response<string>>> Put([FromBody] EstudianteModel estudiante)
        {
            try
            {
                var response = await _estudianteRespository.Put(estudiante);
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
                var response = await _estudianteRespository.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
