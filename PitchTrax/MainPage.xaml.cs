using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PitchTrax
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void AddPitcherButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton("New Pitcher", 3);
        }

        private void AddButton(string name, long id)
        {
            var newButton = new Button { Content = name, FontSize = 20, MinHeight = 70, Margin = new Thickness(5), MinWidth = 400, DataContext = id};
            newButton.Click += ShowPitcherInfo;
            PitcherPanel.Children.Add(newButton);
        }

        private void ShowPitcherInfo(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            var id = (long) button.DataContext;
            BuildGrid(id);
        }

        private void BuildGrid(long id)
        {
            var pitcherInfoGrid = new Grid();
            var pitcher = new {Name = "Sample Pitcher", Pitches = new List<string> {"Fastball", "Curve", "Knuckleball"}};
            pitcherInfoGrid.Children.Add(new TextBlock{Text = pitcher.Name});
            MainPanel.Children.Add(pitcherInfoGrid);
        }
    }
}
