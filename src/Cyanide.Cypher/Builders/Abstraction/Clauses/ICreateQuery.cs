using Cyanide.Cypher.Builders.Query;

namespace Cyanide.Cypher.Builders.Abstraction.Clauses;

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