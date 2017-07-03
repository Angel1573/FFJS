using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Android.Provider;
using Xamarin.Contacts;



namespace Application
{
    [Activity(Label = "SelectieActivity")]
    public class SelectieActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Selectie);

            //maakt knooppen klant en leverancier aan
            var Klant = FindViewById<Android.Widget.Button>(Resource.Id.Klant);
            Klant.Click += Klant_Click;

            var Leverancier = FindViewById<Android.Widget.Button>(Resource.Id.Leverancier);
            Leverancier.Click += Leverancier_Click;     
        }

        private void Klant_Click(object sender, System.EventArgs e)
        {
            //gaat naar klantactivity wanneer er op klant geklikt wordt
            StartActivity(typeof(KlantActivity));
        }

        private void Leverancier_Click(object sender, System.EventArgs e)
        {
            //gaat naar leverancieractivity wanneer er op leverancier geklikt wordt
            StartActivity(typeof(LeverancierActivity));
        }
    }
}
    



