﻿using Cyanide.Cypher.Builders.Query.Abstraction.Clauses.Subclauses;
using Cyanide.Cypher.Builders.Query.Commands;

namespace Cyanide.Cypher.Builders.Query.Abstraction.Clauses;

public interface IOptMatchQuery: IWhereSubQuery
{
    /// <summary>
    /// OPTIONAL MATCH clause does as MATCH <br/>
    /// Sample: OPTIONAL MATCH (p)-[r:DIRECTED]->()
    /// </summary>
    /// <param name="configureOptMatch"></param>
    /// <returns></returns>
    IOptMatchQuery OptionalMatch(Action<OptMatchClause> configureOptMatch);
}