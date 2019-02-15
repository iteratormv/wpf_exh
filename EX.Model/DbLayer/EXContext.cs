namespace EX.Model.DbLayer
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EXContext : DbContext
    {
        // Контекст настроен для использования строки подключения "EXContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "EX.Model.DbLayer.EXContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "EXContext" 
        // в файле конфигурации приложения.
        public EXContext()
            : base("name=EXContext.xml")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}