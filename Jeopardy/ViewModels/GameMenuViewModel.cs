using Jeopardy.Models;
using Jeopardy.Support;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jeopardy.ViewModels {
	public class GameMenuViewModel : ViewModelBase {
		public MainViewModel? MainViewModel { get; set; }

		GameBoardGenerator gameBoardGenerator = new GameBoardGenerator();

		private RelayCommand? _randomBoardCommand;
		public RelayCommand RandomBoardCommand {
			get { return _randomBoardCommand ?? (_randomBoardCommand = new RelayCommand(RandomBoard, RandomBoardCanExecute)); }
		}
		private void RandomBoard(object obj) {
			GameBoardViewModel gameBoardViewModel = new GameBoardViewModel();
			GameBoardModel gameBoard;
			gameBoard = gameBoardGenerator.GenerateRandomBoard(gameBoardViewModel);
			MainViewModel.StartGame(gameBoard, gameBoardViewModel);
		}
		private bool RandomBoardCanExecute(object obj) {
			return MainViewModel != null;
		}

		private RelayCommand? _genFromFileCommand;
		public RelayCommand GenFromFileCommand {
			get { return _genFromFileCommand ?? (_genFromFileCommand = new RelayCommand(GenFromFile, GenFromFileCanExecute)); }
		}
		private void GenFromFile(object obj) {
			GameBoardViewModel gameBoardViewModel = new GameBoardViewModel();
			GameBoardModel gameBoard;
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Json files (*.json) | *.json";
			if (openFileDialog.ShowDialog() == true) {
				gameBoard = gameBoardGenerator.GenerateFromJson(gameBoardViewModel, openFileDialog.FileName);
				if (!gameBoard.Success) {
					MessageBox.Show(gameBoard.FailureMessage);
					return;
				}
				MainViewModel.StartGame(gameBoard, gameBoardViewModel);
			}
		}
		private bool GenFromFileCanExecute(object obj) {
			return MainViewModel != null;
		}
	}
}
