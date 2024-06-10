namespace Calculator;

/// <summary>
/// Interaction logic for LabelWithBorder.xaml
/// </summary>
public partial class LabelWithBorder
{
    public LabelWithBorder()
    {
        InitializeComponent();
    }

    public string Text
    {
        get
        {
            return (string)Welcome.Content;
        }
        set
        {
            Welcome.Content = value;
        }
    }
}