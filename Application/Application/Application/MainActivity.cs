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
        public static string koppelingkey = "MEFBQVRaQnNIcHpFbkhyYVZlQW5xN0tuOGduelowRURsS2ZFa0p5TFhqZEUvUEF6OEdaOFV5OHc2ZlQzVzl5S3gyeG9FaS9mY2o4M09WUkFsd0FRbFFBdldwdGlhYjR1dHcra0kzQ1pWSy9rRnh5RkFDT2NzWngxRm5NeG9oMWFyajQ5Y28xVmJUYVRqbzRLWWREcUxGeXFuaTR2ZU9ic3FZWmlCdWk4UVZnUzlFQUxTSVk1NjFHYkh4RmVYQ3BkdC82dTZxZ2NiYTVlQzg3VTA4dmUrcEo2NFFOS1hkeUZWV3dmL1o5dGVFZHdyTnF0UU1VOTI0VkxaRE5LWkMwNjpvM1lxT2UwSHlwMW1EWDJGMDVBcVllWnp5ZmU4OXBwN0J3eEpLWVRrOU9xOEhuWGo1M0dkSEVTYWlhZnR5UkVQcFhFcUNmdDlWN3pqSWpSUmZlZzcyelF3eXJoekxmM3NXTVFnY1F3U2s1LzZNd3BHYWJrWkI0NlN3MTFySDBoWkZDZ0NtS0NBaXo4QThMZGs0bWVJK2d0bjJjVGhRS1VGS2RKd3NCSW9RTkVJSVB6RmJ5Y1pLNmtMcVNiTVdGNndodkQyTllVS3VlZEdDc0N6dUo5SEdMNEU2TVRXdkd4aU9kUXdreEppZTJsN1pDNWxmbDZxZjE0bk85NnVBTmtM";
        public static string APIkey = "e8c56e54886f4005915425073f127183";
        public static string Authkey;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var Inloggen = FindViewById<Button>(Resource.Id.Inloggen);
            Inloggen.Click += Inloggen_Click;

            Decrypt(koppelingkey);
            Token();
        }
        private void Inloggen_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(AdministratieActivity));
        }

        public static string Decrypt(string koppelingkey)
        {
            //decrypt de koppelingkey van Base64 naar UTF8
            string decodedkey;
            decodedkey = Encoding.UTF8.GetString(Convert.FromBase64String(koppelingkey));
            return decodedkey;
        }

        static async Task<string> Token()
        {   // maak een nieuwe http client aan
            using (HttpClient client = new HttpClient())
            {
                // locatie van de authenticatie server
                var tokenEndpoint = @"https://auth.snelstart.nl/b2b/token";
                var token = "";

                //decodeer de koppeling key en splits hem naar username en password
                string password;
                string username;
                string decoded = Decrypt(koppelingkey);

                var splitted = decoded.Split(':');
                password = splitted[1];
                username = splitted[0];

                //de body van de request.                
                var body = new List<KeyValuePair<string, string>>();
                body.Add(new KeyValuePair<string, string>("grant_type", "password"));
                body.Add(new KeyValuePair<string, string>("username", Convert.ToString(username)));
                body.Add(new KeyValuePair<string, string>("password", Convert.ToString(password)));

                // de request zelf. 
                using (var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(body)))
                {
                    System.IO.File.WriteAllText(@"C:\Users\Freddy\Desktop\Minor Systeemontwikkeling\Response\request.txt", Convert.ToString(response));

                    if (response.IsSuccessStatusCode)
                    {   //bij succesvolle response, haal de access token op.
                        var jsonresult = JObject.Parse(await response.Content.ReadAsStringAsync());
                        token = (string)jsonresult["access_token"];

                        //print de response naar een txt file zodat wij deze kunnen copy pasten
                        System.IO.File.WriteAllText(@"C:\Users\Freddy\Desktop\Minor Systeemontwikkeling\Response\tokenresponse.txt", Convert.ToString(token));
                        Authkey = token;

                    }
                    return token;
                }
            }
        }
    }
}

