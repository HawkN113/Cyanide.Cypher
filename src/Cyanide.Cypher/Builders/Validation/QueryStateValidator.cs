namespace Cyanide.Cypher.Builders.Validation;

internal class QueryStateValidator(bool enableValidation)
{
    private QueryState _currentState = QueryState.None;

    private readonly HashSet<QueryState> _validFirstStates =
    [
        QueryState.Match,
        QueryState.Create,
        QueryState.OptionalMatch
    ];
    
    private readonly Dictionary<QueryState, HashSet<QueryState>> _validTransitions = new()
    {
        { QueryState.None, [QueryState.Match, QueryState.Create] },
        { QueryState.Match, [QueryState.Match, QueryState.OptionalMatch, QueryState.Where, QueryState.Create, QueryState.Delete, QueryState.DetachDelete, QueryState.Return] },
        { QueryState.OptionalMatch, [QueryState.OptionalMatch, QueryState.Where, QueryState.Create, QueryState.Delete, QueryState.Return] },
        { QueryState.Where, [QueryState.Create, QueryState.Delete, QueryState.Return] },
        { QueryState.Create, [QueryState.Return] },
        { QueryState.Delete, [QueryState.Return] },
        { QueryState.DetachDelete, [QueryState.Return] },
        { QueryState.Return, [QueryState.OrderBy] },
        { QueryState.OrderBy, [] }
    };

    public void ValidateTransition(QueryState newState)
    {
        if (!enableValidation) return;
        if (_currentState == QueryState.None && !_validFirstStates.Contains(newState))
        {
            throw new InvalidOperationException(
                $"The first clause must be MATCH or CREATE. Attempted: {newState}.");
        }

        if (!_validTransitions.TryGetValue(_currentState, out var allowedStates) || !allowedStates.Contains(newState))
        {
            throw new InvalidOperationException($"Invalid transition from {_currentState} to {newState}.");
        }

        _currentState = newState;
    }
}