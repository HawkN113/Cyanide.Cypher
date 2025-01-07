namespace Cyanide.Cypher.Builders;

public enum RelationshipType
{
    NonDirect, //-[]-
    Direct, //-[]->
    InDirect, //<-[]-
    UnDirect, //<-[]->
    BiDirect //->[]<-
}