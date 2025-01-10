using System.Text;
using Cyanide.Cypher.Builders.Abstraction.Clauses;
using Cyanide.Cypher.Builders.Abstraction.Subclauses;

namespace Cyanide.Cypher.Builders;

public sealed class CypherQueryBuilder : IOrderBySubQuery, ICreateQuery, IDeleteQuery, IWhereSubQuery, ISelectQuery, IBuildQuery, IMatchQuery, IOptMatchQuery
{
    private readonly StringBuilder _createClauses = new();
    private readonly StringBuilder _deleteClauses = new();
    private readonly StringBuilder _matchClauses = new();
    private readonly StringBuilder _optMatchClauses = new();
    private readonly StringBuilder _whereClauses = new();
    private readonly StringBuilder _returnClauses = new();
    private readonly StringBuilder _orderByClauses = new();

    public IOrderBySubQuery Select(Action<SelectBuilder> configureReturn)
    {
        var returnBuilder = new SelectBuilder(_returnClauses);
        configureReturn(returnBuilder);
        returnBuilder.End();
        return this;
    }

    public IBuildQuery Create(Action<CreateBuilder> configureCreate)
    {
        var createBuilder = new CreateBuilder(_createClauses);
        configureCreate(createBuilder);
        createBuilder.End();
        return this;
    }

    public IBuildQuery OrderBy(Action<OrderByBuilder> configureOrderBy)
    {
        var orderByBuilder = new OrderByBuilder(_orderByClauses);
        configureOrderBy(orderByBuilder);
        orderByBuilder.End();
        return this;
    }

    public IOptMatchQuery Match(Action<MatchBuilder> configureMatch)
    {
        var matchBuilder = new MatchBuilder(_matchClauses);
        configureMatch(matchBuilder);
        matchBuilder.End();
        return this;
    }

    public ISelectQuery OptionalMatch(Action<OptMatchBuilder> configureOptMatch)
    {
        var optMatchBuilder = new OptMatchBuilder(_optMatchClauses);
        configureOptMatch(optMatchBuilder);
        optMatchBuilder.End();
        return this;
    }

    IMatchQuery IOptMatchQuery.Match(Action<MatchBuilder> configureMatch)
    {
        var matchBuilder = new MatchBuilder(_matchClauses);
        configureMatch(matchBuilder);
        matchBuilder.End();
        return this;
    }

    public IMatchQuery Where(Action<WhereBuilder> conditions)
    {
        var whereBuilder = new WhereBuilder(_whereClauses);
        conditions(whereBuilder);
        whereBuilder.End();
        return this;
    }

    public IBuildQuery Delete(Action<DeleteBuilder> configureDelete)
    {
        var deleteBuilder = new DeleteBuilder(_deleteClauses, false);
        configureDelete(deleteBuilder);
        deleteBuilder.End();
        return this;
    }

    public IBuildQuery DetachDelete(Action<DeleteBuilder> configureDelete)
    {
        var deleteBuilder = new DeleteBuilder(_deleteClauses, true);
        configureDelete(deleteBuilder);
        deleteBuilder.End();
        return this;
    }

    public string Build()
    {
        StringBuilder queryBuilder = new();
        var clauses = new List<StringBuilder>
        {
            _matchClauses,
            _optMatchClauses,
            _whereClauses,
            _createClauses,
            _deleteClauses,
            _returnClauses,
            _orderByClauses
        };
        foreach (var clause in clauses)
        {
            AppendClause(clause, queryBuilder);
        }
        return queryBuilder.ToString().Trim();
    }

    private static void AppendClause(StringBuilder clause, StringBuilder queryBuilder)
    {
        if (clause.Length <= 0) return;
        if (queryBuilder.Length > 0)
        {
            queryBuilder.Append(' ');
        }

        queryBuilder.Append(clause);
    }
}