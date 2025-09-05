using Microsoft.EntityFrameworkCore;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateProductType();
            CreateCategory();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void CreateProductType()
        {
            using (DataContext context = new DataContext())
            {
                //create automatimatic product types
                string[] automaticProductTypes = { "Top", "Bottom", "Shoes", "Hats" };
                foreach (string type in automaticProductTypes)
                {
                    if (!context.ProductTypes.Any(s => s.Name == type))//This ensures that we are not creating duplicates
                        context.ProductTypes.Add(new Models.ProductType() { Name = type });
                }
                context.SaveChanges();
                //Will delete later
                foreach (var pt in context.ProductTypes)
                {
                    lstView.Items.Add(pt.Name);
                }
            }
        }
        private void CreateCategory()
        {
            using (DataContext context = new DataContext())
            {
                string[] automaticCategoryTypes = { "Men", "Women", "Unisex" };
                foreach (string cat in automaticCategoryTypes)
                {
                    if (!context.Categories.Any(s => s.Name == cat))
                        context.Categories.Add(new Models.Category() { Name = cat });
                }
                context.SaveChanges();
                //will delete later:)
                foreach (var cat in context.Categories)
                {
                    lstView.Items.Add(cat.Name);
                }
            }

        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            Supplier supplierForm = new Supplier();
            supplierForm.Show();
        }
    }
}
