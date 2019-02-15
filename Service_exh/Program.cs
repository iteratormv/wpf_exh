using Service_exh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service_exh
{
    class ho
    {
        ServiceHost host;
        public void start()
        {
            string ipAdress = "192.168.0.27";
            string uri = @"net.tcp://" + ipAdress + @":3121/Visitor";

            host = new ServiceHost(typeof(VisitorContract), new Uri(uri));

            NetTcpBinding n = new NetTcpBinding();
            n.Security.Mode = SecurityMode.None;

            host.AddServiceEndpoint(typeof(IVisitorContract), n, uri);



            host.Open();

            Console.WriteLine("Service run");
            for(int i=1; i < 2000; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Service run " + i);
            }

        }

        public void stop()
        {
            host.Close();
        }
    }

    class Program
    {
        

        static void Main(string[] args)
        {
            ho h = new ho();
            Task.Factory.StartNew(()=>
            {
                h.start();
            });
            Console.ReadKey();
            h.stop();
        }

        
    }
}
