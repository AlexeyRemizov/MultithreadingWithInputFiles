using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Concurrent;

namespace MultithreadingWithInputFiles
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            GetAllFilesInTheDirectory getAllFilesInTheDirectory = new GetAllFilesInTheDirectory();
            var confWord = "VirtualCallManager";
            var fileDirectory = @"d:\logs!\";
            Dictionary<int, string> ddd = new Dictionary<int, string>();
            
 
            var paths = getAllFilesInTheDirectory.FindAllFilesInDirectory(fileDirectory);
            WordData wd = new WordData();
            foreach ( var path in paths)
            {
                SearchWordContext searchWordContext = new SearchWordContext(path, confWord, ddd);
                
                
                if (ThreadPool.QueueUserWorkItem(new WaitCallback(searchWordContext.ThreadProc), searchWordContext))
                {
                    
                    Thread.Sleep(1000);
                }
                
            }
            foreach (var dd in ddd)
            {
                Console.WriteLine("{0}   {1}", dd.Key, dd.Value);
            }
            
            Console.WriteLine("!!!Enter key!!!");
            Console.ReadKey();
        }


        
    }
}
