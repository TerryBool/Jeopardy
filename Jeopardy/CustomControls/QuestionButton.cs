using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jeopardy.CustomControls {
	public class QuestionButton : Button {

		static QuestionButton() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(QuestionButton), new FrameworkPropertyMetadata(typeof(QuestionButton)));
		}

		public string QuestionCost {
			get => (string)GetValue(QuestionCostProperty);
			set => SetValue(QuestionCostProperty, value);
		}

		public bool WasPressed {
			get => (bool)GetValue(WasPressedProperty);
			set => SetValue(WasPressedProperty, value);
		}

		public bool LastChosen {
			get => (bool)GetValue(LastChosenProperty);
			set => SetValue(LastChosenProperty, value);
		}

		public static readonly DependencyProperty QuestionCostProperty =
			 DependencyProperty.Register("QuestionCost", typeof(string), typeof(QuestionButton));

		public static readonly DependencyProperty WasPressedProperty =
			 DependencyProperty.Register("WasPressed", typeof(bool), typeof(QuestionButton));

		public static readonly DependencyProperty LastChosenProperty =
			 DependencyProperty.Register("LastChosen", typeof(bool), typeof(QuestionButton));
	}
}
