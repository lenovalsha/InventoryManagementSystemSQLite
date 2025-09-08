using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace InventoryManagementSystemSQLite
{
    public partial class Supplier : Form
    {
        private List<Models.Region> regionList;
        public Supplier()
        {
            InitializeComponent();
            AddAllRegions();
        }
        private void AddAllRegions()
        {
            using (DataContext context = new DataContext())
            {
                string[] regions = { "Alberta", "British Columbia", "Manitoba", "New Brunswick",
                    "Newfoundland and Labrador", "Northwest Territories", "Nova Scotia", "Nunavut",
                    "Ontario", "Prince Edward Island", "Quebec", "Saskatchewan", "Yukon" };
                foreach (string region in regions)
                {
                    //see if they already exist
                    if (!context.Region.Any(r => r.Name == region))
                    {
                        //if not then create it
                        context.Region.Add(new Models.Region() { Name = region });
                    }
                }
                context.SaveChanges();
                foreach (var region in context.Region.ToList())
                {
                    cmbRegion.Items.Add(region.Name);
                }
            }
        }
        private void PostalValidation(string postal)
        {
            Regex r = new Regex(@"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]");
            if(r.IsMatch(postal))
                MessageBox.Show("Valid");
            else
                MessageBox.Show("Invalid");
        }
        private void EmailValidation(string email)
        {
            Regex regex = new Regex(@"[a-zA-Z0-9._-]+@[a-zA-Z.-]+\.[a-zA-Z]{2,}");
            if(regex.IsMatch(email))
                MessageBox.Show("Valid");
            else
                MessageBox.Show("Invalid");
        }
        private void PhoneValidation(string phone)
        {
            Regex regex = new Regex(@"^(\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$");
            if (regex.IsMatch(phone))
                MessageBox.Show("Valid");
            else
                MessageBox.Show("Invalid");
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

        private void btnTester_Click(object sender, EventArgs e)
        {
            string capitalized = txtPostalCode.Text.ToUpper();
            //PostalRegex(capitalized);
            string email = txtEmail.Text;
            //EmailValidation(email);
            string phone = txtPhoneNumber.Text;
            PhoneValidation(phone);
        }
    }
}
