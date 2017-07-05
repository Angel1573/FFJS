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
using Xamarin.Forms;

namespace Application
{
    [Activity(Label = "Application", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            //maak een knop om in te loggen
            var Inloggen = FindViewById<Android.Widget.Button>(Resource.Id.Inloggen);
            Inloggen.Click += Inloggen_Click;
        }

        private void Inloggen_Click(object sender, System.EventArgs e)
        {
            // wanneer op de knop gedrukt wordt ga je naar administratieactivity
            StartActivity(typeof(AdministratieActivity));
        }         
    }
}

