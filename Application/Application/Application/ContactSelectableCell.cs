using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Application
{
    class ContactSelectableCell : ViewCell
    {
        public static readonly BindableProperty RelatiecodeProperty =
          BindableProperty.Create("relatiecode", typeof(int), typeof(ContactSelectableCell), 1);

        public static readonly BindableProperty NameProperty =
          BindableProperty.Create("Naam", typeof(string), typeof(ContactSelectableCell), "Frank");

        public int rcode
        {
            get { return (int)GetValue(RelatiecodeProperty); }
            set { SetValue(RelatiecodeProperty, value); }
        }

        public string name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        Label lrcode, lname;


        public ContactSelectableCell()
        {
            lrcode = new Label { HorizontalOptions = LayoutOptions.StartAndExpand };
            lname = new Label { HorizontalOptions = LayoutOptions.EndAndExpand }; ;

            Grid infoLayout = new Grid
            {
                ColumnDefinitions = 
                {
                    new ColumnDefinition { Width = new GridLength(3,GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
                },
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            infoLayout.Children.Add(lrcode, 0, 0);
            infoLayout.Children.Add(lname, 1, 0);

            var cellWrapper = new Grid
            {
                Padding = 10,
                ColumnDefinitions = 
                {
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
                }
            };

            //var cb = new CheckBox();
            //cellWrapper.Children.Add(cb, 0, 0);
            var sw = new Xamarin.Forms.Switch();
            sw.SetBinding(Xamarin.Forms.Switch.IsToggledProperty, "IsSelected");

            cellWrapper.Children.Add(sw, 0, 0);
            cellWrapper.Children.Add(infoLayout, 1, 0);

            View = cellWrapper;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                lrcode.Text = rcode.ToString();
                lname.Text = name;
            }
        }
    }
}