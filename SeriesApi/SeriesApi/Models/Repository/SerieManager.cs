using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Models.EntityFramework;

namespace SeriesApi.Models.Repository
{
    public class SerieManager : IDataRepository<Serie>
    {
        private readonly SeriesDbContext seriesDbContext;

        public SerieManager() { }

        public SerieManager(SeriesDbContext context)
        {
            seriesDbContext = context;
        }
        public Task AddAsync(Serie entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Serie entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<Serie>>> GetAllAsync()
        {
            return await seriesDbContext.Series.Include(s => s.NotesSerie).ToListAsync();
        }

        public async Task<ActionResult<Serie>> GetByIdAsync(int id)
        {
            return await seriesDbContext.Series.Include(s => s.NotesSerie).FirstOrDefaultAsync(s => s.SerieId == id);
        }

        public async Task<ActionResult<IEnumerable<Serie>>> GetAllByStringAsync(string title)
        {
            return await seriesDbContext.Series.Include(s => s.NotesSerie).ThenInclude(n => n.UtilisateurNotant)
                .Where(s => s.Titre.ToUpper().Contains(title.ToUpper())).ToListAsync();
        }

        public Task UpdateAsync(Serie entityToUpdate, Serie entity)
        {
            throw new NotImplementedException();
        }

        Task<ActionResult<Serie>> IDataRepository<Serie>.GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }
    }
}
