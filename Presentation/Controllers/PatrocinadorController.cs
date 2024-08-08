using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrocinadorController : Controller
    {

        private readonly IPatrocinadorRepository _patrocinadorRespository;

        public PatrocinadorController(IPatrocinadorRepository patrocinadorRespository)
        {
            _patrocinadorRespository = patrocinadorRespository;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<PatrocinadorModel>>>> GetAll()
        {
            try
            {
                var response = await _patrocinadorRespository.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Response<PatrocinadorModel>>> GetById([FromRoute] int id)
        {
            try
            {
                var response = await _patrocinadorRespository.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<Response<string>>> Post([FromBody] AuthenticationRequest authenticationRequest)
        {
            try
            {
                var response = await _patrocinadorRespository.Authentication(authenticationRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromBody] PatrocinadorModel patrocinador)
        {
            try
            {
                var response = await _patrocinadorRespository.Add(patrocinador);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Response<string>>> Update([FromBody] PatrocinadorModel patrocinador)
        {
            try
            {
                var response = await _patrocinadorRespository.Update(patrocinador);
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
                var response = await _patrocinadorRespository.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
