using System.ComponentModel;

namespace Cyanide.Cypher.Builders;

/// <summary>
/// Relationship types
/// </summary>
public enum RelationshipType
{
    /// <summary>
    /// Non-directed relation (classic)
    /// </summary>
    [Description("-[]-")]
    NonDirect = 0,

    /// <summary>
    /// Directed relation (classic) 
    /// </summary>
    [Description("-[]->")]
    Direct = 1,
    
    /// <summary>
    /// In directed relation
    /// </summary>
    [Description("<-[]-")]
    InDirect = 2,
    
    /// <summary>
    /// Undirected relation
    /// </summary>
    [Description("<-[]->")]
    UnDirect = 3,

    /// <summary>
    /// Bidirectional relation: 
    /// </summary>
    [Description("->[]<-")]
    BiDirect = 4
}

/// <summary>
/// Outgoing (basic) relationship types
/// </summary>
public enum BasicRelationshipType
{
    /// <summary>
    /// Non-directed relation (classic)
    /// </summary>
    [Description("--")]
    RelatedTo = 0,

    /// <summary>
    /// Directed relation (classic)
    /// </summary>
    [Description("-->")]
    Directed = 1,
    
    /// <summary>
    /// In directed relation
    /// </summary>
    [Description("<--")]
    InDirected = 2,
}