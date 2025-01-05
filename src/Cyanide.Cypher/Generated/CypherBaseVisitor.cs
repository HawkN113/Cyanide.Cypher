//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Experiments\Cyanide.cypher\src\Cyanide.Cypher\Grammar\Cypher.g4 by ANTLR 4.13.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="ICypherVisitor{Result}"/>,
/// which can be extended to create a visitor which only needs to handle a subset
/// of the available methods.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
[System.Diagnostics.DebuggerNonUserCode]
[System.CLSCompliant(false)]
internal partial class CypherBaseVisitor<Result> : AbstractParseTreeVisitor<Result>, ICypherVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.cypherQuery"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCypherQuery([NotNull] CypherParser.CypherQueryContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.matchClause"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitMatchClause([NotNull] CypherParser.MatchClauseContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.matchPattern"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitMatchPattern([NotNull] CypherParser.MatchPatternContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.whereClause"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitWhereClause([NotNull] CypherParser.WhereClauseContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.returnClause"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitReturnClause([NotNull] CypherParser.ReturnClauseContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.condition"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitCondition([NotNull] CypherParser.ConditionContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.comparison"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitComparison([NotNull] CypherParser.ComparisonContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.returnItems"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitReturnItems([NotNull] CypherParser.ReturnItemsContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.groupByItems"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitGroupByItems([NotNull] CypherParser.GroupByItemsContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.returnItem"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitReturnItem([NotNull] CypherParser.ReturnItemContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.groupByItem"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitGroupByItem([NotNull] CypherParser.GroupByItemContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.logicalOperator"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitLogicalOperator([NotNull] CypherParser.LogicalOperatorContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.comparisonOperator"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitComparisonOperator([NotNull] CypherParser.ComparisonOperatorContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.property"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitProperty([NotNull] CypherParser.PropertyContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.value"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitValue([NotNull] CypherParser.ValueContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.label"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitLabel([NotNull] CypherParser.LabelContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.relationshipLabel"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitRelationshipLabel([NotNull] CypherParser.RelationshipLabelContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.alias"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitAlias([NotNull] CypherParser.AliasContext context) { return VisitChildren(context); }
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.relationshipAlias"/>.
	/// <para>
	/// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
	/// on <paramref name="context"/>.
	/// </para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	public virtual Result VisitRelationshipAlias([NotNull] CypherParser.RelationshipAliasContext context) { return VisitChildren(context); }
}
