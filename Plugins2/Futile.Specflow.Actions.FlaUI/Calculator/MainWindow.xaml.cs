using System.Windows;

namespace Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow(string commandLineArgs)
    {
        InitializeComponent();

        if (!string.IsNullOrEmpty(commandLineArgs))
        {
            WelcomeLabel.Text = commandLineArgs;
        }
    }

    private void OnAddClick(object sender, RoutedEventArgs e)
    {
        Result.Text = $"{(double.Parse(First.Text) + double.Parse(Second.Text)):0.00}";
    }

    private void OnSubtractClick(object sender, RoutedEventArgs e)
    {
        Result.Text = $"{(double.Parse(First.Text) - double.Parse(Second.Text)):0.00}";
    }

    private void OnMultiplyClick(object sender, RoutedEventArgs e)
    {
        Result.Text = $"{(double.Parse(First.Text) * double.Parse(Second.Text)):0.00}";
    }

    private void OnDivideClick(object sender, RoutedEventArgs e)
    {
        Result.Text = $"{(double.Parse(First.Text) / double.Parse(Second.Text)):0.00}";
    }
}