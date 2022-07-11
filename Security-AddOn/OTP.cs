using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Security_AddOn
{
    public partial class OTP : Form
    {
        SqlConnection con = new SqlConnection("Data Source=sqlserver.dynamicdna.co.za;Initial Catalog=User-Management-System-Lerato;Persist Security Info=True;User ID=BBD;Password=***********");
        public OTP()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (otp.ToString().Equals(sendOTP.Text))
            {
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("INSERT INTO Clients VALUES('"+fname.Text+"', '"+email.Text+"', '"+password+"')", con);
                    try
                    {
                        com.ExecuteNonQuery();
                        MessageBox.Show("Success");
                        fname.Clear();
                        email.Clear();
                        password.Clear();
                        sendOTP.Clear();
                        fname.Focus();

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Failed");
                    }

                    con.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("Failed to connect to DB");
                }
                
            }

        }

        int otp;
        private void button2_Click(object sender, EventArgs e)
        {
    if (fname.Text.Length > 0 && email.Text.Length > 0 && password.Text.Length > 0)
    {
                Random r = new Random();
                otp = r.Next(10000, 100000);
                sendEmail();
            }
                 else
                {
                    MessageBox.Show("Fill in all fields!");
                    fname.Clear();
                    email.Clear();
                    password.Clear();
                    sendOTP.Clear();
                    fname.Focus();
                }
            
            
        }

        private void sendEmail()
        {
            try
            {

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("data.type.Int32@gmail.com");
                msg.To.Add(email.Text);
                msg.Subject = "Important :OTP";
                msg.Body = "Hi, " + fname.Text + "\n\nOTP: " + otp.ToString();

                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp.gmail.com";
                System.Net.NetworkCredential ntcd = new NetworkCredential();
                   
                ntcd.UserName = "data.type.Int32@gmail.com";
                ntcd.Password = "************************";
                smt.Credentials = ntcd;
                smt.EnableSsl = true;
                smt.Port = 587;
                smt.Send(msg);

                MessageBox.Show("Check your mail for OTP");

            }
            catch (Exception)
            {

                MessageBox.Show("Failed to send email!");
            }
        }
    }
}
