using EFCore.Dominio;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Repo
{
    public class EFCoreRepository : IEFcoreRepository
    {
        private readonly HeroiContexto _contexto;

        public EFCoreRepository(HeroiContexto contexto)
        {
            _contexto = contexto;
        }

        public void Add<T>(T entity) where T : class
        {
            _contexto.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _contexto.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _contexto.Update(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _contexto.SaveChangesAsync()) > 0;
        }

        public async Task<Batalha> GetBatalhaById(int id, bool incluirBatalha)
        {
            IQueryable<Batalha> query = _contexto.Batalhas;

            if (incluirBatalha)
            {
                query = query.Include(b => b.HeroisBatalhas).ThenInclude(heroiB => heroiB.Heroi);
            }

            query = query.OrderBy(b => b.Id);

            return await query.FirstOrDefaultAsync(b => b.Id == id);
           
        }

        public async Task<Batalha[]> GetAllBatalha(bool incluirBatalha)
        {
            IQueryable<Batalha> query = _contexto.Batalhas;

            if (incluirBatalha)
            {
                query = query.Include(b => b.HeroisBatalhas).ThenInclude(heroiB => heroiB.Batalha);
            }

            query = query.OrderBy(b => b.Id);

            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<Heroi[]> GetAllHerois(bool incluirBatalha)
        {
            IQueryable<Heroi> query = _contexto.Herois.Include(h => h.Identidade).Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroiB => heroiB.Batalha);
            }

            query = query.OrderBy(h => h.Id);

            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<Heroi[]> GetHeroisByNome(string nome, bool incluirBatalha)
        {
            IQueryable<Heroi> query = _contexto.Herois.Include(h => h.Identidade).Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroiB => heroiB.Batalha);
            }

            query = query.AsNoTracking().Where(h => h.Nome.Contains(nome)).OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Heroi> GetHeroiById(int id, bool incluirBatalha)
        {
            IQueryable<Heroi> query = _contexto.Herois.Include(h => h.Identidade).Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroiB => heroiB.Batalha);
            }

            query = query.OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
