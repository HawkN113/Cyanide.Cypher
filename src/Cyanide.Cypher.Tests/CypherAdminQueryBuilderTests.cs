using Cyanide.Cypher.Builders.Admin;
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
    public void Translate_With_CREATE_OR_REPLACE_DATABASE_WithNotExists_ReturnsInvalidOperationException()
    {
        // Act
        Assert.Throws<InvalidOperationException>(() =>
        {
            _queryBuilder
                .Create(q =>
                    q.WithDatabase("db")
                        .IfNotExists()
                        .Replace()
                )
                .Build();
        });
    }

    [Fact]
    public void Translate_With_CREATE_OR_REPLACE_DATABASE_WithNotExists_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Create(q =>
                q.WithDatabase("db")
                    .Replace()
            )
            .Build();

        // Assert
        Assert.Equal(
            "CREATE OR REPLACE DATABASE db",
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
                    .WithPassword("'abc'", PasswordType.Encrypted)
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
                    .WithPassword("'abc'", PasswordType.Encrypted, true)
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
    
    #region SHOW
    
    [Fact]
    public void Translate_With_SHOW_DATABASE_WithFields_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Show(q =>
                q.WithDatabase("db")
                    .WithAllFields()
            )
            .Build();

        // Assert
        Assert.Equal(
            "SHOW DATABASE db YIELD *",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_SHOW_DATABASE_WithFieldsAndCount_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Show(q =>
                q.WithDatabase("db")
                    .WithAllFields()
                    .WithCount()
            )
            .Build();

        // Assert
        Assert.Equal(
            "SHOW DATABASE db YIELD * RETURN count(*) AS count",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_SHOW_DATABASE_AsDefault_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Show(q =>
                q.AsDefaultDatabase()
            )
            .Build();

        // Assert
        Assert.Equal(
            "SHOW DEFAULT DATABASE",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_SHOW_DATABASE_AsHome_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Show(q =>
                q.AsHomeDatabase()
            )
            .Build();

        // Assert
        Assert.Equal(
            "SHOW HOME DATABASE",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_SHOW_DATABASES_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Show(q =>
                q.WithDatabases()
            )
            .Build();

        // Assert
        Assert.Equal(
            "SHOW DATABASES",
            resultQuery);
    }
    
    
    [Fact]
    public void Translate_With_SHOW_CURRENT_USER_WithFields_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Show(q =>
                q.AsCurrentUser()
                    .WithAllFields()
            )
            .Build();

        // Assert
        Assert.Equal(
            "SHOW CURRENT USER YIELD *",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_SHOW_CURRENT_USER_WithFieldsAndCount_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Show(q =>
                q.AsCurrentUser()
                    .WithAllFields()
                    .WithCount()
            )
            .Build();

        // Assert
        Assert.Equal(
            "SHOW CURRENT USER YIELD * RETURN count(*) AS count",
            resultQuery);
    }
    
    [Fact]
    public void Translate_With_SHOW_USERS_WithFieldsAndCount_ReturnsCorrectCypherQuery()
    {
        // Act
        var resultQuery = _queryBuilder
            .Show(q =>
                q.WithUsers()
                    .WithAllFields()
                    .WithCount()
            )
            .Build();

        // Assert
        Assert.Equal(
            "SHOW USERS YIELD * RETURN count(*) AS count",
            resultQuery);
    }
    
    #endregion
}