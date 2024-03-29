﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace HealthCheck_ConsoleApp
{
    public partial class HealthCheck
    {
        /// <summary>
        /// model input class for HealthCheck.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"col0")]
            public string Col0 { get; set; }

            [ColumnName(@"col1")]
            public string Col1 { get; set; }

            [ColumnName(@"col2")]
            public string Col2 { get; set; }

            [ColumnName(@"col3")]
            public string Col3 { get; set; }

            [ColumnName(@"col4")]
            public string Col4 { get; set; }

            [ColumnName(@"col5")]
            public string Col5 { get; set; }

            [ColumnName(@"col6")]
            public string Col6 { get; set; }

            [ColumnName(@"col7")]
            public string Col7 { get; set; }

            [ColumnName(@"col8")]
            public string Col8 { get; set; }

            [ColumnName(@"col9")]
            public string Col9 { get; set; }

            [ColumnName(@"col10")]
            public string Col10 { get; set; }

            [ColumnName(@"col11")]
            public string Col11 { get; set; }

            [ColumnName(@"col12")]
            public string Col12 { get; set; }

            [ColumnName(@"col13")]
            public string Col13 { get; set; }

            [ColumnName(@"col14")]
            public string Col14 { get; set; }

            [ColumnName(@"col15")]
            public string Col15 { get; set; }

            [ColumnName(@"col16")]
            public string Col16 { get; set; }

            [ColumnName(@"col17")]
            public string Col17 { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for HealthCheck.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName("PredictedLabel")]
            public string Prediction { get; set; }

            public float[] Score { get; set; }
        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("HealthCheck.zip");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        public static string[] SecondPrediction(string[] input, string[] dataset)
        {
            List<int> output = new List<int>();
            List<int> output2 = new List<int>();
            foreach (var item in dataset)
            {
                int o = 0;
                foreach(var symp in input)
                {
                    if (item.Contains(symp))
                        o++;
                }
                output.Add(o);
                output2.Add(o);
            }
            int firstIndex;

            firstIndex = output.IndexOf(output.Max());
            string first = dataset[firstIndex].Split(',')[0];
            output.RemoveAt(firstIndex);

            firstIndex = output.IndexOf(output.Max());
            string second = dataset[firstIndex].Split(',')[0];
            output.RemoveAt(firstIndex);

            firstIndex = output.IndexOf(output.Max());
            string third = dataset[firstIndex].Split(',')[0];
            output.RemoveAt(firstIndex);


            return new[] {first.Trim(), second.Trim(), third.Trim() }.Distinct().ToArray();
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
