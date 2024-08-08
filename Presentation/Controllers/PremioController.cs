using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremioController : Controller
    {

        private readonly IPremioRepository _premioRespository;

        public PremioController(IPremioRepository premioRespository)
        {
            _premioRespository = premioRespository;
        }

        [HttpGet("patrocinador/{id}")]
        public async Task<ActionResult<Response<List<PremioModel>>>> GetAllById([FromRoute] int id)
        {
            try
            {
                var response = await _premioRespository.GetAllById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<PremioModel>>>> GetAll()
        {
            try
            {
                var response = await _premioRespository.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromBody] PremioModel premio)
        {
            try
            {
                var response = await _premioRespository.Add(premio);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Response<string>>> Update([FromBody] PremioModel premio)
        {
            try
            {
                var response = await _premioRespository.Update(premio);
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
                var response = await _premioRespository.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
