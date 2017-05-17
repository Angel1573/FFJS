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
    [Activity(Label = "KlantTelefoonActivity")]
    public class KlantTelefoonActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.KlantTelefoon);
            // Create your application here

            var Synchroniseer2 = FindViewById<Button>(Resource.Id.Synchroniseer2);
            Synchroniseer2.Click += Synchroniseer2_Click;
        }
        private void Synchroniseer2_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(GeluktActivity));
        }
    }
}