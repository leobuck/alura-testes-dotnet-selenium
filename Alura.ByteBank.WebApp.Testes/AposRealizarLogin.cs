using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;
using System.Reflection;
using Xunit;
using System.Collections.Generic;
using Xunit.Abstractions;
using System.Linq;

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
			driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

			var login = driver.FindElement(By.Id("Email"));
			var senha = driver.FindElement(By.Id("Senha"));
			var btnLogar = driver.FindElement(By.Id("btn-logar"));

			login.SendKeys("andre@email.com");
			senha.SendKeys("senha01");

			// Act
			btnLogar.Click();

			// Assert
			Assert.Contains("Agência", driver.PageSource);
		}

		[Fact]
		public void TentaRealizarLoginSemPreencherCampos()
		{
			// Arrange
			driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

			var btnLogar = driver.FindElement(By.Id("btn-logar"));

			// Act
			btnLogar.Click();

			// Assert
			Assert.Contains("The Email field is required.", driver.PageSource);
			Assert.Contains("The Senha field is required.", driver.PageSource);
		}

		[Fact]
		public void TentaRealizarLoginComSenhaInvalida()
		{
			// Arrange
			driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

			var login = driver.FindElement(By.Id("Email"));
			var senha = driver.FindElement(By.Id("Senha"));
			var btnLogar = driver.FindElement(By.Id("btn-logar"));

			login.SendKeys("andre@email.com");
			senha.SendKeys("senha010");

			// Act
			btnLogar.Click();

			// Assert
			Assert.Contains("Login", driver.PageSource);
		}

		[Fact]
		public void RealizarLoginAcessaMenuECadastraCliente()
		{
			driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

			var login = driver.FindElement(By.Name("Email"));
			var senha = driver.FindElement(By.Name("Senha"));

			login.SendKeys("andre@email.com");
			senha.SendKeys("senha01");

			driver.FindElement(By.CssSelector(".btn")).Click();

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
			driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

			var login = driver.FindElement(By.Name("Email"));
			var senha = driver.FindElement(By.Name("Senha"));

			login.SendKeys("andre@email.com");
			senha.SendKeys("senha01");

			driver.FindElement(By.CssSelector(".btn")).Click();

			driver.FindElement(By.Id("contacorrente")).Click();

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
