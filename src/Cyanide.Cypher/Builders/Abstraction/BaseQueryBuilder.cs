using System.Text;

namespace Cyanide.Cypher.Builders.Abstraction;

internal abstract class BaseQueryBuilder(Dictionary<string, StringBuilder> clauses) : IBaseQuery
{
    protected Dictionary<string, StringBuilder> allClauses => clauses;

    protected IBaseQuery ConfigureAndReturn<TClause>(string key, Action<TClause> configure)
        where TClause : class, IBuilderInitializer, new()
    {
        ConfigureClause(key, configure);
        return this;
    }

    private void ConfigureClause<TClause>(string key, Action<TClause> configure) where TClause : class, IBuilderInitializer, new()
    {
        if (!clauses.TryGetValue(key, out var clauseBuilder))
            throw new ArgumentException($"Invalid clause key: {key}", nameof(key));
        
        var instance = new TClause();
        
        if (instance is not IBuilderInitializer initializer) return;
        
        initializer.Initialize(clauseBuilder);
        configure(instance);
        instance.End();
    }
}