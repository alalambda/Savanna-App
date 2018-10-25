using SavannaApp.Runner;

namespace SavannaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new ImportingLibrary.Importer();
            t.DoImport();

            new GameRunner().Start();
        }
    }
}
