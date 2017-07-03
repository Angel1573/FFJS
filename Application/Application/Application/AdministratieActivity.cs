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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Application
{
    [Activity(Label = "AdministratieActivity")]
        public class AdministratieActivity : Activity
    {
       // de koppeling key, API key en authenticatekeys worden hier aangemaakt. 
        public static string koppelingkey = "MEFBQVRaQnNIcHpFbkhyYVZlQW5xN0tuOGduelowRURsS2ZFa0p5TFhqZEUvUEF6OEdaOFV5OHc2ZlQzVzl5S3gyeG9FaS9mY2o4M09WUkFsd0FRbFFBdldwdGlhYjR1dHcra0kzQ1pWSy9rRnh5RkFDT2NzWngxRm5NeG9oMWFyajQ5Y28xVmJUYVRqbzRLWWREcUxGeXFuaTR2ZU9ic3FZWmlCdWk4UVZnUzlFQUxTSVk1NjFHYkh4RmVYQ3BkdC82dTZxZ2NiYTVlQzg3VTA4dmUrcEo2NFFOS1hkeUZWV3dmL1o5dGVFZHdyTnF0UU1VOTI0VkxaRE5LWkMwNjpvM1lxT2UwSHlwMW1EWDJGMDVBcVllWnp5ZmU4OXBwN0J3eEpLWVRrOU9xOEhuWGo1M0dkSEVTYWlhZnR5UkVQcFhFcUNmdDlWN3pqSWpSUmZlZzcyelF3eXJoekxmM3NXTVFnY1F3U2s1LzZNd3BHYWJrWkI0NlN3MTFySDBoWkZDZ0NtS0NBaXo4QThMZGs0bWVJK2d0bjJjVGhRS1VGS2RKd3NCSW9RTkVJSVB6RmJ5Y1pLNmtMcVNiTVdGNndodkQyTllVS3VlZEdDc0N6dUo5SEdMNEU2TVRXdkd4aU9kUXdreEppZTJsN1pDNWxmbDZxZjE0bk85NnVBTmtM";
        public static string APIkey = "e8c56e54886f4005915425073f127183";
        public static string Authkey;
        //public static string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        //public static string locatie = Path.Combine(path, "koppelingskey.txt");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Administratie);

            //button verder aanmaken
            var Verder = FindViewById<Button>(Resource.Id.Verder);
            Verder.Click += Verder_Click;
        }
        private async void Verder_Click(object sender, System.EventArgs e)
        {
            /* dit is mogelijk code om de koppelingkey aan te kunnen passen. werk nog niet wegens pathing errors.
            if (System.IO.File.Exists(locatie))
            {
                using (TextReader reader = File.OpenText(locatie))
                {
                    koppelingkey = reader.ReadToEnd();
                }
            }
            else
            {
                System.IO.File.WriteAllText(locatie, this.InvKoppelingkey(sender, e));

                using (TextReader reader = File.OpenText(locatie))
                {
                    koppelingkey = reader.ReadToEnd();s
                }
            }  */

            // decrypt de koppelingkey van base64 naar UTF-8
            Decrypt(koppelingkey);
            //wacht tot getrelaties klaar is
            await Getrelaties();
            //ga naar selectieactivity
            StartActivity(typeof(SelectieActivity));    
        }


        //Invullen van koppelingkey in de textbox. wordt pmomenteel niet gebruikt.
        //public string InvKoppelingkey(object sender, EventArgs e)  
        //{
        //    return sender.ToString();         
        //}

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

        public static async Task<string> Getrelaties()
        {
            //opent nieuwe httpclient
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            string result = "";
            //wacht tot token klaar is
            await Token();
            //zet authkey om naar het goede format
            Authkey = "Bearer " + Authkey;


            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIkey);
            client.DefaultRequestHeaders.Add("Authorization", Authkey);

            var uri = "https://b2bapi.snelstart.nl/v1/relaties" + queryString;

            //Get request
            using (var response = await client.GetAsync(uri))
            {
                using (HttpContent content = response.Content)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        //wacht op volledig respons
                        result = await content.ReadAsStringAsync();
                    }
                    return result;
                }
            }
        }

    }
}