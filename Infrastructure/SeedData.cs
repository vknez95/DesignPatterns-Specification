using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Specification.Models;
using System.Collections.Generic;

namespace Specification.Infrastructure
{
    public static class SeedData
    {
        private static List<Album> Albums;
        private static List<Song> Songs;
        private static List<Genre> Genres;
        private static List<GenreAssignment> GenreAssignments;

        public static void Initialize(AppDbContext context)
        {
            // Look for any movies.
            if (context.Songs.Any())
            {
                return;   // DB has been seeded
            }

            SeedAlbums(context);
            SeedGenres(context);
            SeedSongs(context);
            SeedGenreAssignments(context);
        }

        private static void SeedAlbums(AppDbContext context)
        {
            Albums = new List<Album>
            {
                new Album {Artist = "311", Title = "Greatest Hits '93-'03", Year = 2004},
                new Album {Artist = "A Perfect Circle", Title = "10,000 Days", Year = 2006},
                new Album {Artist = "Daft Punk", Title = "Tron Legacy", Year = 2010},
                new Album {Artist = "Deadmau5", Title = "4x4=12", Year = 2011},
                new Album {Artist = "Franklin Brothers", Title = "Lifeboat To Nowhere", Year = 2011},
                new Album {Artist = "Metallica", Title = "...And Justice for All", Year = 1990},
                new Album {Artist = "Modest Mouse", Title = "Good News For People Who Love Bad News", Year = 2004},
                new Album {Artist = "Rage Against the Machine", Title = "Rage Against the Machine", Year = 1992},
                new Album {Artist = "Rage Against the Machine", Title = "Evil Empire", Year = 1996},
                new Album {Artist = "Tool", Title = "10,000 Days", Year = 2006}
            };

            context.Albums.AddRange(Albums);
            context.SaveChanges();
        }

        private static void SeedGenres(AppDbContext context)
        {
            Genres = new List<Genre>
            {
                new Genre {Name = "Alternative"},
                new Genre {Name = "Alt Rock"},
                new Genre {Name = "Jazz"},
                new Genre {Name = "Metal"},
                new Genre {Name = "Progressive"},
                new Genre {Name = "Rap"},
                new Genre {Name = "Rock"},
                new Genre {Name = "Techno"}
            };

            context.Genres.AddRange(Genres);
            context.SaveChanges();
        }

        private static void SeedSongs(AppDbContext context)
        {
            Songs = new List<Song>
            {
                new Song { Artist = "311", AlbumId = Albums.First(a => a.Artist == "311").Id, Title = "Amber", Year = 2004, Rating = 5, Length = RandomSongLength() },
                new Song { Artist = "A Perfect Circle", AlbumId = Albums.First(a => a.Artist == "A Perfect Circle").Id, Title = "The Noose", Year = 2003, Rating = 5, Length = RandomSongLength() },
                new Song { Artist = "A Perfect Circle", AlbumId = Albums.First(a => a.Artist == "A Perfect Circle").Id, Title = "Blue", Year = 2003, Rating = 4, Length = RandomSongLength() },
                new Song { Artist = "A Perfect Circle", AlbumId = Albums.First(a => a.Artist == "A Perfect Circle").Id, Title = "Pet", Year = 2003, Rating = 3, Length = RandomSongLength() },
                new Song { Artist = "Daft Punk", AlbumId = Albums.First(a => a.Artist == "Daft Punk").Id, Title = "The Game Has Changed", Year = 2010, Rating = 4, Length = RandomSongLength() },
                new Song { Artist = "Daft Punk", AlbumId = Albums.First(a => a.Artist == "Daft Punk").Id, Title = "End Of Line", Year = 2010, Rating = 3, Length = RandomSongLength() },
                new Song { Artist = "Daft Punk", AlbumId = Albums.First(a => a.Artist == "Daft Punk").Id, Title = "C.L.U", Year = 2010, Rating = 3, Length = RandomSongLength() },
                new Song { Artist = "Daft Punk", AlbumId = Albums.First(a => a.Artist == "Daft Punk").Id, Title = "Tron Legacy (End Titles)", Year = 2010, Rating = 4, Length = RandomSongLength() },
                new Song { Artist = "Deadmau5", AlbumId = Albums.First(a => a.Artist == "Deadmau5").Id, Title = "Some Chords", Year = 2013, Rating = 5, Length = RandomSongLength() },
                new Song { Artist = "Franklin Brothers", AlbumId = Albums.First(a => a.Artist == "Franklin Brothers").Id, Title = "She Won", Year = 2011, Rating = 4, Length = RandomSongLength() },
                new Song { Artist = "Metallica", AlbumId = Albums.First(a => a.Artist == "Metallica").Id, Title = "Blackened", Year = 1990, Rating = 4, Length = RandomSongLength() },
                new Song { Artist = "Metallica", AlbumId = Albums.First(a => a.Artist == "Metallica").Id, Title = "...And Justice for All", Year = 1990, Rating = 5, Length = RandomSongLength() },
                new Song { Artist = "Metallica", AlbumId = Albums.First(a => a.Artist == "Metallica").Id, Title = "One", Year = 1990, Rating = 5, Length = RandomSongLength() },
                new Song { Artist = "Modest Mouse", AlbumId = Albums.First(a => a.Artist == "Modest Mouse").Id, Title = "One", Year = 1990, Rating = 5, Length = RandomSongLength() },
                new Song { Artist = "Rage Against the Machine", AlbumId = Albums.First(a => a.Artist == "Rage Against the Machine" && a.Year == 1992).Id, Title = "Killing in the Name", Year = 1992, Rating = 4, Length = RandomSongLength() },
                new Song { Artist = "Rage Against the Machine", AlbumId = Albums.First(a => a.Artist == "Rage Against the Machine" && a.Year == 1996).Id, Title = "Bulls on Parade", Year = 1996, Rating = 5, Length = RandomSongLength() },
                new Song { Artist = "Tool", AlbumId = Albums.First(a => a.Artist == "Tool").Id, Title = "The Pot", Year = 2006, Rating = 5, Length = RandomSongLength() },
                new Song { Artist = "Tool", AlbumId = Albums.First(a => a.Artist == "Tool").Id, Title = "Rosetta Stoned", Year = 2006, Rating = 5, Length = RandomSongLength() },
                new Song { Artist = "Tool", AlbumId = Albums.First(a => a.Artist == "Tool").Id, Title = "Wings for Marie (Pt 1)", Year = 2006, Rating = 4, Length = RandomSongLength() },
                new Song { Artist = "Tool", AlbumId = Albums.First(a => a.Artist == "Tool").Id, Title = "10,000 Days (Wings Pt 2)", Year = 2006, Rating = 5, Length = RandomSongLength() }
            };

            context.Songs.AddRange(Songs);
            context.SaveChanges();
        }

