using Alura.ByteBank.WebApp.Testes.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Alura.ByteBank.WebApp.Testes
{
	public class AposRealizarLogin
	{
		private IWebDriver driver;
		public ITestOutputHelper output;

		public AposRealizarLogin(ITestOutputHelper output)
		{
			driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			this.output = output;
		}

		[Fact]
		public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
		{
			// Arrange
			var loginPO = new LoginPO(driver);
			loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

			// Act
			loginPO.PreencherCampos("andre@email.com", "senha01");
			loginPO.Logar();

			// Assert
			Assert.Contains("Agência", driver.PageSource);
		}

		[Fact]
		public void TentaRealizarLoginSemPreencherCampos()
		{
			// Arrange
			var loginPO = new LoginPO(driver);
			loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

			// Act
			loginPO.PreencherCampos("", "");
			loginPO.Logar();

			// Assert
			Assert.Contains("The Email field is required.", driver.PageSource);
			Assert.Contains("The Senha field is required.", driver.PageSource);
		}

		[Fact]
		public void TentaRealizarLoginComSenhaInvalida()
		{
			// Arrange
			var loginPO = new LoginPO(driver);
			loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

			// Act
			loginPO.PreencherCampos("andre@email.com", "senha010");
			loginPO.Logar();

			// Assert
			Assert.Contains("Login", driver.PageSource);
		}

		[Fact]
		public void RealizarLoginAcessaMenuECadastraCliente()
		{
			// Arrange
			var loginPO = new LoginPO(driver);
			loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

			loginPO.PreencherCampos("andre@email.com", "senha01");
			loginPO.Logar();

			driver.FindElement(By.LinkText("Cliente")).Click();

			driver.FindElement(By.LinkText("Adicionar Cliente")).Click();

			driver.FindElement(By.Name("Identificador")).Click();
			driver.FindElement(By.Name("Identificador")).SendKeys("53c537e0-efd8-4101-ab99-2e61f0b3bcb1");
			driver.FindElement(By.Name("CPF")).Click();
			driver.FindElement(By.Name("CPF")).SendKeys("69981034096");
			driver.FindElement(By.Name("Nome")).Click();
			driver.FindElement(By.Name("Nome")).SendKeys("Tobey Garfield");
			driver.FindElement(By.Name("Profissao")).Click();
			driver.FindElement(By.Name("Profissao")).SendKeys("Cientista");

			// Act
			driver.FindElement(By.CssSelector(".btn-primary")).Click();
			driver.FindElement(By.LinkText("Home")).Click();

			// Assert
			Assert.Contains("Logout", driver.PageSource);
		}

		[Fact]
		public void RealizarLoginAcessaListagemDeContas()
		{
			// Arrange
			var loginPO = new LoginPO(driver);
			loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

			loginPO.PreencherCampos("andre@email.com", "senha01");
			loginPO.Logar();

			var homePO = new HomePO(driver);
			homePO.LinkContaCorrentesClick();

			IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.TagName("a"));

            foreach (var element in elements)
            {
				output.WriteLine(element.Text);
            }

			var elemento = (from webElemento in elements
							where webElemento.Text.Contains("Detalhes")
							select webElemento).FirstOrDefault();

			// Act
			elemento.Click();

			// Assert
			//Assert.True(elements.Count == 12);
			Assert.Contains("Voltar", driver.PageSource);
        }
	}
}
