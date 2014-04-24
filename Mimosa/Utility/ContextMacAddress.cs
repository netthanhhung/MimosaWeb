using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Caching;
using Business;

namespace Mimosa.Utility
{
    public class ContextMacAddress
    {
        public List<string> GetListMac()
        {
            if (HttpContext.Current.Cache["MacAddress"] == null)
            {
                return new List<string>();
            }

            return (List<string>)HttpContext.Current.Cache["MacAddress"];
        }

        public static void CheckRequest()
        {
            var macAddress = GetMacAddress();
            var listMacAddress = new List<string>();
            if (HttpContext.Current.Cache["MacAddress"] == null)
            {
                var temp = DateTime.Now.AddDays(1);
                var dateTime = new DateTime(temp.Year, temp.Month, temp.Day);
                listMacAddress.Add(macAddress);
                HttpContext.Current.Cache.Insert("MacAddress", listMacAddress, null, dateTime, Cache.NoSlidingExpiration,
                    CacheItemPriority.Default,
                    null);
                RoomService.InsertCustomer();
            }
            else
            {
                listMacAddress = (List<string>)HttpContext.Current.Cache["MacAddress"];
                var item = listMacAddress.FirstOrDefault(x => x == macAddress);
                if (item == null)
                {
                    listMacAddress.Add(macAddress);
                    HttpContext.Current.Cache["MacAddress"] = listMacAddress;
                    RoomService.InsertCustomer();
                }
            }

        }



        public static string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    //IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            } return sMacAddress;
        }
    }
}