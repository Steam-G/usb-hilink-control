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

            
            Huawei.APIinit();

            InfoSMSBlock(smsCountBox1);
            //ListSMSBlock();

            string smscount = Huawei.SMScount("Inbox");
            int test = Huawei.Notifications("sms");

            // Создаем  делегает, который ссылается на нужный метод
            SomeDelegat sd = new SomeDelegat(ListSMSBlock);
            //sd.Invoke();
            // Вызываем метод с делегатом в качестве аргумента
            tiktak(sd);
        }

        delegate void SomeDelegat();

        private void tiktak(SomeDelegat sd)
        {
            var timer = new System.Threading.Timer(
                e => sd.Invoke(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(10));
        }

        private void ListSMSBlock()
        {
            XmlDocument document = Huawei.SmsList(1, 50/*Huawei.Notifications("sms")*/, 1, 0, 0, 1);

            this.flowLayoutPanel1.BeginInvoke((MethodInvoker)(() => this.flowLayoutPanel1.Controls.Clear()));
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

                
                this.flowLayoutPanel1.BeginInvoke((MethodInvoker)(() => this.flowLayoutPanel1.Controls.Add(messageBox)));
                //flowLayoutPanel1.Controls.Add(messageBox);

            }
        }

        private void InfoSMSBlock(SmsCountBox smsCountBox)
        {
            //SmsCountBox smsCountBox = new SmsCountBox();
            smsCountBox.LocalUnread = Huawei.SMScount("Unread");
            smsCountBox.LocalInbox = Huawei.SMScount("Inbox");
            smsCountBox.LocalOutbox = Huawei.SMScount("Outbox");

            smsCountBox.LocalDraft = Huawei.SMScount("Draft");
            smsCountBox.LocalDeleted = Huawei.SMScount("Deleted");
            smsCountBox.SimUnread = Huawei.SMScount("SimUnread");
            smsCountBox.SimInbox = Huawei.SMScount("SimInbox");
            smsCountBox.SimOutbox = Huawei.SMScount("SimOutbox");
            smsCountBox.SimDraft = Huawei.SMScount("SimDraft");
            smsCountBox.LocalMax = Huawei.SMScount("LocalMax");
            smsCountBox.SimMax = Huawei.SMScount("SimMax");
            smsCountBox.SimUsed = Huawei.SMScount("SimUsed");
            smsCountBox.NewMsg = Huawei.SMScount("NewMsg");

            //flowLayoutPanel1.Controls.Add(smsCountBox);
            //smsCountBox1 = smsCountBox;
            //smsCountBox1.Update();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ListSMSBlock();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListSMSBlock();
        }
    }
}
