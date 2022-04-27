using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var p1 = PersonFactory.GetInstance("张三");
            Console.WriteLine(p1.name);

            var p2 = PersonFactory.GetInstance("李四");
            Console.WriteLine(p2.name);

            var p3 = PersonFactory.GetInstance("张三");
            Console.WriteLine(p3.name);



            Console.WriteLine($"释放实例前Count:{PersonFactory.GetInstaceCount()}");

            Console.WriteLine($"释放实例状态为:{PersonFactory.IDisposable("张三")}");

            Console.WriteLine($"释放实例后Count:{PersonFactory.GetInstaceCount()}");

            PersonFactory.IDisposable();
            Console.WriteLine($"释放所有实例，检查实例总数:{PersonFactory.GetInstaceCount()}");



            Console.WriteLine("Hello World!");



            //while (true)
            //{
                // System.Threading.Thread.Sleep(7000);

                HubConnection connection = new HubConnectionBuilder()
               .WithUrl("http://localhost:5000/event")
               .Build();

                connection.Closed += async (error) =>
                {
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await connection.StartAsync();
                };

                connection.StartAsync();

                connection.On<Object>("event", (obj) =>
                {
                    Console.WriteLine(obj.ToString());
                });
            //}

            Console.ReadLine();



        }
    }
}
