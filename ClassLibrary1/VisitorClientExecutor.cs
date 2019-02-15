//using Microsoft.Analytics.Interfaces;
u//sing Microsoft.Analytics.Types.Sql;
using ClassLibrary1.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    class VisitorClientExecutor
    {
        public event Action<string> serverMessage;

        string mashineName;
        string message;
        VisitorContractClient client;

        //public VisitorClientExecutor(string mashineName, string message, VisitorContractClient client)
        //{
        //    this.mashineName = mashineName;
        //    this.message = message;
        //    this.client = client;
        //}

        public VisitorClientExecutor()
        {
            this.mashineName = Environment.MachineName;
            this.message = Environment.MachineName + " : create";
            this.client = new VisitorContractClient();
        }

        public IEnumerable<Visitor> GetAll()
        {
            message = Environment.MachineName + " : getAll";
            serverMessage(message);
            return client.GetAll();
        }


        //string mashineName = Environment.MachineName;
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