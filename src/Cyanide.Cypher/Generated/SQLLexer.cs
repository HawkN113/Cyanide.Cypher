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
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, IDENTIFIER=24, 
		STRING=25, NUMBER=26, WS=27, COMMENT=28;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "IDENTIFIER", "STRING", 
		"NUMBER", "WS", "COMMENT"
	};


	public SQLLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public SQLLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'SELECT'", "'FROM'", "'*'", "','", "'JOIN'", "'ON'", "'INNER'", 
		"'LEFT'", "'RIGHT'", "'FULL'", "'WHERE'", "'GROUP BY'", "'HAVING'", "'('", 
		"')'", "'='", "'<'", "'>'", "'<='", "'>='", "'<>'", "'AND'", "'OR'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		"IDENTIFIER", "STRING", "NUMBER", "WS", "COMMENT"
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
		4,0,28,198,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,1,0,1,
		0,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,2,1,2,1,3,1,3,1,4,1,4,1,4,
		1,4,1,4,1,5,1,5,1,5,1,6,1,6,1,6,1,6,1,6,1,6,1,7,1,7,1,7,1,7,1,7,1,8,1,
		8,1,8,1,8,1,8,1,8,1,9,1,9,1,9,1,9,1,9,1,10,1,10,1,10,1,10,1,10,1,10,1,
		11,1,11,1,11,1,11,1,11,1,11,1,11,1,11,1,11,1,12,1,12,1,12,1,12,1,12,1,
		12,1,12,1,13,1,13,1,14,1,14,1,15,1,15,1,16,1,16,1,17,1,17,1,18,1,18,1,
		18,1,19,1,19,1,19,1,20,1,20,1,20,1,21,1,21,1,21,1,21,1,22,1,22,1,22,1,
		23,1,23,5,23,154,8,23,10,23,12,23,157,9,23,1,24,1,24,5,24,161,8,24,10,
		24,12,24,164,9,24,1,24,1,24,1,25,4,25,169,8,25,11,25,12,25,170,1,25,1,
		25,4,25,175,8,25,11,25,12,25,176,3,25,179,8,25,1,26,4,26,182,8,26,11,26,
		12,26,183,1,26,1,26,1,27,1,27,1,27,1,27,5,27,192,8,27,10,27,12,27,195,
		9,27,1,27,1,27,1,162,0,28,1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,
		21,11,23,12,25,13,27,14,29,15,31,16,33,17,35,18,37,19,39,20,41,21,43,22,
		45,23,47,24,49,25,51,26,53,27,55,28,1,0,5,3,0,65,90,95,95,97,122,4,0,48,
		57,65,90,95,95,97,122,1,0,48,57,3,0,9,10,13,13,32,32,2,0,10,10,13,13,204,
		0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,
		0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,0,23,
		1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,1,0,0,
		0,0,35,1,0,0,0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,0,43,1,0,0,0,0,45,
		1,0,0,0,0,47,1,0,0,0,0,49,1,0,0,0,0,51,1,0,0,0,0,53,1,0,0,0,0,55,1,0,0,
		0,1,57,1,0,0,0,3,64,1,0,0,0,5,69,1,0,0,0,7,71,1,0,0,0,9,73,1,0,0,0,11,
		78,1,0,0,0,13,81,1,0,0,0,15,87,1,0,0,0,17,92,1,0,0,0,19,98,1,0,0,0,21,
		103,1,0,0,0,23,109,1,0,0,0,25,118,1,0,0,0,27,125,1,0,0,0,29,127,1,0,0,
		0,31,129,1,0,0,0,33,131,1,0,0,0,35,133,1,0,0,0,37,135,1,0,0,0,39,138,1,
		0,0,0,41,141,1,0,0,0,43,144,1,0,0,0,45,148,1,0,0,0,47,151,1,0,0,0,49,158,
		1,0,0,0,51,168,1,0,0,0,53,181,1,0,0,0,55,187,1,0,0,0,57,58,5,83,0,0,58,
		59,5,69,0,0,59,60,5,76,0,0,60,61,5,69,0,0,61,62,5,67,0,0,62,63,5,84,0,
		0,63,2,1,0,0,0,64,65,5,70,0,0,65,66,5,82,0,0,66,67,5,79,0,0,67,68,5,77,
		0,0,68,4,1,0,0,0,69,70,5,42,0,0,70,6,1,0,0,0,71,72,5,44,0,0,72,8,1,0,0,
		0,73,74,5,74,0,0,74,75,5,79,0,0,75,76,5,73,0,0,76,77,5,78,0,0,77,10,1,
		0,0,0,78,79,5,79,0,0,79,80,5,78,0,0,80,12,1,0,0,0,81,82,5,73,0,0,82,83,
		5,78,0,0,83,84,5,78,0,0,84,85,5,69,0,0,85,86,5,82,0,0,86,14,1,0,0,0,87,
		88,5,76,0,0,88,89,5,69,0,0,89,90,5,70,0,0,90,91,5,84,0,0,91,16,1,0,0,0,
		92,93,5,82,0,0,93,94,5,73,0,0,94,95,5,71,0,0,95,96,5,72,0,0,96,97,5,84,
		0,0,97,18,1,0,0,0,98,99,5,70,0,0,99,100,5,85,0,0,100,101,5,76,0,0,101,
		102,5,76,0,0,102,20,1,0,0,0,103,104,5,87,0,0,104,105,5,72,0,0,105,106,
		5,69,0,0,106,107,5,82,0,0,107,108,5,69,0,0,108,22,1,0,0,0,109,110,5,71,
		0,0,110,111,5,82,0,0,111,112,5,79,0,0,112,113,5,85,0,0,113,114,5,80,0,
		0,114,115,5,32,0,0,115,116,5,66,0,0,116,117,5,89,0,0,117,24,1,0,0,0,118,
		119,5,72,0,0,119,120,5,65,0,0,120,121,5,86,0,0,121,122,5,73,0,0,122,123,
		5,78,0,0,123,124,5,71,0,0,124,26,1,0,0,0,125,126,5,40,0,0,126,28,1,0,0,
		0,127,128,5,41,0,0,128,30,1,0,0,0,129,130,5,61,0,0,130,32,1,0,0,0,131,
		132,5,60,0,0,132,34,1,0,0,0,133,134,5,62,0,0,134,36,1,0,0,0,135,136,5,
		60,0,0,136,137,5,61,0,0,137,38,1,0,0,0,138,139,5,62,0,0,139,140,5,61,0,
		0,140,40,1,0,0,0,141,142,5,60,0,0,142,143,5,62,0,0,143,42,1,0,0,0,144,
		145,5,65,0,0,145,146,5,78,0,0,146,147,5,68,0,0,147,44,1,0,0,0,148,149,
		5,79,0,0,149,150,5,82,0,0,150,46,1,0,0,0,151,155,7,0,0,0,152,154,7,1,0,
		0,153,152,1,0,0,0,154,157,1,0,0,0,155,153,1,0,0,0,155,156,1,0,0,0,156,
		48,1,0,0,0,157,155,1,0,0,0,158,162,5,39,0,0,159,161,9,0,0,0,160,159,1,
		0,0,0,161,164,1,0,0,0,162,163,1,0,0,0,162,160,1,0,0,0,163,165,1,0,0,0,
		164,162,1,0,0,0,165,166,5,39,0,0,166,50,1,0,0,0,167,169,7,2,0,0,168,167,
		1,0,0,0,169,170,1,0,0,0,170,168,1,0,0,0,170,171,1,0,0,0,171,178,1,0,0,
		0,172,174,5,46,0,0,173,175,7,2,0,0,174,173,1,0,0,0,175,176,1,0,0,0,176,
		174,1,0,0,0,176,177,1,0,0,0,177,179,1,0,0,0,178,172,1,0,0,0,178,179,1,
		0,0,0,179,52,1,0,0,0,180,182,7,3,0,0,181,180,1,0,0,0,182,183,1,0,0,0,183,
		181,1,0,0,0,183,184,1,0,0,0,184,185,1,0,0,0,185,186,6,26,0,0,186,54,1,
		0,0,0,187,188,5,45,0,0,188,189,5,45,0,0,189,193,1,0,0,0,190,192,8,4,0,
		0,191,190,1,0,0,0,192,195,1,0,0,0,193,191,1,0,0,0,193,194,1,0,0,0,194,
		196,1,0,0,0,195,193,1,0,0,0,196,197,6,27,0,0,197,56,1,0,0,0,8,0,155,162,
		170,176,178,183,193,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
