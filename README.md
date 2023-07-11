# Jeopardy
Jeopardy app made in C# with WPF using "MVVM" structure. 

The app supports up to 4 teams. Allows for generation of random board with small pool of questions or creating a board from json file.

## Json file format for questions
This is the format for Json containing questions.
```json
"QuestionTypes": [
  {"Type": "Pokémon"}
],

"QuestionSets": [
  {
    "QuestionList": [
      {
        "QuestionTxt": "In which generation was Dusknoir introduced?",
        "Answer": "In generation 4",
        "Cost": 100
      }
    ]
  }
]
```
Firstly you need to define all categories for questions in `QuestionTypes` array, then in `QuestionSets` you make a `QuestionList` for every defined category.
`QuestionList` contains: `QuestionTxt` (question), `Answer` (answer to the question) and `Cost` (reward for the question)

There are following restrictions for the board:
  * All `QuestionList`'s must have the same length
  * Number of elements in `QuestionTypes` has to be equal to number of `QuestionList`
  * You can have between 4 and 6 categories
  * You can have between 4 and 6 questions in a category

## How to use
To start the game, first create teams (option will be locked afterwards). Then pick between making board from Json file or generate a random board.

During the game you can choose a question. Once a question is chosen, the + / - signs in the team box will add / subtract score of the last chosen question.

![Screenshot](https://github.com/TerryBool/Jeopardy/assets/139129641/cda420c3-340f-485c-b308-9092d97b57fc)
