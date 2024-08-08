using Domain.Base;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidataController : Controller
    {

        private readonly ICandidataRepository _candidataRespository;

        public CandidataController(ICandidataRepository candidataRepository)
        {
            _candidataRespository = candidataRepository;
        }


        [HttpGet]
        public async Task<ActionResult<Response<List<CandidataModel>>>> GetAll()
        {
            try
            {
                var response = await _candidataRespository.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Response<CandidataModel>>> GetById([FromRoute] int id)
        {
            try
            {
                var response = await _candidataRespository.GetById(id);
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
                var response = await _candidataRespository.Authentication(authenticationRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Response<string>>> Post([FromBody] CandidataModel candidata)
        {
            try
            {
                var response = await _candidataRespository.Add(candidata);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Response<string>>> Update([FromBody] CandidataModel candidata)
        {
            try
            {
                var response = await _candidataRespository.Update(candidata);
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
                var response = await _candidataRespository.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
