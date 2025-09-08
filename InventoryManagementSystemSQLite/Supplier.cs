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
        public int selectedId = 0;
        bool isSelected = false;


        public Supplier()
        {
            InitializeComponent();
            AddAllRegions();
            btnEditSupplier.Enabled = false;
            btnAdd.Visible = false;
            DisableAll();
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
        #region Validation Regex
        private void ValidateAllEntry()
        {
            //Validate them first
            PostalValidation(txtPostalCode.Text);
            PhoneValidation(txtPhoneNumber.Text);
            EmailValidation(txtEmail.Text);
            companyName = txtCompanyName.Text;
            address = txtAddress.Text;
            city = txtCity.Text;
        }
        private void PostalValidation(string postal)
        {
            Regex r = new Regex(@"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]");
            if (r.IsMatch(postal))
                postalCode = r.Match(postal).Value;
            else
            {
                postalCode = string.Empty;
                MessageBox.Show("Invalid postal code");
            }
        }
        private void EmailValidation(string tmpEmail)
        {
            Regex regex = new Regex(@"[a-zA-Z0-9._-]+@[a-zA-Z.-]+\.[a-zA-Z]{2,}");
            if (regex.IsMatch(tmpEmail))
                email = tmpEmail;
            else
            {
                email = string.Empty;
                MessageBox.Show("Invalid email");
            }
        }
        private void PhoneValidation(string phone)
        {
            Regex regex = new Regex(@"^(\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$");
            if (regex.IsMatch(phone))
                phoneNumber = phone;
            else
            {
                phoneNumber = string.Empty;
                MessageBox.Show("Invalid phone");
            }
        }
        #endregion
        #region TextBoxes and User Controls
        private void ClearAll()
        {
            txtCompanyName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            cmbRegion.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }
        private void DisableAll()
        {
            txtCompanyName.Enabled = false;
            txtAddress.Enabled = false;
            txtCity.Enabled = false;
            cmbRegion.Enabled = false;
            txtPostalCode.Enabled = false;
            txtPhoneNumber.Enabled = false;
            txtEmail.Enabled = false;

        }
        private void EnableAll()
        {
            txtCompanyName.Enabled = true;
            txtAddress.Enabled = true;
            txtCity.Enabled = true;
            cmbRegion.Enabled = true;
            txtPostalCode.Enabled = true;
            txtPhoneNumber.Enabled = true;
            txtEmail.Enabled = true;
        }
        #endregion
        private void CreateSupplier()
        {
            using (DataContext context = new DataContext())
            {
                //Validate them first
                ValidateAllEntry();
                //Make sure nothing is null
                if (companyName != string.Empty && address != string.Empty && city != string.Empty &&
                    cmbRegion.Text != string.Empty && phoneNumber != string.Empty && email != string.Empty)
                {
                    //Lets find the selected region and get the Id
                    var region = context.Region.FirstOrDefault(x => x.Name == cmbRegion.Text);
                    if (region != null)
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
                    ClearAll();
                    DisableAll();
                    btnAdd.Enabled = false;
                }
                else
                    MessageBox.Show("Please complete all required field");
            }
        }
        private void ShowAllSuppliers()
        {
            using (DataContext context = new DataContext())
            {
                //This will allow us to pick only the fields that we want to see, (it was originally showing RegionId and products)
                dtvSuppliers.DataSource = context.Supplier.Select(x => new {x.Id, x.CompanyName, x.Address, x.City, x.Region.Name, x.PostalCode, x.Phone, x.Email }).ToList();
            }

        }
        private void UpdateSupplier()
        {
            ValidateAllEntry();
            using (DataContext context = new DataContext())
            {
                if (selectedId != 0)
                {
                    if (companyName != string.Empty && address != string.Empty && city != string.Empty &&
                    cmbRegion.Text != string.Empty && phoneNumber != string.Empty && email != string.Empty)
                    {
                        MessageBox.Show("selectedId = " + selectedId);
                        Models.Supplier supplier = context.Supplier.Find(selectedId);
                        if (supplier != null)
                        {
                            supplier.CompanyName = companyName;
                            supplier.Address = address;
                            supplier.City = city;
                            supplier.RegionId = regionId;
                            supplier.PostalCode = postalCode;
                            supplier.Phone = phoneNumber;
                            supplier.Email = email;
                        }
                        context.SaveChanges();
                    }
                }else
                    MessageBox.Show("Please select a supplier to update");
            }
            ShowAllSuppliers();
        }
        private void DeleteSupplier()
        {
            using (DataContext context = new DataContext())
            {
                if (selectedId != 0)
                {
                    var supplier = context.Supplier.FirstOrDefault(x => x.Id == selectedId);
                    context.Supplier.Remove(supplier);
                    selectedId = 0;
                }
                else
                    MessageBox.Show("Please select a supplier to delete");
                context.SaveChanges();
            }
            ShowAllSuppliers();
        }

        private void btnAddNewSupplier_Click(object sender, EventArgs e)
        {
            ClearAll();
            EnableAll();
            btnAdd.Visible = true;
            btnAdd.Enabled = true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CreateSupplier();
            ShowAllSuppliers();
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
        private void btnEditSupplier_Click(object sender, EventArgs e)
        {
            UpdateSupplier();
        }
        private void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            DeleteSupplier();
        }
        private void dtvSuppliers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Avoid errors when clicking the header row
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtvSuppliers.Rows[e.RowIndex];
                selectedId = int.Parse(row.Cells["Id"].Value.ToString());
                txtCompanyName.Text = row.Cells["CompanyName"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtCity.Text = row.Cells["City"].Value.ToString();
                //cmbRegion.Text = row.Cells["Region"].ToString();
                txtPostalCode.Text = row.Cells["PostalCode"].Value.ToString();
                txtPhoneNumber.Text = row.Cells["Phone"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                btnEditSupplier.Enabled = true;
            }
            else
                btnEditSupplier.Enabled = false;
        }


        private void Supplier_Load(object sender, EventArgs e)
        {

        }
        private void dtvSuppliers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
