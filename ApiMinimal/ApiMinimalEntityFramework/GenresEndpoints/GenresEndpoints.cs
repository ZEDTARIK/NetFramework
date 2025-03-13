using ApiMinimalEntityFramework.DTOs;
using ApiMinimalEntityFramework.Entities;
using ApiMinimalEntityFramework.Repositories;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace ApiMinimalEntityFramework.GenresEndpoints
{
    public static class GenresEndpoints
    {
        public static RouteGroupBuilder MapGenres(this RouteGroupBuilder group) 
        {
            group.MapGet("/", GetAll).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(30)).Tag("genre-cache"));
            group.MapGet("/{id:int}", GetById);
            group.MapPost("/", Create).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(30)).Tag("genre-cache"));
            group.MapPut("/{id:int}", Update).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(30)).Tag("genre-cache"));
            group.MapDelete("/{id:int}", Delete);
            return group;
        }

        static async Task<Ok<List<GenreDTO>>> GetAll(IRepositoryGenre repository, IMapper mapper)
        {
            var genres = await repository.GetAll();
            var genresDTO = mapper.Map<List<GenreDTO>>(genres); // map entities to DTOs
            return TypedResults.Ok(genresDTO);
        }

        static async Task<Results<NotFound, Ok<GenreDTO>>> GetById(int id,
            IRepositoryGenre repository,
            IMapper mapper)
        {
            var genre = await repository.GetById(id);

            if (genre == null)
            {
                return TypedResults.NotFound();
            }
            var genreDTO = mapper.Map<GenreDTO>(genre); // map entity to DTO
            return TypedResults.Ok(genreDTO);
        }

        static async Task<Results<Created<GenreDTO>, ValidationProblem>> Create(CreateGenreDTO createGenreDTO, 
            IRepositoryGenre repository, 
            IOutputCacheStore outputCacheStore,
            IMapper mapper,
            IValidator<CreateGenreDTO> validator)
        {


            var validationResult = await validator.ValidateAsync(createGenreDTO);
            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }
            var genre = mapper.Map<Genre>(createGenreDTO);
            var id = await repository.Create(genre);
            await outputCacheStore.EvictByTagAsync("genre-cache", CancellationToken.None); // evict cache

            var genreDTO = mapper.Map<GenreDTO>(genre);
            return TypedResults.Created($"/Genres/{id}", genreDTO);
        }
        static async Task<Results<NotFound, NoContent>> Update(int id, 
            CreateGenreDTO createGenreDTO,
            IRepositoryGenre repository,
            IOutputCacheStore outputCacheStore,
            IMapper mapper)
        {
            var exist = await repository.Exist(id);
            if (!exist)
            {
                return TypedResults.NotFound();
            }
            var genre = mapper.Map<Genre>(createGenreDTO);
            genre.Id = id;
            await repository.Update(genre);
            await outputCacheStore.EvictByTagAsync("genre-cache", CancellationToken.None); // evict cache
            return TypedResults.NoContent();
        }

        static async Task<Results<NotFound, NoContent>> Delete(int id,
        IRepositoryGenre repository,
        IOutputCacheStore outputCacheStore)
        {
            if (!await repository.Exist(id))
            {
                return TypedResults.NotFound();
            }
            await repository.Delete(id);
            await outputCacheStore.EvictByTagAsync("genre-cache", CancellationToken.None);    // evict cache
            return TypedResults.NoContent();
        }

    }
}
