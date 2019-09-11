using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaweiAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Huawei.APIinit();

            Console.WriteLine("C# Huawei Status API");
            Console.WriteLine();
            Console.WriteLine("(C) JeroxFX 2016");

            reload:
            Console.WriteLine("Mobile Data: " + ToOnOffString(Huawei.isDataEnabled()));
            Console.WriteLine("Sim status: " + ToYesNoString(intToBool(Convert.ToInt32(Huawei.Status("SimStatus")))));
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

            Console.ReadKey();
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
    }
}
