using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using Xamarin.Forms;
using Xamarin;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace Application
{
    public class Checkbox : View
    {
        /// <summary>
        /// The checked state property.
        /// </summary>
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create("Checked",
                typeof(bool),
                typeof(CheckBox),
                false, BindingMode.TwoWay, propertyChanged: OnCheckedPropertyChanged);


        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        public bool Checked
        {
            get
            {
                return (bool)GetValue(CheckedProperty);
            }

            set
            {
                if (this.Checked != value)
                {
                    this.SetValue(CheckedProperty, value);
                    //this.CheckedChanged.Invoke(this, new EventArgs<bool>(value));
                }
            }
        }

        /// <summary>
        /// Called when [checked property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">if set to <c>true</c> [oldvalue].</param>
        /// <param name="newvalue">if set to <c>true</c> [newvalue].</param>
        private static void OnCheckedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkBox = (Checkbox) bindable;
            checkBox.Checked = (bool)newvalue;
        }
    }
}