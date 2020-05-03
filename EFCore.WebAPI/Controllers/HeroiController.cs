using EFCore.Dominio;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        public readonly HeroiContexto _contexto;

        public HeroiController(HeroiContexto contexto)
        {
            _contexto = contexto;
        }

        // GET: api/Heroi
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Heroi());
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

        }

        // GET: api/Heroi/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST: api/Heroi
        [HttpPost]
        public ActionResult Post(Heroi model)
        {
            try
            {
                _contexto.Herois.Add(model);
                _contexto.SaveChanges();

                return Ok("Correto!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT: api/Heroi/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Heroi model)
        {
            try
            {
                if (_contexto.Herois.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                    _contexto.Herois.Update(model);
                    _contexto.SaveChanges();

                    return Ok("Correto!");
                }
                return Ok("Não encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
