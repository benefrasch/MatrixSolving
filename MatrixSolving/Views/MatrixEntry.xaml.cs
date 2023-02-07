using MatrixSolver.Calculation;

namespace MatrixSolving.Views;

public partial class MatrixEntry : ContentView
{
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            var (result, valid) = Calculations.Evaluate(value);
            if (valid)
            {
                cellText.Text = result.ToString();
                Value = result;
            }
            else
                cellText.Text = "-";
        }
    }
    private string _text;

    public double Value { get; set; }


    public MatrixEntry()
    {
        InitializeComponent();

        Text = "";
    }

    public void SetBorderColor(Color newColor)
    {
        cellButton.BorderColor = newColor;
    }

    public event EventHandler Clicked;
    private void CellClicked(object sender, EventArgs e)
    {
        Clicked?.Invoke(this, e);
    }
}
