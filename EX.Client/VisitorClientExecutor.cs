using EX.Client.ServiceReference1;
using EX.Model.DbLayer;
using EX.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX.Client
{
    public class VisitorClientExecutor
    {
        string message;
        string mashineName;
        VisitorContractClient client;

  //      public event Action<string> serverMessage;

        //public VisitorClientExecutor(string message, string mashineName, VisitorContractClient client)
        //{
        //    this.message = message;
        //    this.mashineName = mashineName;
        //    this.client = client;
        //}

        public VisitorClientExecutor()
        {
            this.message = Environment.MachineName;
            this.mashineName = Environment.MachineName + "create";
            this.client = new VisitorContractClient();
        }

        public IEnumerable<Visitor> getAll()
        {
            message = Environment.MachineName + " : getAll";
            VisitorsWithMess cm = client.GetAllWithMess(message);
 //           serverMessage(cm.message);
            return cm.visitors;
        }

        public VisitorsWithMess GetAllWithMess()
        {
            message = Environment.MachineName + " : getAll";
            return client.GetAllWithMess(message);
        }

        //public IEnumerable<Visitor> GetAllVisitors()
        //{
        //    return client.GetAllVisitors();
        //}



        ////     string mashineName = Environment.MachineName;
        ////     NetTcpBinding n = new NetTcpBinding();
        ////     n.Security.Mode = SecurityMode.None;
        ////      ChannelFactory<IVisitorContract> channel = new ChannelFactory<IVisitorContract>(n, new EndpointAddress("net.tcp://192.168.0.29:3121/Visitor"));
        ////      IVisitorContract contract = channel.CreateChannel();


        ////     VisitorCallback vc = new VisitorCallback();
        ////    InstanceContext context = new InstanceContext(vc);
        ////    VisitorContractClient client = new VisitorContractClient(context);
        //VisitorContractClient client = new VisitorContractClient();
        //var collection = client.GetAll();
        //  //  vc.info(Environment.MachineName);
        //    foreach (var item in collection) { Thread.Sleep(20); Console.WriteLine(item.Id); }
    }
}
