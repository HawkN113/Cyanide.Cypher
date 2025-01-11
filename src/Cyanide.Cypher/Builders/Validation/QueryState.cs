namespace Cyanide.Cypher.Builders.Validation;

internal enum QueryState
{
    None,
    Match,
    OptionalMatch,
    Where,
    Create,
    Delete,
    DetachDelete,
    Return,
    OrderBy
}