using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizzIUT
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private int count = -1;
		private int[] good = { 0, 0 };
		private int[] bad = { 0, 0 };
		private int player = 0;
		private string[] questions =
		{
			"Comment s'appelle le héros de Dragon Ball Z?",
			"Où se situe votre IUT?",
			"Quelle est la capitale de Corée du Nord?",
			"Quel est la marque de mon avion ?",
			"Le CCRI devrait-il ajouter la synchronisation\nde la configuration Visual Studio?",
			"Quel est le NOM DE FAMILLE du personnage\nprincipal de Yakuza 0?",
			"Est-ce que le Java est interprêté ?",
			"Quel est le premier mot du nom officiel\nde la France?",
			"Ça va ? (dites oui, ou perdez)",
			"Quelle est la valeur maximale d'un entier\nnon signé codé sur 8 bits?"
		};
		private string[] reponses =
		{
			"Sangoku",
			"Orsay",
			"Pyongyang",
			"Cessna",
			"Oui",
			"Kiryu",
			"Oui",
			"République",
			"Oui",
			"255"
		};
		private int[] difficulte =
		{
			1, 1, 1, 5, 2, 4, 5, 2, 3, 0, 1
		};

		private const int LIGHT_MODE = 0; //No need to have a value for dark mode, as not light mode = dark mode

		public MainWindow()
		{
			InitializeComponent();
		}

		private void BTNValider_Click(object sender, RoutedEventArgs e)
		{
			//I added ToLower so the user doesn't have to have perfectly
			//case matching answer. That would be pretty annoying.
			
			if (TBXReponse.Text.ToLower() == reponses[count].ToLower()) //If answer is good
			{
				MessageBox.Show("Bravo! Bonne réponse!");
				good[player]++;
			}
			else
			{
				MessageBox.Show("Mauvaise réponse!\nLa bonne réponse était \"" + reponses[count] + "\"." + player);
				bad[player]++;
			}
			NextQuestion();
			LBLBonnesReponsesValeur.Content = good[0];
			LBLMauvaisesReponsesValeur.Content = bad[0];
			LBLBonnesReponsesValeur_P2.Content = good[1];
			LBLMauvaisesReponsesValeur_P2.Content = bad[1];

            if (player == 0)
                player = 1;
            else
                player = 0;
            
			//Clear the text box
            TBXReponse.Text = String.Empty;
		}

		private void SDRModeNuit_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			bool isLight = (int)SDRModeNuit.Value == LIGHT_MODE;
			WNDFenetrePrincipale.Background = ChooseColor(isLight);
			LBLBonnesReponses.Background = ChooseColor(isLight);
			LBLBonnesReponsesValeur.Background = ChooseColor(isLight);
			LBLMauvaisesReponses.Background = ChooseColor(isLight);
			LBLMauvaisesReponsesValeur.Background = ChooseColor(isLight);
			LBLQuestion.Background = ChooseColor(isLight);
			LBLTitre.Background = ChooseColor(isLight);
			TBXReponse.Background = ChooseColor(isLight);
			BTNValider.Background = ChooseColor(isLight);

			WNDFenetrePrincipale.Foreground = ChooseColor(!isLight);
			LBLBonnesReponses.Foreground = ChooseColor(!isLight);
			LBLBonnesReponsesValeur.Foreground = ChooseColor(!isLight);
			LBLMauvaisesReponses.Foreground = ChooseColor(!isLight);
			LBLMauvaisesReponsesValeur.Foreground = ChooseColor(!isLight);
			LBLQuestion.Foreground = ChooseColor(!isLight);
			LBLTitre.Foreground = ChooseColor(!isLight);
			TBXReponse.Foreground = ChooseColor(!isLight);
			BTNValider.Foreground =	ChooseColor(!isLight);
		}

		private Brush ChooseColor(bool isLight)
		{
			var bc = new BrushConverter();
			//I prefer that to solid Brush Colors, as we can put more colors. However, please note we
			//weren't given any rules.
			return isLight ? (Brush)bc.ConvertFrom("#FFFFFF") : (Brush)bc.ConvertFrom("#000000");
		}

		private void NextQuestion()
		{
            count = new Random().Next(0, questions.Length - 1);
			LBLQuestion.Content = questions[count] + "\nDifficulté: " + difficulte[count];
		}

		private void WNDFenetrePrincipale_Loaded(object sender, RoutedEventArgs e)
		{
			NextQuestion();
        }

        private void BTNQuitter_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir quitter le quizz?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
                System.Windows.Application.Current.Shutdown();
            }
        }

    }
}
