namespace exhibition.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class contextcontext : DbContext
    {

        public contextcontext()
            : base("name=contextcontext")
        {
        }
        public virtual DbSet<Visitor> Visitors { get; set; }
        public virtual DbSet<DSCollumnSetting> DSCollumnSettings {get;set;}
        public virtual DbSet<DisplaySetting> DisplaySettings { get; set; }
    }

}