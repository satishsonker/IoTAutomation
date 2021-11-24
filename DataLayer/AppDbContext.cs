using IoT.ModelLayer;
using IoT.ModelLayer.Alexa;
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
            modelBuilder
                .Entity<DeviceGroup>()
                .HasMany<DeviceGroupDetail>(x => x.DeviceGroupDetails)
                .WithOne(x => x.DeviceGroup)
                .HasForeignKey(x => x.GroupDetailId);

            modelBuilder
                .Entity<DeviceGroupDetail>()
                .HasOne<Device>(x => x.Device)
                .WithMany(x => x.DeviceGroupDetails)
                .HasForeignKey(x => x.DeviceId);

            modelBuilder
                .Entity<Device>()
                .HasOne<DeviceType>(s => s.DeviceType);

            modelBuilder
                .Entity<DeviceType>()
                .HasMany<DeviceCapability>(x => x.DeviceCapabilities);

            modelBuilder
                .Entity<DeviceCapability>()
                .HasOne<DeviceType>(x => x.DeviceType)
                .WithMany(x => x.DeviceCapabilities)
                .HasForeignKey(x => x.DeviceTypeId);

            modelBuilder.Entity<Device>()
                .HasOne<Room>(s => s.Room)
                .WithMany(g => g.Devices)
                .HasForeignKey(s => s.RoomId);

            modelBuilder
                .Entity<UserPermission>()
                .HasOne<User>(x => x.User);

            modelBuilder
                .Entity<Scene>()
                .HasMany<SceneAction>(s => s.SceneActions);

            modelBuilder
                .Entity<DeviceAction>()
                .HasOne<DeviceType>(x => x.DeviceType)
                .WithMany(x => x.DeviceActions)
                .HasForeignKey(x => x.DeviceTypeId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<SceneAction> SceneActions { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<DeviceAction> DeviceActions { get; set; }
        public DbSet<DeviceCapability> DeviceCapabilities { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<CapabilityType> CapabilityTypes { get; set; }
        public DbSet<CapabilityVersion> CapabilityVersions { get; set; }
        public DbSet<DisplayCategory> DisplayCategorys { get; set; }
        public DbSet<SkillToken> SkillTokens { get; set; }
        public DbSet<CapabilityInterface> CapabilityInterfaces { get; set; }
        public DbSet<CapabilitySupportedProperty> CapabilitySupportedProperties { get; set; }
    }
}
