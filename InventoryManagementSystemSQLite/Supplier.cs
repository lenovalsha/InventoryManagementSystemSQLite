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
        string companyName;
        string address;
        string city;
        int regionId;
        string postalCode;
        string phoneNumber;
        string email;



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
            if (r.IsMatch(postal))
                postalCode = r.Match(postal).Value;
            else
                MessageBox.Show("Invalid Postal");
        }
        private void EmailValidation(string tmpEmail)
        {
            Regex regex = new Regex(@"[a-zA-Z0-9._-]+@[a-zA-Z.-]+\.[a-zA-Z]{2,}");
            if (regex.IsMatch(tmpEmail))
                email = tmpEmail;
            else
                MessageBox.Show("Invalid Email");
        }
        private void PhoneValidation(string phone)
        {
            Regex regex = new Regex(@"^(\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$");
            if (regex.IsMatch(phone))
                phoneNumber = phone;
            else
                MessageBox.Show("Invalid phone");
        }
        private void ShowAllSuppliers()
        {
            using (DataContext context = new DataContext())
            {
                dtvSuppliers.DataSource = context.Supplier.ToList();
                
                
            }
            
        }
        private void CreateSupplier()
        {
            using (DataContext context = new DataContext())
            {
                //Validate them first
                PostalValidation(txtPostalCode.Text);
                PhoneValidation(txtPhoneNumber.Text);
                EmailValidation(txtEmail.Text);
                companyName = txtCompanyName.Text;
                address = txtAddress.Text;
                city = txtCity.Text;
                //Make sure nothing is null
                if (companyName != string.Empty || address != string.Empty || city != string.Empty ||
                    cmbRegion.Text != string.Empty || phoneNumber != string.Empty || email != string.Empty)
                {
                    //Lets find the selected region and get the Id
                    var region = context.Region.FirstOrDefault(x=> x.Name == cmbRegion.Text);
                    if(region != null) 
                        regionId = region.Id;
                    context.Supplier.Add(new Models.Supplier()
                    {
                        CompanyName = companyName,
                        Address = address,
                        City = city,
                        RegionId = regionId,
                        PostalCode = postalCode,
                        Phone = phoneNumber,
                        Email = email
                    });
                    context.SaveChanges();
                }else
                    MessageBox.Show("Please complete all required field");
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
            CreateSupplier();
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

        private void btnShowSuppliers_Click(object sender, EventArgs e)
        {
            ShowAllSuppliers();
        }
    }
}
