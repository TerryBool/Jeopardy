using Jeopardy.Models;
using Jeopardy.Support;
using Jeopardy.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Jeopardy.ViewModels {
	public class GameBoardViewModel : ViewModelBase {
		public ObservableCollection<QuestionButtonViewModel> Questions { get; private set; } = new ObservableCollection<QuestionButtonViewModel>();
		public List<string> QuestionTypes { get; private set; } = new List<string>();

		private MainViewModel? _mainVm;

		private int _rows = 4;
		public int Rows {
			get => _rows;
			set => SetProperty(ref _rows, value);
		}

		private int _columns = 4;
		public int Columns {
			get => _columns;
			set => SetProperty(ref _columns, value);
		}

		private QuestionButtonViewModel ?_lastQuestionPicked = null;
		private int _nQuestionAnswered = 0;
		private int _totalQuestions;

		public void QuestionSelected(QuestionButtonViewModel questionButton) {
			_nQuestionAnswered++;
			if (_lastQuestionPicked != null) _lastQuestionPicked.LastChosen = false;
			_lastQuestionPicked = questionButton;

			_mainVm.SwitchToQuestion(questionButton.Question);

			if (_nQuestionAnswered == _totalQuestions) _mainVm.WasLastQuestion = true;
		}

		public void InitializeBoard(GameBoardModel gameBoard, MainViewModel mainVmRef) {
			Questions = gameBoard.Questions;
			QuestionTypes = gameBoard.QuestionTypes;
			Rows = gameBoard.Rows;
			Columns = gameBoard.Columns;
			_totalQuestions = Questions.Count;
			_mainVm = mainVmRef;
		}
	}
}
