using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Application
{
    class ContactViewModel : INotifyPropertyChanged
    {
        public ContactViewModel()
        {
            Observcontacten = new ObservableCollection<SelectableItemWrapper<Contacten>>(SelectieActivity.Splitklant()
                .Select(pk => new SelectableItemWrapper<Contacten> { Item = pk }));
        }

        private ObservableCollection<SelectableItemWrapper<Contacten>> _contacts;
        public ObservableCollection<SelectableItemWrapper<Contacten>> Observcontacten
        {
            get { return _contacts; }
            set { _contacts = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Contacten> _selectedcontact;
        public ObservableCollection<Contacten> Selectedcontact
        {
            get { return _selectedcontact ?? new ObservableCollection<Contacten>(); }
            private set { _selectedcontact = value; RaisePropertyChanged(); }
        }

        private ICommand _getSelectedItemsCommand;
        public ICommand GetSelectedItemsCommand
        {
            get
            {
                return _getSelectedItemsCommand ??
                    (_getSelectedItemsCommand = new Command(
                        async () =>
                        {
                            Selectedcontact = GetSelectedcontact();
                        }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        ObservableCollection<Contacten> GetSelectedcontact()
        {
            var selected = Observcontacten
                .Where(p => p.IsSelected)
                .Select(p => p.Item)
                .ToList();
            return new ObservableCollection<Contacten>(selected);
        }

        void SelectAll(bool select)
        {
            foreach (var p in Observcontacten)
            {
                p.IsSelected = select;
            }
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}