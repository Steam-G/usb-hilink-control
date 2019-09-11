/*
Huawei Status API
©JeroxFX 2016
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.IO.Compression;
using System.Xml;
using System.Collections.Specialized;
using System.Net.Cache;

namespace HuaweiAPI
{
    class Huawei
    {
        public static WebClient _WB = new WebClient();
        static CookieWebClient CWB = new CookieWebClient();

        public static bool APIinit()
        {
            try
            {
                CWB.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2810.2 Safari/537.36");
                CWB.DownloadString("http://192.168.8.1");
                return true;
            } catch(Exception e)
            {
                return false;
            }
        }

        public static bool isDataEnabled()
        {    
            string response = CWB.DownloadString("http://192.168.8.1/api/dialup/mobile-dataswitch");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/dataswitch");
            string attr = xNode.InnerText;

            if (attr.Contains("1"))
            {
                return true;
            }
            else if (attr.Contains("0"))
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        public static string Status(string info)
        {
            string response = CWB.DownloadString("http://192.168.8.1/api/monitoring/status");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/" + info);
            string attr = xNode.InnerText;
            return attr;
        }

        public static bool IsRoaming()
        {
            string response = CWB.DownloadString("http://192.168.8.1/api/monitoring/status");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/RoamingStatus");
            string attr = xNode.InnerText;


            if (attr.Contains("1"))
            {
                return true;
            }
            else if (attr.Contains("0"))
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        public static int Notifications(string name)
        {
            string response = CWB.DownloadString("http://192.168.8.1/api/monitoring/check-notifications");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/UnreadMessage");
            
            switch (name)
            {
                case "sms":
                    {
                        xNode =  xDoc.DocumentElement.SelectSingleNode("/response/UnreadMessage");
                        break;
                    }
                case "SmsFull":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/SmsStorageFull");
                        break;
                    }
                case "OnlineUpdate":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/OnlineUpdateStatus");
                        break;
                    }
            }
            string attr = xNode.InnerText;
            return Convert.ToInt32(attr);
        }

        public static string NetStatus(string name)
        {
            
            string response = CWB.DownloadString("http://192.168.8.1/api/net/current-plmn");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/FullName");

            switch (name)
            {
                case "State":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/State");
                        break;
                    }
                case "FullName":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/FullName");
                        break;
                    }
                case "ShortName":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/ShortName");
                        break;
                    }
                case "Numeric":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/Numeric");
                        break;
                    }
                case "Rat":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/Rat");
                        break;
                    }
            }
            return xNode.InnerText;
        }

        public static string DeviceInfo(string name)
        {
            string response = CWB.DownloadString("http://192.168.8.1/api/device/information");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/Imei");

            switch (name)
            {
                case "DN":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/DeviceName");
                        break;
                    }
                case "SN":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/SerialNumber");
                        break;
                    }
                case "IMEI":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/Imei");
                        break;
                    }
                case "IMSI":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/Imsi");
                        break;
                    }
                case "ICCID":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/Iccid");
                        break;
                    }
                case "MSISDN":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/Msisdn");
                        break;
                    }
                case "HV":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/HardwareVersion");
                        break;
                    }
                case "SV":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/SoftwareVersion");
                        break;
                    }
                case "WUIV":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/WebUIVersion");
                        break;
                    }
                case "MacAddress":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/MacAddress1");
                        break;
                    }
                case "ProductFamily":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/Msisdn");
                        break;
                    }
                case "supportmode":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/supportmode");
                        break;
                    }
                case "workmode":
                    {
                        xNode = xDoc.DocumentElement.SelectSingleNode("/response/workmode");
                        break;
                    }
            }
            return xNode.InnerText;
        }
    }

    public class CookieWebClient : WebClient
    {
        private CookieContainer m_container = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = m_container;
            }
            return request;
        }
    }
}
