using System.Text;
using Cyanide.Cypher.Builders.Abstraction;

namespace Cyanide.Cypher.Builders;

public sealed class OrderByBuilder(CypherQueryBuilder parent, StringBuilder orderByClauses): INode<OrderByBuilder>
{
    private readonly List<string> _patterns = [];
    private int _countProperties;

    /// <summary>
    /// Add a node (entity) to the ORDER BY clause
    /// </summary>
    /// <returns></returns>
    public OrderByBuilder EmptyNode()
    {
        _patterns.AddRange(NodeHelper.EmptyNode());
        return this;
    }
    
    /// <summary>
    /// Return a property to the ORDER BY clause
    /// Sample: p.bornIn
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public OrderByBuilder Property(string propertyName, string alias)
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
    public OrderByBuilder Node(Entity entity)
    {
        _patterns.AddRange(NodeHelper.Node(entity));
        return this;
    }
    
    public OrderByBuilder Asc(Func<OrderByBuilder, OrderByBuilder> nestedConditions)
    {
        nestedConditions(this);
        return this;
    }
    
    public OrderByBuilder Desc(Func<OrderByBuilder, OrderByBuilder> nestedConditions)
    {
        _patterns.Add("DESC");
        nestedConditions(this);
        return this;
    }

    /// <summary>
    /// End the MATCH clause
    /// </summary>
    /// <returns></returns>
    public CypherQueryBuilder End()
    {
        if (_patterns.Count <= 0) return parent;
        if (orderByClauses.Length > 0)
        {
            orderByClauses.Append(' ');
        }
        orderByClauses.Append("ORDER BY ");
        orderByClauses.Append(_countProperties > 1 ? string.Join(", ", _patterns) : string.Join("", _patterns));
        return parent;
    }
}