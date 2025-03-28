﻿using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders.Queries.Admin.Commands;

public sealed class CreateDbQuery :
    IBuilderInitializer,
    ICreateAdmQueryDatabase,
    INotExistsDatabase
{
    private readonly List<string> _patterns = [];
    private bool _shouldReplaced;
    private bool _ifNotExists;
    private StringBuilder _createDbClauses = new();

    public void Initialize(StringBuilder clauseBuilder)
    {
        _createDbClauses = clauseBuilder;
    }

    public INotExistsDatabase WithDatabase(string databaseName)
    {
        _patterns.Add($"DATABASE {databaseName}");
        return this;
    }

    public IReplaceDatabase IfNotExists()
    {
        _ifNotExists = true;
        _patterns.Add("IF NOT EXISTS");
        return this;
    }

    public void Replace()
    {
        _shouldReplaced = true;
    }

    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_createDbClauses.Length > 0)
        {
            _createDbClauses.Append(' ');
        }

        if (_shouldReplaced && _ifNotExists)
            throw new InvalidOperationException(
                "Use CREATE DATABASE [IF NOT EXISTS] clause to ensure the database is only created if it does not already exist, or use CREATE OR REPLACE DATABASE to create a new database, replacing the existing one if it already exists.");
        _createDbClauses.Append(!_shouldReplaced ? "CREATE " : "CREATE OR REPLACE ");
        _createDbClauses.Append(string.Join(" ", _patterns));
    }
}