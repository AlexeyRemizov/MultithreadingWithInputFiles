using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MultithreadingWithInputFiles
{
    public class SearchWord
    {
        private Object thislock = new Object();
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
