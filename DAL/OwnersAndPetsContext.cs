using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstraction;
using Entities;

namespace DAL
{
    public class OwnersAndPetsContext : DbContext
    {
        public OwnersAndPetsContext(string connectionString) 
            : base(connectionString) { }

        public OwnersAndPetsContext(SQLiteConnection connection)
            : base(connection, true) { }

        public OwnersAndPetsContext() { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().ToTable("Owners");
            modelBuilder.Entity<Owner>().Property(o => o.Id).HasColumnName("Id");
            modelBuilder.Entity<Owner>().Property(o => o.Name).HasColumnName("Name");
            //modelBuilder.Entity<Owner>().Ignore(o => o.Pets);
            modelBuilder.Entity<Owner>().Ignore(o => o.PetsCount);

            modelBuilder.Entity<Pet>().ToTable("Pets");
            modelBuilder.Entity<Pet>().Property(p => p.Id).HasColumnName("Id"); 
            modelBuilder.Entity<Pet>().Property(p => p.Name).HasColumnName("Name"); 
            modelBuilder.Entity<Pet>().Property(p => p.OwnerId).HasColumnName("OwnerId"); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
