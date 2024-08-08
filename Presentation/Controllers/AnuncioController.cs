using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioRepository _anuncioRepository;

        public AnuncioController(IAnuncioRepository anuncioRepository)
        {
            _anuncioRepository = anuncioRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<AnuncioModel>>> GetById([FromRoute] int id)
        {
            try
            {
                var response = await _anuncioRepository.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromBody] AnuncioModel anuncio)
        {
            try
            {
                var response = await _anuncioRepository.Add(anuncio);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> Put(AnuncioModel anuncio)
        {
            var response = await _anuncioRepository.Put(anuncio);

            if (response.Status == 404)
            {
                return NotFound(response.Data);
            }

            if (response.Status == 400)
            {
                return BadRequest(response.Data);
            }

            return Ok(response.Data);
        }
        

        /*
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<string>>> Update([FromBody] AnuncioModel anuncio)
        {
            try
            {
                var response = await _anuncioRepository.Put(anuncio);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        */

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> Delete([FromRoute] int id)
        {
            try
            {
                var response = await _anuncioRepository.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<AnuncioModel>>>> GetAll()
        {
            try
            {
                var response = await _anuncioRepository.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }      

    }
}
