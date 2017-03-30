using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL.Abstraction
{
    public interface IOwnersRepository : IRepository<string, Owner>
    {
        List<Pet> GetPetsByOwnerId(string ownerId);
        List<Pet> GetPetsByOwner(Owner owner);
    }
}
