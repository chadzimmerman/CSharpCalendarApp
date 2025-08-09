using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalendarApp
{
    public partial class AppointmentDetailsForm : Form
    {
        private Appointment _appointment;
        private AppointmentsForm appointmentsForm;
        private User loggedInUser;
        private Appointment currentAppintment;
        public AppointmentDetailsForm(Appointment appointment)
        {
            InitializeComponent();
            _appointment = appointment;
            this.currentAppintment = appointment;

            //var details = new 

            TitleTextbox.Text = _appointment.title;
            DescriptionTextbox.Text = _appointment.description;
            LocationTextbox.Text = _appointment.location;
            ContactTextbox.Text = _appointment.contact;
            TypeTextbox.Text = _appointment.type;
            UrlTextbox.Text = _appointment.url;
            StartTextbox.Text = _appointment.start.ToString();
            EndTextbox.Text = _appointment.end.ToString();
        }

        private void AppointmentDetailsForm_Load(object sender, EventArgs e)
        {

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var appointmentsForm = new AppointmentsForm(loggedInUser, currentAppintment);
            appointmentsForm.ShowDialog();
            this.Close();
        }

        private void CloseAppointmentButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
