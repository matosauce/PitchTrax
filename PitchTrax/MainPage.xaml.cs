using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using PitchTrax.Models;

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
            //show blank screen on right
            //save should call this method later
            AddButton("New Pitcher", 3);
        }

        private void AddButton(string name, int pitcherId)
        {
            var newButton = new Button { Content = name, FontSize = 20, MinHeight = 70, Margin = new Thickness(5), MinWidth = 400, DataContext = pitcherId};
            newButton.Click += ShowPitcherInfo;
            PitcherPanel.Children.Add(newButton);
        }

        private void ShowPitcherInfo(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            var id = (int) button.DataContext;
            FillPitcherData(id);
        }

        private void FillPitcherData(int pitcherId)
        {
            //TODO: replace with call to DB using pitcherId
            var pitcher = new Pitcher
            {
                PitcherId = 1,
                FirstName = "George",
                LastName = "Costanza",
                JerseyNumber = 44, 
                Handedness = 'R',
                KnownPitches = new List<PitchType>
                {
                    new PitchType { PitchTypeId = 1, PitchTypeName = "Fastball" },
                    new PitchType { PitchTypeId = 2, PitchTypeName = "Curveball" }
                }
            };

            PitcherId.Text = pitcher.PitcherId.ToString();
            FirstName.Text = pitcher.FirstName;
            LastName.Text = pitcher.LastName;

            if (pitcher.Handedness == 'L')
                LeftRadioButton.IsChecked = true;
            else
                RightRadioButton.IsChecked = true;

            JerseyNumber.Text = pitcher.JerseyNumber.ToString();

            //TODO: replace with call to DB to get all available pitch types
            var typesAvailable = new List<PitchType>
            {
                new PitchType{PitchTypeId = 1, PitchTypeName = "Fastball"},
                new PitchType{PitchTypeId = 2, PitchTypeName = "Curveball"},
                new PitchType{PitchTypeId = 3, PitchTypeName = "Knuckleball"},
                new PitchType{PitchTypeId = 4, PitchTypeName = "Slider"},
                new PitchType{PitchTypeId = 5, PitchTypeName = "12-6 Curve"},
                new PitchType{PitchTypeId = 5, PitchTypeName = "Circle-Change"}
            };

            if (AvailablePitchTypes.Items == null || KnownPitchTypes.Items == null) return;
            foreach (var t in typesAvailable)
            {
                if (pitcher.KnownPitches.Any(x => x.PitchTypeId == t.PitchTypeId))
                {
                    KnownPitchTypes.Items.Add(new ListBoxItem { Content = t.PitchTypeName });
                }
                else
                {
                    AvailablePitchTypes.Items.Add(new ListBoxItem { Content = t.PitchTypeName });
                }
            }
        }

        private void AddPitchToPitcher(ListBoxItem selectedPitch)
        {
            if (AvailablePitchTypes.Items == null || KnownPitchTypes.Items == null) return;
            AvailablePitchTypes.Items.Remove(selectedPitch);
            KnownPitchTypes.Items.Add(selectedPitch);
        }

        private void RemovePitchFromPitcher(ListBoxItem selectedPitch)
        {
            if (AvailablePitchTypes.Items == null || KnownPitchTypes.Items == null) return;
            KnownPitchTypes.Items.Remove(selectedPitch);
            AvailablePitchTypes.Items.Add(selectedPitch);
        }

        private void AddPitchTypeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (AvailablePitchTypes.SelectedItems == null) return;
            for (var index = AvailablePitchTypes.SelectedItems.Count - 1; index >= 0; --index)
            {
                var o = AvailablePitchTypes.SelectedItems[index];
                var lbi = o as ListBoxItem;
                if (lbi != null)
                    AddPitchToPitcher(lbi);
            }
        }

        private void RemovePitchTypeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (KnownPitchTypes.SelectedItems == null) return;
            for (var index = KnownPitchTypes.SelectedItems.Count - 1; index >= 0; --index)
            {
                var o = KnownPitchTypes.SelectedItems[index];
                var lbi = o as ListBoxItem;
                if (lbi != null)
                    RemovePitchFromPitcher(lbi);
            }
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {

            var list = new List<PitchType>();//TODO: fill properly with data from UI
            var myPitcher = new Pitcher
            {
                PitcherId = Convert.ToInt32(PitcherId.Text),
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                JerseyNumber = Convert.ToInt32(JerseyNumber.Text),
                Handedness = ((LeftRadioButton.IsChecked??false) ? 'L' : 'R'),
                KnownPitches = list
            };
            //TODO: Send call to update table in DB

        }

        private void StartSessionButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO: Start the Session Screen
        }
    }
}
