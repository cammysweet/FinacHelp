using FinacHelp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinacHelp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewMeses : ContentPage
    {
        public ListViewMeses()
        {
            InitializeComponent();
            BindingContext = new ListViewMesesViewModel();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }



    class ListViewMesesViewModel : INotifyPropertyChanged
    {
        #region Atributos
        bool busy;
        DateTime hoje = DateTime.Now;
        MesAno mesAnoAtual;
        #endregion

        #region Propriedades
        public ObservableCollection<MesAno> Items { get; }
        public ObservableCollection<Grouping<int, MesAno>> ItemsGrouped { get; }
        public ICommand RefreshDataCommand { get; }
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }
        #endregion

        #region Construtor
        public ListViewMesesViewModel()
        {
            mesAnoAtual = new MesAno { Mes = hoje.Month, Ano = hoje.Year };
            Items = new ObservableCollection<MesAno>(new[]
            {
                mesAnoAtual,
                new MesAno{ Mes = mesAnoAtual.Mes-1, Ano = mesAnoAtual.Ano },
                new MesAno{ Mes = mesAnoAtual.Mes+1, Ano = mesAnoAtual.Ano }
            });

            var sorted = from item in Items
                         orderby item.Mes
                         group item by item.Mes into itemGroup
                         select new Grouping<int, MesAno>(itemGroup.Key, itemGroup);

            ItemsGrouped = new ObservableCollection<Grouping<int, MesAno>>(sorted);

            RefreshDataCommand = new Command(
                async () => await RefreshData());
        } 
        #endregion

        async Task RefreshData()
        {
            IsBusy = true;
            //Load Data Here
           mesAnoAtual = new MesAno { Mes = hoje.Month, Ano = hoje.Year };

            await Task.Delay(2000);

            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; private set; }

            public Grouping(K key, IEnumerable<T> items)
            {
                Key = key;
                foreach (var item in items)
                    this.Items.Add(item);
            }
        }
    }
}
