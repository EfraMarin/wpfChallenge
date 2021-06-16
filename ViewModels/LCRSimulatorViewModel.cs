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
using System.Configuration;

namespace wpfChallenge.ViewModels
{
    public class LCRSimulatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region Properties
        int _minimumPlayers;

        private int _numberOfPlayers;
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

        StringBuilder _logText;
        public string LogText
        {
            get { return _logText.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _logText.Clear();

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
            _minimumPlayers = Convert.ToInt32(ConfigurationManager.AppSettings["minimumPlayers"].ToString());

            this.NumberOfPlayers = _minimumPlayers;

            this._gameService = new BoardGameService();

            StartGamesCommand = new RelayCommand(async x => await RunGames(), c => CanRunGames());

            this._logText = new StringBuilder();
        }

        ~LCRSimulatorViewModel() { }

        private void NotifyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanRunGames()
        {
            return this._numberOfPlayers >= _minimumPlayers && this.NumberOfGamesToPlay > 0;
        }

        public async Task RunGames()
        {
            LogText = string.Empty;

            Random random = new Random();

            for (int i = 0; i < this._numberOfGamesToPlay; i++)
            {
                Func<BoardGameService, LCRGame> func = (s) => s.RunGame(s.CreateNewLCRGame(this._numberOfPlayers, random));

                var r = await Task.Run(() => func(this._gameService));

                AppendGameResult(r);
            }
            this.LogText = this._logText.ToString();

        }

        private void AppendGameResult(LCRGame gameResult)
        {
            this._logText.AppendLine($"After {gameResult.TurnsTaken} turns, the winner is player {gameResult.Winner.Id}");
        }
    }
}
