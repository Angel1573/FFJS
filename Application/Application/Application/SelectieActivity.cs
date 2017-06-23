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
            //Splitklant();
            StartActivity(typeof(KlantActivity));
        }

        private void Leverancier_Click(object sender, System.EventArgs e)
        {
            //Splitleverancier();
            StartActivity(typeof(LeverancierActivity));
        }

         public static List<Contacten> Splitklant()
        {
            //tosplit is de teruggave van de get
            var tosplit = AdministratieActivity.Getrelaties().Result;
            var klantenlijst = new List<Contacten>();

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
                            string kcode = item.GetValue("relatiecode").ToString();
                            string knaam = item.GetValue("naam").ToString();
                            string ktnummer = item.GetValue("telefoon").ToString();
                            string kmnummer = item.GetValue("mobieleTelefoon").ToString();
                            string kemail = item.GetValue("email").ToString();

                            klantenlijst.Add(new Contacten {Relatiecode = Convert.ToInt32(kcode), Naam = knaam, Telefoonnummer = Convert.ToInt32(ktnummer), MobielTelefoonnummer = Convert.ToInt32(kmnummer), Emailadres = kemail});
                        }                        
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error: " + ex );
                }                
                      
            }   return klantenlijst;            
        }

        public static List<Contacten> Splitleverancier()
        {
            //tosplit is de teruggave van de get
            var tosplit = AdministratieActivity.Getrelaties().Result;
            var leverancierlijst = new List<Contacten>();

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
                            string lcode = item.GetValue("relatiecode").ToString();
                            string lnaam = item.GetValue("naam").ToString();
                            string ltnummer = item.GetValue("telefoon").ToString();
                            string lmnummer = item.GetValue("mobieleTelefoon").ToString();
                            string lemail = item.GetValue("email").ToString();

                            leverancierlijst.Add(new Contacten { Relatiecode = Convert.ToInt32(lcode), Naam = lnaam, Telefoonnummer = Convert.ToInt32(ltnummer), MobielTelefoonnummer = Convert.ToInt32(lmnummer), Emailadres = lemail });
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error: " + ex);
                }
            } return leverancierlijst;
        }
    }
}
    



