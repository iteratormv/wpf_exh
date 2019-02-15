using EX.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Ref
{
    //class ho
    //{
    //    string uri;
    //    string ipAdress;

    //    public void start()
    //    {
    //        ipAdress = "192.168.0.29";
    //        uri = @"net.tcp://" + ipAdress + @":3121/Visitor";

    //        ServiceHost host = new ServiceHost(typeof(VisitorContract), new Uri(uri));

    //        NetTcpBinding n = new NetTcpBinding();
    //        n.Security.Mode = SecurityMode.None;

    //        host.AddServiceEndpoint(typeof(IVisitorContract), n, uri);



    //        host.Open();

    //        Console.WriteLine("Service run");
    //        for (int i = 1; i < 20; i++)
    //        {
    //            Thread.Sleep(1000);
    //            Console.WriteLine(i);
    //        }

    //        host.Close();
    //    }
    //}

    //class Program
    //{


    //    static void Main(string[] args)
    //    {
    //        ho h = new ho();
    //        Task.Factory.StartNew(() =>
    //        {
    //            h.start();
    //        });
    //        Console.ReadKey();
    //    }


    //}

    class Program
    {


        static void Main(string[] args)
        {
            VisitorExecutor v = new VisitorExecutor("192.168.0.29");
            v.statusChanged += st;
            //         Task.Factory.StartNew(v.Start);
            Task.Factory.StartNew(() =>
            {
                v.Start();
            });



            
            for (int i = 1; i < 100; i++) { Thread.Sleep(1000); Console.WriteLine(i); }
        }

        private static void st(string obj)
        {
            Console.WriteLine((string)obj);
        }
    }





}
