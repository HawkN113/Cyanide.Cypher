using BenchmarkDotNet.Attributes;
using Cyanide.Cypher.Builders;
using Cyanide.Cypher.Builders.Queries.General;

namespace Cyanide.Cypher.App;

public class QueriesManipulationBenchmarks
{
    private const string PersonAlies = "person";
    private const string PersonType = "Person";
    private readonly IQuery _queryBuilder = Factory.QueryBuilder();

    [Benchmark]
    public string Query1()
    {
        return _queryBuilder.Match(q =>
                q.WithNode(new Entity(PersonType, "p"))
                    .WithRelation("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Return(q =>
                q.WithField("name", "p")
                    .WithField("name", "c")
            )
            .Build();
    }

    [Benchmark]
    public string Query2()
    {
        return _queryBuilder
            .Match(q =>
                q.WithNode(new Entity(PersonType, PersonAlies))
            )
            .Where(q => q.IsNotNull("person.name"))
            .Create(q => q.WithNode(new Entity("Person", "anotherPerson", [new Field("name", "person.name")])))
            .Build();
    }

    [Benchmark]
    public string Query3()
    {
        return _queryBuilder
            .Create(q =>
                q.WithNode(new Entity("Person", null,
                [
                    new Field("name", "'Andy'"),
                    new Field("title", "'Developer'")
                ])))
            .Build();
    }

    [Benchmark]
    public string Query4()
    {
        return _queryBuilder
            .Match(q =>
                q.WithRelation(new Entity("ACTED_IN", "r"), RelationshipType.Direct,
                    new Entity(PersonType, "n", [new Field("name", "'Laurence Fishburne'")]),
                    new Entity(string.Empty))
            )
            .Delete(q => q.WithNode("r"))
            .Build();
    }

    [Benchmark]
    public string Query5()
    {
        return _queryBuilder
            .Match(q =>
                q.WithNode(new Entity("Movie", "", [new Field("title", "'Wall Street'")]))
                    .WithRelation("ACTED_IN|DIRECTED", RelationshipType.InDirect)
                    .WithNode(new Entity(PersonType, PersonAlies))
            )
            .Return(q =>
                q.WithField("name", PersonAlies, PersonAlies)
            )
            .Build();
    }

    [Benchmark]
    public string Query6()
    {
        return _queryBuilder
            .Match(q =>
                q.WithNode(new Entity(PersonType, "a", [new Field("name", "'Martin Sheen'")]))
            )
            .Match(q =>
                q.WithNode(new Entity("a"))
                    .WithRelation("DIRECTED", RelationshipType.Direct, "r")
                    .WithEmptyNode()
            )
            .Return(q =>
                q.WithField("name", "a").WithField("r")
            )
            .Build();
    }

    [Benchmark]
    public string Query7()
    {
        return _queryBuilder
            .Match(q =>
                q.WithNode(new Entity(PersonType, "p"))
                    .WithRelation("LIVES_IN", RelationshipType.Direct)
                    .WithNode(new Entity("City", "c"))
            )
            .Where(q => q.Query("p.age > 30"))
            .Return(q =>
                q
                    .WithField("name", "p")
                    .WithField("name", "c")
            )
            .Build();
    }
}