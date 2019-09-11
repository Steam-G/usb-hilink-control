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
using System.Threading;

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

        /// <summary>
        /// Запрашивает СМС с устройства в формате XML-документа
        /// </summary>
        /// <param name="PageIndex">Индекс страницы</param>
        /// <param name="ReadCount">Количество сообщений на странице</param>
        /// <param name="BoxType">Тип содержимого</param>
        /// <param name="SortType">Тип сортировки</param>
        /// <param name="Ascending">По возрастанию</param>
        /// <param name="UnreadPreferred">Сперва не прочитанное</param>
        /// <returns></returns>
        public static XmlDocument SmsList(int PageIndex, int ReadCount, int BoxType, int SortType, int Ascending, int UnreadPreferred)
        {
            string response = CWB.DownloadString("http://192.168.8.1/api/webserver/SesTokInfo");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            // Тут нам нужна специальная строка информации о текущей сессии
            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/SesInfo");
                string SesInfo = xNode.InnerText;

            // А тут мы вытаскиваем строку о токене, это все нужно для составления правильного запроса
            xNode = xDoc.DocumentElement.SelectSingleNode("/response/TokInfo");
                string TokInfo = xNode.InnerText;


            // Создаем новый запрос по указанной ссылке
            var request = WebRequest.Create("http://192.168.8.1/api/sms/sms-list");

            // Заполняем хидеры, необходимые для POST запроса в устройство
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["UserAgent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2810.2 Safari/537.36";
            request.Headers["Cookie"] = SesInfo;
            request.Headers.Add("__RequestVerificationToken", TokInfo);

            // Указываем метод отправляемого запроса (GET / POST)
            request.Method = "POST";

            // Формируем запрос и отправляем его
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string xml = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" +
                    //"<request>" +
                    //    "<PageIndex>1</PageIndex>" +
                    //    "<ReadCount>50</ReadCount>" +
                    //    "<BoxType>1</BoxType>" +
                    //    "<SortType>0</SortType>" +
                    //    "<Ascending>0</Ascending>" +
                    //    "<UnreadPreferred>0</UnreadPreferred>" +
                    //"</request>";
                    "<request>" +
                        "<PageIndex>"           + PageIndex.ToString()          + "</PageIndex>" +
                        "<ReadCount>"           + ReadCount.ToString()          + "</ReadCount>" +
                        "<BoxType>"             + BoxType.ToString()            + "</BoxType>" +
                        "<SortType>"            + SortType.ToString()           + "</SortType>" +
                        "<Ascending>"           + Ascending.ToString()          + "</Ascending>" +
                        "<UnreadPreferred>"     + UnreadPreferred.ToString()    + "</UnreadPreferred>" +
                    "</request>";
                streamWriter.Write(xml);
            }

            // Тут мы принимаем ответ и 
            HttpWebResponse response2 = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response2.GetResponseStream())
            {
              StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string responseXml = reader.ReadToEnd();

                // Превратим полученный текст в XML документ и попробуем выдернуть из него информацию по тегам :)
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseXml);

                //// Выдернем содержимое первого попавшегося узла, который находтся в указанном месте!! ))  - тут сейчас берется самая последняя СМС-ка (т.е. самая новая)
                //XmlNode node = xmlDocument.DocumentElement.SelectSingleNode("/response/Messages/Message/Content");

                //Console.WriteLine(" ");
                //Console.WriteLine(node.InnerText);

                return xmlDocument;
            }
            
        }

        public static string USSDsend(string command)
        {
            string response = CWB.DownloadString("http://192.168.8.1/api/webserver/SesTokInfo");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            // Тут нам нужна специальная строка информации о текущей сессии
            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/SesInfo");
            string SesInfo = xNode.InnerText;

            // А тут мы вытаскиваем строку о токене, это все нужно для составления правильного запроса
            xNode = xDoc.DocumentElement.SelectSingleNode("/response/TokInfo");
            string TokInfo = xNode.InnerText;


            // Создаем новый запрос по указанной ссылке
            var request = WebRequest.Create("http://192.168.8.1/api/ussd/send");

            // Заполняем хидеры, необходимые для POST запроса в устройство
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["UserAgent"] = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:66.0) Gecko/20100101 Firefox/66.0";
            request.Headers["Cookie"] = SesInfo;
            request.Headers.Add("__RequestVerificationToken", TokInfo);
           
            //request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            //request.Headers.Add("Accept-Encoding", "gzip, deflate");
            //request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //request.ContentLength = 350;
            //request."Connection"] = " keep-alive";


            // Указываем метод отправляемого запроса (GET / POST)
            request.Method = "POST";

            // Формируем запрос и отправляем его
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string xml = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" +
                    "<request>" +
                        "<content>" + command + "</content>" +
                        "<codeType>" + "CodeType" + "</codeType>" +
                        "<timeout>" + "</timeout>" +
                    "</request>";
                streamWriter.Write(xml);
            }

            // Тут мы принимаем ответ и 
            HttpWebResponse response2 = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response2.GetResponseStream())
            {
                
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string responseXml = reader.ReadToEnd();

                // Превратим полученный текст в XML документ и попробуем выдернуть из него информацию по тегам :)
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseXml);
                Console.WriteLine(xmlDocument.InnerText);

                // Выдернем содержимое первого попавшегося узла, который находтся в указанном месте!! ))  - тут сейчас берется самая последняя СМС-ка (т.е. самая новая)
                while (xmlDocument.InnerText!="OK")
                {
                    Thread.Sleep(1000);
                    response = CWB.DownloadString("http://192.168.8.1/api/ussd/get");
                    xmlDocument.LoadXml(response);
                    Console.WriteLine(xmlDocument.InnerText);
                }

            }

            ussdgetresponse:
            response = CWB.DownloadString("http://192.168.8.1/api/ussd/get");
            XmlDocument xDocUssdGet = new XmlDocument();
            xDocUssdGet.LoadXml(response);
            

            while (xDocUssdGet.InnerText == "111019")
            {
                Console.WriteLine(xDocUssdGet.InnerText);
                Thread.Sleep(1000);
                goto ussdgetresponse;
                
            }

            xDocUssdGet.Save(@"ussdresp.xml");


            // Тут нам нужна специальная строка информации о текущей сессии
            XmlNode xNodeUssdGet = xDocUssdGet.DocumentElement.SelectSingleNode("/response/content");
            string ussdresponse = xNodeUssdGet.InnerText;;

            // GET запрос возвращает нам xml с ответом, 
            // но этот ответ имеет не верную кодировку, 
            // пототму надо провести конвертирование 
            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            byte[] utf8Bytes = win1251.GetBytes(ussdresponse);
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);

            string utfLine = win1251.GetString(win1251Bytes);


            return utfLine;
        }

        //ЧТО_ТО НЕ РАБОТАЕТ!!!!!!
        public static string SMScount()
        {
            string response = CWB.DownloadString("http://192.168.8.1/api/sms/sms-count");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/LocalUnread");
            return xNode.InnerText;
        }


        /// <summary>
        /// Функция отправки СМС, требуется только номер адресата в формате +7xxxxxxxxxx и само сообщение
        /// </summary>
        /// <param name="Phone">Номер получателя СМС</param>
        /// <param name="Content">Сообщение</param>
        public static void SMSsend(string Phone, string Content)
        {
            string response = CWB.DownloadString("http://192.168.8.1/api/webserver/SesTokInfo");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            // Тут нам нужна специальная строка информации о текущей сессии
            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/SesInfo");
            string SesInfo = xNode.InnerText;

            // А тут мы вытаскиваем строку о токене, это все нужно для составления правильного запроса
            xNode = xDoc.DocumentElement.SelectSingleNode("/response/TokInfo");
            string TokInfo = xNode.InnerText;


            // Создаем новый запрос по указанной ссылке
            var request = WebRequest.Create("http://192.168.8.1/api/sms/send-sms");

            // Заполняем хидеры, необходимые для POST запроса в устройство
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["UserAgent"] = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:66.0) Gecko/20100101 Firefox/66.0";
            request.Headers["Cookie"] = SesInfo;
            request.Headers.Add("__RequestVerificationToken", TokInfo);

            //request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            //request.Headers.Add("Accept-Encoding", "gzip, deflate");
            //request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //request.ContentLength = 350;
            //request."Connection"] = " keep-alive";


            // Указываем метод отправляемого запроса (GET / POST)
            request.Method = "POST";

            // Формируем запрос и отправляем его
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string xml = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" +
                    "<request>" +
                        "<Index>" + "-1" + "</Index>" +
                        "<Phones>" +
                            "<Phone>" + Phone + "</Phone>" +
                        "</Phones>" +
                        "<Sca>" + "</Sca>" +
                        "<Content>" + Content + "</Content>" +
                        "<Length>" + Content.Length.ToString() + "</Length>" +
                        "<Reserved>" + "0" + "</Reserved>" +
                        "<Date>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+ "</Date>" +
                    "</request>";
                streamWriter.Write(xml);
            }

            // Тут мы принимаем ответ и 
            HttpWebResponse response2 = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response2.GetResponseStream())
            {

                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string responseXml = reader.ReadToEnd();

                // Превратим полученный текст в XML документ и попробуем выдернуть из него информацию по тегам :)
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseXml);
                Console.WriteLine(xmlDocument.InnerText);
            }

        }

        /// <summary>
        /// Функция отправки СМС сразу нескольким получателям. Номер должен быть в формате +7xxxxxxxxxx
        /// </summary>
        /// <param name="Phone">Массив номеров получателей СМС</param>
        /// <param name="Content">Собщение</param>
        public static void SMSsend(string[] Phone, string Content)
        {
            string response = CWB.DownloadString("http://192.168.8.1/api/webserver/SesTokInfo");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            // Тут нам нужна специальная строка информации о текущей сессии
            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/SesInfo");
            string SesInfo = xNode.InnerText;

            // А тут мы вытаскиваем строку о токене, это все нужно для составления правильного запроса
            xNode = xDoc.DocumentElement.SelectSingleNode("/response/TokInfo");
            string TokInfo = xNode.InnerText;


            // Создаем новый запрос по указанной ссылке
            var request = WebRequest.Create("http://192.168.8.1/api/sms/send-sms");

            // Заполняем хидеры, необходимые для POST запроса в устройство
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["UserAgent"] = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:66.0) Gecko/20100101 Firefox/66.0";
            request.Headers["Cookie"] = SesInfo;
            request.Headers.Add("__RequestVerificationToken", TokInfo);

            // Указываем метод отправляемого запроса (GET / POST)
            request.Method = "POST";

            // Формируем запрос и отправляем его
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string phone="";
                foreach (string number in Phone)
                {
                    phone += "<Phone>"+ number + "</Phone>";
                }
                
                string xml = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" +
                    "<request>" +
                        "<Index>" + "-1" + "</Index>" +
                        "<Phones>" +
                            phone +
                        "</Phones>" +
                        "<Sca>" + "</Sca>" +
                        "<Content>" + Content + "</Content>" +
                        "<Length>" + Content.Length.ToString() + "</Length>" +
                        "<Reserved>" + "0" + "</Reserved>" +
                        "<Date>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</Date>" +
                    "</request>";
                streamWriter.Write(xml);
            }

            // Тут мы принимаем ответ и 
            HttpWebResponse response2 = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response2.GetResponseStream())
            {

                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string responseXml = reader.ReadToEnd();

                // Превратим полученный текст в XML документ и попробуем выдернуть из него информацию по тегам :)
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseXml);
                Console.WriteLine(xmlDocument.InnerText);
            }
        }

        public static void SMSsetRead(string Index)
        {
            string response = CWB.DownloadString("http://192.168.8.1/api/webserver/SesTokInfo");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(response);

            // Тут нам нужна специальная строка информации о текущей сессии
            XmlNode xNode = xDoc.DocumentElement.SelectSingleNode("/response/SesInfo");
            string SesInfo = xNode.InnerText;

            // А тут мы вытаскиваем строку о токене, это все нужно для составления правильного запроса
            xNode = xDoc.DocumentElement.SelectSingleNode("/response/TokInfo");
            string TokInfo = xNode.InnerText;

            // Создаем новый запрос по указанной ссылке
            var request = WebRequest.Create("http://192.168.8.1/api/sms/set-read");

            // Заполняем хидеры, необходимые для POST запроса в устройство
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["UserAgent"] = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:66.0) Gecko/20100101 Firefox/66.0";
            request.Headers["Cookie"] = SesInfo;
            request.Headers.Add("__RequestVerificationToken", TokInfo);

            // Указываем метод отправляемого запроса (GET / POST)
            request.Method = "POST";

            // Формируем запрос и отправляем его
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string xml = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" +
                    "<request>" +
                        "<Index>" + Index + "</Index>" +
                    "</request>";
                streamWriter.Write(xml);
            }

            // Тут мы принимаем ответ и 
            HttpWebResponse response2 = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response2.GetResponseStream())
            {

                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string responseXml = reader.ReadToEnd();

                // Превратим полученный текст в XML документ и попробуем выдернуть из него информацию по тегам :)
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseXml);
                Console.WriteLine(xmlDocument.InnerText);
            }
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
