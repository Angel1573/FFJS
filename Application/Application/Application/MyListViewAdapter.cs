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
    class MylistViewAdapter : BaseAdapter<Person>
    {
        //maakt lijst met personen en context aan
        private List<Person> MItems;
        private Context MContext;

        public MylistViewAdapter(Context context, List<Person> items)
        {
            //stelt de lijst Mitems gelijk aan items en Mcontext aan de context
            MItems = items;
            MContext = context;
        }

        public override int Count
        {
            get { return MItems.Count; }
        }
        public override long GetItemId(int position)
        {
            //geeft de index terug
            return position;
        }

        public override Person this[int position]
        {
            //pakt het huidige persoon
            get { return MItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //vult de rows wanneer deze leeg zijn met de info die aangeleverd wordt
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(MContext).Inflate(Resource.Layout.Listview_row, null, false);
            }
            TextView relatiecodenaam = row.FindViewById<TextView>(Resource.Id.relatiecodenaam);
            relatiecodenaam.Text = MItems[position].relatiecodenaam;
            TextView contactnaam = row.FindViewById<TextView>(Resource.Id.contactnaam);
            contactnaam.Text = MItems[position].contactnaam;

            return row;
        }
    }
}