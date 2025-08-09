using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.Xml;
using System.Drawing.Text;
using System.Data.Entity.Infrastructure;
using System.CodeDom;
using System.Security.Cryptography.X509Certificates;

namespace CalendarApp
{
    public partial class AppointmentsForm : Form
    {
        private AppDbContext _context;
        private int selectedRowIndex = -1;
        private readonly string appointmentId = "appointmentId";
        private readonly int customerId;
        private readonly string type = "type";
        private readonly string start = "start";
        private readonly string end = "end";
        private readonly int userId;
        private readonly string title = "title";
        private readonly string description = "description";
        private readonly string location = "location";
        private readonly string contact = "contact";
        private readonly string url = "url";
        private readonly string createDate = "createDate";
        private readonly string creadedBy = "createdBy";
        private readonly string updateDate = "updateDate";
        private readonly string lastUpdateBy = "lastUpdateBy";
        private User loggedInUser;
        private Appointment appointmentToEdit;
        public AppointmentsForm(User user, Appointment appointment = null)
        {
            InitializeComponent();
            this.Load += AppointmentsForm_Load;
            SetLanguageByTimeZone();
            _context = new AppDbContext();
            this.loggedInUser = user;
            this.appointmentToEdit = appointment;
        }

        private void dataGridViewAppointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void LoadAppointments()
        {
            try
            {
                _context.Database.Connection.Open();
                Debug.WriteLine("Connection open");
                _context.Database.Connection.Close();

                using (var context = new AppDbContext())
                {
                    var rawAppointments = (from a in context.Appointments
                                           join c in context.Customers on a.customerId equals c.customerId
                                           join u in context.Users on a.userId equals u.userId
                                           select new 
                                           {
                                               a.appointmentId,
                                               a.customerId,
                                               customerName = c.customerName,
                                               a.userId,
                                               userName = u.userName,
                                               a.title,
                                               a.type,
                                               a.description,
                                               a.location,
                                               a.contact,
                                               a.url,
                                               a.start,
                                               a.end,
                                               a.createDate,
                                               a.createdBy,
                                               a.lastUpdate,
                                               a.lastUpdateBy
                                           }).ToList();

                    var appointmentList = rawAppointments.Select(a => new AppointmentViewModel
                    {
                        appointmentId = a.appointmentId,
                        customerId = a.customerId,
                        customerName = a.customerName,
                        userId = a.userId,
                        userName = a.userName,
                        title = a.title,
                        type = a.type,
                        description = a.description,
                        location = a.location,
                        contact = a.contact,
                        url = a.url,
                        start = a.start.ToLocalTime(),
                        end = a.end.ToLocalTime(),
                        createDate = a.createDate,
                        createdBy = a.createdBy,
                        lastUpdate = a.lastUpdate,
                        lastUpdateBy = a.lastUpdateBy

                    }).ToList();
                    dataGridViewAppointments.DataSource = appointmentList;
                    dataGridViewAppointments.Refresh();
                }

                //var appointments = _context.Appointments; 
                //var appointmentList = appointments.ToList();

                /*var appointmentList = _context.Appointments
                    .AsNoTracking().ToList().Select(
                    a => new
                    {
                        a.appointmentId,
                        a.title,
                        a.customerId,
                        a.type,
                        Start = a.start.ToLocalTime(),
                        End = a.end.ToLocalTime(),
                        a.userId,
                        a.description,
                        a.location,
                        a.contact,
                        a.url,
                        a.createDate,
                        a.createdBy,
                        a.lastUpdate,
                        a.lastUpdateBy
                    }).ToList();
                dataGridViewAppointments.DataSource = null;
                dataGridViewAppointments.DataSource = appointmentList;*/


                this.Invoke((Action)(() =>
                {
                    if (dataGridViewAppointments == null)
                    {
                        Debug.WriteLine("dataGridViewAppointments is null");
                        return;
                    }
                    /*using (var context = new AppDbContext())
                    {
                        var appointment = context.Appointments.AsNoTracking().ToList();
                        
                        dataGridViewAppointments.DataSource = null;
                        dataGridViewAppointments.DataSource = appointmentList;
                        dataGridViewAppointments.Refresh();
                    }*/


                    Debug.WriteLine("bound datasource.");

                    if (dataGridViewAppointments.Columns["appointmentId"] != null)
                        dataGridViewAppointments.Columns["appointmentId"].HeaderText = Properties.Resources.AppointmentIdHeader;
                    if (dataGridViewAppointments.Columns["customerId"] != null)
                        dataGridViewAppointments.Columns["customerId"].HeaderText = Properties.Resources.CustomerNameHeader;
                    if (dataGridViewAppointments.Columns["type"] != null)
                        dataGridViewAppointments.Columns["type"].HeaderText = Properties.Resources.TypeHeader;
                    if (dataGridViewAppointments.Columns["phone"] != null)
                        dataGridViewAppointments.Columns["phone"].HeaderText = Properties.Resources.PhoneHeader;
                    if (dataGridViewAppointments.Columns["start"] != null)
                        dataGridViewAppointments.Columns["start"].HeaderText = Properties.Resources.StartTimeHeader;
                    if (dataGridViewAppointments.Columns["end"] != null)
                        dataGridViewAppointments.Columns["end"].HeaderText = Properties.Resources.EndTimeHeader;
                    if (dataGridViewAppointments.Columns["userId"] != null)
                        dataGridViewAppointments.Columns["userId"].HeaderText = Properties.Resources.UserIdHeader;
                    if (dataGridViewAppointments.Columns["title"] != null)
                        dataGridViewAppointments.Columns["title"].HeaderText = Properties.Resources.TitleHeader;
                    if (dataGridViewAppointments.Columns["description"] != null)
                        dataGridViewAppointments.Columns["description"].HeaderText = Properties.Resources.DescriptionHeader;
                    if (dataGridViewAppointments.Columns["location"] != null)
                        dataGridViewAppointments.Columns["location"].HeaderText = Properties.Resources.LocationHeader;
                    if (dataGridViewAppointments.Columns["contact"] != null)
                        dataGridViewAppointments.Columns["contact"].HeaderText = Properties.Resources.ContactHeader;
                    if (dataGridViewAppointments.Columns["url"] != null)
                        dataGridViewAppointments.Columns["url"].HeaderText = Properties.Resources.URLHeader;
                    if (dataGridViewAppointments.Columns["createDate"] != null)
                        dataGridViewAppointments.Columns["createDate"].HeaderText = Properties.Resources.CreateDateHeader;
                    if (dataGridViewAppointments.Columns["createdBy"] != null)
                        dataGridViewAppointments.Columns["createdBy"].HeaderText = Properties.Resources.CreatedByHeader;
                    if (dataGridViewAppointments.Columns["lastUpdate"] != null)
                        dataGridViewAppointments.Columns["lastUpdate"].HeaderText = Properties.Resources.LastUpdateHeader;
                    if (dataGridViewAppointments.Columns["lastUpdateBy"] != null)
                        dataGridViewAppointments.Columns["lastUpdateBy"].HeaderText = Properties.Resources.LastUpdateByHeader;
                    Debug.WriteLine("column headers set.");

                    

                    dataGridViewAppointments.MultiSelect = false;
                    dataGridViewAppointments.CellClick += dataGridViewAppointments_CellClick;
                    dataGridViewAppointments.CellDoubleClick += dataGridViewAppointments_CellDoubleClick;
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void dataGridViewAppointments_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewAppointments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
                dataGridViewAppointments.Rows[selectedRowIndex].Selected = true;

                var appointment = dataGridViewAppointments.Rows[selectedRowIndex].DataBoundItem;
                if (appointment != null)
                {
                    var appointmentType = appointment.GetType();
                    titleTextbox.Text = appointmentType.GetProperty("title").GetValue(appointment)?.ToString();
                    descriptionTextbox.Text = appointmentType.GetProperty("description").GetValue(appointment)?.ToString();
                    typeTextbox.Text = appointmentType.GetProperty("type").GetValue(appointment)?.ToString();
                    comboBoxCustomers.Text = appointmentType.GetProperty("customerId").GetValue(appointment)?.ToString();
                    locationTextbox.Text = appointmentType.GetProperty("location").GetValue(appointment)?.ToString();
                    urlTextbox.Text = appointmentType.GetProperty("url").GetValue(appointment)?.ToString();
                    contactTextbox.Text = appointmentType.GetProperty("contact").GetValue(appointment)?.ToString();
                    dateTimePickerStart.Value = (DateTime)appointmentType.GetProperty("start").GetValue(appointment);
                    dateTimePickerEnd.Value = (DateTime)appointmentType.GetProperty("end").GetValue(appointment);
                }
                updateButton.Enabled = true;
            }
        }

        private void AppointmentsForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadAppointments();

            /*if (appointmentToEdit != null)
            {
                titleTextbox.Text = appointmentToEdit.title;
                descriptionTextbox.Text = appointmentToEdit.description;
                typeTextbox.Text = appointmentToEdit.type;
                comboBoxCustomers.Text = appointmentToEdit.customerId.ToString();
                locationTextbox.Text = appointmentToEdit.location;
                urlTextbox.Text = appointmentToEdit.url;
                contactTextbox.Text = appointmentToEdit.contact;
                dateTimePickerStart.Value = appointmentToEdit.start.ToLocalTime();
                dateTimePickerEnd.Value = appointmentToEdit.end.ToLocalTime();
            }*/
        }

        private void UpdateControlTest()
        {
            addButton.Text = Properties.Resources.AddButton;
            updateButton.Text = Properties.Resources.UpdateButton;
            deleteButton.Text = Properties.Resources.DeleteButton;
            refreshButton.Text = Properties.Resources.RefreshButton;
            titleLabel.Text = Properties.Resources.TitleLabel;
            typeLabel.Text = Properties.Resources.TypeLabel;
            customerLabel.Text = Properties.Resources.CustomerLabel;
            startLabel.Text = Properties.Resources.StartLabel;
            endLabel.Text = Properties.Resources.EndLabel;
            descriptionLabel.Text = Properties.Resources.DescriptionLabel;

        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadAppointments();
            ClearInputs();
        }

        private void ClearInputs()
        {
            titleTextbox.Text = "";
            typeTextbox.Text = "";
            descriptionTextbox.Text = "";
        }

        private void DataGridViewAppointments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
                dataGridViewAppointments.Rows[selectedRowIndex].Selected = true;
            }
            else
            {
                selectedRowIndex = -1;
            }
        }

