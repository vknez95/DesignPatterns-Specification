using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Specification.Interfaces;
using Specification.Models;

namespace Specification.Infrastructure
{
    public class SongRepository : ISongRepository
    {
        private readonly AppDbContext _dbContext;

        public SongRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Song> List(ISpecification<Song> specification)
        {
            var songs = _dbContext.Songs
                .Include("GenreAssignments")
                .Include("Album")
                .Where(specification.Criteria)
                .AsEnumerable();

            var songGenres = songs.SelectMany(s => s.GenreAssignments.Select(sa => sa.Genre));

            if (songGenres.Any(s => s == null))
            {
                songs.AddGenres(AllGenres());
            }

            return songs;
        }

        //public IQueryable<Song> List()
        //{
        //    return _dbContext.Songs
        //        .Include("Genres")
        //        .Include("Album")
        //        .AsQueryable();
        //}

        public Song GetById(int id)
        {
            return _dbContext.Songs.Find(id);
        }

        public void Add(Song song)
        {
            _dbContext.Songs.Add(song);
            _dbContext.SaveChanges();
        }

        // Take a predicate - may or may not work with EF
        // Tends to bleed query logic into clients
        public IEnumerable<Song> List(Func<Song, bool> predicate)
        {
            return _dbContext.Songs.Where(predicate);
        }

        // More Query Methods
        public IEnumerable<Song> ListForAlbumId(int albumId)
        {
            return _dbContext.Songs.Where(s => s.AlbumId == albumId);
        }

        public IEnumerable<Song> ListForArtist(string artistName)
        {
            return _dbContext.Songs.Where(s => s.Artist == artistName);
        }

        public IEnumerable<Song> ListForYear(int year)
        {
            return _dbContext.Songs.Where(s => s.Year == year);
        }

        public IEnumerable<string> AllArtists()
        {
            return _dbContext.Songs.Select(s => s.Artist).Distinct();
        }

        public IEnumerable<Genre> AllGenres()
        {
            return _dbContext.Genres;
        }

        public Genre GetGenreById(int id)
        {
            return _dbContext.Genres.Where(g => g.Id == id).FirstOrDefault();
        }
    }

    public static class SongExtensions {
        public static void AddGenres(this IEnumerable<Song> songs, IEnumerable<Genre> allGenres)
        {
            var songsWithGenres = new List<Song>();

            foreach (var song in songs)
            {
                var songWithGenres = new Song();
                
                songWithGenres.Id = song.Id;
                songWithGenres.Title = song.Title;
                songWithGenres.Artist = song.Artist;
                songWithGenres.Length = song.Length;
                songWithGenres.Year = song.Year;
                songWithGenres.AlbumId = song.AlbumId;
                songWithGenres.Rating = song.Rating;
                songWithGenres.Album = song.Album;
                songWithGenres.GenreAssignments = new List<GenreAssignment>();
                
                foreach (var genreAssignment in song.GenreAssignments)
                {
                    songWithGenres.GenreAssignments.Add(new GenreAssignment(){
                        SongId = genreAssignment.SongId,
                        GenreId = genreAssignment.GenreId,
                        Song = genreAssignment.Song,
                        Genre = allGenres.Single(g => g.Id == genreAssignment.GenreId)
                    });
                }

                songsWithGenres.Add(songWithGenres);
            }

            songs = songsWithGenres;
        }
    }
}