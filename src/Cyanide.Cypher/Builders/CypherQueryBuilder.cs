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

    public IOrderBySubQuery Select(Func<SelectBuilder, SelectBuilder> configureReturn)
    {
        var returnBuilder = new SelectBuilder(this, _returnClauses);
        configureReturn(returnBuilder).End();
        return this;
    }

    public IBuildQuery Create(Func<CreateBuilder, CreateBuilder> configureCreate)
    {
        var createBuilder = new CreateBuilder(this, _createClauses);
        configureCreate(createBuilder).End();
        return this;
    }

    public IBuildQuery OrderBy(Func<OrderByBuilder, OrderByBuilder> configureOrderBy)
    {
        var orderByBuilder = new OrderByBuilder(this, _orderByClauses);
        configureOrderBy(orderByBuilder).End();
        return this;
    }

    public IOptMatchQuery Match(Func<MatchBuilder, MatchBuilder> configureMatch)
    {
        var matchBuilder = new MatchBuilder(this, _matchClauses);
        configureMatch(matchBuilder).End();
        return this;
    }

    public ISelectQuery OptionalMatch(Func<OptMatchBuilder, OptMatchBuilder> configureOptMatch)
    {
        var optMatchBuilder = new OptMatchBuilder(this, _optMatchClauses);
        configureOptMatch(optMatchBuilder).End();
        return this;
    }

    IMatchQuery IOptMatchQuery.Match(Func<MatchBuilder, MatchBuilder> configureMatch)
    {
        var matchBuilder = new MatchBuilder(this, _matchClauses);
        configureMatch(matchBuilder).End();
        return this;
    }

    public IMatchQuery Where(Func<WhereBuilder, WhereBuilder> conditions)
    {
        var whereBuilder = new WhereBuilder(this, _whereClauses);
        conditions(whereBuilder).End();
        return this;
    }

    public IBuildQuery Delete(Func<DeleteBuilder, DeleteBuilder> configureDelete)
    {
        var deleteBuilder = new DeleteBuilder(this, _deleteClauses, false);
        configureDelete(deleteBuilder).End();
        return this;
    }

    public IBuildQuery DetachDelete(Func<DeleteBuilder, DeleteBuilder> configureDelete)
    {
        var deleteBuilder = new DeleteBuilder(this, _deleteClauses, true);
        configureDelete(deleteBuilder).End();
        return this;
    }

    public string Build()
    {
        StringBuilder queryBuilder = new();
        AppendClause(_matchClauses, queryBuilder);
        AppendClause(_optMatchClauses, queryBuilder);
        AppendClause(_whereClauses, queryBuilder);
        AppendClause(_createClauses, queryBuilder);
        AppendClause(_deleteClauses, queryBuilder);
        AppendClause(_returnClauses, queryBuilder);
        AppendClause(_orderByClauses, queryBuilder);
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