using IoT.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
namespace IoT.DataLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasOne<DeviceType>(s => s.DeviceType);
            modelBuilder.Entity<Device>()
                .HasOne<Room>(s => s.Room)
                .WithMany(g => g.Devices)
                .HasForeignKey(s => s.RoomId);
            modelBuilder.Entity<Scene>()
                 .HasMany<SceneAction>(s => s.SceneActions);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<SceneAction> SceneActions { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
    }
}
