using Cyanide.Cypher.Builders;

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
    
    [Fact]
    public void Translate_With_CREATE_USER_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithUser("jake")
                    .WithPassword("'abc'")
                    .SetStatus(UserStatus.SUSPENDED)
                    .SetHomeDb("anotherDb")
            )
            .Build();

        // Assert
        Assert.Equal(
            "CREATE USER jake SET PLAINTEXT PASSWORD 'abc' CHANGE NOT REQUIRED SET STATUS SUSPENDED SET HOME DATABASE anotherDb",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_CREATE_USER_SET_ENCRYPTED_PASSWORD_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithUser("jake")
                    .WithPassword("'abc'", PasswordType.ENCRYPTED)
                    .SetStatus(UserStatus.SUSPENDED)
                    .SetHomeDb("anotherDb")
            )
            .Build();

        // Assert
        Assert.Equal(
            "CREATE USER jake SET ENCRYPTED PASSWORD 'abc' CHANGE NOT REQUIRED SET STATUS SUSPENDED SET HOME DATABASE anotherDb",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_CREATE_USER_SET_ENCRYPTED_PASSWORD_WithChange_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithUser("jake")
                    .WithPassword("'abc'", PasswordType.ENCRYPTED, true)
                    .SetStatus(UserStatus.SUSPENDED)
                    .SetHomeDb("anotherDb")
            )
            .Build();

        // Assert
        Assert.Equal(
            "CREATE USER jake SET ENCRYPTED PASSWORD 'abc' CHANGE REQUIRED SET STATUS SUSPENDED SET HOME DATABASE anotherDb",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_CREATE_OR_REPLACE_USER_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithUser("jake")
                    .WithPassword("'abc'")
                    .SetStatus()
                    .SetHomeDb("anotherDb")
                    .Replace()
            )
            .Build();

        // Assert
        Assert.Equal(
            "CREATE OR REPLACE USER jake SET PLAINTEXT PASSWORD 'abc' CHANGE NOT REQUIRED SET STATUS ACTIVE SET HOME DATABASE anotherDb",
            resultQuery);
    }

    #endregion
}