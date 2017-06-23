using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Application
{
    [Activity(Label = "KlantActivity")]
    public class KlantActivity : Activity
    {
        public ContactViewModel ContactViewModel { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ContactViewModel = new ContactViewModel();
            SetContentView(Resource.Layout.Klant);
            // Create your application here
            var OpslaanContacten1 = FindViewById<Button>(Resource.Id.OpslaanContacten1);
            OpslaanContacten1.Click += OpslaanContacten1_Click;

            var ToevoegenContacten1 = FindViewById<Button>(Resource.Id.ToevoegenContacten1);
            ToevoegenContacten1.Click += ToevoegenContacten1_Click;
        }
        public void OpslaanContacten1_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(KlantTelefoonActivity));
        }

        public void ToevoegenContacten1_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(KlantSnelstartActivity));
        }
    }
}