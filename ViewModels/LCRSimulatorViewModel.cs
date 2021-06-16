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

namespace wpfChallenge.ViewModels
{
    public class LCRSimulatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

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


        public int NumberOfGamesToPlay
        {
            get { return _numberOfGamesToPlay; }
            set
            {
                _numberOfGamesToPlay = value;
                NotifyChange();
            }
        }
        public LCRSimulatorViewModel()
        {
            StartGamesCommand = new RelayCommand(x => RunGames(), c => CanRunGames());
        }

        private void NotifyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand StartGamesCommand { get; set; }

        public bool CanRunGames()
        {
            return this._numberOfPlayers >= 3 && this.NumberOfGamesToPlay > 0;
        }
        public void RunGames()
        {

        }

    }
}
