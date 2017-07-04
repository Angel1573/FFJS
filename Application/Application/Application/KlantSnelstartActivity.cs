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

namespace Application
{
    [Activity(Label = "KlantSnelstartActivity")]
    public class KlantSnelstartActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.KlantSnelstart);


            //maakt de synchroniseer knop
            var Synchroniseer1 = FindViewById<Button>(Resource.Id.Synchroniseer1);
            Synchroniseer1.Click += Synchroniseer1_Click;
        }
        private void Synchroniseer1_Click(object sender, System.EventArgs e)
        {
            // gaat naar het Gelukt scherm wanneer er geklikt is.
            StartActivity(typeof(GeluktActivity));
        }
    }
}