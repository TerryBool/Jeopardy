using Jeopardy.Models;
using Jeopardy.Support;
using Jeopardy.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Jeopardy.ViewModels {
	public class MainViewModel : ViewModelBase {

		private GameBoardViewModel gameBoardVm;

		private ViewModelBase _selectedViewModel;

		public ObservableCollection<ViewModelBase> LoadedViewModels { get; private set; } = new ObservableCollection<ViewModelBase>();
		public ObservableCollection<TeamDisplayViewModel> Teams { get; private set; } = new ObservableCollection<TeamDisplayViewModel>();

		public bool WasLastQuestion { get; set; } = false;

		private bool _isInGame = false;
		public bool IsInGame {
			get => _isInGame;
			set => SetProperty(ref _isInGame, value);
		}

		public ViewModelBase SelectedViewModel {
			get { return _selectedViewModel; }
			set {
				_selectedViewModel = value;

				if (!LoadedViewModels.Contains(value)) {
					LoadedViewModels.Add(value);
				}

				OnPropertyChanged("SelectedViewModel");
			}
		}

		private RelayCommand? _addTeamCommand;
		public RelayCommand AddTeamCommand {
			get { return _addTeamCommand ?? (_addTeamCommand = new RelayCommand(AddTeam, AddTeamCanExecute)); }
		}
		private void AddTeam(object obj) {
			TeamDisplayViewModel tdvm = new TeamDisplayViewModel();
			tdvm.MainViewModel = this;
			tdvm.TeamName = "Team " + (Teams.Count + 1).ToString();
			Teams.Add(tdvm);
		}
		private bool AddTeamCanExecute(object obj) {
			return Teams.Count < 4;
		}

		public void SwitchToQuestion(Question question) {
			QuestionDisplayViewModel questionDisplayVm = new QuestionDisplayViewModel();
			questionDisplayVm.Question = question;
			questionDisplayVm.MainViewModel = this;
			questionDisplayVm.SetSize(gameBoardVm.Columns * 210, (gameBoardVm.Rows + 1) * 100);
			SelectedViewModel = questionDisplayVm;

			foreach(TeamDisplayViewModel teamDisplay in Teams) {
				teamDisplay.LastQuestionScore = question.Cost;
			}
		}

		public void ExitQuestion() {
			ViewModelBase questionDisplayVm = SelectedViewModel;
			if (!WasLastQuestion) {
				SelectedViewModel = gameBoardVm;
			} else {
				int highestScore = -100000;
				List<string> winningTeamNames = new List<string>();
				foreach(TeamDisplayViewModel tdvm in Teams) {
					if (tdvm.CurrentScore > highestScore) {
						highestScore = tdvm.CurrentScore;
						winningTeamNames.Clear();
						winningTeamNames.Add(tdvm.TeamName);
					} else if (tdvm.CurrentScore == highestScore) {
						winningTeamNames.Add(tdvm.TeamName);
					}
				}
				WinnerDisplayViewModel winnerDisplayVm = new WinnerDisplayViewModel();
				winnerDisplayVm.MainViewModel = this;
				winnerDisplayVm.InitWinnerText(winningTeamNames);
				SelectedViewModel = winnerDisplayVm;
			}
			LoadedViewModels.Remove(questionDisplayVm);
		}

		public void SwitchToMainMenu() {
			GameMenuViewModel gameMenuViewModel = new GameMenuViewModel();
			gameMenuViewModel.MainViewModel = this;
			ViewModelBase prevVm = SelectedViewModel;
			SelectedViewModel = gameMenuViewModel;
			LoadedViewModels.Remove(prevVm);
			LoadedViewModels.Remove(gameBoardVm);
			foreach(TeamDisplayViewModel teamDisplay in Teams) {
				teamDisplay.Score = "0";
				teamDisplay.IsInGame = false;
			}
			IsInGame = false;
			WasLastQuestion = false;
			gameBoardVm = null;
		}

		public void StartGame(GameBoardModel gameBoard, GameBoardViewModel gameBoardViewModel) {
			IsInGame = true;
			foreach (TeamDisplayViewModel teamDisplay in Teams) {
				teamDisplay.IsInGame = true;
			}
			gameBoardVm = gameBoardViewModel;
			gameBoardVm.InitializeBoard(gameBoard, this);
			ViewModelBase prevVm = SelectedViewModel;
			SelectedViewModel = gameBoardVm;
			LoadedViewModels.Remove(prevVm);
		}

		public MainViewModel() {
			GameMenuViewModel vm = new GameMenuViewModel();
			vm.MainViewModel = this;
			SelectedViewModel = vm;

			TeamDisplayViewModel tdvm = new TeamDisplayViewModel();
			tdvm.MainViewModel = this;
			tdvm.TeamName = "Team 1";
			Teams.Add(tdvm);

			tdvm = new TeamDisplayViewModel();
			tdvm.MainViewModel = this;
			tdvm.TeamName = "Team 2";
			Teams.Add(tdvm);
		}
	}
}
