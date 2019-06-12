using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp13
{
        public delegate int BinaryOp(int x, int у);
    class Program
    {
        private static bool isDone = false;
        static void Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread);
            Console.WriteLine("***** AsyncCallbackDelegate Example *****");
            Console.WriteLine("Main() invoked on thread {0}.",
            Thread.CurrentThread.ManagedThreadId);
            BinaryOp b = new BinaryOp(Add);
            IAsyncResult iftAR = b.BeginInvoke(10, 10,
            new AsyncCallback(AddComplete),
            Plus());

            // var result = b.EndInvoke(iftAR);
            //int numb = b.EndInvoke(iftAR);

            // Предположим, что здесь выполняется какая-то другая работа...
            while (!isDone)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Working....");
            }
            Console.ReadLine();
        }
        static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked on thread {0}.",
            Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            return x + y;
        }
        static void AddComplete(IAsyncResult iftAR)
        {
            Console.WriteLine("AddComplete() invoked on thread {0}.",
            Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Your addition is complete");
            // Теперь получить результат.
            AsyncResult ar = (AsyncResult)iftAR;
            BinaryOp b = (BinaryOp)ar.AsyncDelegate;
            Console.WriteLine("10 + 10 is {0}.", b.EndInvoke(iftAR));
            isDone = true;
            // Получить информационный объект и привести его к string.
            string msg = (string)iftAR.AsyncState;
            Console.WriteLine(msg);
            isDone = true;

        }
        static string Plus()
        {
            return "Main() thanks you for adding these numbers.";
        }
    }
}
