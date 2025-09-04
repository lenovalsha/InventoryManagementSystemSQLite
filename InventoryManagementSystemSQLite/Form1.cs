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
                    if (!context.ProductType.Any(s => s.Name == type))//This ensures that we are not creating duplicates
                        context.ProductType.Add(new Models.ProductType() { Name = type });
                    
                }
                context.SaveChanges();
                //Will delete later
                foreach (var pt in context.ProductType)
                {
                    lstView.Items.Add(pt.Name);
                }
            }
        }
    }
}
