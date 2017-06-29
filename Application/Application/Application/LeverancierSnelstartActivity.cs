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
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LeverancierSnelstart);
            // Create your application here

            var Synchroniseer3 = FindViewById<Button>(Resource.Id.Synchroniseer3);
            Synchroniseer3.Click += Synchroniseer3_Click;
        }
        private void Synchroniseer3_Click(object sender, System.EventArgs e)
        {
            
            StartActivity(typeof(GeluktActivity));
        }

    }
}