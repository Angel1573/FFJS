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
    [Activity(Label = "KlantActivity")]
    public class KlantActivity : Activity
    {
        private List<Person> mItem;
        private ListView MListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Klant);
            // Create your application here
            //SetContentView(Resource.Layout.Main);
            MListView = FindViewById<ListView>(Resource.Id.MyListView);

            mItem = new List<Person>();
            mItem.Add(new Person() { txtname = "1", txtnaam = "Hindrik" });
            mItem.Add(new Person() { txtname = "2", txtnaam = "Frank" });
            mItem.Add(new Person() { txtname = "3", txtnaam = "jesse" });
            MylistViewAdapter adapter = new MylistViewAdapter(this, mItem);
            MListView.Adapter = adapter;

            var OpslaanContacten1 = FindViewById<Button>(Resource.Id.OpslaanContacten1);
            OpslaanContacten1.Click += OpslaanContacten1_Click;

            var ToevoegenContacten1 = FindViewById<Button>(Resource.Id.ToevoegenContacten1);
            ToevoegenContacten1.Click += ToevoegenContacten1_Click;
        }
        private void OpslaanContacten1_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(KlantTelefoonActivity));
        }

        private void ToevoegenContacten1_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(KlantSnelstartActivity));
        }
    }
}