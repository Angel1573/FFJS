using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Application
{
    [Activity(Label = "Application", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var Inloggen = FindViewById<Button>(Resource.Id.Inloggen);
            Inloggen.Click += Inloggen_Click;
        }

        private void Inloggen_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(AdministratieActivity));
        }         
    }
}

