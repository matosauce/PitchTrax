// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using PitchTrax.Controllers;
using PitchTrax.Models;
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

        public PitchTrends()
        {
            InitializeComponent();
        }

        private void LoadChartContents()
        {
            var pitches = _controller.GetPitchesForStatisticsScreen(_pitcherId, 
                _knownPitchesTypes.Single(x => x.PitchTypeName == (string)Type.SelectedItem).PitchTypeId).ToList();
            var lineSeries = LineChart.Series[0] as LineSeries;
            if (lineSeries == null) return;

            if ((string) Statistic.SelectedItem == "Velocity")
            {
                SetAxis(lineSeries, "Velocity");
                lineSeries.ItemsSource =
                    pitches.Select((pitch, index) => new {Index = index + 1, pitch.Velocity}).ToList();
            }
            else if ((string) Statistic.SelectedItem == "Break")
            {
                SetAxis(lineSeries, "Break");
                lineSeries.ItemsSource =
                    pitches.Select((pitch, index) => new { Index = index + 1, pitch.Break }).ToList();
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
    }
}
