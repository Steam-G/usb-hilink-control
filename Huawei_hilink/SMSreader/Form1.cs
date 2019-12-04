using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSreader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void DecodeSMS(string sms)
        {
            try
            {
                char[] ch = sms.ToCharArray();
                //Console.Write(ch);
                //Console.WriteLine();


                string dtstr = "";
                dtstr = dtstr + ch[0] + ch[1] + "." + ch[2] + ch[3] + "." + ch[4] + ch[5] + " " + ch[6] + ch[7] + ":" + ch[8] + ch[9];
                //Console.WriteLine("Текст времени из массива символов: {0}", dtstr);

                string dateString = dtstr;// "13.11.19 14:01";
                try
                {
                    DateTime dateValue = DateTime.Parse(dateString);
                    //Console.WriteLine("'{0}' converted to {1}.", dateString, dateValue);
                    //Console.WriteLine();
                    //Console.WriteLine("Время сбора данных: '{0}'", dateValue);
                    label2.Text = dateValue.ToString("U");
                }
                catch (FormatException)
                {
                    //Console.WriteLine("Unable to convert '{0}'.", dateString);
                    label2.Text = "Дата не определяется";
                }


                string strID = "";
                strID = strID + ch[10] + ch[11];
                int id = Convert.ToInt32(strID, 16);
                //Console.WriteLine("Идентификатор данных (ID): {0}", id);
                //Console.WriteLine();
                label8.Text = NameParam(id);//id.ToString();

                string strMax = "";
                strMax = strMax + ch[12] + ch[13] + ch[14] + ch[15];
                //Console.WriteLine("Максимальное значение в 16-ричной системе исчисления: {0}", strMax);
                

                int max = Convert.ToInt32(strMax, 16);
                //Console.WriteLine("Максимум: {0}", max);
                //Console.WriteLine();
                label4.Text = max.ToString();

                string strMin = "";
                strMin = strMin + ch[16] + ch[17] + ch[18] + ch[19];
                //Console.WriteLine("Минимальное значение в 16-ричной системе исчисления: {0}", strMin);

                int min = Convert.ToInt32(strMin, 16);
                //Console.WriteLine("Минимум: {0}", min);
                //Console.WriteLine();
                label6.Text = min.ToString();

                int dataCount = (ch.Length - 20) / 2;
                string[] strData = new string[dataCount];
                for (int i = 0; i < dataCount; i++)
                {
                    strData[i] = strData[i] + ch[i * 2 + 20] + ch[i * 2 + 21];
                    //Console.Write("'{0}',", strData[i]);
                }
                //Console.WriteLine();
                //Console.WriteLine();

                int[] data = new int[dataCount];
                dataGridView1.RowCount = dataCount;
                chart1.Series[0].Points.Clear();
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline; // тут сами поизменяет/повыбирайте тип вывода графика
                chart1.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                chart1.Series[0].MarkerBorderWidth = 5;
                chart1.Series[0].BorderWidth = 3;


                // если 0 то проверяем рядом стоящую точку, если <128 то 

                for (int i = 0; i < dataCount; i++)
                {
                    Byte x = Convert.ToByte(strData[i], 16);
                    if (x==0)
                    {
                        if (Convert.ToByte(strData[i-1], 16)>x+20 && Convert.ToByte(strData[i - 2], 16) > x + 20)
                        {
                            x = Convert.ToByte(strData[i - 1], 16);
                        }     
                    }
                    float xd = ((float)x / 256) * (max - min) + min;
                    data[i] = (int)xd;
                    Console.Write("'{0}',", data[i]);
                    dataGridView1.Rows[i].Cells[0].Value = i;
                    dataGridView1.Rows[i].Cells[1].Value = data[i];
                    chart1.Series[0].Points.AddXY(i, data[i]);
                }
            }
            catch
            {
                //Console.WriteLine("Строка не соответствует формату '{0}'.", sms);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DecodeSMS(textBox1.Text);
        }

        public string NameParam(int ID)
        {
                var map = new Dictionary<int, string>()
                {
                    //{0, "DinVibro_Poperechn_Reduktor"},
                    //{1, "DinVibro_Prodoln_Reduktor"},
                    //{2, "DinVibro_Vertikaln_Reduktor"},
                    //{3, "DinVibro_Poperechn_Stoika"},
                    //{4, "DinVibro_Prodoln_Stoika"},
                    //{5, "DinVibro_Vertikaln_Stoika"},
                    //{6, "DinVibro_Poperechn_ElectroDv"},
                    //{7, "DinVibro_Prodoln_ElectroDv"},
                    //{8, "DinVibro_Vertikaln_ElectroDv"},
                    //{9, "Vattmetrogramma"},
                    //{10, "DinUsilieNaKanatnoyPodveske"},
                    //{11, "NapryajenieNaUstie"},
                    //{12, "PogrujnoiModul"}
                    {0, "Вибрация поперечная редуктор"},
                    {1, "Вибрация продольная редуктор"},
                    {2, "Вибрация вертикальная редуктор"},
                    {3, "Вибрация поперечная стойка"},
                    {4, "Вибрация продольная стойка"},
                    {5, "Вибрация вертикальная стойка"},
                    {6, "Вибрация поперечная электро двигатель"},
                    {7, "Вибрация продольная электро двигатель"},
                    {8, "Вибрация вертикальная электро двигатель"},
                    {9, "Ваттграмма"},
                    {10, "Усилие на канатной подвеске"},
                    {11, "Напряжение на устье"},
                    {12, "Погружной модуль"}

                };
                string output;
                return map.TryGetValue(ID, out output) ? output : "default";
        }
    }
}
