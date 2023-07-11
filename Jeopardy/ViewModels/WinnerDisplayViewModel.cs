using Jeopardy.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeopardy.ViewModels {
	public class WinnerDisplayViewModel : ViewModelBase {
		public MainViewModel? MainViewModel { get; set; }

		private string _winners;
		public string Winners {
			get => _winners;
			set => SetProperty(ref _winners, value);
		}

		private RelayCommand? _mainMenuCommand;
		public RelayCommand MainMenuCommand {
			get { return _mainMenuCommand ?? (_mainMenuCommand = new RelayCommand(MainMenu, MainMenuCanExecute)); }
		}
		private void MainMenu(object obj) {
			MainViewModel.SwitchToMainMenu();
		}
		private bool MainMenuCanExecute(object obj) {
			return MainViewModel != null;
		}

		public void InitWinnerText(List<string> winners) {
			string txt = winners.First();
			winners.RemoveAt(0);
			foreach(string winner in winners) {
				txt += ", ";
				txt += winner;
			}
			Winners = txt;
		}
	}
}
