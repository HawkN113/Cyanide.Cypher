using Cyanide.Cypher;
using Cyanide.Cypher.Builders.Models;

const string personAlies = "person";
const string personType = "Person";
const string template = "Query: {0}";

var queryBuilder = Factory.QueryBuilder();
var admQueryBuilder = Factory.AdminQueryBuilder();

Console.WriteLine(template, queryBuilder.Match(q =>
        q.WithNode(new Entity(personType, "p"))
            .WithRelation("LIVES_IN", RelationshipType.Direct)
            .WithNode(new Entity("City", "c"))
    )
    .Return(q =>
        q.WithField("name", "p")
            .WithField("name", "c")
    )
    .Build());

Console.WriteLine(template, queryBuilder
    .Match(q =>
        q.WithNode(new Entity(personType, personAlies))
    )
    .Where(q => q.IsNotNull("person.name"))
    .Create(q => q.WithNode(new Entity("Person", "anotherPerson", [new Field("name", "person.name")])))
    .Build());

Console.WriteLine(template, queryBuilder
    .Create(q =>
        q.WithNode(new Entity("Person", null,
        [
            new Field("name", "'Andy'"),
            new Field("title", "'Developer'")
        ])))
    .Build());

Console.WriteLine(template, queryBuilder
    .Match(q =>
        q.WithRelation(new Entity("ACTED_IN", "r"), RelationshipType.Direct,
            new Entity(personType, "n", [new Field("name", "'Laurence Fishburne'")]),
            new Entity(string.Empty))
    )
    .Delete(q => q.WithNode("r"))
    .Build());

Console.WriteLine(template, queryBuilder
    .Match(q =>
        q.WithNode(new Entity("Movie", "", [new Field("title", "'Wall Street'")]))
            .WithRelation("ACTED_IN|DIRECTED", RelationshipType.InDirect)
            .WithNode(new Entity(personType, personAlies))
    )
    .Return(q =>
        q.WithField("name", personAlies, personAlies)
    )
    .Build());

Console.WriteLine(template, queryBuilder
    .Match(q =>
        q.WithNode(new Entity(personType, "a", [new Field("name", "'Martin Sheen'")]))
    )
    .Match(q =>
        q.WithNode(new Entity("a"))
            .WithRelation("DIRECTED", RelationshipType.Direct, "r")
            .WithEmptyNode()
    )
    .Return(q =>
        q.WithField("name", "a").WithField("r")
    )
    .Build());

Console.WriteLine(template, queryBuilder
    .Match(q =>
        q.WithRelation(
            new Entity("r"),
            RelationshipType.Direct,
            new Entity(personType, "", [new Field("name", "'Oliver Stone'")]),
            new Entity("movie"))
    )
    .Return(q => q.WithType("r"))
    .Build());
    
Console.WriteLine(template, queryBuilder
    .Match(q =>
        q.WithNode(new Entity(personType, "p"))
            .WithRelation("LIVES_IN", RelationshipType.Direct)
            .WithNode(new Entity("City", "c"))
    )
    .Where(q => q.Query("p.age > 30"))
    .Return(q =>
        q
            .WithField("name", "p")
            .WithField("name", "c")
    )
    .Build());
    
Console.WriteLine(template, admQueryBuilder
    .Create(q =>
        q.WithUser("jake")
            .WithPassword("'abc'", PasswordType.Encrypted)
            .SetStatus(UserStatus.SUSPENDED)
            .SetHomeDb("anotherDb")
    )
    .Build());

Console.WriteLine(template, admQueryBuilder
    .Show(q =>
        q.AsCurrentUser()
            .WithAllFields()
            .WithCount()
    )
    .Build());

Console.WriteLine(template, admQueryBuilder
    .Show(q =>
        q.WithDatabase("db")
            .WithAllFields()
            .WithCount()
    )
    .Build());

Console.WriteLine(template, admQueryBuilder
    .Create(q =>
        q.WithUser("jake")
            .WithPassword("'abc'")
            .SetStatus()
            .SetHomeDb("anotherDb")
            .Replace()
    )
    .Build());