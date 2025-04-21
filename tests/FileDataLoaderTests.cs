using System;
using System.Text;
using DataExplorer;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests
{
    public class FileDataLoaderTests
    {

        [Fact]
        public void Load_ShouldReturnTrue_WhenProperDataLoaded()
        {
            FileDataLoader loader = new FileDataLoader(GetValidStream());
            Assert.True(loader.Load());
        }

        Stream GetValidStream()
        {
            string sampleData = """
first_name;age;movie_name;score
string; integer; string; integer
tim; 26; inception; 8
tim; 26; pulp_fiction; 8
tamas; 44; inception; 7
tamas; 44; pulp_fiction; 4
dave; 0; inception; 8
dave; 0; ender's_game;8
""";

           return new MemoryStream(Encoding.UTF8.GetBytes(sampleData));
        }
    }
}
