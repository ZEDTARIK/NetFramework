using ApiMinimalEntityFramework.Data;
using ApiMinimalEntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiMinimalEntityFramework.Repositories.Imp
{
    public class RepositoryGenre : IRepositoryGenre
    {
        private readonly AppDbContext context;
        public RepositoryGenre(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Create(Genre genre)
        {
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            return genre.Id;
        }

        public async Task Delete(int id)
        {
            await context.Genres.Where(g => g.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> Exist(int id)
        {
            return await context.Genres.AnyAsync(g => g.Id == id);
        }

        public async Task<List<Genre>> GetAll()
        {
            return await context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetById(int id)
        {
            return await context.Genres.FindAsync(id);
        }

        public async Task Update(Genre genre)
        {
            context.Update(genre);
            await context.SaveChangesAsync();
        }
    }
}
