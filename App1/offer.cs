using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    [Serializable]
    public class offer
    {
        public int ID { get; set; }
        public string url { get; set; }
        public int price { get; set; }
        public int categoryId { get; set; }
        public int local_delivery_cost { get; set; }
        public bool delivery { get; set; }
        public bool manufacturer_warranty { get; set; }
        public string currencyId { get; set; }
        public string picture { get; set; }
        public string typePrefix { get; set; }
        public string vendorCode { get; set; }
        public string model { get; set; }
        public string country_of_origin { get; set; }

        public offer()
        {

        }
    }
}