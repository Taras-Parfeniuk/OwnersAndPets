using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Pet : BaseEntity<string>
    {
        [StringLength(36)]
        public string OwnerId { get; set; }

        public Pet() { }

        public Pet(string name, string ownerId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            OwnerId = ownerId;
        }
    }
}
