using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
            Getrelaties();

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

        public static async Task<string> Getrelaties()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            string result = "";
            await MainActivity.Token();
            var Authkey = "Bearer " + MainActivity.Authkey;
            var APIkey = MainActivity.APIkey;

            Console.WriteLine(Authkey);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIkey);
            client.DefaultRequestHeaders.Add("Authorization", Authkey);

            var uri = "https://b2bapi.snelstart.nl/v1/relaties" + queryString;

            using (var response = await client.GetAsync(uri))
            {
                using (HttpContent content = response.Content)
                {
                    Console.WriteLine(response);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await content.ReadAsStringAsync();
                        System.IO.File.WriteAllText(@"C:\Users\Freddy\Desktop\Minor Systeemontwikkeling\Response\Getresponse.txt", Convert.ToString(result));
                    }
                    return result;
                }
            }
        }

        public static async Task<string> Splitklant()
        {
            var tosplit = await Getrelaties();

            JObject rss = JObject.Parse(tosplit);



        }
    }
}


