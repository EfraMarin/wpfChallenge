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

        public int NumberOfGamesToPlay
        {
            get { return _numberOfGamesToPlay; }
            set
            {
                _numberOfGamesToPlay = value;
                NotifyChange();
            }
        }

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
        public ICommand StartGamesCommand { get; set; }

        BoardGameService _gameService;

        int _shortestGameLength, _largestGameLength;

        double _averageGameLength;

        public int ShortestGameLength
        {
            get => _shortestGameLength;
            set { _shortestGameLength = value; NotifyChange(); }
        }

        public int LargestGameLength
        {
            get => _largestGameLength;
            set { _largestGameLength = value; NotifyChange(); }
        }

        public double AverageGameLength
        {
            get => _averageGameLength;
            set { _averageGameLength = value; NotifyChange(); }
        }

        bool _simulationInProgress = false;

        #endregion

        public LCRSimulatorViewModel()
        {
            _minimumPlayers = Convert.ToInt32(ConfigurationManager.AppSettings["minimumPlayers"].ToString());

            this.NumberOfPlayers = _minimumPlayers;

            this._gameService = new BoardGameService();

            StartGamesCommand = new RelayCommand(async x => await RunGamesSimulation(), c => CanRunSimulations());

            this._logText = new StringBuilder();
        }

        ~LCRSimulatorViewModel() { }

        #region Methods
        /// <summary>
        /// Notifies changes on Binding properties to update UI
        /// </summary>
        /// <param name="propertyName">The name of property that notifies that has changed</param>
        private void NotifyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanRunSimulations()
        {
            return !_simulationInProgress && this._numberOfPlayers >= _minimumPlayers && this.NumberOfGamesToPlay > 0;
        }

        public async Task RunGamesSimulation()
        {
            LogText = string.Empty;
            ResetStatistics();

            Random random = new Random();


            LogToOutPut("Creating game collection to simulate...");

            List<LCRGame> playedGanes = new List<LCRGame>();

            _simulationInProgress = true;

            LogToOutPut("Simulation in progress...");

            for (int i = 0; i < this._numberOfGamesToPlay; i++)
            {
                Func<BoardGameService, LCRGame> func = (s) => s.RunGame(s.CreateNewLCRGame(this._numberOfPlayers, random));

                var r = await Task.Run(() => func(this._gameService));

                playedGanes.Add(r);
            }
            LogToOutPut($"Calculating statistics for {NumberOfGamesToPlay.ToString("#,##")} simulated games...");

            SetStatisticsAsync(playedGanes);

            LogToOutPut("Simulation Finished!");

            _simulationInProgress = false;
            //await LogGameResult(playedGanes);
        }

        /// <summary>
        /// Logs a message on the UI
        /// </summary>
        /// <param name="message">Message to notify</param>
        /// <returns>An awaitable function</returns>
        async Task LogToOutPut(string message)
        {
            await Task.Run(() =>
            {
                this._logText.AppendLine(message);
            });

            NotifyChange(nameof(this.LogText));
        }

        async Task LogGameResult(ICollection<LCRGame> gameResults)
        {
            await Task.Run(() =>
            {
                foreach (var game in gameResults)
                    this._logText.AppendLine($"After {game.TurnsTaken} turns, the winner is player {game.Winner.Id}");

            });

            NotifyChange(nameof(this.LogText));
        }

        void ResetStatistics()
        {
            this.LargestGameLength = 0;
            this.ShortestGameLength = 0;
            this.AverageGameLength = 0;
        }

        /// <summary>
        /// Set statistic values to be displayed on UI
        /// </summary>
        /// <param name="gameResults">List of finished games to extract statistics</param>
        /// <returns>Awaitable function</returns>
        async Task SetStatisticsAsync(ICollection<LCRGame> gameResults)
        {
            await Task.Run(() =>
            {
                gameResults = gameResults.OrderByDescending(x => x.TurnsTaken).ToList();

                this.LargestGameLength = gameResults.First().TurnsTaken;
                this.ShortestGameLength = gameResults.Last().TurnsTaken;
                this.AverageGameLength = gameResults.Average(x => x.TurnsTaken);
            });
        }

        #endregion

    }
}
