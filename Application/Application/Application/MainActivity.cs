using Android.App;
using Android.Widget;
using Android.OS;

namespace Application
{
    [Activity(Label = "Application", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var Inloggen = FindViewById<Button>(Resource.Id.Inloggen);
            Inloggen.Click += Inloggen_Click;
        }
        private void Inloggen_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(AdministratieActivity));
        }
    }
}

