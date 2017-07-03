using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android;

namespace Application
{
    [Activity(Label = "LeverancierSnelstartActivity")]
    public class LeverancierSnelstartActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LeverancierSnelstart);

            //maakt de synchroniseer knop
            var Synchroniseer3 = FindViewById<Button>(Resource.Id.Synchroniseer3);
            Synchroniseer3.Click += Synchroniseer3_Click;
        }
        private void Synchroniseer3_Click(object sender, System.EventArgs e)
        {
            // gaat naar het Gelukt scherm wanneer er geklikt is.
            StartActivity(typeof(GeluktActivity));
        }

    }
}