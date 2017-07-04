using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android;


namespace Application
{
    [Activity(Label = "KlantTelefoonActivity")]
    public class KlantTelefoonActivity : Activity
    {       
        //definieert de lijst met personen en de listview.
        private List<Person> mItem;
        private ListView MListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Op aanmaak van deze pagina de layout aanmaken vanuit de axml
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.KlantTelefoon);
            //maakt de listview aan
            MListView = FindViewById<ListView>(Resource.Id.MyListView);

            //maakt een nieuwe mitem aan.
            mItem = new List<Person>();
            

            //maakt de Synchroniseer button  
            var Synchroniseer2 = FindViewById<Button>(Resource.Id.Synchroniseer2);
            Synchroniseer2.Click += Synchroniseer2_Click;

            //zorgt dat newcontacts aangeroepen wordt zodra er op synchroniseer geklikt wordt
            Synchroniseer2.Click += delegate {
                Newcontacts();
            };
        }
        private void Synchroniseer2_Click(object sender, System.EventArgs e)
        {
            //gaat naar gelukt scherm zodra er geklikt is op synchroniseer.
            StartActivity(typeof(GeluktActivity));
        }

        public async void Newcontacts()
        {
            //een variabele met de getrelaties lijst als waarde
            var tosplit = await AdministratieActivity.Getrelaties();
            
            //definitie van benodigde strings voor de verschillende waarden. 
            string firstName;
            string phone;
            string mobilephone;
            string email;  
            string relatiesoort;

            //doe alleen iets als tosplit groter is dan of gelijk is aan 2
            if (tosplit.Length >= 2)
            {
                //parse de respons naar een JArray
                dynamic obj = JArray.Parse(tosplit);
          
                // kijk naar elk item in obj
                foreach (JObject item in obj)
                {   // check of het een klant of leverancier is
                    if (item.GetValue("relatiesoort").ToString().Contains("Klant"))
                    {   
                        //pakt de benodigde informatie uit tosplit.
                        relatiesoort = item.GetValue("relatiesoort").ToString();
                        firstName = item.GetValue("naam").ToString();
                        phone = item.GetValue("telefoon").ToString();
                        mobilephone = item.GetValue("mobieleTelefoon").ToString();
                        email = item.GetValue("email").ToString();

                        //aanmaken lijst van contentproviders
                        List<ContentProviderOperation> ops = new List<ContentProviderOperation>();

                        //voegt de content to aan de builder.
                        ContentProviderOperation.Builder builder =
                            ContentProviderOperation.NewInsert(ContactsContract.RawContacts.ContentUri);
                        builder.WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountType, null);
                        builder.WithValue(ContactsContract.RawContacts.InterfaceConsts.AccountName, null);
                        ops.Add(builder.Build());

                        //Naam
                        builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
                        builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0);
                        builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
                                          ContactsContract.CommonDataKinds.StructuredName.ContentItemType);
                        //builder.WithValue(ContactsContract.CommonDataKinds.StructuredName.FamilyName, lastName);
                        builder.WithValue(ContactsContract.CommonDataKinds.StructuredName.GivenName, firstName);
                        ops.Add(builder.Build());

                        //Telefoonnummer
                        builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
                        builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0);
                        builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
                                          ContactsContract.CommonDataKinds.Phone.ContentItemType);
                        builder.WithValue(ContactsContract.CommonDataKinds.Phone.Number, phone);
                        builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Type,
                                          ContactsContract.CommonDataKinds.Phone.InterfaceConsts.TypeCustom);
                        builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Label, "Werk");
                        builder.WithValue(ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Label, relatiesoort);
                        ops.Add(builder.Build());

                        //Email
                        builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
                        builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0);
                        builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
                                          ContactsContract.CommonDataKinds.Email.ContentItemType);
                        builder.WithValue(ContactsContract.CommonDataKinds.Email.InterfaceConsts.Data, email);
                        builder.WithValue(ContactsContract.CommonDataKinds.Email.InterfaceConsts.Type,
                                          ContactsContract.CommonDataKinds.Email.InterfaceConsts.TypeCustom);
                        builder.WithValue(ContactsContract.CommonDataKinds.Email.InterfaceConsts.Label, "Werk");
                        builder.WithValue(ContactsContract.CommonDataKinds.Email.InterfaceConsts.Label, relatiesoort);
                        ops.Add(builder.Build());

                        //contentprovider array, en stelt deze gelijk aan de contactscontract met de lijst Ops
                        ContentProviderResult[] res;
                        res = ContentResolver.ApplyBatch(ContactsContract.Authority, ops);
                    }
                }
            }

            #region uitcommented

            //string lkw;
            //string lastName;
            //var Straat = "nieuwelaan 55";
            //var Postcode = "9642EP";
            //var Stad = "Veendam";
            //var contact_saved_message = "gelukt";
            //var contact_not_saved_message = "niet gelukt";

            ////Bedrijf
            //builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
            //builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0);
            //builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
            //                  ContactsContract.CommonDataKinds.Organization.ContentItemType);
            //builder.WithValue(ContactsContract.CommonDataKinds.Organization.InterfaceConsts.Data, lkw + "(" + Relatiesoort + ")");
            //builder.WithValue(ContactsContract.CommonDataKinds.Organization.InterfaceConsts.Type,
            //                  ContactsContract.CommonDataKinds.Organization.InterfaceConsts.TypeCustom);
            //builder.WithValue(ContactsContract.CommonDataKinds.Organization.InterfaceConsts.Label, "Werk");
            //ops.Add(builder.Build());

            //Adres
            //builder = ContentProviderOperation.NewInsert(ContactsContract.Data.ContentUri);
            //builder.WithValueBackReference(ContactsContract.Data.InterfaceConsts.RawContactId, 0);
            //builder.WithValue(ContactsContract.Data.InterfaceConsts.Mimetype,
            //                  ContactsContract.CommonDataKinds.StructuredPostal.ContentItemType);
            //builder.WithValue(ContactsContract.CommonDataKinds.StructuredPostal.Street, Straat);
            //builder.WithValue(ContactsContract.CommonDataKinds.StructuredPostal.Postcode, Postcode);
            //builder.WithValue(ContactsContract.CommonDataKinds.StructuredPostal.City, Stad);
            //ops.Add(builder.Build());
            #endregion

        }

    }
}