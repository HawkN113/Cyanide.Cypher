using Cyanide.Cypher.Builders.Queries.General.Commands;

namespace Cyanide.Cypher.Builders.Queries.General;

public interface IQuery
{
    /// <summary>
    /// CREATE clause <br/>
    /// Sample: CREATE (n:Person)
    /// </summary>
    /// <param name="configureCreate"></param>
    /// <returns></returns>
    ICreateQuery Create(Action<CreateClause> configureCreate);
    
    /// <summary>
    /// MATCH clause <br/>
    /// Sample: MATCH (movie:Movie)
    /// </summary>
    /// <param name="configureMatch"></param>
    /// <returns></returns>
    IMatchQuery Match(Action<MatchClause> configureMatch);
    
    /// <summary>
    /// OPTIONAL MATCH clause does as MATCH <br/>
    /// Sample: OPTIONAL MATCH (p)-[r:DIRECTED]->()
    /// </summary>
    /// <param name="configureOptMatch"></param>
    /// <returns></returns>
    IOptMatchQuery OptionalMatch(Action<OptMatchClause> configureOptMatch);
}