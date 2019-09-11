using HuaweiAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Huawei_hilink
{
    class Program
    {
        static void Main(string[] args)
        {
            Huawei.APIinit();

            #region Начальные примеры

            Console.WriteLine("C# Huawei Status API");
            Console.WriteLine();
            Console.WriteLine("(C) JeroxFX 2016");


            Console.WriteLine("Mobile Data: " + ToOnOffString(Huawei.isDataEnabled()));
            //Console.WriteLine("Sim status: " + ToYesNoString(intToBool(Convert.ToInt32(Huawei.Status("SimStatus")))));
            Console.WriteLine("Roaming: " + ToYesNoString(Huawei.IsRoaming()));
            Console.WriteLine("New SMS's: " + Huawei.Notifications("sms").ToString());
            Console.WriteLine("SMS Storage Full: " + ToYesNoString(intToBool(Huawei.Notifications("OnlineUpdate"))));
            Console.WriteLine("Connection status(901 = Connected, 902 = Disconnected): " + Huawei.Status("ConnectionStatus"));

            Console.WriteLine("Signal Strength: " + Huawei.Status("SignalIcon"));
            Console.WriteLine("CurrentNetworkTypeEx: " + Huawei.Status("CurrentNetworkTypeEx"));

            Console.WriteLine("Network State: " + Huawei.NetStatus("State"));
            Console.WriteLine("Full name: " + Huawei.NetStatus("FullName"));
            Console.WriteLine("Short name: " + Huawei.NetStatus("ShortName"));
            Console.WriteLine("Numeric: " + Huawei.NetStatus("Numeric"));
            Console.WriteLine("Rat: " + Huawei.NetStatus("Rat"));

            Console.WriteLine("Device Name: " + Huawei.DeviceInfo("DN"));
            Console.WriteLine("Serial Number: " + Huawei.DeviceInfo("SN"));
            Console.WriteLine("IMEI: " + Huawei.DeviceInfo("IMEI"));
            Console.WriteLine("IMSI: " + Huawei.DeviceInfo("IMSI"));
            Console.WriteLine("ICCID: " + Huawei.DeviceInfo("ICCID"));
            Console.WriteLine("MSISDN: " + Huawei.DeviceInfo("NSISDN"));
            Console.WriteLine("Hardware Version: " + Huawei.DeviceInfo("HV"));
            Console.WriteLine("Software Version: " + Huawei.DeviceInfo("SV"));
            Console.WriteLine("WebUI Version: " + Huawei.DeviceInfo("WUIV"));
            Console.WriteLine("Mac Address: " + Huawei.DeviceInfo("MacAddress"));
            Console.WriteLine("Product Family: " + Huawei.DeviceInfo("ProductFamily"));
            Console.WriteLine("Supported modes: " + Huawei.DeviceInfo("supportmode"));
            Console.WriteLine("Current Mode: " + Huawei.DeviceInfo("workmode"));
            #endregion



            //Huawei.SMSsend("+79279282836", "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...");
            //string[] myNumb = { "+79174829844", "+79279282836" };
            //Huawei.SMSsend(myNumb, "Проверяем отправку на несколько номеров разом");
            //Console.ReadKey();


            reload:
            XmlDocument document = Huawei.SmsList(1, 30/*Huawei.Notifications("sms")*/, 1, 0, 0, 1);
            document.Save(@"SMSList.xml");
            //Console.WriteLine(document.DocumentElement.SelectSingleNode("/response/Messages/Message/Date").InnerText + " - " + document.DocumentElement.SelectSingleNode("/response/Messages/Message/Content").InnerText);
            //Console.Clear();
            foreach (XmlNode node in document.DocumentElement.SelectNodes("/response/Messages/Message"))
            {
                string phone = node["Phone"].InnerText;
                string date = node["Date"].InnerText;
                string content = node["Content"].InnerText;
                int smstat = int.Parse(node["Smstat"].InnerText);

                Console.WriteLine("\n" + date + "; От: " + phone);
                Console.Write("Смс: ");
                if (smstat == 0) Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
                else Console.ForegroundColor = ConsoleColor.DarkGray; // устанавливаем цвет
                Console.WriteLine(content);
                Console.ResetColor(); // сбрасываем в стандартный
                
            }
            //Console.WriteLine("RUN Tests for Service:");

            ////Выполняем тестовый POST запрос к службе.               
            //SendTestPostRequest(@"http://192.168.8.1/api/sms/sms-list","<PageIndex>1</PageIndex> <ReadCount>20</ReadCount><BoxType>1</BoxType> <SortType>2</SortType> <Ascending>0</Ascending> <UnreadPreferred>0</UnreadPreferred>");

            //Console.WriteLine("Press any key for exit...");
            //reload:

            // Отправка USSD команды
            //Console.WriteLine("USSD ответ: " + Huawei.USSDsend("*100#"));

            foreach (XmlNode node in document.DocumentElement.SelectNodes("/response/Messages/Message"))
            {
                string index = node["Index"].InnerText;
                
                Console.WriteLine("\n");
                Console.Write("Внутренний индекс сообщения: ");
                Console.ForegroundColor = ConsoleColor.Blue; // устанавливаем цвет
                Console.WriteLine(index);
                Console.ResetColor(); // сбрасываем в стандартный
                //Huawei.SMSsetRead(index);
            }

            Console.ReadKey();
            //Console.Clear();
            Thread.Sleep(1000);
            
            goto reload;
        }

        static bool intToBool(int co)
        {
            if (co == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ToYesNoString(bool value)
        {
            return value ? "Yes" : "No";
        }

        public static string ToOnOffString(bool value)
        {
            return value ? "On" : "Off";
        }


        //Метод выполяющий запрос по указанному адресу и получающий ответ.
        static void SendTestPostRequest(string url, string data)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                string postData = data;
                request.ContentType = "application/x-www-form-urlencoded";
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] postByteArray = encoding.GetBytes(postData);
                request.ContentLength = postByteArray.Length;

                System.IO.Stream postStream = request.GetRequestStream();
                postStream.Write(postByteArray, 0, postByteArray.Length);
                postStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine("Response Status Description: " + response.StatusDescription);
                Stream dataSteam = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataSteam);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine("Response: " + responseFromServer);
                reader.Close();
                dataSteam.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                //Если что-то пошло не так, выводим ошибочку о том, что же пошло не так.
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

    }

    class Message
    {
        public string Smstat { get; private set; }
        public string Index { get; private set; }
        public string Phone { get; private set; }
        public string Content { get; private set; }
        public string Date { get; private set; }
        public string Sca { get; private set; }
        public string SaveType { get; private set; }
        public string Priority { get; private set; }
        public string SmsType { get; private set; }

        public Message(string smstat, string index, string phone, string content, string date, string sca,string savetype, string priority, string SmsType)
        {
            Smstat = smstat;
            Index = index;
            Phone = phone;
            Content = content;
            Date = date;
            Sca = sca;
            SaveType = savetype;
            Priority = priority;
            Smstat = smstat;
        }


    }
}
