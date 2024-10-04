using System;
using System.Windows;
using System.Windows.Controls;

namespace ExamenUA1
{
    // JAHSWANT FODOUOP TAKOGUEM
    // Classe principale représentant la fenêtre de l'application
    public partial class MainWindow : Window
    {
        // Variables pour stocker la valeur courante, la valeur précédente et l'opérateur choisi
        private double _currentValue = 0;
        private double _previousValue = 0;
        private string _operator = string.Empty; // Opérateur actuellement sélectionné
        private bool _isOperatorClicked = false; // Indicateur pour savoir si un opérateur a été cliqué
        private Button selectedOperatorButton = null; // Track the selected operator button


        // Constructeur de la fenêtre principale
        public MainWindow()
        {
            InitializeComponent(); // Initialise les composants de l'interface utilisateur
            Display.Text = _currentValue.ToString();
        }

        // Méthode appelée lors du clic sur le bouton "C" pour effacer l'affichage et réinitialiser les variables
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _currentValue = 0; // Réinitialise la valeur courante
            _previousValue = 0; // Réinitialise la valeur précédente
            _operator = string.Empty; // Réinitialise l'opérateur
            Display.Text = "0"; // Réinitialise l'affichage à 0
            _isOperatorClicked = false; // Réinitialise l'état du clic sur un opérateur
            ResetOperatorButtonStyle();
        }

        // Méthode appelée lors du clic sur le bouton "+/-" pour inverser le signe de la valeur affichée
        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Display.Text, out double result))
            {
                result = -result; // Inverse le signe de la valeur
                Display.Text = result.ToString(); // Met à jour l'affichage avec la nouvelle valeur
                _currentValue = result; // Met à jour la valeur courante
            }
        }

        // Méthode appelée lors du clic sur le bouton "%" pour convertir la valeur affichée en pourcentage
        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Display.Text, out double result))
            {
                result /= 100; // Divise la valeur par 100 pour obtenir le pourcentage
                Display.Text = result.ToString(); // Met à jour l'affichage avec la nouvelle valeur
                _currentValue = result; // Met à jour la valeur courante
            }
        }

        // Méthode appelée lors du clic sur un bouton numérique
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            ResetOperatorButtonStyle();

            var button = sender as System.Windows.Controls.Button; // Récupère le bouton cliqué
            string number = button.Content.ToString(); // Extrait le contenu (le chiffre)

            // Si l'affichage est à 0 ou qu'un opérateur a été cliqué, on remplace l'affichage par le chiffre cliqué
            if (Display.Text == "0" || _isOperatorClicked)
            {
                Display.Text = number;
                _isOperatorClicked = false; // Réinitialise l'indicateur d'opérateur cliqué
            }
            else
            {
                Display.Text += number; // Ajoute le chiffre à la valeur actuelle de l'affichage
            }

            // Met à jour la valeur courante avec la valeur affichée
            if (double.TryParse(Display.Text, out double result))
            {
                _currentValue = result;
            }
        }

        // Méthode appelée lors du clic sur le bouton décimal (".")
        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            // Ajoute un point décimal à l'affichage seulement s'il n'en contient pas déjà un
            if (!Display.Text.Contains("."))
            {
                Display.Text += ".";
            }
        }

        // Méthode appelée lors du clic sur un bouton opérateur (+, -, ×, ÷)
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button; // Récupère le bouton cliqué

            // If another operator was previously selected, reset its style
            ResetOperatorButtonStyle();

            // Track the selected operator button and apply the selected style
            selectedOperatorButton = button;

            selectedOperatorButton.Style = (Style)FindResource("SelectedOperatorButtonStyle");

            
            _operator = button.Content.ToString(); // Stocke l'opérateur cliqué
            _previousValue = _currentValue; // Sauvegarde la valeur courante dans la variable précédente
            _isOperatorClicked = true; // Indique qu'un opérateur a été cliqué
        }

        // Méthode appelée lors du clic sur le bouton "=" pour effectuer le calcul
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            // Exécute l'opération en fonction de l'opérateur sélectionné
            switch (_operator)
            {
                case "+":
                    _currentValue = _previousValue + _currentValue;
                    break;
                case "-":
                    _currentValue = _previousValue - _currentValue;
                    break;
                case "×":
                    _currentValue = _previousValue * _currentValue;
                    break;
                case "÷":
                    // Vérifie si la division par zéro est évitée
                    if (_currentValue != 0)
                    {
                        _currentValue = _previousValue / _currentValue;
                    }
                    else
                    {
                        // Affiche un message d'erreur si une tentative de division par zéro est faite
                        Display.Text = "Erreur";
                        _currentValue = 0; // Réinitialise la valeur courante en cas d'erreur
                    }
                    break;
            }

            // Met à jour l'affichage avec le résultat de l'opération
            Display.Text = _currentValue.ToString();
            _isOperatorClicked = false; // Réinitialise l'état de clic de l'opérateur
            _operator = string.Empty; // Réinitialise l'opérateur
        }

        // Method to reset the selected operator button style
        private void ResetOperatorButtonStyle()
        {
            if (selectedOperatorButton != null)
            {
                selectedOperatorButton.Style = (Style)FindResource("OperatorButtonStyle");
                selectedOperatorButton = null;
            }
        }
    }
}
