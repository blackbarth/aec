using AeCAutomation.Services;
using System;
using System.Windows.Forms;

namespace AeCAutomation
{
    public partial class AeCForm : Form
    {
        private readonly ISearchService searchService;
        public AeCForm(ISearchService service)
        {
            this.searchService = service;

            InitializeComponent();
            searchService.CriarBase();
            dataGridView.DataSource = searchService.ListaBuscas();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(btnSearch.Text))
            {
                try
                {
                    searchService.SearchAndStoreData(txtSearch.Text.Trim());
                    dataGridView.DataSource = searchService.ListaBuscas();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }
    }
}


