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
        public static string curWord = "VirtualCallManager";
        public static int i = 0;
        public static void Main(string[] args)
        {
            GetAllFiles getAllFiles = new GetAllFiles();
            SearchWord searchWord = new SearchWord();
            SelectTheDesiredPass selectPass = new SelectTheDesiredPass();
            for (int i = 0; i < getAllFiles.FindAllFilesInDirectory(@"d:\logs!\").Count(); i++)
            {
                if (ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), getAllFiles))
                {

                    Thread.Sleep(1000);
                }
            }
            
            Console.WriteLine("!!!Enter key!!!");
            Console.ReadKey();
        }

        static void ThreadProc(object stateInfo)
        {
           
            GetAllFiles getAllFiles = (GetAllFiles)stateInfo;
            SearchWord searchWord = new SearchWord();
            SelectTheDesiredPass selectPass = ( new SelectTheDesiredPass());
            //var count = getAllFiles.FindAllFilesInDirectory(@"d:\logs!\").Count();//22
            {
                foreach (var wordsNumber in searchWord.FindWordInTheFile(selectPass.ReturnFileFromAll(getAllFiles.FindAllFilesInDirectory(@"d:\logs!\"), i), curWord).OrderByDescending(ws => ws.Value))
                {
                    Console.WriteLine("Substring {0} is in line #{1} and occurs <<< {2} >>> ---{3} ", "VirtualCallManager", wordsNumber.Key, wordsNumber.Value, Thread.CurrentThread.GetHashCode().ToString());

                }
                i++;
            }
            

        }
    }

    
}
