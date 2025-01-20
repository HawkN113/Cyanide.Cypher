using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

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