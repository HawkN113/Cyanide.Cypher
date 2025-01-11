using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders;

public sealed class OrderBySubClause(StringBuilder orderByClauses): INode<OrderBySubClause>, IEmptyNode<OrderBySubClause>
{
    private readonly List<string> _patterns = [];
    private int _countProperties;

    /// <summary>
    /// Add a node (entity) to the ORDER BY clause
    /// </summary>
    /// <returns></returns>
    public OrderBySubClause WithEmptyNode()
    {
        _patterns.AddRange(NodePatternBuilder.CreateEmptyNode());
        return this;
    }
    
    /// <summary>
    /// Return a property to the ORDER BY clause
    /// Sample: p.bornIn
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public OrderBySubClause WithField(string propertyName, string alias)
    {
        _patterns.Add($"{alias}.{propertyName}");
        _countProperties += 1;
        return this;
    }

    /// <summary>
    /// Add a node (entity) to the ORDER BY clause
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public OrderBySubClause WithNode(Entity entity)
    {
        _patterns.AddRange(NodePatternBuilder.CreateNode(entity));
        return this;
    }
    
    public void Descending()
    {
        _patterns.Add(" DESC");
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    internal void End()
    {
        if (_patterns.Count <= 0) return;
        if (orderByClauses.Length > 0)
        {
            orderByClauses.Append(' ');
        }
        orderByClauses.Append("ORDER BY ");
        orderByClauses.Append(_countProperties > 1 ? string.Join(", ", _patterns) : string.Join("", _patterns));
    }
}