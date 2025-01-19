using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Cyanide.Cypher.App;

var config = DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator);

BenchmarkRunner.Run<QueriesManipulationBenchmarks>(config);
BenchmarkRunner.Run<AdminQueriesManipulationBenchmarks>(config);

Console.ReadKey();