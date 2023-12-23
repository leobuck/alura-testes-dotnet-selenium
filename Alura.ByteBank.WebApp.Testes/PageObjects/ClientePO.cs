using OpenQA.Selenium;

namespace Alura.ByteBank.WebApp.Testes.PageObjects
{
	public class ClientePO
	{
		private IWebDriver driver;
		private By campoIdentificador;
		private By campoCpf;
		private By campoNome;
		private By campoProfissao;
		private By btnSalvar;
		private By btnAdicionarCliente;
		private By btnHome;

		public ClientePO(IWebDriver driver)
		{
			this.driver = driver;
			campoIdentificador = By.Name("Identificador");
			campoCpf = By.Name("CPF");
			campoNome = By.Name("Nome");
			campoProfissao = By.Name("Profissao");
			btnSalvar = By.CssSelector(".btn-primary");
			btnAdicionarCliente = By.LinkText("Adicionar Cliente");
			btnHome = By.LinkText("Home");
		}

		public void PreencherCampos(string identificador, string cpf, string nome, string profissao)
		{
			driver.FindElement(campoIdentificador).SendKeys(identificador);
			driver.FindElement(campoCpf).SendKeys(cpf);
			driver.FindElement(campoNome).SendKeys(nome);
			driver.FindElement(campoProfissao).SendKeys(profissao);
		}

		public void Salvar()
		{
			driver.FindElement(btnSalvar).Click();
		}

		public void AdicionarCliente()
		{
			driver.FindElement(btnAdicionarCliente).Click();
		}

		public void VaiParaHome()
		{
			driver.FindElement(btnHome).Click();
		}
	}
}
