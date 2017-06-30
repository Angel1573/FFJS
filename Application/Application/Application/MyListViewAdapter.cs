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
        private List<Person> MItems;
        private Context MContext;

        public MylistViewAdapter(Context context, List<Person> items)
        {
            MItems = items;
            MContext = context;
        }

        public override int Count
        {
            get { return MItems.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override Person this[int position]
        {
            get { return MItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(MContext).Inflate(Resource.Layout.Listview_row, null, false);
            }
            TextView txtname = row.FindViewById<TextView>(Resource.Id.txtname);
            txtname.Text = MItems[position].txtname;
            TextView txtNaam = row.FindViewById<TextView>(Resource.Id.txtNaam);
            txtNaam.Text = MItems[position].txtnaam;


            return row;
        }
    }
}