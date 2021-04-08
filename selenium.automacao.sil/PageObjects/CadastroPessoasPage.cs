using System.Threading;
using System.Linq;
using System;

using Xunit;
using OpenQA.Selenium;
using opentech.automacao.core;
using OpenQA.Selenium.Support.UI;

namespace selenium.automacao.sil.PageObjects
{
    public class CadastroPessoasPage
    {
        private Geradores gerador;
        public string CpfGerado { get; private set; }
        private string FRAME  = "frawinActive_0";
        private string FRAME1 = "frawinActive_1";

        private string cpfComPontuacao;
        private static string _RG;
        private static string _CNH;

        //Campos Cadastro Motorista - completo
        //Aba - Documentos
        private By abaDocumentos;
        private By txtCPF;
        private By txtRG;
        private By cmbOrgaoExp;
        private By edtDataEmissao;
        private By cmbVinculo;
        // * CNH
        private By txtNrDoc;
        private By txtNrReg;
        private By cmbUF;
        private By edtEmissaoHab;
        private By edtVencimentoHab;
        private By cmbCategoria;
        //Aba - Dados Pessoais
        private By abaDadosPessoais;
        private By txtNome;
        private By txtNat;
        private By selecNat;
        private By txtNascimento;
        private By cmbSexo;
        private By txtPai;
        private By txtMae;
        // * Endereço
        private By txtLogradouro;
        private By txtNumero;
        private By txtBairro;
        private By txtCidade;
        private By selecCidadeEndereco;
        private By txtDDDFixo;
        private By txtTelefone;
        //Aba - Referencias
        private By abaReferencias;
        private By txtNomeReferencia;
        private By cmbTipoRelacionamento;
        private By txtCidRef;
        private By selecCidRef;
        private By txtDDDFone1;
        private By txtTelefoneReferencia;
        private By btnAddReferencia;

        private By btnSalvar;
        private By btnEnviarPesquisa;

        //Optin
        private By btnConfirmaOptin;
        private By btnCancelaOptin;
        private By btnFechaAviso;
        private By btnFechaOptin;
        
        //Web Driver
        private IWebDriver driver;

        public CadastroPessoasPage(IWebDriver driver)
        {
            gerador = new Geradores();
            this.driver = driver;

            txtCPF = By.Id("txtCPF");
            abaDocumentos = By.Id("ui-id-1");
            txtRG = By.Id("txtRG");
            cmbOrgaoExp = By.Id("cmbOrgaoExp");
            edtDataEmissao = By.Id("edtDataEmissao");
            cmbVinculo = By.Id("cmbVinculo");

            txtNrDoc = By.Id("txtNrDoc");
            txtNrReg = By.Id("txtNrReg");
            cmbUF = By.Id("cmbUF");
            edtEmissaoHab = By.Id("edtEmissaoHab");
            edtVencimentoHab = By.Id("edtVencimentoHab");
            cmbCategoria = By.Id("cmbCategoria");

            abaReferencias = By.Id("ui-id-2");
            txtNomeReferencia = By.Id("txtNomeReferencia");
            cmbTipoRelacionamento = By.Id("cmbTipoRelacionamento");
            txtCidRef = By.Id("txtCidRef");
            selecCidRef = By.LinkText("PORTO ALEGRE/RS");
            txtDDDFone1 = By.Id("txtDDDFone1");
            txtTelefoneReferencia = By.Id("txtTelefoneReferencia");
            btnAddReferencia = By.Id("tlbEspecialidade_tlbEspecialidadeBtnAppendImg");

            abaDadosPessoais = By.Id("ui-id-3");
            txtNome = By.Id("txtNome");
            txtNat = By.Id("txtNat");
            selecNat = By.LinkText("PORTO ALEGRE/RS");
            txtNascimento = By.Id("txtNascimento");
            cmbSexo = By.Id("cmbSexo");
            txtPai = By.Id("txtPai");
            txtMae = By.Id("txtMae");

            txtLogradouro = By.Id("txtLogradouro");
            txtNumero = By.Id("txtNumero");
            txtBairro = By.Id("txtBairro");
            txtCidade = By.Id("txtCid");
            selecCidadeEndereco = By.LinkText("PORTO ALEGRE/RS");
            txtDDDFixo = By.Id("txtDDDFixo");
            txtTelefone = By.Id("txtTelefone");
            
            btnSalvar = By.Id("tlbPrincipal_btnSalvarImg");
            btnEnviarPesquisa = By.XPath("/html/body/div[6]/div[3]/div/button[1]");

            btnConfirmaOptin = By.Id("btnEnviar");
            btnCancelaOptin = By.Id("btnCancelar");
            btnFechaAviso = By.XPath("/html/body/div[3]/div[3]/div/button/span");
            btnFechaOptin = By.XPath("//*[@id='OpenPopup_ctl00_HCB1']/img");
            
        }

