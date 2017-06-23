using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.IEnumerable;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Application
{
    class Contacten
    {
        public int Relatiecode { get; set; }
        public string Naam { get; set; }
        public int Telefoonnummer { get; set; }
        public int MobielTelefoonnummer { get; set; }
        public string Emailadres { get; set; }
    }
}