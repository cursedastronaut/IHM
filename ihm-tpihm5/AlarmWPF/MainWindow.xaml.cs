using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Media;

namespace AlarmWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Un cercle
        private Ellipse ellipse;
        //3 aiguilles
        private Line minutes;
        private Line hours;
        private Line seconds;
        //timer
        DispatcherTimer timer;
        private List<int> alarmDisabled = new List<int>();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            //Définit combien de secondes entre chaque déclenchement de l'événement Tick 
            timer.Interval = TimeSpan.FromSeconds(1);
            //Associe une procédure événementielle à l'événement Tick du Timer, il vous faut écrire cette procédure événementielle
            timer.Tick += clockUpdate;
            timer.Tick += alertUpdate;
            //Lance le Timer, obligatoire sinon rien ne se passe
            timer.Start();

            ellipse = new Ellipse();
            CNVClock.Children.Add(ellipse);
            ellipse.Width = CNVClock.Width;
            ellipse.Height = CNVClock.Height;
            ellipse.Stroke = Brushes.Gray; ellipse.StrokeThickness = 1;

            //Seconds
            seconds = new Line();
            CNVClock.Children.Add(seconds);
            seconds.Stroke = Brushes.Red; seconds.StrokeThickness = 1;
            //Le point d'origine est au centre du cercle
            seconds.X1 = ellipse.Width / 2;
            seconds.Y1 = ellipse.Height / 2;

            minutes = new Line();
            CNVClock.Children.Add(minutes);
            minutes.Stroke = Brushes.Blue; minutes.StrokeThickness = 1;
            //Le point d'origine est au centre du cercle
            minutes.X1 = ellipse.Width / 2;
            minutes.Y1 = ellipse.Height / 2;

            hours = new Line();
            CNVClock.Children.Add(hours);
            hours.Stroke = Brushes.Green; hours.StrokeThickness = 1;
            //Le point d'origine est au centre du cercle
            hours.X1 = ellipse.Width / 2;
            hours.Y1 = ellipse.Height / 2;

            clockUpdateSeconds();
            clockUpdateMinutes();
            clockUpdateHours();
        }

        private void clockUpdate(object? sender, EventArgs e)
        {
            clockUpdateSeconds();
            clockUpdateMinutes();
            clockUpdateHours();
        }

        private void alertUpdate(object? sender, EventArgs e)
        {
            for (int i = 0; i < LST_AlertTimes.Items.Count; ++i)
            {
                bool isAlarmDisabled = false;
                int index = -1; // Index in isAlarmDisabled
                for (int j = 0; !isAlarmDisabled && j < alarmDisabled.Count; ++j)
                {
                    isAlarmDisabled = (alarmDisabled[j] == i);
                    index = j;
                }
                string currentHour = DateTime.Now.Hour.ToString();
                if (currentHour.Length == 1)
                    currentHour = '0' + currentHour;
                string currentMinute = DateTime.Now.Minute.ToString();
                if (currentMinute.Length == 1)
                    currentMinute = '0' + currentMinute;


                bool isItAlarmTime = ((currentHour + ":" + currentMinute) == (string)LST_AlertTimes.Items.GetItemAt(i));

                if (!isAlarmDisabled && isItAlarmTime)
                {
                    SoundPlayer alarmSound = new SoundPlayer("alarm.wav");
                    alarmSound.Play();
                    MessageBox.Show("Time: " + TXT_Hour.Text + ":" + TXT_Minutes.Text + "\nAlarm n°" + i + " is ringing!", "Alert");
                    alarmDisabled.Add(i);
                } else if (isAlarmDisabled && !isItAlarmTime)
                {
                    alarmDisabled.RemoveAt(index);
                }

            }
        }

        private void clockUpdateMinutes()
        {
            double longueurAiguille = ellipse.Width / 2;
            minutes.X2 = ellipse.Width / 2 + Math.Cos(15 * Math.PI / 30 - DateTime.Now.Minute * Math.PI / 30) * longueurAiguille;
            minutes.Y2 = ellipse.Height / 2 + Math.Sin(-15 * Math.PI / 30 + DateTime.Now.Minute * Math.PI / 30) * longueurAiguille;
        }

        private void clockUpdateSeconds()
        {
            double longueurAiguille = ellipse.Width / 2;
            seconds.X2 = ellipse.Width / 2 + Math.Cos(15 * Math.PI / 30 - DateTime.Now.Second * Math.PI / 30) * longueurAiguille;
            seconds.Y2 = ellipse.Height / 2 + Math.Sin(-15 * Math.PI / 30 + DateTime.Now.Second * Math.PI / 30) * longueurAiguille;
        }

        private void clockUpdateHours()
        {
            double longueurAiguille = ellipse.Width / 2;
            hours.X2 = ellipse.Width / 2 + Math.Cos(15 * Math.PI / 30 - DateTime.Now.Hour * Math.PI / 30) * longueurAiguille;
            hours.Y2 = ellipse.Height / 2 + Math.Sin(-15 * Math.PI / 30 + DateTime.Now.Hour * Math.PI / 30) * longueurAiguille;
        }

        //Ajout d'une horaire d'alerte
        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            LST_AlertTimes.Items.Add("" + TXT_Hour.Text + ":" + TXT_Minutes.Text);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BTN_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (LST_AlertTimes.Items.Count != 0 && LST_AlertTimes.SelectedIndex >= 0)
                LST_AlertTimes.Items.RemoveAt(LST_AlertTimes.SelectedIndex);
        }
    }
}
