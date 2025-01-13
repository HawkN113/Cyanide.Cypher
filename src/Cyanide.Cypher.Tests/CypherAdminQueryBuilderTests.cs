using Cyanide.Cypher.Builders.Admin;

namespace Cyanide.Cypher.Tests;

public class CypherAdminQueryBuilderTests
{
    private readonly IAdminQuery _queryBuilder = Factory.AdminQueryBuilder();
    
    #region CREATE
    
    [Fact]
    public void Translate_With_CREATE_DATABASE_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithDatabase("db")
            )
            .Build();

        // Assert
        Assert.Equal(
            "CREATE DATABASE db",
            resultQuery);
    }

    [Fact]
    public void Translate_With_CREATE_DATABASE_WithNotExists_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithDatabase("db")
                    .IfNotExists()
            )
            .Build();

        // Assert
        Assert.Equal(
            "CREATE DATABASE db IF NOT EXISTS",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_CREATE_OR_REPLACE_DATABASE_WithNotExists_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithDatabase("db")
                    .IfNotExists()
                    .Replace()
            )
            .Build();

        // Assert
        Assert.Equal(
            "CREATE OR REPLACE DATABASE db IF NOT EXISTS",
            resultQuery);
    }

    #endregion
}