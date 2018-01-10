using System.Collections.Generic;
using System.Linq;
using Specification.Models;

namespace Specification.Infrastructure
{
    public class SmartPlaylistRepository
    {
        private readonly AppDbContext _dbContext;

        public SmartPlaylistRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SmartPlaylist GetByName(string name)
        {
            return _dbContext.SmartPlaylists.FirstOrDefault(s => s.Name.ToLower() == name.ToLower());
        }

        public void Add(SmartPlaylist playlist)
        {
            _dbContext.SmartPlaylists.Add(playlist);
            _dbContext.SaveChanges();
        }

        public void Remove(SmartPlaylist playlist)
        {
            _dbContext.SmartPlaylists.Remove(playlist);
            _dbContext.SaveChanges();
        }

        public IEnumerable<SmartPlaylist> List()
        {
            return _dbContext.SmartPlaylists.AsEnumerable();
        }

        public SmartPlaylist FindById(int id)
        {
            return _dbContext.SmartPlaylists.SingleOrDefault(s => s.Id == id);
        }
    }
}