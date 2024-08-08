using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloRepository _articuloRepository;

        public ArticuloController(IArticuloRepository articuloRepository)
        {
            _articuloRepository = articuloRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ArticuloModel>>> GetById([FromRoute] int id)
        {
            try
            {
                var response = await _articuloRepository.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromBody] ArticuloModel articulo)
        {
            try
            {
                var response = await _articuloRepository.Add(articulo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ArticuloModel articulo)
        {
            var response = await _articuloRepository.Put(articulo);

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
                var response = await _articuloRepository.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<ArticuloModel>>>> GetAll()
        {
            try
            {
                var response = await _articuloRepository.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
