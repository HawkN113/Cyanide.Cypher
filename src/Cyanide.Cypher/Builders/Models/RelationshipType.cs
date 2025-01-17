using System.ComponentModel;

namespace Cyanide.Cypher.Builders.Models;

/// <summary>
/// Relationship types
/// </summary>
public enum RelationshipType
{
    [Description("Non-directed relation (classic): -[]-")]
    NonDirect = 0,

    [Description("Directed relation (classic): -[]->")]
    Direct = 1,

    [Description("Indirected relation: <-[]-")]
    InDirect = 2,

    [Description("Undirected relation: <-[]->")]
    UnDirect = 3,

    [Description("Bidirectional relation: ->[]<-")]
    BiDirect = 4
}