using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
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

namespace Application
{
    [Activity(Label = "KlantSnelstartActivity")]
    public class KlantSnelstartActivity : Activity
    {
        public static string koppelingkey = AdministratieActivity.koppelingkey;
        public static string APIkey = AdministratieActivity.APIkey;
        public static string Authkey;
        public static bool gelukt;
        public static List<TPerson> clist;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.KlantSnelstart);

            //maakt de synchroniseer knop
            var Synchroniseer1 = FindViewById<Button>(Resource.Id.Synchroniseer1);
            Synchroniseer1.Click += Synchroniseer1_Click;
        }
        private async void Synchroniseer1_Click(object sender, System.EventArgs e)
        {
            // gaat naar het Gelukt scherm wanneer er geklikt is.
            await Acontacts();
            await Postrelaties();

            if (gelukt == true)
            {
                StartActivity(typeof(GeluktActivity));
            }
            else
            {
                StartActivity(typeof(MisluktActivity));
            }
        }

        public static string Decrypt(string koppelingkey)
        {
            //decrypt de koppelingkey van Base64 naar UTF8
            string decodedkey;
            decodedkey = Encoding.UTF8.GetString(Convert.FromBase64String(koppelingkey));
            return decodedkey;
        }

        public static async Task<string> Token()
        {   // maak een nieuwe http client aan
            using (HttpClient client = new HttpClient())
            {
                // locatie van de authenticatie server
                var tokenEndpoint = @"https://auth.snelstart.nl/b2b/token";

                //maakt een lege token string aan
                string token = "";

                //decodeer de koppeling key en splits hem naar username en password
                string password;
                string username;
                string decoded = Decrypt(koppelingkey);

                var splitted = decoded.Split(':');
                username = splitted[0];
                password = splitted[1];


                //de body van de request. Deze koppelt de elementen aan elkaar (de grant type = password, username = de gesplitte username, password = het gesplitte password.           
                var body = new List<KeyValuePair<string, string>>();
                body.Add(new KeyValuePair<string, string>("grant_type", "password"));
                body.Add(new KeyValuePair<string, string>("username", Convert.ToString(username)));
                body.Add(new KeyValuePair<string, string>("password", Convert.ToString(password)));

                // de request zelf. 
                using (var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(body)))
                {
                    if (response.IsSuccessStatusCode)
                    {   //bij succesvolle response, haal de access token op.
                        var jsonresult = JObject.Parse(await response.Content.ReadAsStringAsync());
                        token = (string)jsonresult["access_token"];

                        //print de response naar een txt file zodat wij deze kunnen copy pasten 
                        Authkey = token;
                    }
                    return token;
                }
            }
        }

        public async Task<List<TPerson>> Acontacts()
        {
            clist = new List<TPerson>();

            var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;

            string[] projection = { 
                ContactsContract.Contacts.InterfaceConsts.DisplayName, ContactsContract.CommonDataKinds.Phone.Number, ContactsContract.CommonDataKinds.Email.InterfaceConsts.Data};

            var cursor = ManagedQuery(uri, projection, null, null, ContactsContract.Contacts.InterfaceConsts.Id);

            if (cursor.MoveToFirst())
            {
                do
                {   /*"Contact ID: {0}, Contact Name: {1}, Telefoonnummer {2}",*/
                    clist.Add(new TPerson()
                    {
                        contactnaam = cursor.GetString(cursor.GetColumnIndex(projection[0])),
                        telefoonnummer = cursor.GetString(cursor.GetColumnIndex(projection[1])),
                        email = cursor.GetString(cursor.GetColumnIndex(projection[2]))
                    });

                } while (cursor.MoveToNext());
            }
            return clist;
        }

        public static async Task<string> Postrelaties()
        {
            using (HttpClient client = new HttpClient())
            {
                // locatie van de authenticatie server
                var PostEndpoint = @"https://b2bapi.snelstart.nl/v1/relaties?";
                var Post = "";
                await Token();


                //decodeer de koppeling key en splits hem naar username en password
                string password;
                string username;
                string decoded = Decrypt(koppelingkey);

                var splitted = decoded.Split(':');
                password = splitted[1];
                username = splitted[0];

                Authkey = "Bearer " + Authkey;

                // Request headers
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIkey);
                client.DefaultRequestHeaders.Add("Authorization", Authkey);

                string naam;
                string tnummer;
                string email;
                var body = new List<KeyValuePair<string, string>>();

                foreach (var item in clist)
                {
                    naam = item.contactnaam;
                    tnummer = item.telefoonnummer;
                    email = item.email;

                    //de body van de request.
                    body.Add(new KeyValuePair<string, string>("naam", naam));
                    body.Add(new KeyValuePair<string, string>("telefoon", tnummer));
                    body.Add(new KeyValuePair<string, string>("email", email));
                    body.Add(new KeyValuePair<string, string>("relatiesoort", "Klant"));
                }

                // de request zelf. 
                using (var response = await client.PostAsync(PostEndpoint, new FormUrlEncodedContent(body)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        gelukt = true;
                    }
                    else
                    {
                        gelukt = false;
                    }

                } return Post;
            }
        }
    }
}