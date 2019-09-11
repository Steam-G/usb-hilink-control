#Confirmed working devices:
Huawei E3531

#Use

    Huawei.isDataEnabled() //Returns a bool based on mobile data
    Huawei.isRoaming() //Returns a bool based on Roaming
  
    Huawei.Status()
      available infos:
      ConnectionStatus //901 = Connected - 902 = Disconnected
      SignalStrength
      SignalIcon //A string based on the signal strength (0 - 5)
      CurrentNetworkType
      simlockStatus //0 = No 1=Yes
      WanIPAddress
      WanIPv6Address
      ServiceStatus
      SimStatus
      classify
    Huawei.NetStatus
      State
      FullName //Returns network operator's name
      ShotName //Returns network operator's short name
      Numeric //Returns numeric network operator
      Rat //I don't know what id does
    Huawei.DeviceInfo()
      DN //Returns Device Name
      SN //Returns Serial Number
      IMEI //Returns IMEI
      IMSI //Returns IMSI
      ICCID //Return ICCID
      MSISDN //Returns MSISDN
      HV //Returns Hardware Version
      SV //Returns Software Version
      WUIV //Returns WebUI Version
      MacAddress //Returns MacAddress
      ProductFamily //Returns Product Family
      supportmode //Returns Supported modes
      workmode //Returns Current mode
    Huawei.Notifications()
      sms //Returns number of new messages
      SmsFull //Returns a boolean base on the sms store
      OnlineUpdate //I don't know
    
    
  eg(Huawei.DeviceInfo("IMEI");
