using Jeopardy.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using static Jeopardy.Models.JsonDeserializationClasses;
using System.IO.IsolatedStorage;
using System.Runtime.Versioning;
using System.Windows.Documents;

namespace Jeopardy.Models {
	public class GameBoardGenerator {
		public GameBoardModel GenerateTestingBoard(GameBoardViewModel gameBoardVmRef) {
			GameBoardModel Board = new GameBoardModel();
			for (int i = 0; i < 25; i++) {
				Board.Questions.Add(new QuestionButtonViewModel(gameBoardVmRef, new Question("PEEPEEPOOPOO?", "VERY STINKY", i)));
			}
			for (int i = 0; i < 5; i++) {
				Board.QuestionTypes.Add(i.ToString());
			}
			Board.Success = true;
			return Board;
		}

		public GameBoardModel GenerateFromJson(GameBoardViewModel gameBoardVmRef, string fileName) {
			GameBoardModel board = new GameBoardModel();
			if (Path.GetExtension(fileName) != ".json") {
				board.FailureMessage = "Wrong file type!";
				board.Success = false;
				return board;
			}
			QuestionListList? list;
			string fileContent = File.ReadAllText(fileName);

			try {
				list = JsonSerializer.Deserialize<QuestionListList>(fileContent);
			} catch (Exception e) {
				board.FailureMessage = e.Message;
				board.Success = false;
				return board;
			}

			if (!IsBoardValid(list)) {
				board.FailureMessage = "Invalid Board";
				board.Success = false;
				return board;
			}

			foreach(QuestionType t in list.QuestionTypes) {
				board.QuestionTypes.Add(t.Type);
			}

			int nQuestions = list.QuestionSets.First().Questions.Count;
			board.Rows = nQuestions;
			board.Columns = list.QuestionTypes.Count;

			for(int i = 0; i < nQuestions; i++) {
				foreach (QuestionList ql in list.QuestionSets) {
					Question q = ql.Questions.First();
					ql.Questions.RemoveAt(0);
					board.Questions.Add(new QuestionButtonViewModel(gameBoardVmRef, q));
				}
			}
			board.Success = true;
			return board;
		}
		private bool IsBoardValid(QuestionListList questionSets) {
			if (questionSets == null || questionSets.QuestionTypes == null) return false;
			if (questionSets.QuestionTypes.Count != questionSets.QuestionSets.Count) return false;

			int nQuestionTypes = questionSets.QuestionSets.Count;
			if (nQuestionTypes < 4 || nQuestionTypes > 6) return false;

			int nQuestions = questionSets.QuestionSets.First().Questions.Count;
			if (nQuestions < 4 || nQuestions > 6) return false;

			foreach(QuestionList ql in questionSets.QuestionSets) {
				if (nQuestions != ql.Questions.Count) return false;
			}

			return true;
		}

		public GameBoardModel GenerateRandomBoard(GameBoardViewModel gameBoardVm) {
			GameBoardModel board = new GameBoardModel();

			RandomQuestionsCategories? categories;
			string fileContent = File.ReadAllText(@"../../../Resources/RandomGenerationQuestions.json");
			categories = JsonSerializer.Deserialize<RandomQuestionsCategories>(fileContent);

			Random random = new Random(Guid.NewGuid().GetHashCode());
			List<RandomQuestionsCategory> selectedCats = new List<RandomQuestionsCategory>();

			for(int i = 0; i < 5; i++) {
				int val = random.Next(categories.QuestionCategories.Count);
				RandomQuestionsCategory cat = categories.QuestionCategories[val];
				selectedCats.Add(cat);
				categories.QuestionCategories.Remove(cat);
			}

			foreach (RandomQuestionsCategory cat in selectedCats) {
				board.QuestionTypes.Add(cat.Category);
			}
			for (int i = 0; i < 5; i++) {
				foreach (RandomQuestionsCategory qc in selectedCats) {
					Question q = qc.QuestionList.First();
					qc.QuestionList.RemoveAt(0);
					board.Questions.Add(new QuestionButtonViewModel(gameBoardVm, q));
				}
			}
			board.Rows = 5;
			board.Columns = 5;
			board.Success = true;
			return board;
		}
	}
}
