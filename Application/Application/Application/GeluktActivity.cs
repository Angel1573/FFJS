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

namespace Application
{
    [Activity(Label = "GeluktActivity")]
    public class GeluktActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Gelukt);

            //maakt de Meer toevoegen button  
            var MeerToevoegen = FindViewById<Button>(Resource.Id.MeerToevoegen);
            MeerToevoegen.Click += MeerToevoegen_Click;
        }
        private void MeerToevoegen_Click(object sender, System.EventArgs e)
        {
            //verwijst naar selectieactivity 
            StartActivity(typeof(SelectieActivity));
        }
    }
}