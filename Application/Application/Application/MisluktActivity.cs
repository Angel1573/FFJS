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
    class MisluktActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Mislukt);

            //maakt de Meer toevoegen button  
            var OpnieuwProberen = FindViewById<Button>(Resource.Id.OpnieuwProberen);
            OpnieuwProberen.Click += OpnieuwProberen_Click;
        }
        private void OpnieuwProberen_Click(object sender, System.EventArgs e)
        {
            //verwijst naar selectieactivity 
            StartActivity(typeof(SelectieActivity));
        }
    }
}