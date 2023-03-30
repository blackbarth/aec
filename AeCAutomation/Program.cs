using AeCAutomation.Repository;
using AeCAutomation.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AeCAutomation
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Cria uma instância do contêiner de injeção de dependência Ninject
            var kernel = new StandardKernel();

            // Registra as dependências da aplicação no contêiner
            kernel.Bind<ISearchRepository>().To<SearchRepository>();
            kernel.Bind<ISearchService>().To<SearchService>();


            Application.Run(new AeCForm(kernel.Get<ISearchService>()));
        }
    }
}
