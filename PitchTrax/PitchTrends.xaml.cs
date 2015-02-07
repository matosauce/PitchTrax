// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using PitchTrax.Controllers;
using PitchTrax.Models;
using Stats;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace PitchTrax
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PitchTrends
    {
        private readonly PitchController _controller = new PitchController();
        private readonly PitcherController _pitcherController = new PitcherController();
        private int _pitcherId;
        private List<PitchType> _knownPitchesTypes;
        private const int ErrorInt = -1;

        public PitchTrends()
        {
            InitializeComponent();
        }

        private void LoadChartContents()
        {
            var pitches = _controller.GetPitchesForStatisticsScreen(_pitcherId, 
                _knownPitchesTypes.Single(x => x.PitchTypeName == (string)Type.SelectedItem).PitchTypeId).ToList();
            var lineSeries = LineChart.Series[0] as LineSeries;
            var trend = LineChart.Series[1] as LineSeries;
            if (lineSeries == null) return;

            if ((string) Statistic.SelectedItem == "Velocity")
            {
                SetAxis(lineSeries, "Velocity");
                SetAxis(trend, "Velocity");
                var points =
                    pitches.Select((pitch, index) => new {Index = (double)index + 1, Velocity = (double)pitch.Velocity}).ToList();
                var trendLine = LineFitting.FitLine(points.Select(x => x.Index).ToArray(), points.Select(x => x.Velocity).ToArray(), 1).ToList();
                var trends = new List<double>();
                for (var i = 0; i < points.Count; i++)
                {
                    trends.Add(i*trendLine[1] + trendLine[0]);
                }
                lineSeries.ItemsSource = points;
                if (trend != null)
                    trend.ItemsSource = trends.Select((pitch, index) => new { Index = (double)index + 1, Velocity = pitch }).ToList();
            }
            else if ((string) Statistic.SelectedItem == "Break")
            {
                SetAxis(lineSeries, "Break");
                SetAxis(trend, "Break");
                var points =
                    pitches.Select((pitch, index) => new { Index = (double)index + 1, Break = (double)pitch.Break }).ToList();
                var trendLine = LineFitting.FitLine(points.Select(x => x.Index).ToArray(), points.Select(x => x.Break).ToArray(), 1).ToList();
                var trends = new List<double>();
                for (var i = 0; i < points.Count; i++)
                {
                    trends.Add(i * trendLine[1] + trendLine[0]);
                }
                lineSeries.ItemsSource = points;
                if (trend != null)
                    trend.ItemsSource = trends.Select((pitch, index) => new { Index = (double)index + 1, Break = pitch }).ToList();
            }
        }

        private static void SetAxis(LineSeries series, string dependent)
        {
            series.DependentValuePath = dependent;
            series.IndependentValuePath = "Index";
        }

        private void LoadComboBoxes()
        {
            Type.ItemsSource = _knownPitchesTypes.Select(x => x.PitchTypeName).ToList();
            Statistic.ItemsSource = new List<string> {"Velocity", "Break"};
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                _pitcherId = (int) e.Parameter;
            }
            
            _knownPitchesTypes = _pitcherController.GetPitchTypesKnownByPitcher(_pitcherId).ToList();
            LoadComboBoxes();
        }

        private void Statistic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Type.SelectedItem != null)
            {
                LoadChartContents();
            }
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Statistic.SelectedItem != null)
            {
                LoadChartContents();
            }
        }


        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (_pitcherId != ErrorInt)
                Frame.Navigate(typeof(HotColdZone), _pitcherId);
        }
    }
}