        private void DataGridViewAppointments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
                dataGridViewAppointments.Rows[selectedRowIndex].Selected = true;

                var appointment = dataGridViewAppointments.Rows[selectedRowIndex].DataBoundItem;
                if (appointment != null)
                {
                    var appointmentType = appointment.GetType();
                    titleTextbox.Text = appointmentType.GetProperty("title").GetValue(appointment)?.ToString();
                    typeTextbox.Text = appointmentType.GetProperty("type").GetValue(appointment)?.ToString();
                    dateTimePickerStart.Text = appointmentType.GetProperty("start").GetValue(appointment)?.ToString(); ;
                    dateTimePickerEnd.Text = appointmentType.GetProperty("end").GetValue(appointment)?.ToString(); ;
                    descriptionTextbox.Text = appointmentType.GetProperty("description").GetValue(appointment)?.ToString(); ;
                }
                updateButton.Enabled = true;
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewAppointments.CurrentRow == null)
                {
                    MessageBox.Show("Please double click an appointment to update.");
                    return;
                }

                int appointmentId = (int)dataGridViewAppointments.CurrentRow.Cells["appointmentId"].Value;

                Console.WriteLine($"New Start Time {dateTimePickerStart.Value:MM/dd/yyyy hh:mm tt}");
                Console.WriteLine($"New End Time {dateTimePickerEnd.Value:MM/dd/yyyy hh:mm tt}");

