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
        List<string> ListData;
        ListView dataListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ListData = new List<string>();
            SetContentView(Resource.Layout.KlantTelefoon);
            // Create your application here
<<<<<<< HEAD
            ListData.Add("Jesse");
            ListData.Add("Freddy");
            ListData.Add("Frank");

            dataListView = FindViewById<ListView>(Resource.Id.KlantInfoView2);

            ArrayAdapter<string> listAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ListData);

            dataListView.Adapter = listAdapter;

=======
>>>>>>> 6b545e7aa8d5464f77ef82f547c4f1d0b2ea249d

            var Synchroniseer2 = FindViewById<Button>(Resource.Id.Synchroniseer2);
            Synchroniseer2.Click += Synchroniseer2_Click;

            Synchroniseer2.Click += delegate {
                Newcontacts();
            };
        }
        private void Synchroniseer2_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(GeluktActivity));
        }

        public void Newcontacts()
        {
            var tosplit = AdministratieActivity.Getrelaties().Result;
            List<ContentProviderOperation> ops = new List<ContentProviderOperation>();
           
            string firstName;
            string phone;
            string mobilephone;
            string email; 
            string lkw;
            string relatiesoort;
            //string lastName;
            
            //var Straat = "nieuwelaan 55";
            //var Postcode = "9642EP";
            //var Stad = "Veendam";
            //var contact_saved_message = "gelukt";
            //var contact_not_saved_message = "niet gelukt";

            if (tosplit.Length >= 2)
            {
                //parse de respons naar een JArray
                dynamic obj = JArray.Parse(tosplit);
          

                // kijk naar elk item in obj
                foreach (JObject item in obj)
                {   // check of het een klant of leverancier is
                    if (item.GetValue("relatiesoort").ToString().Contains("Klant"))
                    {   
                        relatiesoort = item.GetValue("relatiesoort").ToString();
                        string kcode = item.GetValue("relatiecode").ToString();
                        firstName = item.GetValue("naam").ToString();
                        phone = item.GetValue("telefoon").ToString();
                        mobilephone = item.GetValue("mobieleTelefoon").ToString();
                        email = item.GetValue("email").ToString();




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
                        builder.WithValue(ContactsContract.CommonDataKinds.Email.InterfaceConsts.Label, relatiesoort);
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
                    }
                }
            }

            #region uitcommented
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

            //Contact toevoegen
            ContentProviderResult[] res;

            res = ContentResolver.ApplyBatch(ContactsContract.Authority, ops);
        }

    }
}