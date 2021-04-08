using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using selenium.automacao.sil.Helpers;

namespace selenium.automacao.sil.Fixture
{
    public class TestFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        //Setup
        public TestFixture()
        {
            Driver = new ChromeDriver(TestHelper.PastaDoExecutavel);
        }

        //TearDown
        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
