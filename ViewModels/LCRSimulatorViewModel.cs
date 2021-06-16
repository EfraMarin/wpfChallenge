using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using wpfChallenge.Models;
using wpfChallenge.Interfaces;
using wpfChallenge.Services;

namespace wpfChallenge.ViewModels
{
    public class LCRSimulatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region Properties
        private int _numberOfPlayers = 3;
        public int NumberOfPlayers
        {
            get { return _numberOfPlayers; }
            set
            {
                try
                {
                    if (value < 3)
                    {
                        MessageBox.Show("The number of players should be 3 or more");
                        return;
                    }

                    _numberOfPlayers = value;
                    NotifyChange();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Just numbers allowed");
                }
            }
        }

        private int _numberOfGamesToPlay = 1;

        string _logText = "...";
        public string LogText
        {
            get { return _logText; }
            set
            {
                _logText = value;
                NotifyChange();
            }
        }

        public int NumberOfGamesToPlay
        {
            get { return _numberOfGamesToPlay; }
            set
            {
                _numberOfGamesToPlay = value;
                NotifyChange();
            }
        }
        public ICommand StartGamesCommand { get; set; }

        ICollection<IBoardGame> _gamesList;

        BoardGameService _gameService;

        #endregion
        public LCRSimulatorViewModel()
        {
            this._gameService = new BoardGameService();

            StartGamesCommand = new RelayCommand(async x => await RunGames(), c => CanRunGames());

        }

        ~LCRSimulatorViewModel() { }

        private void NotifyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanRunGames()
        {
            return this._numberOfPlayers >= 3 && this.NumberOfGamesToPlay > 0;
        }

        public async Task RunGames()
        {
            this._logText = string.Empty;

            //this._gamesList = CreateGamesList();

            for (int i = 0; i < this._numberOfGamesToPlay; i++)
            {
                Func<BoardGameService, LCRGame> func = (s) => s.RunGame(s.CreateNewLCRGame(this._numberOfPlayers));

                var r = func(this._gameService);
            }

        }

        private void LogResults(LCRGame[] results)
        {

        }
    }
}
