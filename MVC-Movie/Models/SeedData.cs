using AnimeScrapperLibrary;
using Data;
using Microsoft.EntityFrameworkCore;

namespace MVC_Movie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using(var context = new MvcMovieContext(serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
        {
            if(context.Movie.Any() && context.Animes.Any())
            {
                return;
            }
            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Price = 7.99M,
                    Rating = "UA"
                },
                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Price = 8.99M,
                    Rating = "T"
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Price = 9.99M,
                    Rating = "T"
                },
                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "A"
                }
            );

            if(context.Animes.Any())
            {
                return;
            }

            AnimeWebScrapper animeScrapper = new AnimeWebScrapper();
            animeScrapper.Load();

            List<AnimeScrapped> animeScrapped = animeScrapper.AnimeList;

            foreach(var anime in animeScrapped)
            {
                context.Animes.Add(new Anime()
                {
                    Title = anime.Title,
                    ImageURL = anime.ImageURL,
                    Metadata = anime.Metadata,
                    Rating = anime.Rating,
                    Description = anime.Description
                });
            }

            context.SaveChanges();
        }
    }
}
