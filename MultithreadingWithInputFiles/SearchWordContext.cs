using System;
using System.Collections.Concurrent;
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
        private static object _lockthis = new object();
        private string _filePath;
        private string _searchWord;
        private Dictionary<string, string> _ResultStat;

        public ManualResetEvent manualEvent;
        public string FilePath { get { return _filePath; } }
        public string SearchWord { get { return _searchWord; } }
        public Dictionary<string, string> ResultStat { get { return _ResultStat; } }

        public SearchWordContext(string filePath, string searchWord, Dictionary<string,string> resultStat, ManualResetEvent manualEvent)
        {
            _filePath = filePath;
            _searchWord = searchWord;
            _ResultStat = resultStat;
            this.manualEvent = manualEvent;
        }

        public void ThreadProc(object stateInfo)
        {
            SearchWordContext searchWordContext = (SearchWordContext)stateInfo;
            {
                {
                    var results = FindWordInTheFile(searchWordContext.FilePath, searchWordContext.SearchWord);
                    foreach (var wordsNumber in results)
                    {
                        lock (_lockthis)
                        {
                            ResultStat.Add( wordsNumber.Key , wordsNumber.Value);
                        }
                    }
                    searchWordContext.manualEvent.Set();
                }                
            }
        }

        public Dictionary<string, string> FindWordInTheFile(string filePath, string searchWord)
        {
            
            var wordsStat = new Dictionary<string, string>();
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

                            wordsStat[numberOfString + "----->" + filePath] = inputLine;
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
