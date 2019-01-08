using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab3
{
    class Program
    {
        public static Logs logs;
        private static object sync = new Object();
        static void Main(string[] args)
        {
            
            List<Thread> listThread = new List<Thread>();
            logs = new Logs(Environment.CurrentDirectory + @"\log.txt");
            for (int i = 0; i < 10; i++)
            {
                listThread.Add(new Thread(DoWork));
                
            }
            for (int i = 0; i < 10; i++)
            {
                listThread[i].Start();
            }
            Console.ReadLine();
        }
        public static void DoWork()
        {

                lock (sync)
                {
                    logs.writeEven("Поток "  + Thread.CurrentThread.ManagedThreadId +" начал работу");
                }
                

                Console.WriteLine("Поток " + Thread.CurrentThread.ManagedThreadId + " начал работу");
                //Thread.Sleep(10000);

                lock (sync)
                {
                    logs.writeEven("Поток " + Thread.CurrentThread.ManagedThreadId + " начал работу");
                }
                Console.WriteLine("Поток " + Thread.CurrentThread.ManagedThreadId + " окончил работу");


            }

        }

    }
    public class Logs
    {
        public String fileName;
        String text;

        public Logs(String fileName)
        {
            this.fileName = fileName;
        }

        public void writeEven(String textEvent)
        {
            text = "";

            try
            {
                using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default))
                {
                    text = sr.ReadToEnd();
                }
                using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }

                using (StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now + " " + textEvent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }

