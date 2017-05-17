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
    [Activity(Label = "LeverancierActivity")]
    public class LeverancierActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Leverancier);
            // Create your application here
            var OpslaanContacten2 = FindViewById<Button>(Resource.Id.OpslaanContacten2);
            OpslaanContacten2.Click += OpslaanContacten2_Click;

            var ToevoegenContacten2 = FindViewById<Button>(Resource.Id.ToevoegenContacten2);
            ToevoegenContacten2.Click += ToevoegenContacten2_Click;
        }

        private void OpslaanContacten2_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(LeverancierTelefoonActivity));
        }

        private void ToevoegenContacten2_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(LeverancierSnelstartActivity));
        }
    }
}