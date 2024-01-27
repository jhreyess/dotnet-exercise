using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using PokeApi.controller;
using PokeApi.models;
using PokeApi.service;
using PokeApi.views;

namespace PokeApi
{
 
    public partial class MainWindow : Window, IMainWindow
    {

        private MainViewModel vm;

        public MainWindow()
        {
            InitializeComponent();
            vm = new MainViewModel(this, new Service(PokemonAPI.Instance.httpClient));
            DataContext = vm;
        }

        void IMainWindow.showError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void IMainWindow.showLoading(bool visible)
        {
            loadingProgressBar.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
        }

        private void OnExportBtnClick(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = PokemonDataGrid;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            dialog.DefaultExt = "csv";
            if (dialog.ShowDialog() == true) { 
                vm.ExportData(dataGrid, dialog.FileName);

                MessageBox.Show("Datos almacenados en: " + dialog.FileName);

                EmailDialog emailDialog = new EmailDialog();
                bool? result = emailDialog.ShowDialog();

                if (result == true)
                {
                    vm.SendByEmail(dialog.FileName, emailDialog.EnteredEmail);
                }
            }
        }

        private void pokemons_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if(e.VerticalOffset + e.ViewportHeight == e.ExtentHeight)
            {
                if(!vm.IsLoading && string.IsNullOrEmpty(vm.FilterText))
                {
                    string? next = vm.NextPage;
                    if (next != null) { vm.GetAll(url: next); }
                }
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PokemonDataGrid.ItemsSource == vm.SearchResult)
            {
                PokemonDataGrid.ItemsSource = vm.Pokemons;
            }

            var item = (PokemonListItem)((ComboBox)sender).SelectedItem;
            if (item != null)
            {
                if (item.Name == "All")
                {
                    vm.GetAll(removeOld: true);
                }
                else
                {
                    vm.GetByType(item.Name);
                }
            }
        }

        private void OnFilterBtnClick(object sender, RoutedEventArgs e)
        {
            string name = SearchBox.Text;
            if(!string.IsNullOrEmpty(name))
            {
                vm.GetPokemon(name);
                PokemonDataGrid.ItemsSource = vm.SearchResult;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchButton.IsEnabled = !string.IsNullOrEmpty(SearchBox.Text);
        }
    }
}

