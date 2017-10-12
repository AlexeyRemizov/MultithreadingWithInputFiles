using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;
using System.Text.RegularExpressions;



namespace MultithreadingWithInputFiles
{
    public class GetAllFiles  
    {
        private Object thislock = new Object();
        public List<string> FindAllFilesInDirectory(string directory)
        {
            string[] dirs;
            List<string> dirsList = new List<string>();
            dirs = Directory.GetFiles(directory, "*");
            dirsList.AddRange(dirs);
            return dirsList;
        }

        public string ReturnFileFromAll(List<string> dirs, int flag)
        {
            List<string> dirsList = dirs;
            var flags = flag;
            for(int i=0; i<flags; i++)
                dirsList.Remove(dirsList.Last());
            return dirsList.Last().ToString();
        }
        
        public Dictionary<int, string> FindWordInTheFile(string linkOfFile, string curWord)
        {
            var wordsStat = new Dictionary<int, string>();
            int amountInTheLine;
            try
            {
                using (var streamReader = new StreamReader(linkOfFile))
                {
                    int numberOfString = 0;
                    string inputLine;
                    while (!streamReader.EndOfStream)
                    {
                        inputLine = streamReader.ReadLine();
                        numberOfString++;
                        amountInTheLine = new Regex(curWord).Matches(inputLine).Count;
                        if (amountInTheLine > 0)
                        {
                            lock (thislock)
                            {
                                wordsStat[numberOfString] = linkOfFile;
                            }
                        }
                    }
                }
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine("The programm failed with an error.");
                Console.WriteLine(ex.ToString());
            }
            
            return wordsStat;
        }
    }
}
