using HuaweiAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleSHSNUsmsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string sms = "16111900580C02ED02DF5B009292DB806E6E92928080494980805B5B808080802549929292926E49000049495B495B5B12126E805B5B5B5BA592252549498049929292926E8037376E6E3737C9C98080";

            //DecodeSMS(sms);

            //reload:
            //Console.Clear();
            //XmlDocument document = Huawei.SmsList(1, 1, 1, 0, 0, 0);
            //document.Save(@"SMSList.xml");
            ////Console.WriteLine(document.DocumentElement.SelectSingleNode("/response/Messages/Message/Date").InnerText + " - " + document.DocumentElement.SelectSingleNode("/response/Messages/Message/Content").InnerText);
            ////Console.Clear();
            //foreach (XmlNode node in document.DocumentElement.SelectNodes("/response/Messages/Message"))
            //{
            //    string phone = node["Phone"].InnerText;
            //    string date = node["Date"].InnerText;
            //    string content = node["Content"].InnerText;
            //    int smstat = int.Parse(node["Smstat"].InnerText);

            //    Console.WriteLine("\n" + date + "; От: " + phone);
            //    Console.Write("Смс: ");
            //    if (smstat == 0) Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
            //    else Console.ForegroundColor = ConsoleColor.DarkGray; // устанавливаем цвет
            //    Console.WriteLine(content);
            //    Console.ResetColor(); // сбрасываем в стандартный

            //    DecodeSMS(content);
            //}

            ////Console.ReadKey();

            //Thread.Sleep(5000);

            //goto reload;

            //Создание делегата для типа Timer.
            TimerCallback timeCB = new TimerCallback(PrintLastSMS);

            //Установка параметров таймера.
            Timer t = new Timer(
                timeCB, //Тип делегата TimerCallback.
                null,   //Информация для вызываемого метода или null.
                0,      //Время ожидания для старта.
                15000);  //Интервал между вызовами (в миллисекундах).
            Console.WriteLine("Нажмите <<Enter>> для завершения работы...");
            Console.ReadLine();
        }
        private static void PrintLastSMS(object state)
        {
            Console.Clear();
            XmlDocument document = Huawei.SmsList(1, 1, 1, 0, 0, 0);
            document.Save(@"SMSList.xml");
            //Console.WriteLine(document.DocumentElement.SelectSingleNode("/response/Messages/Message/Date").InnerText + " - " + document.DocumentElement.SelectSingleNode("/response/Messages/Message/Content").InnerText);
            //Console.Clear();
            foreach (XmlNode node in document.DocumentElement.SelectNodes("/response/Messages/Message"))
            {
                string phone = node["Phone"].InnerText;
                string date = node["Date"].InnerText;
                string content = node["Content"].InnerText;
                int smstat = int.Parse(node["Smstat"].InnerText);

                Console.WriteLine("\nВремя приема: {0}; От: {1}", date, phone);
                Console.Write("Смс: ");
                if (smstat == 0) Console.ForegroundColor = ConsoleColor.Green; // устанавливаем цвет
                else Console.ForegroundColor = ConsoleColor.DarkGray; // устанавливаем цвет
                Console.WriteLine(content);
                Console.ResetColor(); // сбрасываем в стандартный
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Кол-во символов в сообщении: {0}", content.Length);
                Console.ResetColor(); // сбрасываем в стандартный

                DecodeSMS(content);
            }
        }
        private static void DecodeSMS(string sms)
        {
            try
            {
                char[] ch = sms.ToCharArray();
                Console.Write(ch);
                Console.WriteLine();


                string dtstr = "";
                dtstr = dtstr + ch[0] + ch[1] + "." + ch[2] + ch[3] + "." + ch[4] + ch[5] + " " + ch[6] + ch[7] + ":" + ch[8] + ch[9];
                Console.WriteLine("Текст времени из массива символов: {0}", dtstr);

                string dateString = dtstr;// "13.11.19 14:01";
                try
                {
                    DateTime dateValue = DateTime.Parse(dateString);
                    //Console.WriteLine("'{0}' converted to {1}.", dateString, dateValue);
                    Console.WriteLine();
                    Console.WriteLine("Время сбора данных: '{0}'", dateValue);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '{0}'.", dateString);
                }


                string strID = "";
                strID = strID + ch[10] + ch[11];
                int id = Convert.ToInt32(strID,16);
                Console.WriteLine("Идентификатор данных (ID): {0}", id);
                Console.WriteLine();


                string strMax = "";
                strMax = strMax + ch[12] + ch[13] + ch[14] + ch[15];
                Console.WriteLine("Максимальное значение в 16-ричной системе исчисления: {0}", strMax);

                int max = Convert.ToInt32(strMax, 16);
                Console.WriteLine("Максимум: {0}", max);
                Console.WriteLine();


                string strMin = "";
                strMin = strMin + ch[16] + ch[17] + ch[18] + ch[19];
                Console.WriteLine("Минимальное значение в 16-ричной системе исчисления: {0}", strMin);

                int min = Convert.ToInt32(strMin, 16);
                Console.WriteLine("Минимум: {0}", min);
                Console.WriteLine();


                int dataCount = (ch.Length - 20) / 2;
                string[] strData = new string[dataCount];
                for (int i = 0; i < dataCount; i++)
                {
                    strData[i] = strData[i] + ch[i * 2 + 20] + ch[i * 2 + 21];
                    Console.Write("'{0}',", strData[i]);
                }
                Console.WriteLine();
                Console.WriteLine();

                int[] data = new int[dataCount];
                for (int i = 0; i < dataCount; i++)
                {
                    Byte x = Convert.ToByte(strData[i], 16);
                    float xd = ((float)x / 256) * (max - min) + min;
                    data[i] = (int)xd;
                    Console.Write("'{0}',", data[i]);
                }
            }
            catch {
                Console.WriteLine("Строка не соответствует формату '{0}'.", sms);
            }
        }
    }
    public class PLCsms : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


    }
}
