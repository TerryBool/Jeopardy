using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeopardy.Models {
	public class Question {
		public string QuestionTxt { get; set; }
		public string Answer { get; set; }
		public int Cost { get; set; }

		public Question() {
			QuestionTxt = "";
			Answer = "";
		}

		public Question(string question, string answer, int cost) {
			QuestionTxt = question;
			Answer = answer;
			Cost = cost;
		}
	}
}
