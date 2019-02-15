using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [ServiceContract]
    interface IVisitorContract
    {
        [OperationContract]
        IEnumerable<Visitor> GetAll();

        [OperationContract]
        IEnumerable<Visitor> GetAllm(string m);
    }


    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding n = new NetTcpBinding();
            n.Security.Mode = SecurityMode.None;
            ChannelFactory<IVisitorContract> channel = new ChannelFactory<IVisitorContract>(n, new EndpointAddress("net.tcp://192.168.0.27:3121/Visitor"));
            IVisitorContract contract = channel.CreateChannel();
           // string m = Environment.MachineName;
            var collection = contract.GetAllm("GetAll");
            foreach (var item in collection) Console.WriteLine(item.Id);
            Console.ReadKey();
        }
    }
}
