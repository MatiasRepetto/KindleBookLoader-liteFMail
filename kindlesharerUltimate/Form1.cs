using System;
using System.Windows.Forms;
using System.Net.Mail;

namespace kindlesharerUltimate
{
    public partial class Form1 : Form
    {
        string path = "./credentials.txt";
        bool tbc1 = false;
        bool tbc2 = false;
        bool tbc3 = false;

        public Form1()
        {
            InitializeComponent();
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s;
                    string[] lines = new string[3];
                    int i = 0;
                    while ((s = sr.ReadLine()) != null)
                    {
                        lines[i] = s;
                        i = i + 1;
                    }
                    textBox1.Text = lines[0];
                    textBox2.Text = lines[1];
                    textBox3.Text = lines[2];
                    sr.Close();
                };
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            label4.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(textBox1.Text);
                        sw.WriteLine(textBox2.Text);
                        sw.WriteLine(textBox3.Text);
                        sw.Close();
                    };
                }
                else {
                    if (tbc1 || tbc2 || tbc3)
                    {
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine(textBox1.Text);
                            sw.WriteLine(textBox2.Text);
                            sw.WriteLine(textBox3.Text);
                            sw.Close();
                        };
                    }
                    using (StreamReader sr = File.OpenText(path))
                    {
                        string s;
                        string[] lines = new string[3];
                        int i = 0;
                        while ((s = sr.ReadLine()) != null)
                        {
                            lines[i] = s;
                            i = i + 1;
                        }
                        textBox1.Text = lines[0];
                        textBox2.Text = lines[1];
                        textBox3.Text = lines[2];
                        sr.Close();
                    };
                }

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(textBox1.Text);
                mail.To.Add(textBox3.Text);

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(label4.Text);
                mail.Attachments.Add(attachment);

                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(textBox1.Text, textBox2.Text);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                MessageBox.Show("Your book was successfully sended, this could take some minutes to reflect in your Kindle", "Successfully sended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label4.Text = "You can send another book, just search !!";

            } catch (Exception ex) {

                MessageBox.Show(ex.Message);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            tbc1 = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tbc2 = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            tbc3 = true;
        }
    }
}