using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USB_MTS_Control
{
    public partial class MessageBox : UserControl
    {
        private string _DateTimeMessage;
        private string _PhoneNumber;
        private string _TextMessage;

        public string DateTimeMessage
        {
            get { return _DateTimeMessage; }
            set
            {
                if (_DateTimeMessage != value)
                {
                    _DateTimeMessage = value;
                    label1.Text = value;
                }
            }
        }

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set
            {
                if (_PhoneNumber != value)
                {
                    _PhoneNumber = value;
                    label2.Text = value;
                }
            }
        }

        public string TextMessage
        {
            get { return _TextMessage; }
            set
            {
                if (_TextMessage != value)
                {
                    _TextMessage = value;
                    textBox1.Text = value;
                }
            }
        }


        public MessageBox()
        {
            InitializeComponent();
        }


    }
}
