﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using Xunit;

namespace Alura.ByteBank.WebApp.Testes
{
	public class NavegandoNaPaginaHome
	{
		[Fact]
		public void CarregaPaginaHomeEVerificaTituloDaPagina()
		{
			// Arrange
			IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

			// Act
			driver.Navigate().GoToUrl("https://localhost:44309");

			// Assert
			Assert.Contains("WebApp", driver.Title);
		}

		[Fact]
		public void CarregadaPaginaHomeVerificaExistenciaLinkLoginEHome()
		{
			// Arrange
			IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

			// Act
			driver.Navigate().GoToUrl("https://localhost:44309");

			// Assert
			Assert.Contains("Login", driver.PageSource);
			Assert.Contains("Home", driver.PageSource);
		}

		[Fact]
		public void LogandoNoSistema()
		{
			IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

			driver.Navigate().GoToUrl("https://localhost:44309/");
			driver.Manage().Window.Size = new System.Drawing.Size(842, 674);
			driver.FindElement(By.LinkText("Login")).Click();
			driver.FindElement(By.Id("Email")).Click();
			driver.FindElement(By.Id("Email")).SendKeys("andre@email.com");
			driver.FindElement(By.Id("Senha")).SendKeys("senha01");
			driver.FindElement(By.Id("btn-logar")).Click();
			driver.FindElement(By.CssSelector(".btn")).Click();
			driver.Close();
		}

		[Fact]
		public void ValidaLinkDeLoginNaHome()
		{
			// Arrange
			IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			driver.Navigate().GoToUrl("https://localhost:44309/");

			var linkLogin = driver.FindElement(By.LinkText("Login"));

			// Act
			linkLogin.Click();

			// Assert
			Assert.Contains("img", driver.PageSource);
		}

		[Fact]
		public void TentaAcessarPaginaSemEstarLogado()
		{
			// Arrange
			IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

			// Act
			driver.Navigate().GoToUrl("https://localhost:44309/Agencia/Index");

			// Assert 
			Assert.Contains("401", driver.PageSource);
		}

		[Fact]
		public void AcessaPaginaSemEstarLogadoVerificaURL()
		{
			// Arrange
			IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

			// Act
			driver.Navigate().GoToUrl("https://localhost:44309/Agencia/Index");

			// Assert 
			Assert.Contains("https://localhost:44309/Agencia/Index", driver.Url);
			Assert.Contains("401", driver.PageSource);
		}
	}
}
