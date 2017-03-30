using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class BaseEntity<TKey> where TKey : IComparable
    {
        [StringLength(36)]
        public TKey Id { get; set; }
        [StringLength(36)]
        public string Name { get; set; }
    }
}
