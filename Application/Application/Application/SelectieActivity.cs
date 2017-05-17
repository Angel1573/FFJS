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
    [Activity(Label = "SelectieActivity")]
    public class SelectieActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Selectie);
            // Create your application here
            var Klant = FindViewById<Button>(Resource.Id.Klant);
            Klant.Click += Klant_Click;

            var Leverancier = FindViewById<Button>(Resource.Id.Leverancier);
            Leverancier.Click += Leverancier_Click;
        }
        private void Klant_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(KlantActivity));
        }

        private void Leverancier_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(LeverancierActivity));
    }
   }
  }

