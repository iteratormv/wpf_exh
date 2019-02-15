//using ClientRef.ServiceReference1;
//using ClientRef.ServiceReference1;
//using ClientRef.ServiceReference1;
using EX.Client;
using EX.Client.ServiceReference1;
using EX.Model.DbLayer;
using EX.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Visitor = EX.Model.DbLayer.Visitor;

namespace ClientRef
{

    public class VisitorContract : IVisitorContract
    {

        public IEnumerable<Visitor> GetAll()
        {
            return new VisitorRepository().GetAllVisitors();
        }
    }

    [ServiceContract]
    interface IVisitorContract
    {
        [OperationContract]
        IEnumerable<Visitor> GetAll();
    }

    //public class VisitorCallback : IVisitorContractCallback
    //{
    //    public string mashine;
    //    public Action<string> info;

    //    public VisitorCallback() { mashine = "none"; }


    //    public void GetInfo()
    //    {
    //        mashine = Environment.MachineName;
    //    }
    //}



    class Program
    {
        static void Main(string[] args)
        {
            
          //  string mashineName = Environment.MachineName;
          //  //     NetTcpBinding n = new NetTcpBinding();
          //  //     n.Security.Mode = SecurityMode.None;
          //  //      ChannelFactory<IVisitorContract> channel = new ChannelFactory<IVisitorContract>(n, new EndpointAddress("net.tcp://192.168.0.29:3121/Visitor"));
          //  //      IVisitorContract contract = channel.CreateChannel();


          //  //     VisitorCallback vc = new VisitorCallback();
          //  //    InstanceContext context = new InstanceContext(vc);
          //  //    VisitorContractClient client = new VisitorContractClient(context);
            VisitorContractClient client = new VisitorContractClient();
            var collection = client.GetAllWithMess("kuku");
          ////  vc.info(Environment.MachineName);
            foreach (var item in collection.visitors) { Thread.Sleep(20); Console.WriteLine(item.Id); }
            Console.WriteLine(collection.message);



        }
    }
}
