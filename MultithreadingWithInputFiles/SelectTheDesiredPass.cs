using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingWithInputFiles
{
    public class SelectTheDesiredPass
    {
        public string ReturnFileFromAll(List<string> dirs, int flag)
        {
            List<string> dirsList = dirs;
            var flags = flag;
            for (int i = 0; i < flags; i++)
                dirsList.Remove(dirsList.Last());
            return dirsList.Last().ToString();
        }
    }
}
