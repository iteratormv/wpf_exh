using Service_exh.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service_exh.Models
{
    [ServiceContract]
    interface IVisitorContract
    {
        [OperationContract]
        IEnumerable<Visitor> GetAll();

        [OperationContract]
        IEnumerable<Visitor> GetAllm(string mes);
    }

    [DataContract]
    class VisitorContract : IVisitorContract
    {
        ExhFromBaseContext context;

        public VisitorContract() { this.context = new ExhFromBaseContext(); }

        public IEnumerable<Visitor> GetAll() { return context.Visitors; }

        public IEnumerable<Visitor> GetAllm(string mes)
        {
            Console.WriteLine(mes);
            return context.Visitors;
        }


    }

}
