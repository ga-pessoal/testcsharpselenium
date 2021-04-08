using System;
using OpenQA.Selenium;
using System.Threading;

namespace selenium.automacao.sil.PageObjects
{
    class MenuSuperiorPage
    {
        //Operacao
        private By operacao;
        private By clipasNet;
        private By perfilProfissional;
        private By operacaoCadastroCompleto;

        //Cadastro
        private By cadastro;
        private By profissional;
        private By cadastroCompleto;

        //Web Driver
        private IWebDriver driver;

        public MenuSuperiorPage(IWebDriver driver)
        {
            this.driver = driver;

            operacao = By.XPath("/html/body/form/div[4]/div[8]/div/div/ul/li[3]/a");
            clipasNet = By.LinkText("Clipas Net");
            perfilProfissional = By.LinkText("Perfil Profissional");
            operacaoCadastroCompleto = By.XPath("//*[@id='navbar-menu']/div/div/ul/li[3]/ul/li[2]/ul/li[2]/ul/li[1]/a");

            cadastro = By.LinkText("Cadastro");
            profissional = By.LinkText("Profissional");
            cadastroCompleto = By.XPath("//*[@id='navbar-menu']/div/div/ul/li[2]/ul/li[4]/ul/li[2]/a");
        }

        public void CadastroMotoristaCompleto(string local)
        {
            if (local == "ClipasNet")
            {
                driver.FindElement(operacao).Click();
                driver.FindElement(clipasNet).Click();
                driver.FindElement(perfilProfissional).Click();
                driver.FindElement(operacaoCadastroCompleto).Click();
            }
            else if (local == "Cadastro")
            {
                driver.FindElement(cadastro).Click();
                driver.FindElement(profissional).Click();
                driver.FindElement(cadastroCompleto).Click();
            }
            
        }
    }
}
