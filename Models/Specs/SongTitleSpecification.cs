using System;
using System.Linq.Expressions;
using Specification.Interfaces;

namespace Specification.Models.Specs
{
    public class SongTitleSpecification : ISpecification<Song>
    {
        public string SearchString { get; set; }

        public SongTitleSpecification(string searchString)
        {
            SearchString = searchString;
        }

        public Expression<Func<Song, bool>> Criteria
        {
            get { return s => s.Title.Contains(SearchString); }
        }
    }
}