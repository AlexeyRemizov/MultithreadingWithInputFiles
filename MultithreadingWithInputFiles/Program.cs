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
            var paths = getAllFilesInTheDirectory.FindAllFilesInDirectory(fileDirectory);
            foreach ( var path in paths)
            {
                SearchWordContext searchWordContext = new SearchWordContext(path, confWord);
                if (ThreadPool.QueueUserWorkItem(new WaitCallback(searchWordContext.ThreadProc), searchWordContext))
                {

                    Thread.Sleep(1000);
                }
            }
            
            Console.WriteLine("!!!Enter key!!!");
            Console.ReadKey();
        }


        
    }
}