        public void CadastroMotoristaCompleto()
        {
            _RG = gerador.NumeroRandomico(9);
            _CNH = gerador.NumeroRandomico(11);

            //Espera localizar o campo e logo em seguida começa o cadastro de motorista
            var rgLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(txtRG));

            Assert.Equal(null, rgLocaliza.GetAttribute("disabled"));            
            rgLocaliza.SendKeys(_RG);

            //driver.FindElement(txtRG).SendKeys(rg);
            driver.FindElement(cmbOrgaoExp).SendKeys("ssp" + Keys.Tab);
            driver.FindElement(edtDataEmissao).SendKeys("10/03/2008");
            driver.FindElement(cmbVinculo).SendKeys("AGREGADO" + Keys.Tab);
            
            driver.FindElement(txtNrDoc).SendKeys(_CNH);
            driver.FindElement(txtNrReg).SendKeys(_CNH);
            driver.FindElement(cmbUF).SendKeys("RS" + Keys.Tab);
            driver.FindElement(edtEmissaoHab).SendKeys("10/03/2008");
            driver.FindElement(edtVencimentoHab).SendKeys("21/09/2030");
            driver.FindElement(cmbCategoria).SendKeys("AE" + Keys.Tab);
            
            driver.FindElement(abaReferencias).Click();
            
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) {
                    driver.FindElement(txtNomeReferencia).SendKeys("MAE Referencia");
                    driver.FindElement(cmbTipoRelacionamento).SendKeys("MAE" + Keys.Tab);
                }
                if (i == 1) {
                    driver.FindElement(txtNomeReferencia).SendKeys("PAI Referencia");
                    driver.FindElement(cmbTipoRelacionamento).SendKeys("PAI" + Keys.Tab);
                }
                if (i == 2) {
                    driver.FindElement(txtNomeReferencia).SendKeys("AVO Referencia");
                    driver.FindElement(cmbTipoRelacionamento).SendKeys("AVO" + Keys.Tab);
                }
                driver.FindElement(txtCidRef).SendKeys("PORTO ALEGRE/R");
                driver.FindElement(txtCidRef).SendKeys("S");
                driver.FindElement(selecCidRef).Click();
                driver.FindElement(txtDDDFone1).SendKeys("51");

                string numeroRandomico = gerador.NumeroRandomico(9);
                driver.FindElement(txtTelefoneReferencia).SendKeys(numeroRandomico);
                
