using Jeopardy.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeopardy.Models {
	public class GameBoardModel {
		public ObservableCollection<QuestionButtonViewModel> Questions { get; set; } = new ObservableCollection<QuestionButtonViewModel>();
		public List<string> QuestionTypes { get; set; } = new List<string>();
		public int Rows { get; set; }
		public int Columns { get; set; }
		public bool Success { get; set; }
		public string? FailureMessage { get; set; }
	}
}
