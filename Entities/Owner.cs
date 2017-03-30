using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Owner : BaseEntity<string>
    {
        public Owner() { }

        public Owner(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }

        public int PetsCount { get; set; }
    }
}
