using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jeopardy.Models {
	public class JsonDeserializationClasses {
		public class QuestionListList {
			[JsonPropertyName("QuestionTypes")]
			public List<QuestionType>? QuestionTypes { get; set; }

			[JsonPropertyName("QuestionSets")]
			public List<QuestionList>? QuestionSets { get; set; }
		}

		public class QuestionList {
			[JsonPropertyName("QuestionList")]
			public List<Question>? Questions { get; set; }
		}

		public class QuestionType {
			[JsonPropertyName("Type")]
			public string? Type { get; set; }
		}

		public class RandomQuestionsCategories {
			[JsonPropertyName("QuestionCategories")]
			public List<RandomQuestionsCategory>? QuestionCategories { get; set; }
		}

		public class RandomQuestionsCategory {
			[JsonPropertyName("Category")]
			public string? Category { get; set; }
			[JsonPropertyName("QuestionList")]
			public List<Question>? QuestionList { get; set; }
		}
	}
}
