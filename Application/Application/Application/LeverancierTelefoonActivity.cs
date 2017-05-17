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
    [Activity(Label = "LeverancierTelefoonActivity")]
    public class LeverancierTelefoonActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LeverancierTelefoon);
            // Create your application here

            var Synchroniseer4 = FindViewById<Button>(Resource.Id.Synchroniseer4);
            Synchroniseer4.Click += Synchroniseer4_Click;
        }
        private void Synchroniseer4_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(GeluktActivity));
        }
    }
}