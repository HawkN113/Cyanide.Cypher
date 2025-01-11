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

    public IOrderBySubQuery Return(Action<ReturnClause> configureReturn)
    {
        var returnBuilder = new ReturnClause(_returnClauses);
        configureReturn(returnBuilder);
        returnBuilder.End();
        return this;
    }

    public IBuildQuery Create(Action<CreateClause> configureCreate)
    {
        var createBuilder = new CreateClause(_createClauses);
        configureCreate(createBuilder);
        createBuilder.End();
        return this;
    }

    public IBuildQuery OrderBy(Action<OrderBySubClause> configureOrderBy)
    {
        var orderByBuilder = new OrderBySubClause(_orderByClauses);
        configureOrderBy(orderByBuilder);
        orderByBuilder.End();
        return this;
    }

    public IOptMatchQuery Match(Action<MatchClause> configureMatch)
    {
        var matchBuilder = new MatchClause(_matchClauses);
        configureMatch(matchBuilder);
        matchBuilder.End();
        return this;
    }

    public ISelectQuery OptionalMatch(Action<OptMatchClause> configureOptMatch)
    {
        var optMatchBuilder = new OptMatchClause(_optMatchClauses);
        configureOptMatch(optMatchBuilder);
        optMatchBuilder.End();
        return this;
    }

    IMatchQuery IOptMatchQuery.Match(Action<MatchClause> configureMatch)
    {
        var matchBuilder = new MatchClause(_matchClauses);
        configureMatch(matchBuilder);
        matchBuilder.End();
        return this;
    }

    public IMatchQuery Where(Action<WhereSubClause> conditions)
    {
        var whereBuilder = new WhereSubClause(_whereClauses);
        conditions(whereBuilder);
        whereBuilder.End();
        return this;
    }

    public IBuildQuery Delete(Action<DeleteClause> configureDelete)
    {
        var deleteBuilder = new DeleteClause(_deleteClauses, false);
        configureDelete(deleteBuilder);
        deleteBuilder.End();
        return this;
    }

    public IBuildQuery DetachDelete(Action<DeleteClause> configureDelete)
    {
        var deleteBuilder = new DeleteClause(_deleteClauses, true);
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