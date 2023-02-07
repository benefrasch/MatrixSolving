using MatrixSolver.Calculation;
using Microsoft.Maui.Graphics;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;

namespace MatrixSolving.Views;

public partial class MatrixPage : ContentPage
{
    private readonly int size;
    private readonly MatrixEntry[,] leftSide;
    private readonly MatrixEntry[] rightSide;
    private readonly CalculatorKeyboard calculatorKeyboard;
    private MatrixEntry CurrentEntry
    {
        set
        {
            _currentEntry?.SetBorderColor(Colors.Gray);
            _currentEntry = value;
            _currentEntry.SetBorderColor(Color.FromArgb("fa1e34"));
        }
        get
        {
            return _currentEntry;
        }
    }
    private MatrixEntry _currentEntry;
    public MatrixPage(int size)
    {
        InitializeComponent();
        this.size = size;

        //add keyboard
        MainStack.Add(calculatorKeyboard = new CalculatorKeyboard(), 0, 1);
        //update text in entry when modifying it through the keyboard
        calculatorKeyboard.TextChanged += delegate
        {
            CurrentEntry.Text = calculatorKeyboard.Text;
        };
        calculatorKeyboard.SolveClicked += SolveSystem;

        //initialize MatrixEntry Arrays
        leftSide = new MatrixEntry[size, size];
        rightSide = new MatrixEntry[size];
        //make grid the size of the matrix
        Grid matrix = new()
        {
            RowDefinitions = new RowDefinitionCollection(),
            ColumnDefinitions = new ColumnDefinitionCollection(),
            VerticalOptions = LayoutOptions.Start,
            RowSpacing = 5,
            ColumnSpacing = 5,
        };
        for (int i = 0; i < size; i++)
        {
            matrix.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            matrix.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
        matrix.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        //populate grid with new entries
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                AddMatrixEntry(matrix, column, row, true);
            }
            AddMatrixEntry(matrix, size, row, false);
        }
        MatrixScrollView.Content = matrix;

    }


    /**
     * Adds a new Matrix entry field at specified corordinates in the Grid
     * also adds it accordingly to the arrays for later use
     * **/
    private void AddMatrixEntry(Grid matrix, int column, int row, bool left)
    {
        MatrixEntry entry = new();
        if (column == 0 && row == 0)
            CurrentEntry = entry;
        entry.Clicked += delegate
        {
            CurrentEntry = entry;
            calculatorKeyboard.Text = entry.Text;
        };
        matrix.Add(entry, column, row);

        if (left)
            leftSide[row, column] = entry;
        else
            rightSide[row] = entry;
    }

    private void SolveSystem(object sender, System.EventArgs e)
    {
        double[,] left = new double[size, size];
        double[] right = new double[size];
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < size; column++)
            {
                left[row, column] = leftSide[row, column].Value;
            }
            right[row] = rightSide[row].Value;
        }

        double[] result = Calculations.GaussianElimination(left, right);
        this.ShowPopup(new ResultView(result));
    }
}