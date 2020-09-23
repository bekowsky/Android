using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace App1
{

   

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        List<offer> ItemList;

        public void Get()
        {
            ItemList.Clear();
            String URLString = "https://yastatic.net/market-export/_/partner/help/YML.xml";
            XmlTextReader reader = new XmlTextReader(URLString);
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(reader);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlSerializer serializer = new XmlSerializer(typeof(offer));
            foreach (XmlNode xnode in xRoot)
            {
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "offers")
                    {
                        foreach (XmlNode children in childnode.ChildNodes)
                        {
                            offer ofer;
                            using (var sr = new StringReader(children.OuterXml))
                                ofer = (offer)serializer.Deserialize(sr);
                            ofer.ID = Convert.ToInt32(children.Attributes[0].Value);
                            ItemList.Add(ofer);
                        }
                    }
                }
            }

        }

      
        public void FillList()
        {
            ListView listView = FindViewById<ListView>(Resource.Id.listView1);

            
            ArrayAdapter<int> adapter = new ArrayAdapter<int>(
        this, Android.Resource.Layout.SimpleListItem1, ItemList.Select(x => x.ID).ToList());
            
            listView.SetAdapter(adapter);
        }
        public async Task GetAsync()
        {
            await Task.Run(() => Get());
            FillList();
        }


        public void SelectItem(Object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(Activity1));
            offer offer = ItemList.ElementAt(eventArgs.Position);
            string jsonobject = JsonConvert.SerializeObject(offer);
            intent.PutExtra("link_object",jsonobject);
            StartActivity(intent);
        }

       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            
            ItemList = new List<offer>();

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            

            ListView listView = FindViewById<ListView>(Resource.Id.listView1);
            listView.ItemClick +=  (sender, args) =>  SelectItem(sender,args);

            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += async (sender, args) => await GetAsync();



        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