                if (!ValidateAppointmentTimes(appointmentId)) 
                {
                    return;
                }

                using (var context = new AppDbContext())
                {
                    var appointment = context.Appointments.FirstOrDefault(c => c.appointmentId == appointmentId);

                    if (appointment != null)
                    {
                        appointment.title = titleTextbox.Text;
                        appointment.description = descriptionTextbox.Text;
                        appointment.type = typeTextbox.Text;
                        appointment.url = urlTextbox.Text;
                        appointment.contact = contactTextbox.Text;
                        appointment.location = locationTextbox.Text;
                        appointment.customerId = (int)comboBoxCustomers.SelectedValue;
                        appointment.userId = loggedInUser.userId;
                        appointment.start = dateTimePickerStart.Value;
                        appointment.end = dateTimePickerEnd.Value;
                        appointment.lastUpdateBy = loggedInUser.userName;
                        appointment.createdBy = loggedInUser.userName;
                        appointment.lastUpdate = DateTime.Now;
                        appointment.createDate = DateTime.Now;

                    }
                    context.SaveChanges();
                    MessageBox.Show("Customer updated successfully!"); //add appointment update message to resources.
                    using (var newContext = new AppDbContext())
                    {
                        dataGridViewAppointments.DataSource = null;
                        dataGridViewAppointments.DataSource = newContext.Appointments.AsNoTracking().ToList();
                        dataGridViewAppointments.Refresh();
                    }
                  
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating appointment: {ex.Message}");
            }
            
        }

        private bool ValidateAppointmentTimes(int? appointmentId = null)
        {
            bool isValid = true;
            TimeZoneInfo estZone = GetEasternStandardTimeZone();

            DateTime startLocal = DateTime.SpecifyKind(dateTimePickerStart.Value, DateTimeKind.Local);
            DateTime endLocal = DateTime.SpecifyKind(dateTimePickerEnd.Value, DateTimeKind.Local);
            DateTime startEst = TimeZoneInfo.ConvertTime(dateTimePickerStart.Value, TimeZoneInfo.Local, estZone);
            DateTime endEst = TimeZoneInfo.ConvertTime(dateTimePickerEnd.Value, TimeZoneInfo.Local, estZone);
            TimeSpan startTime = startEst.TimeOfDay;
            TimeSpan endTime = endEst.TimeOfDay;

            DateTime businessStart = new DateTime(startEst.Year, startEst.Month, startEst.Day, 9, 0, 0, DateTimeKind.Unspecified);
            DateTime businessEnd = new DateTime(startEst.Year, startEst.Month, startEst.Day, 17, 0, 0, DateTimeKind.Unspecified);

            businessStart = DateTime.SpecifyKind(businessStart, DateTimeKind.Unspecified);
            businessEnd = DateTime.SpecifyKind(businessEnd, DateTimeKind.Unspecified);

            Console.WriteLine($"{startEst:MM/dd/yyyy hh:mm tt}");
            Console.WriteLine($"{endEst:MM/dd/yyyy hh:mm tt}");
            Console.WriteLine($"{businessStart:MM/dd/yyyy hh:mm tt}");
            Console.WriteLine($"{businessEnd:MM/dd/yyyy hh:mm tt}");


            if (startTime < businessStart.TimeOfDay || endTime > businessEnd.TimeOfDay ||
                startEst.DayOfWeek == DayOfWeek.Saturday || startEst.DayOfWeek == DayOfWeek.Saturday ||
                endEst.DayOfWeek == DayOfWeek.Saturday || endEst.DayOfWeek == DayOfWeek.Sunday ||
                endEst <= startEst)
            {
                MessageBox.Show("Appointments must be scheduled between 9:00 AM and 5:00 PM EST, Monday to Friday, with end time after start time.");
                return false;
            }
            
            using (var context = new AppDbContext())
            {
                var overlappingAppointments = context.Appointments.Where(a => (appointmentId == null || a.appointmentId != appointmentId) &&
                    a.start < endLocal && a.end > startLocal).ToList();

                foreach (var appt in overlappingAppointments)
                {
                    DateTime apptStartUtc = DateTime.SpecifyKind(appt.start, DateTimeKind.Utc);
                    DateTime apptEndUtc = DateTime.SpecifyKind(appt.end, DateTimeKind.Utc);

                    DateTime apptStartEst = TimeZoneInfo.ConvertTimeFromUtc(apptStartUtc, estZone);
                    DateTime apptEndEst = TimeZoneInfo.ConvertTimeFromUtc(apptEndUtc, estZone);

                    Console.WriteLine($"Overlap Found: Appt ID: {appt.appointmentId}");
                }
                if (overlappingAppointments.Any())
                {
                    List<string> overlapStrings = overlappingAppointments.Select(a =>
                    {
                        DateTime aStartUtc = DateTime.SpecifyKind(a.start, DateTimeKind.Utc);
                        DateTime aEndUtc = DateTime.SpecifyKind(a.end, DateTimeKind.Utc);

                        DateTime aStartEST = TimeZoneInfo.ConvertTimeFromUtc(aStartUtc, estZone);
                        DateTime aEndEST = TimeZoneInfo.ConvertTimeFromUtc(aEndUtc, estZone);
                        return $"Appointment ID {a.appointmentId}";
                    }).ToList();

                    string overlaps = string.Join("\n", overlapStrings);
                    MessageBox.Show($"Appointments cannot overlap. This appointment overlaps with:\n{overlaps}");
                    return false;
                }
            } return true;
        }


        private static TimeZoneInfo GetEasternStandardTimeZone()
        {
            return TimeZoneInfo.CreateCustomTimeZone(
                id: "Eastern StandardTime",
                baseUtcOffset: TimeSpan.FromHours(-5),
                displayName: "Eastern StandardTime",
                standardDisplayName: "Eastern StandardTime",
                daylightDisplayName: null,
                adjustmentRules: new TimeZoneInfo.AdjustmentRule[] {}
                ); 
        }
        private void LoadCustomers()
        {
            using (var context = new AppDbContext())
            {
                var customers = context.Customers.Select(c => new { c.customerId, c.customerName }).ToList();
                comboBoxCustomers.DisplayMember = "customerName";
                comboBoxCustomers.ValueMember = "customerId";
                comboBoxCustomers.DataSource = customers;

            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                string title = titleTextbox.Text.Trim();
                string description = descriptionTextbox.Text.Trim();
                int customerId = (int)comboBoxCustomers.SelectedValue;
                int userId = loggedInUser.userId;
                DateTime start = dateTimePickerStart.Value;
                DateTime end = dateTimePickerEnd.Value;
                string type = typeTextbox.Text.Trim();
                string contact = contactTextbox.Text.Trim();
                string location =locationTextbox.Text.Trim();
                string url = urlTextbox.Text.Trim();
                string createdBy = loggedInUser.userName;
                string lastUpdateBy = loggedInUser.userName;
                DateTime createDate = DateTime.Now;
                DateTime lastUpdate = DateTime.Now;



                if (string.IsNullOrWhiteSpace(titleTextbox.Text) || string.IsNullOrWhiteSpace(descriptionTextbox.Text) || string.IsNullOrWhiteSpace(typeTextbox.Text))
                {
                    MessageBox.Show(Properties.Resources.FillAllFieldsMessage ?? "Please fill in all fields.");
                    return;
                }
                if (!ValidateAppointmentTimes())
                {
                    return;
                }
                using (var context = new AppDbContext())
                {
                   var newAppointment = new Appointment
                    {
                        title = title,
                        description = description,
                        customerId = customerId,
                        userId = userId,
                        start = start,
                        end = end,
                        type = type,
                        contact = contact,
                        url = url,
                        location = location,
                        createDate = createDate,
                        createdBy = createdBy,
                        lastUpdate = lastUpdate,
                        lastUpdateBy = lastUpdateBy,
                    };
                    try
                    {
                        context.Appointments.Add(newAppointment);
                        context.SaveChanges();
                        MessageBox.Show("Appointment added successfully");
                        //add appoint message to resources
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    ClearInputs();
                    LoadAppointments();
                }

            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                MessageBox.Show($"Error Loading Appointments: {ex.Message}, inner: {innerException}");
            }
        }

        private void descriptionLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewAppointments.CurrentRow == null)
                {
                    MessageBox.Show("Please select an appointment to delete.");
                    //this is hardcoded, fix with dynamic alert later in resources
                    return;
                }

                int appointmentId = (int)dataGridViewAppointments.CurrentRow.Cells["appointmentId"].Value;

                using (var context = new AppDbContext())
                {
                    var appointment = context.Appointments.FirstOrDefault(c => c.appointmentId == appointmentId);
                    if (appointmentId != null)
                    {
                        MessageBox.Show("Are you sure you want to delete this appointment?");
                        context.Appointments.Remove(appointment);
                        context.SaveChanges();
                        MessageBox.Show("Appointment successfully deleted!");
                        //update this in resources to be AppointmentDeleteMessage and ConfirmDeleteAppointmentMessage.
                        LoadAppointments();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer: {ex.Message}");
            }
        }

        private void titleTextbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

