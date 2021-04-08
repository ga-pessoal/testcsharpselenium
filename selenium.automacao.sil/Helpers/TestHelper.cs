using System.IO;
using System.Reflection;


namespace selenium.automacao.sil.Helpers
{
    public static class TestHelper
    {
        public static string PastaDoExecutavel => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
