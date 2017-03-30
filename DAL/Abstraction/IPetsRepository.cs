using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL.Abstraction
{
    public interface IPetsRepository : IRepository<string, Pet>
    {
        List<Pet> GetByOwner(Owner owner);
        List<Pet> GetByOwnerId(string ownerId);
    }
}
