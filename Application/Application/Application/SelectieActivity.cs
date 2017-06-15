using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
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
            
            var Klant = FindViewById<Android.Widget.Button>(Resource.Id.Klant);
            Klant.Click += Klant_Click;

            var Leverancier = FindViewById<Android.Widget.Button>(Resource.Id.Leverancier);
            Leverancier.Click += Leverancier_Click;     
        }

        private void Klant_Click(object sender, System.EventArgs e)
        {
            Splitklant();
            StartActivity(typeof(KlantActivity));
        }

        private void Leverancier_Click(object sender, System.EventArgs e)
        {
            Splitleverancier();
            StartActivity(typeof(LeverancierActivity));
        }

        public static async Task<string> Getrelaties()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            string result = "";
            //wacht tot token klaar is
            await AdministratieActivity.Token();
            //zet authkey om naar het goede format
            var Authkey = "Bearer " + AdministratieActivity.Authkey;
            //APIkey uit administratiescherm halen
            var APIkey = AdministratieActivity.APIkey;

            Console.WriteLine(Authkey);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIkey);
            client.DefaultRequestHeaders.Add("Authorization", Authkey);

            var uri = "https://b2bapi.snelstart.nl/v1/relaties" + queryString;

            //Get request
            using (var response = await client.GetAsync(uri))
            {
                using (HttpContent content = response.Content)
                {
                    Console.WriteLine(response);
                    if (response.IsSuccessStatusCode)
                    {
                        //wacht op volledig respons
                        result = await content.ReadAsStringAsync();                        
                    }
                    return result;
                }
            }
        }

        public static Task DisplayAlert(String title, String message, String cancel);

        public static async void Splitklant()
        {
            //tosplit is de teruggave van de get
            var tosplit = Getrelaties().Result;

            if (tosplit.Length >= 2)
            {
                //parse de respons naar een JArray
                dynamic obj = JArray.Parse(tosplit);

                try
                {   // kijk naar elk item in obj
                    foreach (JObject item in obj)
                    {   // check of het een klant of leverancier is
                        if (item.GetValue("relatiesoort").ToString().Contains("Klant"))
                        {
                            string knaam = item.GetValue("naam").ToString();
                            string ktnummer = item.GetValue("telefoon").ToString();
                            string kmnummer = item.GetValue("mobieleTelefoon").ToString();
                            string kemail = item.GetValue("email").ToString();

                            Console.WriteLine("naam = " + knaam + " & tnummer = " + ktnummer + " & mnummer = " + kmnummer + " & email = " + kemail);
                        }                        
                    }
                }
                catch (Exception ex)
                {
                    
                }                         
            }             
        }

        public static async void Splitleverancier()
        {
            //tosplit is de teruggave van de get
            var tosplit = Getrelaties().Result;

            if (tosplit.Length >= 2)
            {
                //parse de respons naar een JArray
                dynamic obj = JArray.Parse(tosplit);

                try
                {   // kijk naar elk item in obj
                    foreach (JObject item in obj)
                    {   // check of het een klant of leverancier is
                        if (item.GetValue("relatiesoort").ToString().Contains("Leverancier"))
                        {
                            string lnaam = item.GetValue("naam").ToString();
                            string ltnummer = item.GetValue("telefoon").ToString();
                            string lmnummer = item.GetValue("mobieleTelefoon").ToString();
                            string lemail = item.GetValue("email").ToString();

                            Console.WriteLine("naam = " + lnaam + " & tnummer = " + ltnummer + " & mnummer = " + lmnummer + " & email = " + lemail);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("error again:", ex.ToString(), "cancel");
                }
            }
        }
    }
}
    



