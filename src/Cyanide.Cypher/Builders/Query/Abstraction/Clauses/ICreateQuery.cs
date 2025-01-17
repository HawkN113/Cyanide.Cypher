using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders.Query.Abstraction.Clauses;

public interface ICreateQuery: IReturnQuery
{
    /// <summary>
    /// CREATE clause <br/>
    /// Sample: CREATE (n:Person)
    /// </summary>
    /// <param name="configureCreate"></param>
    /// <returns></returns>
    ICreateQuery Create(Action<CreateClause> configureCreate);
}