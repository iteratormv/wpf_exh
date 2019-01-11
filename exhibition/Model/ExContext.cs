namespace exhibition.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ExContext : DbContext
    {
        // Контекст настроен для использования строки подключения "ExContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "exhibition.Model.ExContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "ExContext" 
        // в файле конфигурации приложения.
        public ExContext()
            : base("name=ExContext")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
        public virtual DbSet<DisplaySetting> DisplaySettings { get; set; }
        public virtual DbSet<DSCollumnSetting> DSCollumnSettings { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}