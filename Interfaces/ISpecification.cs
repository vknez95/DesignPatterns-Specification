using System;
using System.Linq.Expressions;

namespace Specification.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}