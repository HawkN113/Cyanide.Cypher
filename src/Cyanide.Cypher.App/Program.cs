using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Cyanide.Cypher.App;

var config = DefaultConfig.Instance
    .WithOptions(ConfigOptions.DisableOptimizationsValidator)
    .AddJob(Job.Dry)
    .AddLogger(ConsoleLogger.Default)
    .AddColumn(TargetMethodColumn.Method, StatisticColumn.Min, StatisticColumn.Max)
    .AddExporter(RPlotExporter.Default, CsvExporter.Default)
    .AddAnalyser(EnvironmentAnalyser.Default);

BenchmarkRunner.Run<QueriesManipulationBenchmarks>(config);
BenchmarkRunner.Run<AdminQueriesManipulationBenchmarks>(config);