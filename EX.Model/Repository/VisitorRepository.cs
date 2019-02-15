using EX.Model.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace EX.Model.Repository
{
    public class VisitorRepository
    {
        EXContext context;
        ExelData exelData;
        //event Action<string> changeStatusServer;

        public VisitorRepository()
        {
            context = new EXContext();

            if (!context.Database.Exists())
            {
                exelData = new ExelData();
                exelData.setDataToCollection(context.Visitors);
                context.SaveChanges();
            }
        }

        public VisitorRepository(string file)
        {
            context = new EXContext();
            if (!context.Database.Exists())
            {
                exelData = new ExelData(file);
                exelData.setDataToCollection(context.Visitors);
                context.SaveChanges();
            }
        }

        public IEnumerable<Visitor> GetAllVisitors()
        {
            return context.Visitors;
        }

        public Visitor GetVisitor(int Id)
        {
            var visitor = context.Visitors.Where(s => s.Id == Id).FirstOrDefault();
            return visitor;
        }

        public void AddOrUpdateVisitor(Visitor visitor)
        {
            context.Visitors.AddOrUpdate(visitor);
            context.SaveChanges();
        }

        public void RemoveVisitor(Visitor visitor)
        {
            context.Visitors.Remove(visitor);
            context.SaveChanges();
        }

        public void RemoveVisitorById(int Id)
        {
            var visitor = context.Visitors.Where(s => s.Id == Id).FirstOrDefault();
            context.Visitors.Remove(visitor);
            context.SaveChanges();
        }

        public VisitorsWithMess GetAllWithMess(string message)
        {
            ///bpvtybkcz cnfnec
            EXContext context = new EXContext();
            VisitorsWithMess visitorsWithMess = new VisitorsWithMess { message = message, visitors = context.Visitors };
            //changeStatusServer(message);
            return visitorsWithMess;
        }
    }
}
