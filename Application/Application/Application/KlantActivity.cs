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

namespace Application
{
    [Activity(Label = "KlantActivity")]
    public class KlantActivity : Activity
    {
        //definieert de lijst met personen en de listview.
        private List<Person> mItem;
        private ListView MListView;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Klant);
            //maakt de listview aan
            MListView = FindViewById<ListView>(Resource.Id.MyListView);


            var tosplit = await AdministratieActivity.Getrelaties();

            dynamic obj = JArray.Parse(tosplit);
            mItem = new List<Person>();

            // kijk naar elk item in obj
            foreach (JObject item in obj)
            {   // check of het een klant of leverancier is
                if (item.GetValue("relatiesoort").ToString().Contains("Klant"))
                {

                    //pakt de klantcode en first name
                    string kcode = item.GetValue("relatiecode").ToString();
                    string firstName = item.GetValue("naam").ToString();

                    // Zet alles in de Listview
                    mItem.Add(new Person() { relatiecodenaam = kcode, contactnaam = firstName });

                }
            }

            //maakt een adapter voor de listview met de items er in
            MylistViewAdapter adapter = new MylistViewAdapter(this, mItem);
            MListView.Adapter = adapter;

            //maakt de buttons opslaancontacten en toevoegen contacten
            var OpslaanContacten1 = FindViewById<Button>(Resource.Id.OpslaanContacten1);
            OpslaanContacten1.Click += OpslaanContacten1_Click;

            var ToevoegenContacten1 = FindViewById<Button>(Resource.Id.ToevoegenContacten1);
            ToevoegenContacten1.Click += ToevoegenContacten1_Click;

        }

        private void OpslaanContacten1_Click(object sender, System.EventArgs e)
        {
            //wanneer er geklikt wordt, ga naar klant telefoonactivity
            StartActivity(typeof(KlantTelefoonActivity));
        }

        private void ToevoegenContacten1_Click(object sender, System.EventArgs e)
        {
            //wanneer er geklikt wordt, ga naar klant snelstart activity
            StartActivity(typeof(KlantSnelstartActivity));
        }
    }
}