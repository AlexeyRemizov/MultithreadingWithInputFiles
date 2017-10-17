using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using Xunit;

namespace MultithreadingWithInputFiles.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CheckAmountOfAllFilesTest()
        {
            GetAllFilesInTheDirectory getFiles = new GetAllFilesInTheDirectory();

            var result = getFiles.FindAllFilesInDirectory(@"d:\logs!\").Count;

            Assert.Equal(22, result);
        }

        [Fact]
        public void ChechLastFileOnNull()
        {
            GetAllFilesInTheDirectory getFiles = new GetAllFilesInTheDirectory();
            var fileDirectory = ConfigurationManager.AppSettings[@"filedirectory_"];

            var result = getFiles.FindAllFilesInDirectory(@"d:\logs!\")[getFiles.FindAllFilesInDirectory(@"d:\logs!\").Count - 1];

            Assert.NotNull(result);
        }

        
    }
}
