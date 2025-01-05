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

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="CypherParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
[System.CLSCompliant(false)]
internal interface ICypherVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.cypherQuery"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCypherQuery([NotNull] CypherParser.CypherQueryContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.matchClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMatchClause([NotNull] CypherParser.MatchClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.matchPattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMatchPattern([NotNull] CypherParser.MatchPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.whereClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhereClause([NotNull] CypherParser.WhereClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.returnClause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnClause([NotNull] CypherParser.ReturnClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondition([NotNull] CypherParser.ConditionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.comparison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparison([NotNull] CypherParser.ComparisonContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.returnItems"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnItems([NotNull] CypherParser.ReturnItemsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.groupByItems"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupByItems([NotNull] CypherParser.GroupByItemsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.returnItem"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnItem([NotNull] CypherParser.ReturnItemContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.groupByItem"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupByItem([NotNull] CypherParser.GroupByItemContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.logicalOperator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogicalOperator([NotNull] CypherParser.LogicalOperatorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.comparisonOperator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparisonOperator([NotNull] CypherParser.ComparisonOperatorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.property"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProperty([NotNull] CypherParser.PropertyContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValue([NotNull] CypherParser.ValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.label"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLabel([NotNull] CypherParser.LabelContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.relationshipLabel"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelationshipLabel([NotNull] CypherParser.RelationshipLabelContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.alias"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAlias([NotNull] CypherParser.AliasContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="CypherParser.relationshipAlias"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelationshipAlias([NotNull] CypherParser.RelationshipAliasContext context);
}
