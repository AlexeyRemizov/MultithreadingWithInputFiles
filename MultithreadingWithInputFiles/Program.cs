using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Concurrent;
using System.Configuration;

namespace MultithreadingWithInputFiles
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            string confWord = ConfigurationManager.AppSettings["confword_"];
            var fileDirectory = ConfigurationManager.AppSettings[@"filedirectory_"];

            GetAllFilesInTheDirectory getAllFilesInTheDirectory = new GetAllFilesInTheDirectory();
            var paths = getAllFilesInTheDirectory.FindAllFilesInDirectory(fileDirectory);
            var amount = paths.Count();
            ManualResetEvent[] manualEvents = new ManualResetEvent[amount];

            int i = 0;
            Dictionary<string, string> outputData = new Dictionary<string, string>();
            foreach ( var path in paths)
            {
                manualEvents[i] = new ManualResetEvent(false);
                SearchWordContext searchWordContext = new SearchWordContext(path, confWord, outputData, manualEvents[i]);
                i++;
                
                if (ThreadPool.QueueUserWorkItem(new WaitCallback(searchWordContext.ThreadProc), searchWordContext))
                {
                    
                    Thread.Sleep(1000);
                }
                
            }
            WaitHandle.WaitAll(manualEvents);
            foreach (var outData in outputData)
            {
                Console.WriteLine("{0}   {1}", outData.Key, outData.Value);
            }
            
            Console.WriteLine("!!!Enter key!!!");
            Console.ReadKey();
        }


        
    }
}
