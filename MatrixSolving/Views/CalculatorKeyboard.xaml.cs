using System.ComponentModel;

namespace MatrixSolving.Views;

public partial class CalculatorKeyboard : ContentView
{
    public event EventHandler TextChanged;
    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
            OnPropertyChanged();
            TextChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    private string _text;
    public CalculatorKeyboard()
    {
        InitializeComponent();
        Text = "";
        EquationEntry.SetBinding(Entry.TextProperty, "Text");
        BindingContext = this;
    }

    private void Add_Character(object sender, EventArgs e)
    {
        Text += ((Button)sender).Text;
    }

    private void Delete_Pressed(object sender, EventArgs e)
    {
        if (Text.Length > 0)
            Text = Text.Remove(Text.Length - 1, 1);
    }

    public event EventHandler SolveClicked;
    private void Solve_Clicked(object sender, EventArgs e)
    {
        SolveClicked?.Invoke(this, EventArgs.Empty);
    }
}