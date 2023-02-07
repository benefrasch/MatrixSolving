using System.Collections.ObjectModel;

namespace MatrixSolving.Views;

public partial class SizeSelectPage : ContentPage
{
    public class SizeListItem
    {
        public string Text { get; set; }
        public string ImageSource { get; set; }
        public int Size { get; set; }
    }

    public ObservableCollection<SizeListItem> Items { get; set; }

    public SizeSelectPage()
    {
        InitializeComponent();


        Items = new()
            {
                new (){Text= "2x2",ImageSource="matrix2.png",Size= 2 },
                new (){Text= "3x3",ImageSource="matrix3.png",Size= 3 },
                new (){Text= "4x4",ImageSource="matrix4.png",Size= 4 },
                new (){Text= "5x5",ImageSource="matrix5.png",Size= 5 },
            };

        MyListView.ItemsSource = Items;
    }

    async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
            return;

        await Navigation.PushAsync(new MatrixPage(((SizeListItem)e.Item).Size));

        //Deselect Item
        ((ListView)sender).SelectedItem = null;
    }

}