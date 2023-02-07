using CommunityToolkit.Maui.Views;

namespace MatrixSolving.Views;

public partial class ResultView : Popup
{
	public ResultView(double[] result)
	{
		InitializeComponent();

		string resultString = "";
		for (int i = 0; i < result.Length; i++)
		{
			resultString += "X" + i + ":  " + result[i]+ '\n';
		}
		resultString = resultString.Trim();
		resultLabel.Text = resultString;
	}


}