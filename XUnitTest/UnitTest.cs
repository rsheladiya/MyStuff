using ConsoleApp;
using System;
using System.Linq;
using Xunit;

namespace XUnitTest
{
    public class UnitTest
    {
        [Fact]
        public void WithouthttpAtStartOfLink_NoLinks()
        {
            var Links = LinkChecker.GetLinks("<a href =\"google.com\"/>");
            Assert.Equal(Links.Count(), 0);
            //Assert.Equal(Links.First(), "google.com");
           // Assert.True(true);
        }
        [Fact]
        public void WithouthttpAtStartOfLink_LinkParser()
        {
            var Links = LinkChecker.GetLinks("<a href =\"http://google.com\"/>");
            Assert.Equal(Links.Count(), 1);
            Assert.Equal(Links.First(), "http://google.com");
         
        }
    }
}
