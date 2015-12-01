using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Windows.Input;

namespace LeagueManager.WPF.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        public List<string> Champions { get; set; }

        public TeamViewModel MyTeam { get; set; }

        public TeamViewModel EnemyTeam { get; set; }

        public ICommand SendSetupCommand { get; set; }
        public ICommand NewGameCommannd { get; set; }

        public MainViewModel()
        {
            MyTeam = new TeamViewModel();
            EnemyTeam = new TeamViewModel();
            SendSetupCommand = new RelayCommand(SendSetup);
            NewGameCommannd = new RelayCommand(NewGame);
            //Deze halen we later op vanuit de server
            Champions = new List<string>(){
              "Shaco",
              "Sona",
              "Vayne",
              "Leblanc",
              "Aatrox"
          };
        }

        private void NewGame()
        {
            this.MyTeam = new TeamViewModel();
            this.EnemyTeam = new TeamViewModel();
            RaisePropertyChanged("MyTeam");
            RaisePropertyChanged("EnemyTeam");
        }

        private void SendSetup()
        {
           //Send to service
        }
    }
}