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
    [Activity(Label = "AdministratieActivity")]
    public class AdministratieActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Administratie);
            // Create your application here
            var Verder = FindViewById<Button>(Resource.Id.Verder);
            Verder.Click += Verder_Click;
        }
        private void Verder_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(SelectieActivity));
        }
    }
}