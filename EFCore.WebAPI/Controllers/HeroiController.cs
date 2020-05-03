using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        private readonly IEFcoreRepository _repo;

        public HeroiController(IEFcoreRepository repo)
        {
            _repo = repo;
        }
        // GET: api/Heroi
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var herois = await _repo.GetAllHerois(true);
                if (herois != null)
                {
                    return Ok(herois);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não existe registros");
        }

        // GET: api/Heroi/5
        [HttpGet("{id}", Name = "GetHeroi")]
        public async Task<IActionResult> GetIdAsync(int id)
        {
            try
            {
                var herois = await _repo.GetHeroiById(id, true);
                if (herois != null)
                {
                    return Ok(herois);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não existe regitros");
        }

        // POST: api/Heroi
        [HttpPost]
        public async Task<IActionResult> PostAsync(Heroi model)
        {
            try
            {
                _repo.Add(model);
                if (await _repo.SaveChangeAsync())
                {
                    return Ok("Correto!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

            return BadRequest("Não salvou!");
        }

        // PUT: api/Heroi/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, Heroi model)
        {
            try
            {
                var herois = await _repo.GetBatalhaById(id);
                if (herois != null)
                {
                    _repo.Update(herois);

                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok("Correto!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não foi alterdo!");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var heroi = await _repo.GetBatalhaById(id);
                if (heroi != null)
                {
                    _repo.Delete(heroi);

                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok("Correto!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não foi deletado");
        }
    }
}
