using EX.Model.DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Model.Repository
{
    public class VisitorData
    {
        EXContext context;
        ExelData exelData;

        public VisitorData()
        {
            context = new EXContext();
            if (!context.Database.Exists())
            {
                exelData = new ExelData();
                exelData.setDataToCollection(context.Visitors);
            }
        }

        public VisitorData(string file)
        {
            if (!context.Database.Exists())
            {
                context = new EXContext();
                exelData = new ExelData(file);
                exelData.setDataToCollection(context.Visitors);
            }
        }

        public IEnumerable<Visitor> GetAll()
        {
            return context.Visitors;
        }
    }
}
