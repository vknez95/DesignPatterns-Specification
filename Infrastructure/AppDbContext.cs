using Microsoft.EntityFrameworkCore;
using Specification.Models;

namespace Specification.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GenreAssignment> GenreAssignments { get; set; }
        public DbSet<SmartPlaylist> SmartPlaylists { get; set; }

        public void Initialize()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            
            SeedData.Initialize(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().ToTable("Album");
            modelBuilder.Entity<Song>().ToTable("Song");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<GenreAssignment>().ToTable("GenreAssignment");
            modelBuilder.Entity<SmartPlaylist>().ToTable("SmartPlaylist");
            modelBuilder.Entity<GenreAssignment>().HasKey(g => new { g.SongId, g.GenreId });
        }
    }
}