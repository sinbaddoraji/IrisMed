﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace HealthCheck_WebApi
{
    public partial class HealthCheck
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Text.FeaturizeText(@"col1", @"col1")      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col2", @"col2"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col3", @"col3"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col4", @"col4"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col5", @"col5"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col6", @"col6"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col7", @"col7"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col8", @"col8"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col9", @"col9"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col10", @"col10"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col11", @"col11"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col12", @"col12"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col13", @"col13"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col14", @"col14"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col15", @"col15"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col16", @"col16"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col17", @"col17"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"col1",@"col2",@"col3",@"col4",@"col5",@"col6",@"col7",@"col8",@"col9",@"col10",@"col11",@"col12",@"col13",@"col14",@"col15",@"col16",@"col17"}))      
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(@"col0", @"col0"))      
                                    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator:mlContext.BinaryClassification.Trainers.FastForest(new FastForestBinaryTrainer.Options(){NumberOfTrees=4,FeatureFraction=1F,LabelColumnName=@"col0",FeatureColumnName=@"Features"}), labelColumnName: @"col0"))      
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(@"PredictedLabel", @"PredictedLabel"));

            return pipeline;
        }
    }
}
