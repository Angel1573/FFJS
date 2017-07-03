using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;

namespace Application
{
    [Activity(Label = "KlantActivity")]
    public class KlantActivity : Activity
    {
        private List<Person> mItem;
        private ListView MListView;


        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Klant);
            // Create your application here
            //SetContentView(Resource.Layout.Main);

            MListView = FindViewById<ListView>(Resource.Id.MyListView);

            var tosplit = await AdministratieActivity.Getrelaties();

            dynamic obj = JArray.Parse(tosplit);
            mItem = new List<Person>();

            // kijk naar elk item in obj
            foreach (JObject item in obj)
            {   // check of het een klant of leverancier is
                if (item.GetValue("relatiesoort").ToString().Contains("Klant"))
                {
                   
                    string kcode = item.GetValue("relatiecode").ToString();
                    string firstName = item.GetValue("naam").ToString();

                    
                    // Zet alles in de Listview
                    mItem.Add(new Person() { txtname = kcode, txtnaam = firstName });
                }
            }

            MylistViewAdapter adapter = new MylistViewAdapter(this, mItem);
            MListView.Adapter = adapter;

            var OpslaanContacten1 = FindViewById<Button>(Resource.Id.OpslaanContacten1);
            OpslaanContacten1.Click += OpslaanContacten1_Click;

            var ToevoegenContacten1 = FindViewById<Button>(Resource.Id.ToevoegenContacten1);
            ToevoegenContacten1.Click += ToevoegenContacten1_Click;

        }

        private void OpslaanContacten1_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(KlantTelefoonActivity));
        }

        private void ToevoegenContacten1_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(KlantSnelstartActivity));
        }
    }
}