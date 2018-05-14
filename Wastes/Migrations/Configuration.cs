namespace Wastes.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Wastes.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Wastes.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Wastes.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.AddOrUpdate(
                role => role.Name,
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "Performer" }
                );

            context.WasteStatuses.AddOrUpdate(
                status => status.Name,
                new WasteStatus { Name = "На склад" },
                new WasteStatus { Name = "Натоварен" },
                new WasteStatus { Name = "В междинна площадка" },
                new WasteStatus { Name = "В инсинератор" }
                );

            context.WasteTypes.AddOrUpdate(
                type => type.Name,
                new WasteType { Name = "Опасен" },
                new WasteType { Name = "Средно опасен" },
                new WasteType { Name = "Безопасен" }
                );
        }
    }
}
