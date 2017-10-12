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
        public List<string> FindAllFilesInDirectory(string directory)
        {
            string[] dirs;
            List<string> dirsList = new List<string>();
            dirs = Directory.GetFiles(directory, "*");
            dirsList.AddRange(dirs);
            return dirsList;
        }   
    }
}
