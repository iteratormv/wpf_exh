using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using EX.Model.DbLayer;
using EX.Model.Repository;

namespace EX.Service
{
    public class VisitorContract : IVisitorContract
    {
        VisitorRepository rep;

        public VisitorContract()
        {
            rep = new VisitorRepository();
        }

        public event Action<string> MessageToServer;
        string mes;

        public IEnumerable<Visitor> GetAllVisitors()
        {
            return new VisitorRepository().GetAllVisitors();
        }


        public VisitorsWithMess GetAllWithMess(string m)
        {
            VisitorsWithMess v = new VisitorRepository().GetAllWithMess(m);

 //           MessageToServer(m);
            return v;
        }

        public Visitor GetVisitor(int Id)
        {
            return rep.GetVisitor(Id);
        }

        public void AddOrUpdate(Visitor visitor)
        {
            rep.AddOrUpdateVisitor(visitor);
        }

        public void RemoveVisitor(Visitor visitor)
        {
            rep.RemoveVisitor(visitor);
        }

        public void RemoveVisistorById(int Id)
        {
            rep.RemoveVisitorById(Id);
        }
    }

    //  [ServiceContract(CallbackContract =typeof(IVisitorClient))]
    [ServiceContract]
    public interface IVisitorContract
    {

        [OperationContract]
        IEnumerable<Visitor> GetAllVisitors();

        [OperationContract]
        VisitorsWithMess GetAllWithMess(string m);

        [OperationContract]
        Visitor GetVisitor(int Id);

        [OperationContract]
        void AddOrUpdate(Visitor visitor);

        [OperationContract]
        void RemoveVisitor(Visitor visitor);

        [OperationContract]
        void RemoveVisistorById(int Id);
    }

    //[ServiceContract]
    //internal interface IVisitorClient
    //{
    //    [OperationContract(IsOneWay = true)]
    //    void GetInfo();
    //}
}
