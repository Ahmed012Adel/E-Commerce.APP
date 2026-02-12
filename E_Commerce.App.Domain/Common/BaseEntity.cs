using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Domain.Common
{
    public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public required TKey Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? lastModifiedBy { get; set; }
        public DateTime? LastModifideOn { get; set; }
    }
}
