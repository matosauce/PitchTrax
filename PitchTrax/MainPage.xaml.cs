using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using PitchTrax.Controllers;
using PitchTrax.DAOs;
using PitchTrax.Models;
using PitchTrax.SQLite;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PitchTrax
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        private PitcherController _controller = new PitcherController();
        public MainPage()
        {
            InitializeComponent();
            RefreshPitcherList();
        }

        private void AddPitcherButton_OnClick(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            PitcherId.Text = "-1";
            FirstName.Text = string.Empty;
            LastName.Text = string.Empty;

            LeftRadioButton.IsChecked = false;
            RightRadioButton.IsChecked = false;

            JerseyNumber.Text = string.Empty;
            var connection = new PitchTraxDatabase().GetAsyncConnection();
            var pitchTypeDao = new PitchTypeDAO();
            var availablePitchTypes = pitchTypeDao.GetAllPitchTypes(connection);
            if (AvailablePitchTypes.Items == null || KnownPitchTypes.Items == null) return;
            AvailablePitchTypes.Items.Clear();
            KnownPitchTypes.Items.Clear();
            foreach (var t in availablePitchTypes)
                AvailablePitchTypes.Items.Add(new ListBoxItem { Content = t.PitchTypeName, DataContext = t.PitchTypeId});
        }

        private void AddPitcherToLeft(Pitcher pitcher)
        {
            var pitcherButton = new Button { Content = pitcher.FirstName + " " + pitcher.LastName, FontSize = 20, MinHeight = 70, Margin = new Thickness(5), MinWidth = 400, DataContext = pitcher.PitcherId };
            pitcherButton.Click += ShowPitcherInfo;
            var deleteButton = new Button { Content = "Remove", FontSize = 20, MinHeight = 70, Margin = new Thickness(5), MinWidth = 40, DataContext = pitcher.PitcherId };
            deleteButton.Click += RemovePitcher;
            var sp = new StackPanel {Orientation = Orientation.Horizontal};
            sp.Children.Add(pitcherButton);
            sp.Children.Add(deleteButton);
            PitcherPanel.Children.Add(sp);
        }

        private void RemovePitcher(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            var pitcherId = (int)button.DataContext;
            var connection = new PitchTraxDatabase().GetAsyncConnection();
            var pitcherDao = new PitcherDAO();
            pitcherDao.DeleteExistingPitcher(connection, pitcherId);
            RefreshPitcherList();
        }

        private void RefreshPitcherList()
        {
            PitcherPanel.Children.Clear();

            var connection = new PitchTraxDatabase().GetAsyncConnection();
            var pitcherDao = new PitcherDAO();

            var pitchers = pitcherDao.GetAllPitchers(connection);
            foreach (var p in pitchers)
            {
                AddPitcherToLeft(p);
            }
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
            var connection = new PitchTraxDatabase().GetAsyncConnection();
            var pitcherDao = new PitcherDAO();

            var pitcher = pitcherDao.GetPitcherById(connection, pitcherId);

            PitcherId.Text = pitcher.PitcherId.ToString();
            FirstName.Text = pitcher.FirstName;
            LastName.Text = pitcher.LastName;

            if (pitcher.Handedness == "L")
                LeftRadioButton.IsChecked = true;
            else
                RightRadioButton.IsChecked = true;

            JerseyNumber.Text = pitcher.JerseyNumber.ToString();

            ////TODO: replace with call to DB to get all available pitch types
            //var typesAvailable = new List<PitchType>
            //{
            //    new PitchType{PitchTypeId = 1, PitchTypeName = "Fastball"},
            //    new PitchType{PitchTypeId = 2, PitchTypeName = "Curveball"},
            //    new PitchType{PitchTypeId = 3, PitchTypeName = "Knuckleball"},
            //    new PitchType{PitchTypeId = 4, PitchTypeName = "Slider"},
            //    new PitchType{PitchTypeId = 5, PitchTypeName = "12-6 Curve"},
            //    new PitchType{PitchTypeId = 5, PitchTypeName = "Circle-Change"}
            //};
            var pitchTypeDao = new PitchTypeDAO();
            var availablePitchTypes = pitchTypeDao.GetAllPitchTypes(connection);
            if (AvailablePitchTypes.Items == null || KnownPitchTypes.Items == null) return;
            AvailablePitchTypes.Items.Clear();
            KnownPitchTypes.Items.Clear();
            foreach (var t in availablePitchTypes)
            {
            //    if (pitcher.KnownPitches.Any(x => x.PitchTypeId == t.PitchTypeId))
            //    {
                //KnownPitchTypes.Items.Add(new ListBoxItem { Content = t.PitchTypeName });
            //    }
            //    else
            //    {
                AvailablePitchTypes.Items.Add(new ListBoxItem { Content = t.PitchTypeName, DataContext = t.PitchTypeId });
            //    }
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

            _controller.InsertPitcher(PitcherId.Text, FirstName.Text,
                LastName.Text, JerseyNumber.Text, ((LeftRadioButton.IsChecked ?? false) ? "L" : "R"));
            
            RefreshPitcherList();
            ClearInputs();
        }

        private void StartSessionButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO: Start the Session Screen
        }
    }
}
