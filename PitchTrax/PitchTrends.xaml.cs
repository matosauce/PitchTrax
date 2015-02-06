// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
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
            if (lineSeries != null)
                lineSeries.ItemsSource = pitches.Select(x => x.Velocity);
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

        private void Type_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadChartContents();
        }
    }
}
