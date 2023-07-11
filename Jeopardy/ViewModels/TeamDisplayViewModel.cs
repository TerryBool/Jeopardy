using Jeopardy.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeopardy.ViewModels {
	public class TeamDisplayViewModel : ViewModelBase {
		
		public MainViewModel? MainViewModel { get; set; }
		public int LastQuestionScore { get; set; } = 0;
		public int CurrentScore { get; set; } = 0;

		private bool _isInGame = false;
		public bool IsInGame {
			get => _isInGame;
			set => SetProperty(ref _isInGame, value);
		}

		private string _teamName = "";
		public string TeamName {
			get => _teamName;
			set => SetProperty(ref _teamName, value);
		}

		private string _score = "0";
		public string Score {
			get => _score;
			set {
				int score;
				if (!int.TryParse(value, out score)) {
					_score = CurrentScore.ToString();
					OnPropertyChanged("Score");
					return;
				}
				CurrentScore = score;
				_score = value;
				OnPropertyChanged("Score");
			}
		}

		private RelayCommand? _addPointsCommand;
		public RelayCommand AddPointsCommand {
			get { return _addPointsCommand ?? (_addPointsCommand = new RelayCommand(AddPoints, AddPointsCanExecute)); }
		}
		private void AddPoints(object obj) {
			CurrentScore += LastQuestionScore;
			Score = CurrentScore.ToString();
		}
		private bool AddPointsCanExecute(object obj) {
			return true;
		}

		private RelayCommand? _removePointsCommand;
		public RelayCommand RemovePointsCommand {
			get { return _removePointsCommand ?? (_removePointsCommand = new RelayCommand(RemovePoints, RemovePointsCanExecute)); }
		}
		private void RemovePoints(object obj) {
			CurrentScore -= LastQuestionScore;
			Score = CurrentScore.ToString();
		}
		private bool RemovePointsCanExecute(object obj) {
			return true;
		}

		private RelayCommand? _removeTeamCommand;
		public RelayCommand RemoveTeamCommand {
			get { return _removeTeamCommand ?? (_removeTeamCommand = new RelayCommand(RemoveTeam, RemoveTeamCanExecute)); }
		}
		private void RemoveTeam(object obj) {
			if (MainViewModel == null) return;
			MainViewModel.Teams.Remove(this);
		}
		private bool RemoveTeamCanExecute(object obj) {
			if (MainViewModel == null) return false;
			if (MainViewModel.Teams.Count <= 2) return false;
			return true;
		}
	}
}
