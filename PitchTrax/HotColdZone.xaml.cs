using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using Windows.UI.Xaml.Navigation;
using PitchTrax.Controllers;

namespace PitchTrax
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HotColdZone
    {
        private Dictionary<Button, int> _zones;
        private int _pitcherId;
        private readonly PitchController _controller = new PitchController();
        private const double FiftyPercent = .5;
        private const double TwentyPercent = .2;
        private const double TenPercent = .1;

        public HotColdZone()
        {
            InitializeComponent();
            AddZonesToList();
        }

        private void AddZonesToList()
        {
            _zones = new Dictionary<Button, int> { 
            { Zone0, 0 }, 
            { Zone1, 0 }, 
            { Zone2, 0 },
            { Zone3, 0 },
            { Zone4, 0 },
            { Zone5, 0 },
            { Zone6, 0 },
            { Zone7, 0 },
            { Zone8, 0 }};
        }

        private void FillZoneColors()
        {
            var pitches = _controller.GetAllPitchesByOnePitcher(_pitcherId).ToList();
            foreach (var pitch in pitches)
            {
                if (pitch.Zone == 0)
                    _zones[Zone0]++;
                if (pitch.Zone == 1)
                    _zones[Zone1]++;
                if (pitch.Zone == 2)
                    _zones[Zone2]++;
                if (pitch.Zone == 3)
                    _zones[Zone3]++;
                if (pitch.Zone == 4)
                    _zones[Zone4]++;
                if (pitch.Zone == 5)
                    _zones[Zone5]++;
                if (pitch.Zone == 6)
                    _zones[Zone6]++;
                if (pitch.Zone == 7)
                    _zones[Zone7]++;
                if (pitch.Zone == 8)
                    _zones[Zone8]++;
            }
            foreach (var zone in _zones.Keys)
            {
                if (_zones[zone]/(double) pitches.Count() > FiftyPercent)
                {
                    zone.Background = new SolidColorBrush(Colors.DarkRed);
                }
                else if (_zones[zone]/(double) pitches.Count() > TwentyPercent)
                {
                    zone.Background = new SolidColorBrush(Colors.Red);
                }
                else if (_zones[zone] / (double)pitches.Count() > TenPercent)
                {
                    zone.Background = new SolidColorBrush(Colors.DeepSkyBlue);
                }
                else
                {
                    zone.Background = new SolidColorBrush(Colors.DarkBlue);
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                _pitcherId = (int)e.Parameter;
            }
            FillZoneColors();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (PitchTrends), _pitcherId);
        }
    }
}