                driver.FindElement(btnAddReferencia).Click();
            }            

            driver.FindElement(abaDadosPessoais).Click();
            driver.FindElement(txtNome).SendKeys("Teste Nome da Silva");
            driver.FindElement(txtNat).SendKeys("PORTO ALEGRE/RS");
            driver.FindElement(selecNat).Click();
            driver.FindElement(txtNascimento).SendKeys("18/03/1993" + Keys.Tab);
            driver.FindElement(cmbSexo).SendKeys("Mas" + Keys.Tab);
            driver.FindElement(txtPai).SendKeys("Teste PAI da Silva");
            driver.FindElement(txtMae).SendKeys("Teste MAE da Silva");

            driver.FindElement(txtLogradouro).SendKeys("RUA JULIA DIB");
            driver.FindElement(txtNumero).SendKeys("1598");
            driver.FindElement(txtBairro).SendKeys("SANTA ROSA");
            driver.FindElement(txtCidade).SendKeys("PORTO ALEGRE/RS");
            driver.FindElement(selecCidadeEndereco).Click();
            driver.FindElement(txtDDDFixo).SendKeys("51");
            driver.FindElement(txtTelefone).SendKeys("33672524");

            driver.FindElement(btnSalvar).Click();

            var btnEnviarPesquisaLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(btnEnviarPesquisa));
            btnEnviarPesquisaLocaliza.Click();

            var btnFecharAlertaLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[3]/div/button/span")));
            btnFecharAlertaLocaliza.Click();
        }

        public void AlteraCadastroMotorista(int status)
        {
            
        }

        public void ValidaCamposInativos()
        {
            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(FRAME);

            var txtRGLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(txtRG));
            
            Assert.Equal("true", txtRGLocaliza.GetAttribute("disabled"));
            Assert.Equal("true", driver.FindElement(cmbOrgaoExp).GetAttribute("disabled"));
            Assert.Equal("true", driver.FindElement(edtDataEmissao).GetAttribute("disabled"));
            Assert.Equal("true", driver.FindElement(cmbVinculo).GetAttribute("disabled"));
            Assert.Equal("true", driver.FindElement(txtNrDoc).GetAttribute("disabled"));
            Assert.Equal("true", driver.FindElement(txtNrReg).GetAttribute("disabled"));
            Assert.Equal("true", driver.FindElement(edtEmissaoHab).GetAttribute("disabled"));
            Assert.Equal("true", driver.FindElement(edtVencimentoHab).GetAttribute("disabled"));
            Assert.Equal("true", driver.FindElement(cmbCategoria).GetAttribute("disabled"));
        }

        //Optin - conforme proposto pela LGPD
        public void ConfirmaOptin(int status)
        {
            CpfGerado = gerador.GeradorCPF();
            //driver.SwitchTo().Frame(FRAME);

            if (status==0) //Livre de Optin
            {
                driver.FindElement(txtCPF).SendKeys(CpfGerado);
                driver.FindElement(txtCPF).SendKeys(Keys.Tab);
            }
            else if(status == 1 || status == 2)
            {
                cpfComPontuacao = driver.FindElement(txtCPF).GetAttribute("value");
                driver.FindElement(txtCPF).SendKeys(CpfGerado + Keys.Tab);
                cpfComPontuacao = driver.FindElement(txtCPF).GetAttribute("value");

                driver.SwitchTo().ParentFrame();
                driver.SwitchTo().Frame(FRAME1);

                driver.FindElement(txtNome).SendKeys("Teste Automacao Optin");
                driver.FindElement(txtTelefone).SendKeys("47999118329");

                //Verifica se é mesmo CPF inserido na tela de cadastro de motorista
                Assert.Equal(cpfComPontuacao, driver.FindElement(txtCPF).GetAttribute("value"));

                driver.FindElement(btnConfirmaOptin).Click();
                
                var btnFechaAvisoLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(btnFechaAviso));
                btnFechaAvisoLocaliza.Click();

                driver.SwitchTo().ParentFrame();
                driver.FindElement(btnFechaOptin).Click();
                driver.SwitchTo().Frame(FRAME);

                var tituloCadastroLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.Id("OpenFormTitulo1")));
                tituloCadastroLocaliza.GetAttribute("value");

            }
        }

        public void CancelaOptin()
        {
            CpfGerado = gerador.GeradorCPF();
            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(FRAME);

            driver.FindElement(txtCPF).SendKeys(CpfGerado + Keys.Tab);
            cpfComPontuacao = driver.FindElement(txtCPF).GetAttribute("value");

            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(FRAME1);

            //Verifica se é mesmo CPF inserido na tela de cadastro de motorista
            Assert.Equal(cpfComPontuacao, driver.FindElement(txtCPF).GetAttribute("value"));

            var btnCancelaOptinLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(btnCancelaOptin));
            btnCancelaOptinLocaliza.Click();

            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(FRAME);
        }

        public void ValidaOptinCancelado()
        {
            driver.FindElement(txtCPF).SendKeys(CpfGerado + Keys.Tab);
            cpfComPontuacao = driver.FindElement(txtCPF).GetAttribute("value");

            Thread.Sleep(5000);

            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(FRAME1);

            var txtNomeLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(txtNome));
            Assert.Equal("", txtNomeLocaliza.GetAttribute("value"));
            Assert.Equal("", driver.FindElement(txtTelefone).GetAttribute("value"));
            Assert.Equal(cpfComPontuacao, driver.FindElement(txtCPF).GetAttribute("value"));
        }

        public void ValidaOptin(int status)
        {
            _CNH = gerador.NumeroRandomico(11);

            var abaDocumentosLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(abaDocumentos));
            abaDocumentosLocaliza.Click();

            driver.FindElement(txtCPF).SendKeys(CpfGerado + Keys.Tab);
            cpfComPontuacao = driver.FindElement(txtCPF).GetAttribute("value");

            if (status == 0)
            {
                var txtNrDocLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(txtNrDoc));
                txtNrDocLocaliza.Clear();
                txtNrDocLocaliza.SendKeys(_CNH);

                driver.FindElement(txtNrReg).SendKeys(_CNH);
                driver.FindElement(btnSalvar).Click();
            }
            else if (status == 1 || status == 2)
            {
                Thread.Sleep(5000);

                driver.SwitchTo().ParentFrame();
                driver.SwitchTo().Frame(FRAME1);
                
                var txtNomeLocaliza = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(txtNome));
                Assert.Contains("TESTE AUTOMACAO OPTIN", txtNomeLocaliza.GetAttribute("value"));
                Assert.Equal("(47) 99911-8329", driver.FindElement(txtTelefone).GetAttribute("value"));
                Assert.Equal(cpfComPontuacao, driver.FindElement(txtCPF).GetAttribute("value"));
            }


        }
    }
}
