namespace Service_exh.database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ExhFromBaseContext : DbContext
    {
        public ExhFromBaseContext()
            : base("name=ExhFromBaseContext")
        {
        }

        public virtual DbSet<Visitor> Visitors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
