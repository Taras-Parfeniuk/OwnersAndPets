using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstraction;
using Entities;

namespace DAL.EFRepositories
{
    public class PetsRepository : BaseRepository<string, Pet>, IPetsRepository
    {
        public PetsRepository() 
            : base(DbContextFactory.CreateContext()) { }

        public PetsRepository(DbContext context)
            : base(context) { }

        public override void Add(Pet pet)
        {
            Context.Set<Pet>().Add(pet);
            SaveChanges();
        }

        public override void Update(Pet pet)
        {
            Context.Database.ExecuteSqlCommand(
                    "insert or replace into Pets (Id, Name, OwnerId) values ('" + pet.Id + "', '" + pet.Name + "', '" + pet.OwnerId + "');"
                );
            SaveChanges();
        }

        public List<Pet> GetByOwnerId(string ownerId)
        {
            return GetMany(p => p.OwnerId == ownerId);
        }

        public List<Pet> GetByOwner(Owner owner)
        {
            return GetByOwnerId(owner.Id);
        }
    }
}
