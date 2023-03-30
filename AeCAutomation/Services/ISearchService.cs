using AeCAutomation.Domain.Entity;
using System.Data;

namespace AeCAutomation.Services
{
    public interface ISearchService
    {

        void Salvar(Busca busca);

        void CriarBase();

        DataTable ListaBuscas();

        bool Existe(Busca buscar);
        void SearchAndStoreData(string searchTerm);
    }
}
