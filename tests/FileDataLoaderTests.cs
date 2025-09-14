using System.Text;
using DataExplorer;

namespace Tests
{
    public class FileDataLoaderTests
    {
        [Fact]
        public void Load_ShouldReturnTrue_WhenValidSmallDataLoaded()
        {
            FileDataLoader loader = new FileDataLoader(GetSmallValidStream());
            Assert.True(loader.Load());
        }

        [Fact]
        public void Load_ShouldReturnTrue_WhenValidDataLoaded()
        {
            FileDataLoader loader = new FileDataLoader(GetValidStream());
            Assert.True(loader.Load());
        }

        [Fact]
        public void Load_ShouldReturnFalse_WhenInvalidDataLoadedWithTooManyEntries()
        {
            FileDataLoader loader = new FileDataLoader(GetInvalidStreamWithTooMuchEntries());
            Assert.False(loader.Load());
        }

        [Fact]
        public void Load_EmptyStream_ReturnsFalse()
        {
            using var stream = new MemoryStream();
            var loader = new FileDataLoader(stream);

            Assert.False(loader.Load());
        }

        [Fact]
        public void Load_MismatchedHeadersAndTypes_ReturnsFalse()
        {
            var csv = "Col1;Col2\nINTEGER\n1;hello\n";
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csv));
            var loader = new FileDataLoader(stream);

            Assert.False(loader.Load());
        }

        [Fact]
        public void Load_UnknownColumnType_ReturnsFalse()
        {
            var csv = "Col1\nFOO\n1\n";
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csv));
            var loader = new FileDataLoader(stream);

            Assert.Throws<NotImplementedException>(() => loader.Load());
        }

        [Fact]
        public void Load_DataLineWithWrongColumnCount_ReturnsFalse()
        {
            var csv = "Col1;Col2\nINTEGER;STRING\n1\n";
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(csv));
            var loader = new FileDataLoader(stream);

            Assert.False(loader.Load());
        }

        [Fact]
        public void Load_ShouldReturnFalse_WhenInvalidDataLoadedWithTooLittleEntries()
        {
            FileDataLoader loader = new FileDataLoader(GetInvalidStreamWithTooLittleEntries());
            Assert.False(loader.Load());
        }

        [Fact]
        public void GetColumnTypes_ShouldReturnCorrectColumnTypes_WhenSmallDataIsLoaded()
        {
            FileDataLoader loader = new FileDataLoader(GetSmallValidStream());
            loader.Load();
            ColumnType[] types = loader.GetColumnTypes();
            Assert.Equal([ColumnType.INTEGER, ColumnType.STRING], types);
        }

        [Fact]
        public void GetColumnTypes_ShouldReturnCorrectColumnTypes_WhenDataIsLoaded()
        {
            FileDataLoader loader = new FileDataLoader(GetValidStream());
            loader.Load();
            ColumnType[] types = loader.GetColumnTypes();
            Assert.Equal([ColumnType.STRING, ColumnType.INTEGER, ColumnType.STRING, ColumnType.INTEGER], types);
        }

        [Fact]
        public void GetHeaders_ShouldReturnCorrectHeaders_WhenDataIsLoaded()
        {
            FileDataLoader loader = new FileDataLoader(GetValidStream());
            loader.Load();
            string[] headers = loader.GetHeaders();
            Assert.Equal(["first_name", "age", "movie_name", "score"], headers);
        }

        [Fact]
        public void GetHeaders_ShouldReturnCorrectHeaders_WhenSmallDataIsLoaded()
        {
            FileDataLoader loader = new FileDataLoader(GetSmallValidStream());
            loader.Load();
            string[] headers = loader.GetHeaders();
            Assert.Equal(["header1", "header2"], headers);
        }

        [Fact]
        public void GetData_ShouldReturnCorrectData_WhenDataIsLoaded()
        {
            FileDataLoader loader = new FileDataLoader(GetValidStream());
            loader.Load();
            IColumn[] data = loader.GetData();
            Assert.Equal(4, data.Length);
            foreach (var column in data)
                Assert.Equal(6, column.GetSize());
        }

        [Fact]
        public void GetData_ShouldReturnCorrectData_WhenSmallDataIsLoaded()
        {
            FileDataLoader loader = new FileDataLoader(GetSmallValidStream());
            loader.Load();
            IColumn[] data = loader.GetData();
            Assert.Equal(2, data.Length);
            foreach (var column in data)
                Assert.Equal(1, column.GetSize());
        }

        static MemoryStream GetValidStream()
        {
            string sampleData = """
first_name;age;movie_name;score
string;integer;string;integer
tim;26;inception;8
tim;26;pulp_fiction;8
tamas;44;inception;7
tamas;44;pulp_fiction;4
dave;0;inception;8
dave;0;ender's_game;8
""";

            return new MemoryStream(Encoding.UTF8.GetBytes(sampleData));
        }

        static MemoryStream GetSmallValidStream()
        {
            string sampleData = "header1;header2\ninteger;string\n1;hello";
            return new MemoryStream(Encoding.UTF8.GetBytes(sampleData));
        }

        static MemoryStream GetInvalidStreamWithTooMuchEntries()
        {
            string sampleData = "header1;header2\ninteger;string\n1;hello;f";
            return new MemoryStream(Encoding.UTF8.GetBytes(sampleData));
        }

        static MemoryStream GetInvalidStreamWithTooLittleEntries()
        {
            string sampleData = "header1;header2\ninteger;string\n1";
            return new MemoryStream(Encoding.UTF8.GetBytes(sampleData));
        }
    }
}
