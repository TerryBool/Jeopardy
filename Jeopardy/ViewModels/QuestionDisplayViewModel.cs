using Jeopardy.Models;
using Jeopardy.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeopardy.ViewModels {
	public class QuestionDisplayViewModel : ViewModelBase {

		private RelayCommand _exitCommand;
		private RelayCommand _showAnswerCommand;

		public MainViewModel MainViewModel { get; set; }

		private Question ?_question;
		public Question Question {
			get => _question;
			set => SetProperty(ref _question, value);
		}

		private bool _showAnswer;
		public bool ShowAnswer {
			get => _showAnswer;
			set => SetProperty(ref _showAnswer, value);
		}

		private int _height;
		public int Height {
			get => _height;
			set => SetProperty(ref _height, value);
		}

		private int _halfHeight;
		public int HalfHeight {
			get => _halfHeight;
			set => SetProperty(ref _halfHeight, value);
		}

		private int _width;
		public int Width {
			get => _width;
			set => SetProperty(ref _width, value);
		}

		public RelayCommand ShowAnswerCommand {
			get { return _showAnswerCommand ?? (_showAnswerCommand = new RelayCommand(ShowAnswerFunc, ShowAnswerCanExecute)); }
		}

		private void ShowAnswerFunc(object obj) {
			ShowAnswer = true;
		}

		private bool ShowAnswerCanExecute(object obj) {
			return true;
		}

		public RelayCommand ExitCommand {
			get { return _exitCommand ?? (_exitCommand = new RelayCommand(Exit, ExitCanExecute)); }
		}

		private void Exit(object obj) {
			MainViewModel.ExitQuestion();
		}

		private bool ExitCanExecute(object obj) {
			return true;
		}

		public void SetSize(int width, int height) {
			height += 4;
			Width = width;
			Height = height;
			HalfHeight = height / 2;
		}

		public QuestionDisplayViewModel() {
			Question = new Question("Question is missing", "And so does the Answer \n Dumbass", 0);
			_showAnswer = false;
		}
	}
}
