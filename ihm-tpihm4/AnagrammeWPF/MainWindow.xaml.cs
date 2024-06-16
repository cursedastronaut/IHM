using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace AnagrammeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private const int ATTEMPTS_NUMBER = 5;

        private int     attemptsLeft = ATTEMPTS_NUMBER;
        private int     currentWordIndex = 0;
        private string  attempsLeftString;
        private int     currentGame = 0;
        
        private string[] tabMots;
        //ajouter d'autres propriétés ici si besoin
        //
        //
        //


        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WIN_Main_Loaded(object sender, RoutedEventArgs e)
        {
            tabMots = new string[]{"LUNDI", "MARDI", "MERCREDI", "JEUDI", "VENDREDI", "SAMEDI", "DIMANCHE"};
            attempsLeftString = LABEL_AttemptsLeft.Content.ToString();
            nouvellePartie();
		}

        //ajouter vos autres méthodes ici
        //
        //
        //
        static string melanger(string chaine)
        {
            Random rng = new Random();
            char[] characters = chaine.ToCharArray();
            int n = characters.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                char value = characters[k];
                characters[k] = characters[n];
                characters[n] = value;
            }
            return new string(characters);
        }

        private void nouvellePartie()
        {
            attemptsLeft = ATTEMPTS_NUMBER;
            TXT_Proposition.Text = "";
			LABEL_AttemptsLeft.Content = attempsLeftString + attemptsLeft;
			LIST_Attempts.Items.Clear();
			++currentGame;
            Random rng = new Random();
            currentWordIndex = rng.Next(tabMots.Length - 1);
            LABEL_Anagram.Content = melanger(tabMots[currentWordIndex]);
        }

        private void motCorrect()
        {
			if (
				MessageBox.Show(
					"Bravo, veux-tu rejouer?",
					"Gagné",
					MessageBoxButton.YesNo,
					MessageBoxImage.Question
				)
				== MessageBoxResult.No
		    )
			{
				Application.Current.Shutdown();
			}
            LIST_History.Items.Add("Partie " + currentGame + " - " + tabMots[currentWordIndex] + " - Gagnée - " + (ATTEMPTS_NUMBER - attemptsLeft));
            nouvellePartie();
		}

        private void motIncorrect()
        {
			if (attemptsLeft == 0)
            {
				if (
					MessageBox.Show(
						"Perdu, le mot était "
						+ tabMots[currentWordIndex]
						+ ". Veux-tu rejouer?",
						"Perdu!",
						MessageBoxButton.YesNo,
						MessageBoxImage.Question
					)
					== MessageBoxResult.No
				)
				{
					Application.Current.Shutdown();
				}
				LIST_History.Items.Add("Partie " + currentGame + " - " + tabMots[currentWordIndex] + " - Perdue - " + (ATTEMPTS_NUMBER - attemptsLeft));
				nouvellePartie();
            } else
            {
                --attemptsLeft;
                LIST_Attempts.Items.Add(TXT_Proposition.Text);
                TXT_Proposition.Text = "";
            }

        }

        private void BTN_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (tabMots[currentWordIndex] == TXT_Proposition.Text)
                motCorrect();
            else
                motIncorrect();
            LABEL_AttemptsLeft.Content = attempsLeftString + attemptsLeft;
        }

        private void TXT_Proposition_TextChanged(object sender, TextChangedEventArgs e)
        {
            TXT_Proposition.Text = TXT_Proposition.Text.ToUpper();
        }

		private void BTN_StartOver_Click(object sender, RoutedEventArgs e)
		{
			if (
					MessageBox.Show(
						"Ceci annulera la partie en cours, es-tu sûr?",
						"Recommencer?",
						MessageBoxButton.YesNo,
						MessageBoxImage.Question
					)
					== MessageBoxResult.Yes
			)
            {
				LIST_History.Items.Add("Partie " + currentGame + " - " + tabMots[currentWordIndex] + " - Perdue - " + (ATTEMPTS_NUMBER - attemptsLeft));
                nouvellePartie();
			}
		}

		private void BTN_Quit_Click(object sender, RoutedEventArgs e)
		{
			if (
					MessageBox.Show(
						"Es-tu sûr de vouloir quitter?",
						"Quitter?",
						MessageBoxButton.YesNo,
						MessageBoxImage.Question
					)
					== MessageBoxResult.Yes
			)
			{
                Application.Current.Shutdown();
			}
		}
	}
}
