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
    public class BatalhaController : ControllerBase
    {

        public HeroiContexto _contexto;

        public BatalhaController(HeroiContexto contexto)
        {
            _contexto = contexto;
        }

        // GET: api/Batalha
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Batalha());
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }

        }

        // GET: api/Batalha/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public ActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST: api/Batalha
        [HttpPost]
        public ActionResult Post(Batalha model)
        {
            try
            {
                _contexto.Batalhas.Add(model);
                _contexto.SaveChanges();

                return Ok("Correto!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT: api/Batalha/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Batalha model)
        {
            try
            {
                if (_contexto.Batalhas.AsNoTracking().FirstOrDefault(b => b.Id == id) != null)
                {
                    _contexto.Batalhas.Update(model);
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
        public ActionResult Delete(int id)
        {
            //_contexto.Batalhas.Remove();
            //_contexto.SaveChanges();
            return Ok();
        }
    }
}
