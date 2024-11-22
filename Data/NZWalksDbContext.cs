using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions):base(dbContextOptions)   
        {
            
        }
        public DbSet<Difficulty>difficulties { get; set; }  
        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> walks { get; set; }
        public DbSet<Image> images { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>
            {
                new Difficulty 
                {
                    Id= Guid.Parse("a596bb82-f61a-40bd-a7c2-8b14e764aa6d"),
                    Name="Easy"
                } ,

                new Difficulty 
                {
                    Id= Guid.Parse("e362b264-a682-48fb-8fe6-f8eb914134ba"),
                    Name="Medium"
                } ,

                new Difficulty
                {
                    Id= Guid.Parse("f9010147-e264-40f8-ace3-5672565b1c6e"),
                    Name="Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>
            {

                new Region
                {
                    Id =Guid.Parse("370bbc1c-addb-4128-91c3-e90f4b4b1c99"),
                    Name = "Auckland",
                    Code = "AUK",
                    RegionImageUrl = "AUK-image.jph" 
                } ,
                new Region
                {
                    Id =Guid.Parse("4edb47c8-3f14-4f67-b8ab-6a7328b5f145"),
                    Name = "QueensTown",
                    Code = "QST",
                    RegionImageUrl = "QST-image.jph"
                } ,
                new Region
                {
                    Id =Guid.Parse("29cf8535-ee40-4002-9eae-39a6172e8637"),
                    Name = "Northland",
                    Code = "NTH",
                    RegionImageUrl = "NTH-image.jph"
                } 
            };
            modelBuilder.Entity<Region>().HasData(regions); 
        }
    }

}
