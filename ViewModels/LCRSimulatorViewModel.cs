using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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
                _numberOfPlayers = value;
                NotifyChange(nameof(NumberOfPlayers));
            }
        }

        private int _numberOfGamesToPlay;

        public int NumberOfGamesToPlay
        {
            get { return _numberOfGamesToPlay; }
            set
            {
                _numberOfGamesToPlay = value;
                NotifyChange(nameof(NumberOfGamesToPlay));
            }
        }

        private void NotifyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartGame()
        {

        }

    }
}
