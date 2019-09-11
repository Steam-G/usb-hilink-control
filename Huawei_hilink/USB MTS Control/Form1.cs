using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using HuaweiAPI;

namespace USB_MTS_Control
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            ListSMSBlock();
            //string smscount = Huawei.SMScount();
        }

        private void ListSMSBlock()
        {
            XmlDocument document = Huawei.SmsList(1, 50/*Huawei.Notifications("sms")*/, 1, 0, 0, 1);
            foreach (XmlNode node in document.DocumentElement.SelectNodes("/response/Messages/Message"))
            {
                string phone = node["Phone"].InnerText;
                string date = node["Date"].InnerText;
                string content = node["Content"].InnerText;
                int smstat = int.Parse(node["Smstat"].InnerText);
                string index = node["Index"].InnerText;

                MessageBox messageBox = new MessageBox();
                messageBox.Name = index;
                messageBox.DateTimeMessage = date;
                messageBox.PhoneNumber = phone;
                messageBox.TextMessage = content;
                flowLayoutPanel1.Controls.Add(messageBox);

            }
            

        }
    }
}
