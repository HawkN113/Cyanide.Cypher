namespace Cyanide.Cypher.Builders.Abstraction;

public interface IDeleteQuery: IOptMatchQuery
{
    IMatchQuery Delete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
    IMatchQuery DetachDelete(Func<DeleteBuilder, DeleteBuilder> configureDelete);
}

public interface IWhereSubQuery
{
    IMatchQuery Where(Func<WhereBuilder, WhereBuilder> conditions);
}

public interface ICreateQuery: ISelectQuery
{
    IMatchQuery Create(Func<CreateBuilder, CreateBuilder> configureCreate);
}
public interface IMatchQuery: ISelectQuery
{
    IOptMatchQuery Match(Func<MatchBuilder, MatchBuilder> configureMatch);
}
public interface IOptMatchQuery: IMatchQuery, IWhereSubQuery
{
    ISelectQuery OptionalMatch(Func<OptMatchBuilder, OptMatchBuilder> configureOptMatch);
}

public interface IOrderBySubQuery: IBuildQuery
{
    IBuildQuery OrderBy(Func<OrderByBuilder, OrderByBuilder> configureOrderBy);
}

public interface ISelectQuery: IBuildQuery
{
    IOrderBySubQuery Select(Func<SelectBuilder, SelectBuilder> configureReturn);
}

public interface IBuildQuery
{
    string Build();
}