        private static void SeedGenreAssignments(AppDbContext context)
        {
            GenreAssignments = new List<GenreAssignment>
            {
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "Amber").Id, GenreId = Genres.First(g => g.Name == "Alternative").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "The Noose").Id, GenreId = Genres.First(g => g.Name == "Progressive").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "Blue").Id, GenreId = Genres.First(g => g.Name == "Progressive").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "Pet").Id, GenreId = Genres.First(g => g.Name == "Progressive").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "The Game Has Changed").Id, GenreId = Genres.First(g => g.Name == "Techno").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "End Of Line").Id, GenreId = Genres.First(g => g.Name == "Techno").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "C.L.U").Id, GenreId = Genres.First(g => g.Name == "Techno").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "Tron Legacy (End Titles)").Id, GenreId = Genres.First(g => g.Name == "Techno").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "Some Chords").Id, GenreId = Genres.First(g => g.Name == "Techno").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "She Won").Id, GenreId = Genres.First(g => g.Name == "Jazz").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "Blackened").Id, GenreId = Genres.First(g => g.Name == "Metal").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "...And Justice for All").Id, GenreId = Genres.First(g => g.Name == "Metal").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Artist == "Metallica" && s.Title == "One").Id, GenreId = Genres.First(g => g.Name == "Metal").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Artist == "Modest Mouse" && s.Title == "One").Id, GenreId = Genres.First(g => g.Name == "Alt Rock").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "Killing in the Name").Id, GenreId = Genres.First(g => g.Name == "Rap").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "Bulls on Parade").Id, GenreId = Genres.First(g => g.Name == "Rap").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "The Pot").Id, GenreId = Genres.First(g => g.Name == "Rock").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "Rosetta Stoned").Id, GenreId = Genres.First(g => g.Name == "Rock").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "Wings for Marie (Pt 1)").Id, GenreId = Genres.First(g => g.Name == "Rock").Id },
                new GenreAssignment() { SongId = Songs.First(s => s.Title == "10,000 Days (Wings Pt 2)").Id, GenreId = Genres.First(g => g.Name == "Rock").Id }
            };

            context.GenreAssignments.AddRange(GenreAssignments);
            context.SaveChanges();
        }

        private static TimeSpan RandomSongLength()
        {
            var random = new Random();

            int minutes = random.Next(7) + 1;
            int seconds = random.Next(60);

            return new TimeSpan(0, 0, minutes, seconds);
        }
    }
}