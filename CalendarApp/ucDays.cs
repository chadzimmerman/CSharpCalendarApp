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
using CalendarApp;

namespace CalendarApp
{
    public partial class ucDays : UserControl
    {
        string _day, date, weekday;
        public DateTime DateRepresented { get; set; }
        public ucDays(string day)
        {
            InitializeComponent();
            _day = day;
            label1.Text = _day;
            checkBox1.Hide();
            date = Calendar._month + "/" + _day + "/" + Calendar._year;
        }

        private void sundays()
        {
            try
            {
                DateTime day = DateTime.Parse(date);
                weekday = day.ToString("ddd");
                if (weekday == "Sun")
                {
                    label1.ForeColor = Color.FromArgb(255, 128, 128);
                }
                else
                {
                    label1.ForeColor = Color.FromArgb(64, 64, 64);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                checkBox1.Checked = true;
                this.BackColor = Color.FromArgb(255, 150, 79);
            } else
            {
                checkBox1.Checked = false;
                this.BackColor = Color.White;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ucDays_Load(object sender, EventArgs e)
        {
            sundays();
        }

        public void LoadAppointments(List<Appointment> appointments)
        {
            flowLayoutPanelAppointments.Controls.Clear();

            foreach (var appt in appointments)
            {
                var apptPanel = new Panel
                {
                    BackColor = Color.LightSkyBlue,
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(3),
                    Margin = new Padding(2),
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Cursor = Cursors.Hand,
                    Tag = appt
                };

                var apptLabel = new Label
                {
                    Text = $"{appt.start.ToLocalTime():hh:mm tt} - {appt.title}",
                    AutoSize = true
                };

                apptPanel.Controls.Add(apptLabel);
                apptLabel.Cursor = Cursors.Hand;
                apptLabel.AutoSize = true;
                apptLabel.BackColor = Color.Transparent;
                apptPanel.Click += AppointmentPanel_Click;
                apptLabel.Click += AppointmentPanel_Click;

                flowLayoutPanelAppointments.Controls.Add(apptPanel);
            }
        }

        private void AppointmentPanel_Click(object sender, EventArgs e)
        {
            Control clickedControl = sender as Control;

            var panel = clickedControl as Panel ?? clickedControl?.Parent as Panel;
            if (panel?.Tag is Appointment appointment)
            {
                var detailsForm = new AppointmentDetailsForm(appointment);
                detailsForm.ShowDialog();
            }
            }
        }
    }

