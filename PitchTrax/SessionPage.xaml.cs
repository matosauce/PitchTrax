using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using PitchTrax.Controllers;
using PitchTrax.Models;
using WinRTXamlToolkit.Controls.Extensions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PitchTrax
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionPage
    {
        private int _pitcherId;
        private int _sessionId;

        private const int ErrorInt = -1;

        private SessionController _sessionController;
        private PitchController _pitchController;
        private PitcherController _pitcherController;

        public SessionPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (args.Parameter != null)
                _pitcherId = (int) args.Parameter;

            if (_pitcherId == -1)
                Frame.Navigate(typeof (MainPage));

            _sessionController = new SessionController();
            _sessionId = _sessionController.CreateNewSession(_pitcherId, DateTime.Now);
            _pitcherController = new PitcherController();
            _pitchController = new PitchController();
            FillPitcherData();
            InitializeSessionHistoryPanel();
        }

        private void InitializeSessionHistoryPanel()
        {
            DateTextBlock.Text = DateTime.Now.ToString();
        }

        private void FillPitcherData()
        {
            var pitcher = _pitcherController.GetPitcherById(_pitcherId);
            PitcherNameBlock.Text = pitcher.LastName + ", " + pitcher.FirstName;
            JerseyNumberBlock.Text = pitcher.JerseyNumber.ToString();
            HandednessBlock.Text = pitcher.Handedness;
            FillKnownPitchList(pitcher);
        }

        private void FillKnownPitchList(Pitcher pitcher)
        {
            var knownPitchTypes = _pitcherController.GetPitchTypesKnownByPitcher(pitcher.PitcherId);

            foreach (var pitchNameButton in knownPitchTypes.Select(knownPitchType => new RadioButton
            {
                Content = knownPitchType.PitchTypeName,
                DataContext = knownPitchType.PitchTypeId,
                FontSize = 20,
                GroupName = "KnownPitches"
            }))
            {
                KnowPitchList.Children.Add(pitchNameButton);
            }
        }

        private void Zone_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var zone = GetZoneNumber(button);

            var selectedPitchTypeRadioButton =
                KnowPitchList.Children.FirstOrDefault(x => x is RadioButton && (((RadioButton) x).IsChecked ?? false)) as RadioButton;

            var pitchTypeId = ErrorInt;
            if (selectedPitchTypeRadioButton != null)
            {
                pitchTypeId = (int) selectedPitchTypeRadioButton.DataContext;
            }

            int velocity;
            try
            {
                velocity = Convert.ToInt32(VelocityTextBox.Text);
            }
            catch (FormatException)
            {
                velocity = ErrorInt;
            }

            int breakAmount;
            try
            {
                breakAmount = Convert.ToInt32(BreakTextBox.Text);
            }
            catch (FormatException)
            {
                breakAmount = ErrorInt;
            }

            var pitch = _pitchController.ThrowNewPitch(_pitcherId, pitchTypeId, _sessionId, velocity, zone, breakAmount);
            UpdateSessionHistoryPanel(pitch);
        }

        private void UpdateSessionHistoryPanel(Pitch pitch)
        {
            const int fontSize = 30;
            const int indexAfterHeader = 1;

            var oldInt = Convert.ToInt32(NumberOfPitchesTextBlock.Text) + 1;
            NumberOfPitchesTextBlock.Text = oldInt.ToString();

            var pitchType = _pitchController.GetPitchTypeByPitchId(pitch.PitchId);

            var pitchTypeNameTextBlock = new TextBlock
            {
                Text = pitchType.PitchTypeName,
                FontSize = fontSize,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var velocityTextBlock = new TextBlock
            {
                Text = pitch.Velocity == ErrorInt ? "n/a" : pitch.Velocity + " MPH",
                FontSize = fontSize,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            
            var pitchGrid = new Grid();
            pitchGrid.Children.Add(pitchTypeNameTextBlock);
            pitchGrid.Children.Add(velocityTextBlock);

            SessionHistoryPanel.Children.Insert(1, pitchGrid);
        }

        private static int GetZoneNumber(Button button)
        {
            if (button == null)
                return ErrorInt;

            var name = button.Name;
            var lastChar = name.Substring(name.Length - 1);
            var zone = Convert.ToInt32(lastChar);
            return (0 <= zone && zone <= 8) ? zone : ErrorInt;
        }

        private void CloseSessionButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (MainPage));
        }
    }
}
