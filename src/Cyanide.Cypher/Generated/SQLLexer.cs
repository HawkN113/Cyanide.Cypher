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

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.0")]
[System.CLSCompliant(false)]
public partial class SQLLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, IDENTIFIER=8, 
		WS=9;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "IDENTIFIER", 
		"WS"
	};


	public SQLLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public SQLLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "';'", "'SELECT'", "'FROM'", "'WHERE'", "','", "'.'", "'='"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, "IDENTIFIER", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "SQL.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static SQLLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,9,59,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,6,
		2,7,7,7,2,8,7,8,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,1,2,1,2,1,2,1,
		2,1,3,1,3,1,3,1,3,1,3,1,3,1,4,1,4,1,5,1,5,1,6,1,6,1,7,1,7,5,7,48,8,7,10,
		7,12,7,51,9,7,1,8,4,8,54,8,8,11,8,12,8,55,1,8,1,8,0,0,9,1,1,3,2,5,3,7,
		4,9,5,11,6,13,7,15,8,17,9,1,0,3,3,0,65,90,95,95,97,122,4,0,48,57,65,90,
		95,95,97,122,3,0,9,10,13,13,32,32,60,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,
		0,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,
		1,0,0,0,1,19,1,0,0,0,3,21,1,0,0,0,5,28,1,0,0,0,7,33,1,0,0,0,9,39,1,0,0,
		0,11,41,1,0,0,0,13,43,1,0,0,0,15,45,1,0,0,0,17,53,1,0,0,0,19,20,5,59,0,
		0,20,2,1,0,0,0,21,22,5,83,0,0,22,23,5,69,0,0,23,24,5,76,0,0,24,25,5,69,
		0,0,25,26,5,67,0,0,26,27,5,84,0,0,27,4,1,0,0,0,28,29,5,70,0,0,29,30,5,
		82,0,0,30,31,5,79,0,0,31,32,5,77,0,0,32,6,1,0,0,0,33,34,5,87,0,0,34,35,
		5,72,0,0,35,36,5,69,0,0,36,37,5,82,0,0,37,38,5,69,0,0,38,8,1,0,0,0,39,
		40,5,44,0,0,40,10,1,0,0,0,41,42,5,46,0,0,42,12,1,0,0,0,43,44,5,61,0,0,
		44,14,1,0,0,0,45,49,7,0,0,0,46,48,7,1,0,0,47,46,1,0,0,0,48,51,1,0,0,0,
		49,47,1,0,0,0,49,50,1,0,0,0,50,16,1,0,0,0,51,49,1,0,0,0,52,54,7,2,0,0,
		53,52,1,0,0,0,54,55,1,0,0,0,55,53,1,0,0,0,55,56,1,0,0,0,56,57,1,0,0,0,
		57,58,6,8,0,0,58,18,1,0,0,0,3,0,49,55,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
