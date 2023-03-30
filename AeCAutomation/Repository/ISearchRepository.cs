using AeCAutomation.Domain.Entity;
using System.Data;

namespace AeCAutomation.Repository
{
    public interface ISearchRepository
    {
         void Salvar(Busca busca);

        void CriarBase();

        DataTable ListaBuscas();

        DataTable Pesquisa(Busca busca);
    }
}
