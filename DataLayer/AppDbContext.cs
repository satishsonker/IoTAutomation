using IoT.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace IoT.DataLayer
{
   public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasOne<DeviceType>(s => s.DeviceType)
                .WithMany(g => g.Devices)
                .HasForeignKey(s => s.DeviceTypeId);
            modelBuilder.Entity<Device>()
                .HasOne<Room>(s => s.Room)
                .WithMany(g => g.Devices)
                .HasForeignKey(s => s.RoomId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
