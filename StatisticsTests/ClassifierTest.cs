using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statistics;

namespace StatisticsTests
{
    [TestClass]
    public class ClassifierTest
    {
        [TestMethod]
        public void Classify_ThreePitches_CorrectPitchTypesClassified()
        {
            var table = new DataTable();
            table.Columns.Add("Pitch");
            table.Columns.Add("Velocity", typeof(double));
            table.Columns.Add("Break", typeof(double));

            //training data. 
            table.Rows.Add("Fastball", 90, 2);
            table.Rows.Add("Fastball", 87, 4);
            table.Rows.Add("Fastball", 91, 0);
            table.Rows.Add("Fastball", 95, 1);
            table.Rows.Add("CurveBall", 81, 18);
            table.Rows.Add("CurveBall", 82, 13); 
            table.Rows.Add("CurveBall", 78, 19);
            table.Rows.Add("CurveBall", 80, 18);
            table.Rows.Add("Changeup", 74, 4);
            table.Rows.Add("Changeup", 80, 5);
            table.Rows.Add("Changeup", 71, 2);
            table.Rows.Add("Changeup", 73, 5);

            var classifier = new PitchClassifier();
            classifier.TrainClassifier(table);
            //output would be transgender.
            Assert.AreEqual(classifier.Classify(new double[] {91, 2}), "Fastball");
            Assert.AreEqual(classifier.Classify(new double[] { 69, 7 }), "Changeup");
            Assert.AreEqual(classifier.Classify(new double[] { 83, 14 }), "CurveBall");
        }
    }
}
