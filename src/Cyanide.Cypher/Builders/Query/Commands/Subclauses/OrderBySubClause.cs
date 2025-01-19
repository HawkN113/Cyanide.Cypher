using System.Text;
using Cyanide.Cypher.Builders.Abstraction;
using Cyanide.Cypher.Builders.Abstraction.Nodes;
using Cyanide.Cypher.Builders.Helper;

namespace Cyanide.Cypher.Builders.Query.Commands.Subclauses;

public sealed class OrderBySubClause : 
    IBuilderInitializer,
    INode<OrderBySubClause>,
    IEmptyNode<OrderBySubClause>
{
    private readonly List<string> _patterns = [];
    private int _countProperties;
    private StringBuilder _orderByClauses = new();
    
    public void Initialize(StringBuilder clauseBuilder)
    {
        _orderByClauses = clauseBuilder;
    }

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
    /// Return a property to the ORDER BY clause <br/>
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
    
    /// <summary>
    /// Add DESC for the ORDER BY clause <br/>
    /// Sample: ORDER BY p.name DESC
    /// </summary>
    public void Descending()
    {
        _patterns.Add(" DESC");
    }
    
    public void End()
    {
        if (_patterns.Count <= 0) return;
        if (_orderByClauses.Length > 0)
        {
            _orderByClauses.Append(' ');
        }
        _orderByClauses.Append("ORDER BY ");
        _orderByClauses.Append(_countProperties > 1 ? string.Join(", ", _patterns) : string.Join("", _patterns));
    }
}