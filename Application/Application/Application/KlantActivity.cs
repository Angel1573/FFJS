﻿using System;
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

using Xamarin.Forms;


namespace Application
{
    [Activity(Label = "KlantActivity")]
    public class KlantActivity : Activity
    {
        //definieert de lijst met personen en de listview.
        private List<Person> mItem;
        private Android.Widget.ListView MListView;
        public static string text;


        protected async override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Klant);
            //maakt de listview aan
            MListView = FindViewById<Android.Widget.ListView>(Resource.Id.MyListView);


            var tosplit = await AdministratieActivity.Getrelaties();

            dynamic obj = JArray.Parse(tosplit);
            mItem = new List<Person>();

            // kijk naar elk item in obj
            foreach (JObject item in obj)
            {   // check of het een klant of leverancier is
                if (item.GetValue("relatiesoort").ToString().Contains("Klant"))
                {
                    //als de relatiesoort eigen bevat, skip deze.
                    if (item.GetValue("relatiesoort").ToString().Contains("Eigen"))
                    {
                        continue;
                    }
                    else
                    {
                        //pakt de klantcode en first name
                        string kcode = item.GetValue("relatiecode").ToString();
                        string firstName = item.GetValue("naam").ToString();

                        // Zet alles in de Listview
                        mItem.Add(new Person() { relatiecodenaam = kcode, contactnaam = firstName });
                        
                    }

                }
            }

            //maakt een adapter voor de listview met de items er in
            MylistViewAdapter adapter = new MylistViewAdapter(this, mItem);
            MListView.Adapter = adapter;
            MListView.ChoiceMode = ChoiceMode.Multiple;

            //maakt de buttons opslaancontacten en toevoegen contacten
            var OpslaanContacten1 = FindViewById<Android.Widget.Button>(Resource.Id.OpslaanContacten1);
            OpslaanContacten1.Click += OpslaanContacten1_Click;

            var ToevoegenContacten1 = FindViewById<Android.Widget.Button>(Resource.Id.ToevoegenContacten1);
            ToevoegenContacten1.Click += ToevoegenContacten1_Click;

        }

        public void OpslaanContacten1_Click(object sender, System.EventArgs e)
        {
            text = listselected();
            //wanneer er geklikt wordt, ga naar klant telefoonactivity
            StartActivity(typeof(KlantTelefoonActivity));
          
        }

        private void ToevoegenContacten1_Click(object sender, System.EventArgs e)
        {
            //wanneer er geklikt wordt, ga naar klant snelstart activity
            StartActivity(typeof(KlantSnelstartActivity));
        }
      
        public string listselected()
        {
            var arr = FindViewById<Android.Widget.ListView>(Resource.Id.MyListView).CheckedItemPositions;
            var data = new StringBuilder();

            for (var i = 0; i < arr.Size(); i++)
            {
                data.AppendLine(string.Format("{0}, {1}", arr.KeyAt(i), arr.ValueAt(i)));
            }
            text = data.ToString();

            return text;
        }


    }
}