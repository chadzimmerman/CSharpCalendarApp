using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalendarApp
{
    public partial class CustomerDatabaseForm : Form
    {
        private AppDbContext _context;
        private int selectedRowIndex = -1;
        private readonly string customerName = "customerName";
        private readonly string customerId = "customerId";
        private readonly string phone = "phone";
        private readonly string address = "address";
        private List<Address> addresses;
        private List<City> cities;
        private List<Country> countries;
        public CustomerDatabaseForm()
        {
            InitializeComponent();
            SetLanguageByTimeZone();
            _context = new AppDbContext();
            this.Shown += CustomerDatabaseForm_Shown;
            PhoneTextbox.KeyPress += PhoneTextbox_KeyPress;
            CountryComboBox.SelectedIndexChanged += CountryComboBox_SelectedIndexChange;
            Loadcountries();
            DatagridViewCustomers.CellDoubleClick += DatagridViewCustomers_CellDoubleClick;
            
        }

        private void PhoneTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            };
        }

        private void CustomerDatabaseForm_Shown(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void SetLanguageByTimeZone()
        {
            try
            {
                TimeZoneInfo localZone = TimeZoneInfo.Local;
                string timeZoneId = localZone.Id;
                string cultureName = timeZoneId == "Japan Standard Time" ? "ja-JP" : "en-US";
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                this.Controls.Clear();
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Setting Language: {ex.Message}");
            }
        }

        private void UpdateControlTest()
        {
            AddButton.Text = Properties.Resources.AddButton;
            UpdateButton.Text = Properties.Resources.UpdateButton;
            DeleteButton.Text = Properties.Resources.DeleteButton;
            RefreshButton.Text = Properties.Resources.RefreshButton;
            NameLabel.Text = Properties.Resources.CustomerNameLabel;
            PhoneLabel.Text = Properties.Resources.PhoneLabel;
            AddressLabel.Text = Properties.Resources.AddressLabel;

        }

        private void LoadCustomers()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    cities = context.Cities.ToList();
                    countries = context.Countries.ToList();
                    addresses = context.Addresses.ToList();
                }

                Debug.WriteLine("Starting Loading Customers");
                if (_context == null)
                {
                    Debug.WriteLine("AppDbContext is null.");
                    MessageBox.Show("database context is not initialized");
                    return;
                }

                _context.Database.Connection.Open();
                Debug.WriteLine("Connection open");
                _context.Database.Connection.Close();

                //var customers = _context.Customers.Include(c => c.address).ThenInclude(a => a.city).ThenInclude(ci => ci.country).ToList();
                //DatagridViewCustomers.DataSource = customers;
                var customerList = (
                    from c in _context.Customers
                    join a in _context.Addresses on c.addressId equals a.addressId
                    join ci in _context.Cities on a.cityId equals ci.cityId
                    join co in _context.Countries on ci.countryId equals co.countryId
                    select new CustomerViewModel
                    {
                        CustomerId = c.customerId,
                        CustomerName = c.customerName,
                        AddressId = a.addressId,
                        Address = a.address,
                        Address2 = a.address2,
                        Phone = a.phone,
                        PostalCode = a.postalCode.ToString(),
                        CityId = ci.cityId,
                        City = ci.city,
                        CountryId = co.countryId,
                        Country = co.country,
                        IsActive = c.active == 1
                    }).ToList();

                DatagridViewCustomers.DataSource = customerList;


                this.Invoke((Action)(() =>
                {
                    

                    DatagridViewCustomers.DataSource = null;
                    DatagridViewCustomers.DataSource = customerList;
                    Debug.WriteLine("bound datasource.");

                    if (DatagridViewCustomers.Columns["customerId"] != null)
                        DatagridViewCustomers.Columns["customerId"].HeaderText = Properties.Resources.CustomerIdHeader;
                    if (DatagridViewCustomers.Columns["customerName"] != null)
                        DatagridViewCustomers.Columns["customerName"].HeaderText = Properties.Resources.CustomerNameHeader;
                    if (DatagridViewCustomers.Columns["address"] != null)
                        DatagridViewCustomers.Columns["address"].HeaderText = Properties.Resources.AddressHeader;
                    if (DatagridViewCustomers.Columns["phone"] != null)
                        DatagridViewCustomers.Columns["phone"].HeaderText = Properties.Resources.PhoneHeader;
                    if (DatagridViewCustomers.Columns["address2"] != null)
                        DatagridViewCustomers.Columns["address2"].HeaderText = "Address 2:";
                    if (DatagridViewCustomers.Columns["postalCode"] != null)
                        DatagridViewCustomers.Columns["postalCode"].HeaderText = "Postal Code:";
                    if (DatagridViewCustomers.Columns["city"] != null)
                        DatagridViewCustomers.Columns["city"].HeaderText = "City";
                    if (DatagridViewCustomers.Columns["country"] != null)
                        DatagridViewCustomers.Columns["country"].HeaderText = "Country:";
                    if (DatagridViewCustomers.Columns["active"] != null)
                        DatagridViewCustomers.Columns["active"].HeaderText = "Is Active:";
                    Debug.WriteLine("column headers set.");


                    DatagridViewCustomers.MultiSelect = false;
                    DatagridViewCustomers.CellClick += DatagridViewCustomers_CellClick;
                    DatagridViewCustomers.CellDoubleClick += DatagridViewCustomers_CellDoubleClick;
                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadCustomers();
            ClearInputs();
        }
        private void ClearInputs()
        {
            NameTextbox.Text = "";
            PhoneTextbox.Text = "";
            AddressTextbox.Text = "";
            Address2Textbox.Text = "";
            PostalCodeTextbox.Text = "";
            IsActiveCheckbox.Checked = true;
            CountryComboBox.SelectedIndex = 0;
            CityComboBox.SelectedIndex = 0;
        }

        private void Loadcountries()
        {
            using (var context = new AppDbContext())
            {
                CountryComboBox.SelectedIndexChanged -= CountryComboBox_SelectedIndexChange;
                var countries = context.Countries.Select(c => new {c.countryId, c.country}).ToList();
                CountryComboBox.DataSource = countries;
                CountryComboBox.DisplayMember = "country";
                CountryComboBox.ValueMember = "countryId";

                var usCountry = countries.FirstOrDefault(c => c.country.ToLower() == "us");
                if (usCountry != null)
                {
                    CountryComboBox.SelectedValue = usCountry.countryId;
                    CountryComboBox_SelectedIndexChange(CountryComboBox, EventArgs.Empty);
                }
                else if (countries.Count > 0)
                {
                    CountryComboBox.SelectedIndex = 0;
                }
               CountryComboBox.SelectedIndexChanged += CountryComboBox_SelectedIndexChange;
            }
        }

        private void CountryComboBox_SelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                if (CountryComboBox.SelectedValue == null)
                {
                    CityComboBox.DataSource = null;
                    return;
                }
                int selectedCountryId;
                if (CountryComboBox.SelectedValue is int)
                {
                    selectedCountryId = (int)CountryComboBox.SelectedValue;
                }
                else if (CountryComboBox.SelectedValue is string && int.TryParse(CountryComboBox.SelectedValue.ToString(), out var parseId))
                {
                    selectedCountryId = parseId;
                } else
                {
                    CityComboBox.DataSource = null;
                    return;
                }
                
                using (var context = new AppDbContext())
                {
                    var cities = context.Cities.Where(c => c.countryId == selectedCountryId).Select
                        (c => new { c.cityId, c.city }).ToList();
                    CityComboBox.DataSource = cities;
                    CityComboBox.DisplayMember = "city";
                    CityComboBox.ValueMember = "cityId";
                }

            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = NameTextbox.Text.Trim();
                string phone = PhoneTextbox.Text.Trim();
                string address = AddressTextbox.Text.Trim();
                string address2 = Address2Textbox.Text.Trim(); ;
                int cityId = (int)CityComboBox.SelectedValue;
                int countryId = (int)CountryComboBox.SelectedValue;
                int postalCode = int.Parse(PostalCodeTextbox.Text.Trim());
                string createdBy = "";
                string lastUpdateBy = "";
                int active = IsActiveCheckbox.Checked ? 1 : 0;
                DateTime createDate = DateTime.Now;
                DateTime lastUpdate = DateTime.Now;



                if (string.IsNullOrWhiteSpace(NameTextbox.Text) || string.IsNullOrWhiteSpace(PhoneTextbox.Text) || 
                    string.IsNullOrWhiteSpace(AddressTextbox.Text) || string.IsNullOrWhiteSpace(Address2Textbox.Text)
                    || string.IsNullOrWhiteSpace(PostalCodeTextbox.Text))
                {
                    MessageBox.Show(Properties.Resources.FillAllFieldsMessage ?? "Please fill in all fields.");
                    return;
                }

                using (var context = new AppDbContext())
                {
                    var newAddress = new Address
                    {
                        address = address,
                        address2 = address2,
                        cityId = cityId,
                        postalCode = postalCode,
                        phone = phone,
                        createdBy = createdBy,
                        lastUpdateBy = lastUpdateBy
                    };

                    try
                    {
                        context.Addresses.Add(newAddress);
                        context.Database.Log = Console.WriteLine;
                        context.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine(ex.InnerException?.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    var newCustomer = new Customer
                    {
                        customerName = customerName,
                        addressId = newAddress.addressId,
                        active = active,
                        createDate = createDate,
                        createdBy = createdBy,
                        lastUpdate = lastUpdate,
                        lastUpdateBy = lastUpdateBy,
                    };

                    context.Customers.Add(newCustomer);
                    context.SaveChanges();


                    ClearInputs();
                    

                    MessageBox.Show(Properties.Resources.CustomerAddedMessage ?? "Customer added successfully");
                    LoadCustomers();
                }

            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                MessageBox.Show($"Error Loading Customers: {ex.Message}, inner: {innerException}");
            }
        }

        private void DatagridViewCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
                DatagridViewCustomers.Rows[selectedRowIndex].Selected = true;
            }
            else
            {
                selectedRowIndex = -1;
            }
        }

        private void DatagridViewCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && DatagridViewCustomers.Rows[e.RowIndex].DataBoundItem is CustomerViewModel customer)
            {
                CountryComboBox.SelectedValue = customer.CountryId;
                CityComboBox.SelectedValue = customer.CityId;
                PhoneTextbox.Text = customer.Phone;
                PostalCodeTextbox.Text = customer.PostalCode;
                AddressTextbox.Text = customer.Address;
                Address2Textbox.Text = customer.Address2;
                NameTextbox.Text = customer.CustomerName;
                IsActiveCheckbox.Checked = customer.IsActive;
            }

            DataGridViewRow selectedRow = DatagridViewCustomers.Rows[e.RowIndex];

            

        }
        private Address GetAddressById(int addressId)
        {
            using (var context = new AppDbContext())
            {
                return addresses.FirstOrDefault(a => a.addressId == addressId);
            }

        }

        private City GetCityById(int cityId)
        {
            using (var context = new AppDbContext())
            {
                return cities.FirstOrDefault(a => a.cityId == cityId);
            }

        }
        private Country GetCountriesById(int countryId)
        {
            using (var context = new AppDbContext())
            {
                return countries.FirstOrDefault(a => a.countryId == countryId);
            }

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DatagridViewCustomers.CurrentRow == null)
                {
                    MessageBox.Show("Please select a customer to update.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(NameTextbox.Text) || string.IsNullOrWhiteSpace(PhoneTextbox.Text) || string.IsNullOrWhiteSpace(AddressTextbox.Text))
                {
                    MessageBox.Show(Properties.Resources.FillAllFieldsMessage ?? "Please fill in all fields.");
                    return;
                }

                int customerId = (int)DatagridViewCustomers.CurrentRow.Cells["customerId"].Value;

                using (var context = new AppDbContext())
                {
                    var customer = context.Customers.FirstOrDefault(c => c.customerId == customerId);

                    if (customer != null)
                    {
                        customer.customerName = NameTextbox.Text;
                        if (customer.addressId != null)
                        {
                            var address = context.Addresses.FirstOrDefault(a => a.addressId == customer.addressId);

                            if (address != null)
                            {
                                address.phone = PhoneTextbox.Text;
                                address.address = AddressTextbox.Text;
                            }

                            context.SaveChanges();
                            MessageBox.Show(Properties.Resources.CustomerUpdatedMessage);
                            LoadCustomers();
                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating customer: {ex.Message}");
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DatagridViewCustomers.CurrentRow == null)
                {
                    MessageBox.Show("Please select a customer to delete.");
                    //this is hardcoded, fix with dynamic alert later
                    return;
                }

                int customerId = (int)DatagridViewCustomers.CurrentRow.Cells["customerId"].Value;

                using (var context = new AppDbContext())
                {
                    var customer = context.Customers.FirstOrDefault(c => c.customerId == customerId);
                    if (customerId != null)
                    {
                        MessageBox.Show(Properties.Resources.ConfirmDeleteMessage);
                        context.Customers.Remove(customer);
                        context.SaveChanges();
                        MessageBox.Show(Properties.Resources.CustomerDeletedMessage);
                        LoadCustomers();
                    }
                }
            
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer: {ex.Message}");
            }
            
        }

        private void NameTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
