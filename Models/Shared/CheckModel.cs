using System;
using System.Linq.Expressions;

namespace Specification.Models.Shared
{
    public class CheckModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}