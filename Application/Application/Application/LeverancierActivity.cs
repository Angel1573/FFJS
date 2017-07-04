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
using Newtonsoft.Json.Linq;
using System.Windows;

namespace Application
{
    [Activity(Label = "LeverancierActivity")]
    public class LeverancierActivity : Activity
    {
        //definieert de lijst met personen en de listview.
        private List<Person> mItem;
        private ListView MListView;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Leverancier);
            //maakt de listview aan
            MListView = FindViewById<ListView>(Resource.Id.MyListView);
            
            //een variabele met de getrelaties lijst als waarde
            var tosplit = await AdministratieActivity.Getrelaties();
            
            //parse de respons naar een JArray
            dynamic obj = JArray.Parse(tosplit);
            //maakt een nieuwe mitem aan.
            mItem = new List<Person>();
            
            // kijk naar elk item in obj
            foreach (JObject item in obj)
            {   // check of het een klant of leverancier is
                if (item.GetValue("relatiesoort").ToString().Contains("Leverancier"))
                {
                    if (item.GetValue("relatiesoort").ToString().Contains("Eigen"))
                    {
                        continue;
                    }
                    else
                    {
                        //haalt kcode en firstname uit tosplit
                        string kcode = item.GetValue("relatiecode").ToString();
                        string firstName = item.GetValue("naam").ToString();

                        if (Convert.ToInt32(kcode) < 0)
                        {
                            continue;
                        }
                        else
                        {
                            // Zet alles in de Listview
                            mItem.Add(new Person() { relatiecodenaam = kcode, contactnaam = firstName });
                        }
                    }

                }
            }
            
            //maakt een adapter voor de listview met de items er in
            MylistViewAdapter adapter = new MylistViewAdapter(this, mItem);
            MListView.Adapter = adapter;

            //maakt de buttons opslaancontacten en toevoegen contacten
            var OpslaanContacten2 = FindViewById<Button>(Resource.Id.OpslaanContacten2);
            OpslaanContacten2.Click += OpslaanContacten2_Click;

            var ToevoegenContacten2 = FindViewById<Button>(Resource.Id.ToevoegenContacten2);
            ToevoegenContacten2.Click += ToevoegenContacten2_Click;
        }

        private void OpslaanContacten2_Click(object sender, System.EventArgs e)
        {
            //wanneer er geklikt wordt, ga naar leveracnier telefoonactivity
            StartActivity(typeof(LeverancierTelefoonActivity));
        }

        private void ToevoegenContacten2_Click(object sender, System.EventArgs e)
        {
            //wanneer er geklikt wordt, ga naar leverancier snelstart activity
            StartActivity(typeof(LeverancierSnelstartActivity));
        }
    }
}