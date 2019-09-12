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
    public partial class SmsCountBox : UserControl
    {
        private string _LocalUnread;
        private string _LocalInbox;
        private string _LocalOutbox;
        private string _LocalDraft;
        private string _LocalDeleted;
        private string _SimUnread;
        private string _SimInbox;
        private string _SimOutbox;
        private string _SimDraft;
        private string _LocalMax;
        private string _SimMax;
        private string _SimUsed;
        private string _NewMsg;

        public string LocalUnread
        {
            get { return _LocalUnread; }
            set
            {
                if (_LocalUnread != value)
                {
                    _LocalUnread = value;
                    string[] record = { "Непрочитанные сообщения", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string LocalInbox
        {
            get { return _LocalInbox; }
            set
            {
                if (_LocalInbox != value)
                {
                    _LocalInbox = value;
                    string[] record = { "Всего принято", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string LocalOutbox
        {
            get { return _LocalOutbox; }
            set
            {
                if (_LocalOutbox != value)
                {
                    _LocalOutbox = value;
                    string[] record = { "Отправлено", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string LocalDraft
        {
            get { return _LocalDraft; }
            set
            {
                if (_LocalDraft != value)
                {
                    _LocalDraft = value;
                    string[] record = { "Черновиков", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string LocalDeleted
        {
            get { return _LocalDeleted; }
            set
            {
                if (_LocalDeleted != value)
                {
                    _LocalDeleted = value;
                    string[] record = { "Удалено", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string SimUnread
        {
            get { return _SimUnread; }
            set
            {
                if (_SimUnread != value)
                {
                    _SimUnread = value;
                    string[] record = { "Непрочитанные на Sim", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string SimInbox
        {
            get { return _SimInbox; }
            set
            {
                if (_SimInbox != value)
                {
                    _SimInbox = value;
                    string[] record = { "Принятых на Sim", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string SimOutbox
        {
            get { return _SimOutbox; }
            set
            {
                if (_SimOutbox != value)
                {
                    _SimOutbox = value;
                    string[] record = { "Отправленных из Sim", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string SimDraft
        {
            get { return _SimDraft; }
            set
            {
                if (_SimDraft != value)
                {
                    _SimDraft = value;
                    string[] record = { "Черновики на Sim", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string LocalMax
        {
            get { return _LocalMax; }
            set
            {
                if (_LocalMax != value)
                {
                    _LocalMax = value;
                    string[] record = { "Максимальный объем", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string SimMax
        {
            get { return _SimMax; }
            set
            {
                if (_SimMax != value)
                {
                    _SimMax = value;
                    string[] record = { "Максимум на Sim", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string SimUsed
        {
            get { return _SimUsed; }
            set
            {
                if (_SimUsed != value)
                {
                    _SimUsed = value;
                    string[] record = { "Использовано на Sim", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public string NewMsg
        {
            get { return _NewMsg; }
            set
            {
                if (_NewMsg != value)
                {
                    _NewMsg = value;
                    string[] record = { "Новые сообщения", value };
                    dgvSms.Rows.Add(record);
                }
            }
        }

        public SmsCountBox()
        {
            InitializeComponent();
        }
    }
}
