using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Wastes.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public int PerformerId { get; set; }
        public virtual ICollection<Protocol> Protocols { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<History> History { get; set; }
        public DbSet<Photo> Photoes { get; set; }
        public DbSet<Protocol> Protocols { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<StorageType> StorageTypes { get; set; }
        public DbSet<Waste> Wastes { get; set; }
        public DbSet<WasteStatus> WasteStatuses { get; set; }
        public DbSet<WasteType> WasteTypes { get; set; }
}
}