using Jeopardy.Models;
using Jeopardy.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeopardy.ViewModels {
	public class QuestionButtonViewModel : ViewModelBase {

		private bool _wasPressed = false;
		private bool _lastChosen = false;
		private string? _questionCost;

		private GameBoardViewModel _gameBoardVmRef;

		public Question Question { get; private set; }

		public bool WasPressed {
			get => _wasPressed;
			set => SetProperty(ref _wasPressed, value);
		}

		public bool LastChosen {
			get => _lastChosen;
			set => SetProperty(ref _lastChosen, value);
		}

		public string QuestionCost {
			get => _questionCost == null ? "" : _questionCost;
			set => SetProperty(ref _questionCost, value);
		}

		private RelayCommand _questionSelectedCommand;

		public RelayCommand QuestionSelectedCommand {
			get { return _questionSelectedCommand ?? (_questionSelectedCommand = new RelayCommand(QuestionSelected, QuestionSelectedCanExecute)); }
		}

		private void QuestionSelected(object obj) {
			LastChosen = true;
			WasPressed = true;

			_gameBoardVmRef.QuestionSelected(this);
		}

		private bool QuestionSelectedCanExecute(object obj) {
			return !WasPressed;
		}

		public QuestionButtonViewModel(GameBoardViewModel gameBoardViewModel, string cost) {
			_gameBoardVmRef = gameBoardViewModel;
			Question = new Question();
			QuestionCost = cost;
		}

		public QuestionButtonViewModel(GameBoardViewModel gameBoardViewModel, Question question) {
			_gameBoardVmRef = gameBoardViewModel;
			Question = question;
			QuestionCost = Question.Cost.ToString();
		}
	}
}
