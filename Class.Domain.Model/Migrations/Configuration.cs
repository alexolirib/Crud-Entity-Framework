namespace Class.Domain.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClassDomainModel.ClassContext>
    {
        public Configuration() //console - enable-migrations (habilitei)
        {
            AutomaticMigrationsEnabled = false; //bom deixar falso para não ser feito automatico
        }

        protected override void Seed(ClassDomainModel.ClassContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
