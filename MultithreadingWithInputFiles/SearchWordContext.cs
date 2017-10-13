using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingWithInputFiles
{
    public class SearchWordContext
    {
        private string _filePath;
        private string _searchWord;

        public string FilePath { get { return _filePath; } }

        public string SearchWord { get { return _searchWord; } }

        public SearchWordContext(string filePath, string searchWord)
        {
            _filePath = filePath;
            _searchWord = searchWord;
        }

        public void ThreadProc(object stateInfo)
        {
            SearchWordContext searchWordContext = (SearchWordContext)stateInfo;
            {
                IOrderedEnumerable<KeyValuePair<int, string>> results = FindWordInTheFile(searchWordContext.FilePath,
                    searchWordContext.SearchWord).OrderByDescending(ws => ws.Value);
                foreach (var wordsNumber in results)
                {
                    Console.WriteLine("Substring {0} is in line #{1} and occurs <<< {2} >>> ---{3} ", searchWordContext.SearchWord,
                        wordsNumber.Key, wordsNumber.Value, Thread.CurrentThread.GetHashCode().ToString());

                }
            }
        }

        public Dictionary<int, string> FindWordInTheFile(string filePath, string searchWord)
        {
            
            var wordsStat = new Dictionary<int, string>();
            int amountInTheLine;
            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    int numberOfString = 0;
                    string inputLine;
                    while (!streamReader.EndOfStream)
                    {
                        inputLine = streamReader.ReadLine();
                        numberOfString++;
                        amountInTheLine = new Regex(searchWord).Matches(inputLine).Count;
                        if (amountInTheLine > 0)
                        {

                                wordsStat[numberOfString] = filePath;
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
