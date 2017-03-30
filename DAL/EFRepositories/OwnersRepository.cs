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
    public class OwnersRepository : BaseRepository<string, Owner>, IOwnersRepository
    {
        private IPetsRepository _petsRepository;

        private IPetsRepository Pets => _petsRepository ?? (_petsRepository = new PetsRepository(Context));

        public OwnersRepository() 
            : base(DbContextFactory.CreateContext()) { }

        public OwnersRepository(DbContext context)
            :base(context) { }

        public List<Pet> GetPetsByOwner(Owner owner)
        {
            return Pets.GetByOwner(owner);
        }

        public List<Pet> GetPetsByOwnerId(string ownerId)
        {
            return Pets.GetByOwnerId(ownerId);
        }

        public override void Add(Owner owner)
        {
            base.Add(owner);
        }

        public override void Remove(Owner owner)
        {
            base.Remove(owner);
            Pets.GetByOwnerId(owner.Id).ForEach(p => Pets.Remove(p));
        }
    }
}
