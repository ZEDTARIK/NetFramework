using ApiMinimalEntityFramework.Entities;

namespace ApiMinimalEntityFramework.Repositories
{
    public interface IRepositoryGenre
    {
        Task<int> Create(Genre genre);
        Task<Genre?> GetById(int id);
        Task<List<Genre>> GetAll();
        Task<bool> Exist(int id);
        Task Update(Genre genre);

        Task Delete(int id);
    }
}
