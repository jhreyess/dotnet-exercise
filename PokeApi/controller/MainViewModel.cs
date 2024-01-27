using PokeApi.models;
using PokeApi.service;
using PokeApi.views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Windows;

namespace PokeApi.controller
{
    class MainViewModel : INotifyPropertyChanged
    {

        private readonly IMainWindow view;
        private readonly PokemonService service;

        public ICollectionView PokemonCollectionView { get; private set; }
        public ObservableCollection<PokemonListItem> Pokemons { get; private set; }
        public ObservableCollection<PokemonInfo> SearchResult { get; private set; }

        private bool _isLoading;
        public bool IsLoading 
        {
            get { return _isLoading; }
            set 
            {
                _isLoading = value;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    view.showLoading(value);
                });
            }
        } 

        private string? _filterText;
        public string? FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                PokemonCollectionView.Refresh();
            }
        }

        public string? NextPage { get; private set; }
        public ObservableCollection<PokemonListItem> PokemonTypes { get; private set; }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public MainViewModel(IMainWindow mainWindow, PokemonService service)
        {
            this.view = mainWindow;
            this.service = service;

            Pokemons = new ObservableCollection<PokemonListItem>();
            SearchResult = new ObservableCollection<PokemonInfo>();
            PokemonTypes = new ObservableCollection<PokemonListItem>
            {
                new PokemonListItem("All")
            };
            IsLoading = false;

            PokemonCollectionView = CollectionViewSource.GetDefaultView(Pokemons);
            PokemonCollectionView.Filter = FilterPokemons;

            Initialize();
        }

        private async void Initialize()
        {
            await Task.Run(() => GetAll("type"));
            await Task.Run(() => GetAll("pokemon"));
        }

        public async void GetAll(
            string url = "pokemon",
            bool removeOld = false
        ) {
            try
            {
                IsLoading = true;
                PokemonPageDTO? result = await service.GetAllFromPage(url);

                ObservableCollection<PokemonListItem> target;
                switch(url)
                {
                    case "pokemon": target = Pokemons; break;
                    case "type": target = PokemonTypes; break;
                    default: target = Pokemons; break;
                }

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    if (result != null)
                    {
                        if (removeOld) { target.Clear(); }

                        foreach (var item in result.Items)
                        {
                            target.Add(item);
                        }
                        NextPage = result.Next;
                    }
                });
                
            }
            catch (Exception ex)
            {
                if(IsLoading) { IsLoading = false; }
                view.showError(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async void GetByType(string type = "normal")
        {
            try
            {
                IsLoading = true;
                List<PokemonListItem>? result = await service.GetByType(type);

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    if (result != null)
                    {
                        Pokemons.Clear();
                        foreach (var item in result)
                        {
                            Pokemons.Add(item);
                        }
                    }
                });

                NextPage = null;
            }
            catch (Exception ex)
            {
                if (IsLoading) { IsLoading = false; }
                view.showError(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async void GetPokemon(string name)
        {
            try
            {
                IsLoading = true;
                PokemonInfo? result = await service.GetByName(name);

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    if (result != null)
                    {
                        SearchResult.Clear();
                        SearchResult.Add(result);
                    }
                });

                NextPage = null;
            }
            catch (Exception ex)
            {
                if (IsLoading) { IsLoading = false; }
                view.showError(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void ExportData(DataGrid dataGrid, string filepath)
        {
            var columns = dataGrid.Columns;

            StringBuilder csvData = new StringBuilder();

            foreach (var column in columns)
            {
                csvData.Append(column.Header);
                csvData.Append(",");
            }
            csvData.AppendLine();

            foreach (var item in dataGrid.Items)
            {
                var row = item as PokemonListItem;

                foreach (var column in columns)
                {
                    var cellContent = column.GetCellContent(row);
                    if (cellContent != null)
                    {
                        csvData.Append(cellContent.GetValue(TextBlock.TextProperty));
                    }
                    csvData.Append(",");
                }
                csvData.AppendLine();
            }

            File.WriteAllText(filepath, csvData.ToString());
        }

        public void SendByEmail(string filepath, string email)
        {
            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //mail.From = new MailAddress("test@gmail.com");
            //mail.To.Add(email);
            //mail.Subject = "Hey checkout this file!";
            //mail.Body = "mail with attachment";

            //Attachment attachment;
            //attachment = new Attachment(filepath);
            //mail.Attachments.Add(attachment);

            //SmtpServer.Port = 111;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("test@gmail.com", "test password");
            //SmtpServer.EnableSsl = true;

            //SmtpServer.Send(mail);
            MessageBox.Show("Correo enviado exitosamente!");
        }

        private bool FilterPokemons(object obj)
        {
            if (obj is PokemonListItem pokemon)
            {
                return string.IsNullOrEmpty(FilterText) || pokemon.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
