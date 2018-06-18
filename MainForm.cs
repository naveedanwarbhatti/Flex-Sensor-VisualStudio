using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SerialPortListener.Serial;
using System.IO;

namespace SerialPortListener
{
    public partial class MainForm : Form
    {
        string Complete_path;
        int writeTrue = 0;
        int writeFalse = 0;
        SerialPortManager _spManager;
        public MainForm()
        {
            InitializeComponent();

            UserInitialization();
        }

      
        private void UserInitialization()
        {
            _spManager = new SerialPortManager();
            SerialSettings mySerialSettings = _spManager.CurrentSerialSettings;
            serialSettingsBindingSource.DataSource = mySerialSettings;
            portNameComboBox.DataSource = mySerialSettings.PortNameCollection;
            baudRateComboBox.DataSource = mySerialSettings.BaudRateCollection;
            dataBitsComboBox.DataSource = mySerialSettings.DataBitsCollection;
            parityComboBox.DataSource = Enum.GetValues(typeof(System.IO.Ports.Parity));
            stopBitsComboBox.DataSource = Enum.GetValues(typeof(System.IO.Ports.StopBits));

            _spManager.NewSerialDataRecieved += new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved);
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
        }

        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _spManager.Dispose();   
        }

        void _spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            if (this.InvokeRequired)
            {
                // Using this.Invoke causes deadlock when closing serial port, and BeginInvoke is good practice anyway.
                this.BeginInvoke(new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved), new object[] { sender, e });
                return;
            }

            int maxTextLength = 1000; // maximum text length in text box
            if (tbData.TextLength > maxTextLength)
                tbData.Text = tbData.Text.Remove(0, tbData.TextLength - maxTextLength);

            // This application is connected to a GPS sending ASCCI characters, so data is converted to text
            string str = Encoding.ASCII.GetString(e.Data);
            
            tbData.AppendText(str);
            progressBar1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            progressBar1.RightToLeftLayout = true;

            progressBar2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            progressBar2.RightToLeftLayout = true;

            progressBar3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            progressBar3.RightToLeftLayout = true;

            progressBar4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            progressBar4.RightToLeftLayout = true;

            progressBar5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            progressBar5.RightToLeftLayout = true;
           
            string[] needo = tbData.Lines;
            if ((needo.Length - 2)>0)
            {
                String theString = needo[needo.Length - 2];
                string[] words = theString.Split(' ');

                if (words[0] == "C:")
                {
                    try
                    {
                        double OldRange = 4.5 ;
                        double NewRange = 100;
                        double NewValue = (((Convert.ToDouble(words[1]) - 0) * NewRange) / OldRange) + 0;
                        if (Convert.ToDouble(words[1]) >= 0)
                            progressBar11.Value = Convert.ToInt16(NewValue);
                    }
                    catch { }
                }

                if (words[0] == "R1:")
                {
                    try
                    {
                        label13.Text = words[1];
                        label14.Text = words[3];
                        label15.Text = words[5];
                        label17.Text = words[7];
                        label24.Text = words[9];
                        label25.Text = words[11];
                        label27.Text = words[13];
                        label29.Text = words[15];
                        label30.Text = words[17];
                        label31.Text = words[19];
                    }
                    catch { }
                }

                if (words[0] == "B1:")
                {
                    try
                    {
                        if (Convert.ToDouble(words[1]) < 0)
                            label22.Text = "0";
                        else
                            label22.Text = words[1];

                        if (Convert.ToDouble(words[3]) < 0)
                            label23.Text = "0";
                        else
                            label23.Text = words[3];

                        if (Convert.ToDouble(words[5]) < 0)
                            label26.Text = "0";
                        else
                            label26.Text = words[5];

                        if (Convert.ToDouble(words[7]) < 0)
                            label33.Text = "0";
                        else
                            label33.Text = words[7];

                        if (Convert.ToDouble(words[9]) < 0)
                            label35.Text = "0";
                        else
                            label35.Text = words[9];



                        if (Convert.ToDouble(words[11]) < 0)
                            label1.Text = "0";
                        else
                            label1.Text = words[9];

                        if (Convert.ToDouble(words[13]) < 0)
                            label2.Text = "0";
                        else
                            label2.Text = words[13];

                        if (Convert.ToDouble(words[15]) < 0)
                            label3.Text = "0";
                        else
                            label3.Text = words[15];

                        if (Convert.ToDouble(words[17]) < 0)
                            label4.Text = "0";
                        else
                            label4.Text = words[17];

                        if (Convert.ToDouble(words[19]) < 0)
                            label5.Text = "0";
                        else
                            label5.Text = words[19];







                        if (Convert.ToDouble(words[1]) >= 0)
                            progressBar1.Value = Convert.ToInt16(Convert.ToDouble(words[1]));
                        else
                            progressBar1.Value = 0;

                        if (Convert.ToDouble(words[3]) >= 0)
                            progressBar2.Value = Convert.ToInt16(Convert.ToDouble(words[3]));
                        else
                            progressBar2.Value = 0;

                        if (Convert.ToDouble(words[5]) >= 0)
                            progressBar3.Value = Convert.ToInt16(Convert.ToDouble(words[5]));
                        else
                            progressBar3.Value = 0;

                        if (Convert.ToDouble(words[7]) >= 0)
                            progressBar4.Value = Convert.ToInt16(Convert.ToDouble(words[7]));
                        else
                            progressBar4.Value = 0;

                        if (Convert.ToDouble(words[9]) >= 0)
                            progressBar5.Value = Convert.ToInt16(Convert.ToDouble(words[9]));
                        else
                            progressBar5.Value = 0;

                        if (Convert.ToDouble(words[11]) >= 0)
                            progressBar6.Value = Convert.ToInt16(Convert.ToDouble(words[11]));
                        else
                            progressBar6.Value = 0;

                        if (Convert.ToDouble(words[13]) >= 0)
                            progressBar7.Value = Convert.ToInt16(Convert.ToDouble(words[13]));
                        else
                            progressBar7.Value = 0;

                        if (Convert.ToDouble(words[15]) >= 0)
                            progressBar8.Value = Convert.ToInt16(Convert.ToDouble(words[15]));
                        else
                            progressBar8.Value = 0;

                        if (Convert.ToDouble(words[17]) >= 0)
                            progressBar9.Value = Convert.ToInt16(Convert.ToDouble(words[17]));
                        else
                            progressBar9.Value = 0;

                        if (Convert.ToDouble(words[19]) >= 0)
                            progressBar10.Value = Convert.ToInt16(Convert.ToDouble(words[19]));
                        else
                            progressBar10.Value = 0;

                        if (writeTrue == 1 & writeFalse == 0)
                        {
                            using (StreamWriter sw = File.AppendText(Complete_path))
                            {
                                sw.WriteLine("S1:" + label22.Text + " S2:" + label23.Text + " S3:" + label26.Text + " S4:" + label33.Text + " S5:" + label35.Text + " S6:" + label1.Text + " S7:" + label2.Text + " S8:" + label3.Text + " S9:" + label4.Text + " S10:" + label5.Text + " Comfort:" + words[21]);
                            }
                        }

                    }
                    catch { }
                }

                
            }
            
            tbData.ScrollToCaret();

        }

        // Handles the "Start Listening"-buttom click event
        private void btnStart_Click(object sender, EventArgs e)
        {
            _spManager.StartListening();
            groupBox2.Enabled = true;
        }

        // Handles the "Stop Listening"-buttom click event
        private void btnStop_Click(object sender, EventArgs e)
        {
            _spManager.StopListening();
            groupBox2.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                String theString = textBox1.Text;
                string[] words = theString.Split(' ');
                Complete_path = path + "\\" + words[0] + ".txt";
                label1.Text = Complete_path;
                button2.Enabled = true;
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0)
                button1.Enabled = true;
            if (textBox1.TextLength == 0)
                button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            writeTrue = 1;
            writeFalse = 0;

            String theString = textBox1.Text;
            string[] words = theString.Split(' ');
            Complete_path = folderBrowserDialog1.SelectedPath + "\\" + words[0] + ".txt";
            label1.Text = Complete_path;
            using (StreamWriter sw = File.AppendText(Complete_path))
            {
                sw.WriteLine("Name: " + textBox1.Text);
                sw.WriteLine(" ");
            }
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            writeTrue = 0;
            writeFalse = 1;
            button3.Enabled = false;
        }
    }
}
