using AeCAutomation.Domain.Entity;
using AeCAutomation.Repository;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data;
using System.Text.RegularExpressions;

namespace AeCAutomation.Services
{
    public class SearchService : ISearchService

    {
        private readonly ISearchRepository repository;
        public SearchService(ISearchRepository repository)
        {
            this.repository = repository;
        }

        public void CriarBase()
        {
            repository.CriarBase();
        }

        public bool Existe(Busca buscar)
        {
            bool retorno = false;
            try
            {
                var existe = repository.Pesquisa(buscar);
                if (existe.Rows.Count > 0)
                    retorno = true;

                return retorno;
            }
            catch (System.Exception)
            {

                throw;
            };
        }

        public DataTable ListaBuscas()
        {
            return repository.ListaBuscas();
        }

        public void Salvar(Busca busca)
        {

            if (!Existe(busca))
            {
                repository.Salvar(busca);
            }

        }

        public void SearchAndStoreData(string searchTerm)
        {
            // Inicializando o WebDriver
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");

            using (var driver = new ChromeDriver(driverService, chromeOptions))
            {
                // Navegar para o site desejado
                driver.Navigate().GoToUrl("https://www.aec.com.br/");

                System.Threading.Thread.Sleep(3000);
                // Encontrar o elemento de pesquisa e preenchê-lo com o termo de pesquisa
                var botaoPesquisar = driver.FindElement(By.ClassName("buscar"));
                botaoPesquisar.Click();
                System.Threading.Thread.Sleep(3000);

                var searchBox = driver.FindElement(By.Name("s")); // Altere o 'By.Name("s")' de acordo com o elemento de pesquisa do site
                searchBox.SendKeys(searchTerm);
                searchBox.Submit();

                // Aguarde os resultados carregarem
                System.Threading.Thread.Sleep(2000);

                // Colete os resultados da pesquisa
                var searchResults = driver.FindElements(By.CssSelector(".cardPost.mb-5")); // Altere o seletor CSS de acordo com os elementos de resultado do site


                // Insira os resultados no banco de dados
                foreach (var result in searchResults)
                {
                    string titulo = result.FindElement(By.ClassName("tres-linhas")).Text; // Altere o seletor de acordo com o elemento de título do resultado
                    string area = result.FindElement(By.ClassName("hat")).Text;
                    string entrada = result.FindElement(By.TagName("small")).Text;
                    string autor = "";
                    string data = "";
                    string pattern = @"Publicado por (\w+(\s\w+)*) em (\d{2}\/\d{2}\/\d{4})";



                    Match match = Regex.Match(entrada, pattern);
                    if (match.Success)
                    {
                        autor = match.Groups[1].Value;
                        data = match.Groups[3].Value;
                    }

                    string descricao = result.FindElement(By.ClassName("duas-linhas")).Text; // Altere o seletor de acordo com o elemento de URL do resultado

                    var registro = new Busca
                    {
                        Titulo = titulo,
                        Area = area,
                        Autor = autor,
                        Data = data,
                        Descricao = descricao
                    };

                    Salvar(registro);

                }


                driver.Quit();

            }
        }
    }
}
