using System;
using System.Linq.Expressions;
using Specification.Interfaces;

namespace Specification.Models.Specs
{
    public class AllSongsSpecification : ISpecification<Song>
    {
        public Expression<Func<Song, bool>> Criteria { get; } = s => true;
    }
}