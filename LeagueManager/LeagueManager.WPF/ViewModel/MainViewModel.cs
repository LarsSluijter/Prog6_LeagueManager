using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Timers;
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

        public int GameId { get; set; }

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

        private LMService.ILMService service;
        private Timer timer;

        private void SendSetup()
        {
            service = new LMService.LMServiceClient();

            LMService.SetupContract setup = new LMService.SetupContract();
            setup.Adc = MyTeam.Adc;
            setup.Jungle = MyTeam.Jungle;
            setup.Mid = MyTeam.Mid;
            setup.Supp = MyTeam.Supp;
            setup.Top = MyTeam.Top;
            setup.PlayerName = "Linksonder";

            this.GameId = service.SendSetup(setup);
            timer.Interval = 2000;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var game = service.GetGameResult(this.GameId);

            if (game.Winner != null)
            {
                this.Winner = game.Winner.PlayerName;
                timer.Stop();
            }
        }

        public string Winner { get; set; }
    }
}