using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace selenium.automacao.sil.PageObjects
{
    public class LoginPage
    {
        public string cliente { get; set; }
        
        private IWebDriver driver;
        private WebDriverWait wait;

        private string URL = "http://silpaineldesenv.opentechgr.com.br/Login.aspx";
        //private string URL = "http://sil.opentechgr.com.br/";
        private string login = "GABRIELM";
        private string senha = "1900.Desen";

        private By txtUser;
        private By txtPass;
        private By btnAutenticar;
        private By txtCliente;
        private By btnValidarCliente;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;

            txtUser = By.Id("txtUser");
            txtPass = By.Id("txtPass");
            btnAutenticar = By.Id("btnAutenticar");
            txtCliente = By.Id("txtCliente");
            
            btnValidarCliente = By.Id("btnValidarCliente");
        }
        
        public void Login()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(URL);

            //Insere Login e Senha
            var loginDigitado = driver.FindElement(txtUser).GetAttribute("value");
            
            if(loginDigitado == login)
            {
                driver.FindElement(txtPass).SendKeys(senha);
            }
            else
            {
                driver.FindElement(txtUser).SendKeys(login);
                driver.FindElement(txtPass).SendKeys(senha);
            }
            driver.FindElement(btnAutenticar).Click();

            //Selecionar cliente

            //OPENTECH TESTE
            driver.FindElement(txtCliente).SendKeys(cliente);

            var SelecionarCliente = new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.ElementToBeClickable(By.LinkText(cliente)));
            SelecionarCliente.Click();

            driver.FindElement(btnValidarCliente).Click();
        }
    }
}