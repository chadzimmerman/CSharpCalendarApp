using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;
using System.Data.Entity.Infrastructure.Interception;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

namespace CalendarApp
{
    public partial class Calendar : Form
    {
        public static int _year, _month;
        private User loggedInUser;
        
        public Calendar(User user)
        {
            InitializeComponent();
            this.loggedInUser = user;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Calendar_Load(object sender, EventArgs e)
        {
            showDays(DateTime.Now.Month, DateTime.Now.Year);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _month -= 1;
            if (_month < 1)
            {
                _month = 1;
                _year += 1;
            }
            showDays(_month, _year);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            _month += 1;
            if (_month > 12)
            {
                _month = 12;
                _year -= 1;
            }
            showDays(_month, _year);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBox1.SelectedItem.ToString();
            if (selected == "Appointments")
            {
                AppointmentsForm appointmentsForm = new AppointmentsForm(loggedInUser);
                appointmentsForm.Show();
            }
            else if (selected == "Customers")
            {
                CustomerDatabaseForm customerDatabaseForm = new CustomerDatabaseForm();
                customerDatabaseForm.Show();
            }
            else if (selected == "Sign Off") 
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
            else if (selected == "Reports")
            {
                GenerateReports();
            }
        }

        private void GenerateReports()
        {
            var report1 = Report_AppointmentsByUser();
            var report2 = Report_ContactSchedule();
            var report3 = Report_ReportsByTypeAndMonth();
            MessageBox.Show($"Appointments By Users:\n {string.Join("\n", report1)}" +
                $"Schedules by Each Contact:\n {string.Join("\n", report2)}\n" +
                $"Number of Appointments By Type and Month:\n {string.Join("\n", report3)}");
        }

        public List<string> Report_AppointmentsByUser()
        {
            using (var context = new AppDbContext())
            {
                var grouped = context.Appointments
                    .AsEnumerable()
                    .GroupBy(a => a.userId)
                    .Select(g => new
                    {
                        UserId = g.Key,
                        Count = g.Count(),
                        Appointments = g.Select(a => $"{a.title} ({a.start} - {a.end})").ToList()
                    }).ToList();
                List<string> result = new List<string>();

                foreach (var user in grouped)
                {
                    result.Add($"User ID: {user.UserId}, Appointment: {user.Count}");
                    result.AddRange(user.Appointments.Select(a => " " + a));
                    result.Add("");
                }
                return result;
            }
        }

        public List<string> Report_ContactSchedule()
        {
            using (var context = new AppDbContext())
            {
                var appointmentsByContact = context.Appointments
                    .GroupBy(a => a.contact).Select(global => new
                    {
                    ContactId = global.Key,
                    Appointments = global.OrderBy(a => a.start).ToList()
                    })
                    .ToList();

                List<string> result = new List<string>();
                foreach(var contactGroup in  appointmentsByContact)
                {
                    result.Add($"Contact ID: {contactGroup.ContactId}");
                    foreach(var appt in contactGroup.Appointments)
                    {
                        result.Add($" Title: {appt.title}, Start: {appt.start}, End: {appt.end}, Type: {appt.type}");
                    }
                    result.Add("");
                }
                return result;
            }
        }

        public List<string> Report_ReportsByTypeAndMonth()
        {
            using (var context = new AppDbContext())
            {
                var grouped = context.Appointments
                    .AsEnumerable()
                    .GroupBy(a => new { a.start.Month, a.type })
                    .Select(g => new
                    {
                        Month = g.Key.Month,
                        Type = g.Key.type,
                        Count = g.Count()
                    }).ToList();
                return grouped.Select(g => $"Month: {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Month)}, " +
                $"Type: {g.Type}, Count: {g.Count}").ToList();
            }
        }

        private void showDays(int month, int year)
        {
            flowLayoutPanel2.Controls.Clear();
            _year = year;
            _month = month;
            string monthName = new DateTimeFormatInfo().GetMonthName(month);
            monthLabel.Text = monthName.ToUpper() + " " + year;

            DateTime startofTheMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int startDayOffset = (int)startofTheMonth.DayOfWeek;

            var allAppointments = GetAllAppointments();

            for (int i = 0; i < startDayOffset; i++)
            {
                ucDays uc = new ucDays("");
                flowLayoutPanel2.Controls.Add(uc);
            }
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime currentDate = new DateTime(year, month, day);
                var dayAppointments = allAppointments.Where(a => a.start.Date == currentDate.Date).ToList();
                ucDays uc = new ucDays(day.ToString());
                uc.DateRepresented = currentDate;
                uc.LoadAppointments(dayAppointments);
                flowLayoutPanel2.Controls.Add(uc);
            }
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //this sets it to local time, but it seems confusing that they need to schedule it only in EST, but it's displayed in local. 
        private List<Appointment> GetAllAppointments()
        {
            using (var context = new AppDbContext())
            {
                var appointments = context.Appointments.ToList();
                foreach (var appointment in appointments)
                {
                    appointment.start = appointment.start.ToLocalTime();
                    appointment.end = appointment.end.ToLocalTime();
                }
                return appointments;
            }
        }
    }
}
