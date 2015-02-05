// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

using System.Collections.Generic;
using System.Linq;
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

        public PitchTrends()
        {
            InitializeComponent();
        }

        private void LoadChartContents()
        {
            var pitches = _controller.GetPitchesForStatisticsScreen(0, 0).ToList();

            var lineSeries = LineChart.Series[0] as LineSeries;
            if (lineSeries != null)
                lineSeries.ItemsSource = pitches.Select(x => x.Velocity);
        }

        private void LoadComboBoxes()
        {
            Type.ItemsSource = _pitcherController.GetPitchTypesKnownByPitcher(0);
            Statistic.ItemsSource = new List<string> {"Velocity", "Break"};
        }
    }
}
