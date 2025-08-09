using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Net.Http;
using System.Drawing.Text;
using Newtonsoft.Json.Linq;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using System.Resources;
using System.Threading;
using System.IO;

namespace CalendarApp
{
    public partial class LoginForm : Form
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly ResourceManager _rm;
        public LoginForm()
        {
            InitializeComponent();
            _context = new AppDbContext();
            _httpClient = new HttpClient();
            setLanguageByRegion();
            _rm = new ResourceManager("CalenderApp.Translations", GetType().Assembly);

        }

        public void setLanguageByRegion()
        {
            try
            {
                RegionInfo region = RegionInfo.CurrentRegion;
                string cultureName = region.TwoLetterISORegionName == "JP" ? "ja-JP" : "en-US";
                CultureInfo culture = new CultureInfo(cultureName);

                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

                var resourceManager = new System.ComponentModel.ComponentResourceManager(this.GetType());
                resourceManager.ApplyResources(UsernameLabel, "UsernameLabel", culture);
                resourceManager.ApplyResources(PasswordLabel, "PasswordLabel", culture);
                resourceManager.ApplyResources(LoginButton, "LoginButton", culture);
                resourceManager.ApplyResources(this, "$this", culture);

                var resManager = new System.Resources.ResourceManager(this.GetType().FullName, GetType().Assembly);
                //string testString = resManager.GetString("UsernameLabel.Text", culture);
                //MessageBox.Show($"Region: {region.TwoLetterISORegionName}, culture: {cultureName}, usernamelabel: {UsernameLabel.Text}, {PasswordLabel.Text}, {testString}");


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Setting Language: {ex.Message}");
            }
            
        }



        private string MapCountryToCulture(string countryCode)
        {
            if (countryCode == "JP") return "ja-JP";
            else return "en-US";
        }

        private void RefreshForm()
        {
            foreach (Control control in Controls)
            {
                control.Refresh();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                var userName = UsernameTextbox.Text.Trim();
                var password = PasswordTextbox.Text;
                var user = _context.Users.FirstOrDefault(u => u.userName == userName);
                if (user == null)
                {
                    MessageBox.Show("Please enter your Username.");
                }
                else if (user != null && user.password == null)
                {
                    MessageBox.Show("Please enter your Password.");
                }
                else if (user != null && user.password != password)
                {
                    MessageBox.Show("Invalid Username or Password");
                }
                else
                {
                    int userId = user.userId;
                    Calendar calendar = new Calendar(user);
                    calendar.Show();
                    Hide();
                    AlertUserUpcomingAppointments(userId);
                    LogLogin(userName);
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Connection Error");
                Debug.WriteLine($"Error: {ex.ToString()}");
                MessageBox.Show($"Connection Error: {ex.Message}\n\nStackTrace: {ex.StackTrace}\n\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "none")}");
            }
        }

        public static void LogLogin(string userName)
        {
            string logFilePath = "Login_History.txt";
            string logEntry = $"{DateTime.Now:G} - User {userName} logged in.";
            try
            {
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AlertUserUpcomingAppointments(int userId)
        {
            using (var context = new AppDbContext())
            {
                DateTime now = DateTime.UtcNow;
                DateTime infifteenMinutes = now.AddMinutes(15);

                var upcomingAppointments = context.Appointments.Where(a => a.userId == userId && a.start >= now &&
                a.start <= infifteenMinutes).ToList();

                if (upcomingAppointments.Any())
                {
                    string message = "You have upcoming appointment(s):\n\n";
                    foreach (var appt in upcomingAppointments)
                    {
                        message += $"{appt.title} at {appt.start.ToLocalTime():hh:mm tt}\n";
                        MessageBox.Show(message, "Upcoming Appointments", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                } 
            }
        }

        private void CalendarLogo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void UsernameLabel_Click(object sender, EventArgs e)
        {

        }

        private void PasswordLabel_Click(object sender, EventArgs e)
        {

        }

        private void UsernameTextbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
