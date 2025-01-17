using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Cyanide.Cypher.App;

var config = new ManualConfig();
config.WithOptions(ConfigOptions.DisableOptimizationsValidator);

BenchmarkRunner.Run<QueriesManipulationBenchmarks>();
BenchmarkRunner.Run<AdminQueriesManipulationBenchmarks>();
Console.ReadKey();