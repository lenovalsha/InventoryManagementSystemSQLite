using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystemSQLite
{
    public partial class Supplier : Form
    {
        public Supplier()
        {
            InitializeComponent();
        }
        private void AddRegions()
        {
            using (DataContext context = new DataContext())
            {
                string[] regions = { "Alberta", "British Columbia", "Manitoba", "New Brunswick",
                    "Newfoundland and Labrador", "Northwest Territories", "Nova Scotia", "Nunavut",
                    "Ontario", "Prince Edward Island", "Quebec", "Saskatchewan", "Yukon" };
                foreach (string region in regions)
                {
                    //see if they already exist
                    if (!context.Regions.Any(r => r.Name == region))
                    {
                        //if not then create it
                        context.Regions.Add(new Models.Region() { Name = region });
                    }
                }

            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Supplier_Load(object sender, EventArgs e)
        {

        }

        private void btnAddNewSupplier_Click(object sender, EventArgs e)
        {

        }
    }
}
