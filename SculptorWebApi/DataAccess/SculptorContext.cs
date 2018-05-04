namespace SculptorWebApi.DataAccess
{
    using SculptorWebApi.DataAccess.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SculptorContext : DbContext
    {
        // Your context has been configured to use a 'SculptorContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SculptorWebApi.DataAccess.SculptorContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SculptorContext' 
        // connection string in the application configuration file.
        public SculptorContext()
            : base("name=SculptorContext")
        {
            base.Configuration.LazyLoadingEnabled = false;
            base.Configuration.ProxyCreationEnabled = false;
        }

        //public SculptorContext(string dbName)
        //    : base(string.Format("name={0}", dbName))
        //{
        //    //Database.Log = s => Debug.WriteLine(s);
        //    base.Configuration.LazyLoadingEnabled = false;
        //    base.Configuration.ProxyCreationEnabled = false;
        //}

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<PredictiveModel> PredictiveModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //EXAMPLE: Couples an attribute to the CodeFirst convetnions
            //modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<CaseSensitiveAttribute, bool>(
            //    CompoundEnum.CaseSensitive.ToString(),
            //    (property, attributes) => attributes.Single().IsEnabled));
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.HasDefaultSchema("public");
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}