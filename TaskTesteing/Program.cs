using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTesteing
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task.WhenAll(DoWork1(), DoWork2()).Wait();
            // Task.WhenAll(DoWork1()).Wait();
            //Task.WhenAll(DoWork2()).Wait();
            //Task.WhenAll(DoWork3()).Wait();
            Task.WhenAll(DoWorkMulti()).Wait();
            Console.WriteLine($"number of threads {Process.GetCurrentProcess().Threads.Count}");
            Console.ReadLine();
        }
        static async Task<string> DoTaskAsync(string name, int timeout, int id)
        {
            var start = DateTime.Now;
            Console.WriteLine("Enter {0}, {1} , Thread -  {2}", name, timeout , System.Threading.Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(timeout);
            //if(id % 2==0)
            //{
            //    throw new ApplicationException("error occured");
            //}
            Console.WriteLine("Exit {0}, {1},  Thread -  {2}", name, (DateTime.Now - start).TotalMilliseconds, System.Threading.Thread.CurrentThread.ManagedThreadId);
            return name;
        }

        static async Task DoWork1()
        {
            var start = DateTime.Now;
            var t1 = DoTaskAsync("t1.1", 10010,1);
            var t2 = DoTaskAsync("t1.2", 5000,1);
            var t3 = DoTaskAsync("t1.3", 5000,1);

            await t1; await t2; await t3;

            Console.WriteLine("DoWork1 results: {0} , total - {1}", String.Join(", ", t1.Result, t2.Result, t3.Result), (DateTime.Now - start).TotalMilliseconds);
        }

        static async Task DoWork2()
        {
            var start = DateTime.Now;
            var t1 = DoTaskAsync("t2.1", 3000,1);
            var t2 = DoTaskAsync("t2.2", 2000,1);
            var t3 = DoTaskAsync("t2.3", 1000,1);

            await Task.WhenAll(t1, t2, t3);

            Console.WriteLine("DoWork2 results: {0} , total - {1}", String.Join(", ", t1.Result, t2.Result, t3.Result), (DateTime.Now - start).TotalMilliseconds);
        }
        static async Task DoWorkMulti()
        {
            var start = DateTime.Now;
            List<Task<string>> taskLst = new List<Task<string>>();
            for(int i=0;i<10000000;i++)
            {
                taskLst.Add(DoTaskAsync("Multi." + i, 1000,i));
            }
            var result =await  Task.WhenAll(taskLst);
            
           
           //Task.WaitAll(taskLst.ToArray());
            Console.WriteLine("multi total time : {0}" , (DateTime.Now - start).TotalMilliseconds);
        }
        static async Task DoWork3()
        {
            var start = DateTime.Now;
            var t1 = await  DoTaskAsync("t3.1", 3000,1);
            var t2 = await DoTaskAsync("t3.2", 2000,2);
            var t3 = await DoTaskAsync("t3.3", 1000,1);

      

            Console.WriteLine("DoWork3 results: {0} , total - {1}", String.Join(", ", t1, t2, t3), (DateTime.Now - start).TotalMilliseconds);
        }


    }
}
