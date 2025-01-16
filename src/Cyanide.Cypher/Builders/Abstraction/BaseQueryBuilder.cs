using System.Text;

namespace Cyanide.Cypher.Builders.Abstraction;

internal abstract class BaseQueryBuilder(Dictionary<string, StringBuilder> clauses)
{
    protected Dictionary<string, StringBuilder> allClauses => clauses;

    protected TInterface ConfigureQueryBuilder<TInterface, T>(string key, Action<T> configure)
        where T : class, IBuilderInitializer, new()
        where TInterface : class
    {
        ConfigureClause(key, configure);
        return (TInterface)(object)this;
    }

    private void ConfigureClause<T>(string key, Action<T> configure)
        where T : class, IBuilderInitializer, new()
    {
        if (!clauses.TryGetValue(key, out var clauseBuilder))
            throw new InvalidOperationException($"Invalid clause key: {nameof(key)}");

        var instance = new T();
        instance.Initialize(clauseBuilder);
        configure(instance);
        instance.End();
    }
}