//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Experiments\Cyanide.cypher\src\Cyanide.Cypher\Grammar\SQL.g4 by ANTLR 4.13.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="SQLParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
[System.CLSCompliant(false)]
public interface ISQLListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="SQLParser.query"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterQuery([NotNull] SQLParser.QueryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SQLParser.query"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitQuery([NotNull] SQLParser.QueryContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SQLParser.select_clause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSelect_clause([NotNull] SQLParser.Select_clauseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SQLParser.select_clause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSelect_clause([NotNull] SQLParser.Select_clauseContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SQLParser.from_clause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFrom_clause([NotNull] SQLParser.From_clauseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SQLParser.from_clause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFrom_clause([NotNull] SQLParser.From_clauseContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SQLParser.where_clause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhere_clause([NotNull] SQLParser.Where_clauseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SQLParser.where_clause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhere_clause([NotNull] SQLParser.Where_clauseContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SQLParser.column_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterColumn_list([NotNull] SQLParser.Column_listContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SQLParser.column_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitColumn_list([NotNull] SQLParser.Column_listContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SQLParser.column"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterColumn([NotNull] SQLParser.ColumnContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SQLParser.column"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitColumn([NotNull] SQLParser.ColumnContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SQLParser.table_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTable_name([NotNull] SQLParser.Table_nameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SQLParser.table_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTable_name([NotNull] SQLParser.Table_nameContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SQLParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondition([NotNull] SQLParser.ConditionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SQLParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondition([NotNull] SQLParser.ConditionContext context);
}
