using BenchmarkDotNet.Attributes;
using Cyanide.Cypher.Builders;
using Cyanide.Cypher.Builders.Admin;

namespace Cyanide.Cypher.App;

public class AdminQueriesManipulationBenchmarks
{
    private readonly IAdminQuery _admQueryBuilder = Factory.AdminQueryBuilder();

    [Benchmark]
    public string AdminQuery1()
    {
        return _admQueryBuilder
            .Create(q =>
                q.WithUser("jake")
                    .WithPassword("'abc'", PasswordType.Encrypted)
                    .SetStatus(UserStatus.SUSPENDED)
                    .SetHomeDb("anotherDb")
            )
            .Build();
    }

    [Benchmark]
    public string AdminQuery2()
    {
        return _admQueryBuilder
            .Show(q =>
                q.WithDatabase("db")
                    .WithAllFields()
                    .WithCount()
            )
            .Build();
    }

    [Benchmark]
    public string AdminQuery3()
    {
        return _admQueryBuilder
            .Show(q =>
                q.WithDatabase("db")
                    .WithAllFields()
                    .WithCount()
            )
            .Build();
    }

    [Benchmark]
    public string AdminQuery4()
    {
        return _admQueryBuilder
            .Create(q =>
                q.WithUser("jake")
                    .WithPassword("'abc'")
                    .SetStatus()
                    .SetHomeDb("anotherDb")
                    .Replace()
            )
            .Build();
    }
}