using Xunit;
using OpenQA.Selenium;

using selenium.automacao.sil.Fixture;
using selenium.automacao.sil.PageObjects;

namespace selenium.automacao.sil.Tests
{
    [Collection("Chrome Driver")]
    public class OptinTests
    {
        private IWebDriver driver;
        private static string _clienteLivre = "OPENTECH - TESTE | 10.887.479/0001-83";
        private string _clienteNaoObrigatorio = "OPENTECH - FROTA | 46.809.176/0001-90";
        private string _clienteObrigatorio = "OPENTECH - DIVERSOS | 05.291.010/0002-28";

        private CadastroPessoasPage cadastroPessoaPage;
        private MenuSuperiorPage menuSuperiorPage;
        private LoginPage loginPage;

        public OptinTests(TestFixture fixture)
        {
            driver = fixture.Driver;
            cadastroPessoaPage = new CadastroPessoasPage(driver);
            menuSuperiorPage = new MenuSuperiorPage(driver);
        }

        //Cenários de execução
        //Locais - ClipasNet, Cadastro

        //1- Cadastrar CPF no optin
        //2- CPF já cadastrado 
            //Não deve abrir janela
            //* Deve abrir uma janela com a mensagem 
        [Theory, Trait("Priority", "1"), Trait("Category", "Optin")]
        [InlineData("ClipasNet", new int[] { 1,2 }, new string[]{ "Cadastro de perfil profissional - completo", "Em cumprimento a LGPD, foi enviado um link para o" })]
        [InlineData("Cadastro", new int[] { 1, 2 }, new string[]{ "Cadastro de perfil profissional - completo", "Em cumprimento a LGPD, foi enviado um link para o" }
        )]
        public void CadastroOptin(
            string local,
            int[] cenario,
            string[] asstert
        )
        {
            loginPage = new LoginPage(driver);
            loginPage.cliente = _clienteNaoObrigatorio;
            loginPage.Login();

            //Acessar modal de cadastro de motorista completo
            menuSuperiorPage.CadastroMotoristaCompleto(local);

            //Para cada cenário { 1 cadastro ,2 cadastrodomesmo } o teste deverá ser executado
            cadastroPessoaPage.ValidaCamposInativos();
            for (int i = 0; i < cenario.Length; i++)
            {
                if (cenario[i] == 1) // Cadastro
                {
                    cadastroPessoaPage.ConfirmaOptin(1);
                }
                else if (cenario[i] == 2) // Cadastro com mesmo CPF
                {
                    cadastroPessoaPage.ValidaOptin(1);
                }

                //Motorista Cadastrado com sucesso no optin  e retorna para modal de cadastro
                Assert.Contains(asstert[i], driver.PageSource);
            }
        }

        //3- CPF cancelado - validar campos ativos 
            //Deve mostrar mensagem - Cancelamento para Armazenamento de dados
        //4- Tentar novamente CPF cancelado para ver se da para cadastrar
            //Deve abrir modal com mensagem - Como Controlador de dados pessoais
        [Theory, Trait("Priority", "2"), Trait("Category", "Optin")]
        [InlineData("ClipasNet", new int[] { 1, 2 }, new string[]{"Cadastro de perfil profissional - completo", "Em cumprimento a LGPD, enviaremos um link para o" })]
        [InlineData("Cadastro", new int[] { 1, 2 }, new string[] { "Cadastro de perfil profissional - completo", "Em cumprimento a LGPD, enviaremos um link para o" })]
        public void CancelaOptin(
            string local,
            int[] cenario,
            string[] asstert
        )
        {
            loginPage = new LoginPage(driver);
            loginPage.cliente = _clienteNaoObrigatorio;
            loginPage.Login();

            //Acessar modal de cadastro de motorista completo
            menuSuperiorPage.CadastroMotoristaCompleto(local);

            //Para cada status o teste deverá ser executado
            for (int i = 0; i < cenario.Length; i++)
            {
                if (cenario[i] == 1) // Cancela Optin
                {
                    cadastroPessoaPage.CancelaOptin();
                }
                else if (cenario[i] == 2) // Cadastro com optin cancelado
                {
                    cadastroPessoaPage.ValidaOptinCancelado();
                }

                //Motorista Cadastrado com sucesso no optin  e retorna para modal de cadastro
                Assert.Contains(asstert[i], driver.PageSource);
            }
        }

        //Cenários de bloqueio da tela de cadastro de motorista
        //0 - livre
        //1 - não obrigratório
        //2 - obrigátório
        [Theory, Trait("Priority", "3"), Trait("Category", "Optin")]
        [InlineData(0, "ClipasNet", "OPENTECH - TESTE | 10.887.479/0001-83")]
        [InlineData(1, "ClipasNet", "OPENTECH - FROTA | 46.809.176/0001-90")]
        [InlineData(2, "ClipasNet", "OPENTECH - DIVERSOS | 05.291.010/0002-28")]
        [InlineData(0, "Cadastro", "OPENTECH - TESTE | 10.887.479/0001-83")]
        [InlineData(1, "Cadastro", "OPENTECH - FROTA | 46.809.176/0001-90")]
        [InlineData(2, "Cadastro", "OPENTECH - DIVERSOS | 05.291.010/0002-28")]
        public void DadoClienteCadastraOptinComStatus(int status, string local, string cliente)
        {
            loginPage = new LoginPage(driver);
            loginPage.cliente = cliente;
            loginPage.Login();
            
            //Acessar modal de cadastro de motorista completo
            menuSuperiorPage.CadastroMotoristaCompleto(local);
            if (status == 2)
            {
                //Valida os campos inativos quando entra na tela
                //Confirma Optin enviando o status
                cadastroPessoaPage.ValidaCamposInativos();
                cadastroPessoaPage.ConfirmaOptin(status);

                //Valida o Resultado
                cadastroPessoaPage.ValidaCamposInativos();
                Assert.Contains("Cadastro de perfil profissional - completo", driver.PageSource);

                //Ajuste na parte de referencias
                cadastroPessoaPage.ValidaOptin(status);
                Assert.Contains("", driver.PageSource);
            }
            else
            {
                //Valida os campos inativos quando entra na tela
                //Confirma Optin enviando o status
                //Cadastra o motorista
                cadastroPessoaPage.ValidaCamposInativos();
                cadastroPessoaPage.ConfirmaOptin(status);
                cadastroPessoaPage.CadastroMotoristaCompleto();
                
                //Valida Se Cadastrou o Motorista e voltou para a tela de cadastro
                Assert.Contains("Cadastro de perfil profissional - completo", driver.PageSource);

                //Ajuste na parte de referencias na hora de alterar o cadastro
                cadastroPessoaPage.ValidaOptin(status);
                if (status == 1)
                {
                    Assert.Contains("Em cumprimento a LGPD, foi enviado um link para o", driver.PageSource);
                }
                else
                {
                    Assert.Contains("Cadastro realizado com sucesso! Deseja enviar para a pesquisa?", driver.PageSource);
                }
                
            }
        }
    }
}
