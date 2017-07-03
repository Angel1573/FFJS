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
    [Activity(Label = "LeverancierActivity")]
    public class LeverancierActivity : Activity
    {


        private List<Person> mItem;
        private ListView MListView;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Leverancier);
            // Create your application here

            MListView = FindViewById<ListView>(Resource.Id.MyListView);

            var tosplit = await AdministratieActivity.Getrelaties();

            dynamic obj = JArray.Parse(tosplit);
            mItem = new List<Person>();

            // kijk naar elk item in obj
            foreach (JObject item in obj)
            {   // check of het een klant of leverancier is
                if (item.GetValue("relatiesoort").ToString().Contains("Leverancier"))
                {

                    string kcode = item.GetValue("relatiecode").ToString();
                    string firstName = item.GetValue("naam").ToString();


                    // Zet alles in de Listview
                    mItem.Add(new Person() { txtname = kcode, txtnaam = firstName });
                }
            }

            MylistViewAdapter adapter = new MylistViewAdapter(this, mItem);
            MListView.Adapter = adapter;


            var OpslaanContacten2 = FindViewById<Button>(Resource.Id.OpslaanContacten2);
            OpslaanContacten2.Click += OpslaanContacten2_Click;

            var ToevoegenContacten2 = FindViewById<Button>(Resource.Id.ToevoegenContacten2);
            ToevoegenContacten2.Click += ToevoegenContacten2_Click;
        }

        private void OpslaanContacten2_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(LeverancierTelefoonActivity));
        }

        private void ToevoegenContacten2_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(LeverancierSnelstartActivity));
        }
    }
}