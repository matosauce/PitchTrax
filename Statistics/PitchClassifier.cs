﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Statistics
{
    //http://www.codeproject.com/Articles/318126/Naive-Bayes-Classifier
    public class PitchClassifier
    {
        private DataSet _dataSet = new DataSet();

        public DataSet DataSet
        {
            get { return _dataSet; }
            set { _dataSet = value; }
        }

        public void TrainClassifier(DataTable table)
        {
            _dataSet.Tables.Add(table);

            //table
            var gaussianDistribution = _dataSet.Tables.Add("Gaussian");
            gaussianDistribution.Columns.Add(table.Columns[0].ColumnName);

            //columns
            for (int i = 1; i < table.Columns.Count; i++)
            {
                gaussianDistribution.Columns.Add(table.Columns[i].ColumnName + "Mean");
                gaussianDistribution.Columns.Add(table.Columns[i].ColumnName + "Variance");
            }

            //calc data
            var results = (from myRow in table.AsEnumerable()
                group myRow by myRow.Field<string>(table.Columns[0].ColumnName)
                into g
                select new {Name = g.Key, Count = g.Count()}).ToList();

            foreach (var t in results)
            {
                var row = gaussianDistribution.Rows.Add();
                row[0] = t.Name;

                var a = 1;
                for (var i = 1; i < table.Columns.Count; i++)
                {
                    row[a] = SelectRows(table, i, string.Format("{0} = '{1}'",
                        table.Columns[0].ColumnName, t.Name)).Mean();
                    row[++a] = SelectRows(table, i,
                        string.Format("{0} = '{1}'",
                            table.Columns[0].ColumnName, t.Name)).Variance();
                    a++;
                }
            }
        }

        public IEnumerable<double> SelectRows(DataTable table, int column, string filter)
        {
            var rows = table.Select(filter);
            return rows.Select(t => (double) t[column]).ToList();
        }


        public string Classify(double[] obj)
        {
            var score = new Dictionary<string, double>();

            var results = (from myRow in _dataSet.Tables[0].AsEnumerable()
                group myRow by myRow.Field<string>(
                    _dataSet.Tables[0].Columns[0].ColumnName)
                into g
                select new {Name = g.Key, Count = g.Count()}).ToList();

            for (var i = 0; i < results.Count; i++)
            {
                var subScoreList = new List<double>();
                int a = 1, b = 1;
                for (var k = 1; k < _dataSet.Tables["Gaussian"].Columns.Count; k = k + 2)
                {
                    var mean = Convert.ToDouble(_dataSet.Tables["Gaussian"].Rows[i][a]);
                    var variance = Convert.ToDouble(_dataSet.Tables["Gaussian"].Rows[i][++a]);
                    var result = MathFunction.NormalDist(obj[b - 1], mean, MathFunction.SquareRoot(variance));
                    subScoreList.Add(result);
                    a++;
                    b++;
                }

                double finalScore = 0;
                foreach (var t in subScoreList)
                {
                    if (finalScore == 0)
                    {
                        finalScore = t;
                        continue;
                    }

                    finalScore = finalScore*t;
                }

                score.Add(results[i].Name, finalScore*0.5);
            }

            var maxOne = score.Max(c => c.Value);
            var name = (from c in score
                where c.Value == maxOne
                select c.Key).First();

            return name;
        }
    }
}