using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly IEFcoreRepository _repo;

        public BatalhaController(IEFcoreRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Batalha
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var herois = await _repo.GetAllBatalha();
                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

        }

        // GET: api/Batalha/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<ActionResult> GetIdAsync(int id)
        {
            try
            {
                var batalhas = await _repo.GetBatalhaById(id,true);
                if (batalhas !=null)
                {
                    return Ok(batalhas);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não encontrado!");
        }

        // POST: api/Batalha
        [HttpPost]
        public async Task<IActionResult> PostAsync(Batalha model)
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

        //PUT: api/Batalha/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Batalha model)
        {
            try
            {
                var batalha = await _repo.GetBatalhaById(id);
                if (batalha != null)
                {
                    _repo.Update(batalha);

                    if (await _repo.SaveChangeAsync())
                        return Ok("Correto!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não foi alterdo!");
        }

        //DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var heroi = await _repo.GetBatalhaById(id);
                if (heroi != null)
                {
                    _repo.Delete(heroi);

                    if (await _repo.SaveChangeAsync())
                        return Ok("Correto!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não deletado");
        }
    }
}