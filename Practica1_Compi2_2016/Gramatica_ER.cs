using System;
using System.Collections.Generic;
using System.Text;
using Irony.Parsing;

namespace Practica1_Compi2_2016
{
    class Gramatica_ER : Grammar
    {
        public Gramatica_ER()
        {
            //creacion de los terminales, y las ER que utilizara la gramatica
            #region Terminales

            IdentifierTerminal id = new IdentifierTerminal("identificador");
            NumberLiteral num = new NumberLiteral("numero");
            RegexBasedTerminal ascii = new RegexBasedTerminal("ascii", "[^\020\013\010\009]");



            #endregion

            //definicion de no terminales
            #region No Terminales

            NonTerminal S0 = new NonTerminal("S0"),
                        S1 = new NonTerminal("S1"),
                        L_CONTENIDO = new NonTerminal("L_CONTENIDO"),
                        E_CONT = new NonTerminal("E_CONT"),
                        CONTENIDO = new NonTerminal("CONT"),
                        CONJUNTO = new NonTerminal("CONJUNTO"),
                        RETORNO = new NonTerminal("RETORNO"),
                        L_CONJUNTO = new NonTerminal("L_CONJUNTO"),
                        EXP_REGULAR = new NonTerminal("EXP_REGULAR"),
                        CUANTIFICADORES = new NonTerminal("CUANTIFICADORES"),
                        OPERADOR = new NonTerminal("OPERADOR"),
                        OPERANDO = new NonTerminal("OPERANDO"),
                        RESERV = new NonTerminal("RESERV"),
                        TIPO = new NonTerminal("TIPO"),
                        RESERV_LIST = new NonTerminal("RESERV_LIST"),
                        RESERV_WORD = new NonTerminal("RESERV_WORD");

            #endregion


            #region Gramatica

            //S0 es el inicio de la gramatica

            S0.Rule = S1;

            S1.Rule = ToTerm("%%") + L_CONTENIDO + ToTerm("%%");

            L_CONTENIDO.Rule = ToTerm("CONJ") + ToTerm(":") + id + ToTerm("->") + CONJUNTO + ToTerm(";") + L_CONTENIDO
                | id + ToTerm("->") + EXP_REGULAR + ToTerm("->") + RETORNO + ToTerm(";") + L_CONTENIDO
                | ToTerm("error") + ToTerm("->") + EXP_REGULAR + ToTerm("->") + ToTerm("error") 
                    + ToTerm("(") + ToTerm("yyline") + ToTerm(",") + ToTerm("yyrow") + ToTerm(",") + ToTerm("yytext")
                    + ToTerm(")") + ToTerm(";") + E_CONT
                | Empty
                ;

            E_CONT.Rule = MakeStarRule(E_CONT, CONTENIDO)
                ;
            
            CONTENIDO.Rule = ToTerm("CONJ") + ToTerm(":") + id + ToTerm("->") + CONJUNTO + ToTerm(";")
                | id + ToTerm("->") + EXP_REGULAR + ToTerm("->") + RETORNO + ToTerm(";")
                ;

            RETORNO.Rule = ToTerm("retorno") + ToTerm("(") + id + ToTerm(",") + ToTerm("yytext") + ToTerm(",") + TIPO + ToTerm(",") + ToTerm("yyline") + ToTerm(",") + ToTerm("yyrow") + ToTerm(")") + RESERV
                | ToTerm("retorno") + ToTerm("(") + id + ToTerm(",") + ToTerm("yyline") + ToTerm(",") + ToTerm("yyrow") + ToTerm(")")
                ;

            RESERV.Rule = ToTerm("->")  + ToTerm("RESERV") + ToTerm("[") + RESERV_LIST + ToTerm("]")
                | Empty
                ;

            RESERV_LIST.Rule = MakePlusRule(RESERV_LIST,RESERV_WORD)
                ;

            RESERV_WORD.Rule = ToTerm("\"") + OPERANDO + ToTerm("\"") + ToTerm("->") + ToTerm("retorno") + ToTerm("(") + id + ToTerm(",") + ToTerm("yyline") + ToTerm(",") + ToTerm("yyrow") + ToTerm(")") + ToTerm(";")
                ;

            CONJUNTO.Rule = CONJUNTO + ToTerm(",") + CONJUNTO
                | CONJUNTO + ToTerm("~") + CONJUNTO
                | OPERANDO
                ;

            EXP_REGULAR.Rule = EXP_REGULAR + EXP_REGULAR + OPERADOR
                | EXP_REGULAR + CUANTIFICADORES
                | id
                | ToTerm("\'") + ascii + ToTerm("\'")
                | ToTerm("\"") + ascii + ToTerm("\"")
                | ToTerm("\\\'")
                | ToTerm("\\\"")
                | ToTerm("\\n")
                | ToTerm("\\t")
                | ToTerm("[:todo:]")
                | ToTerm("[:blanco:]")
                ;

            OPERADOR.Rule = ToTerm(".")
                | ToTerm("|")
                ;

            CUANTIFICADORES.Rule = ToTerm("+")
                | ToTerm("*")
                | ToTerm("?")
                ;

            TIPO.Rule = ToTerm("string")
                | ToTerm("char")
                | ToTerm("int")
                | ToTerm("float")
                | ToTerm("bool")
                ;

            OPERANDO.Rule = ascii
                | id
                | num
                ;

            this.Root = S0;
            //this.MarkReservedWords("CONJ", "retorno", "yytext", "yyline", "yyrow", "retorno", "RESERV");
            this.RegisterOperators(1, ",");
            this.RegisterOperators(2, "~");
            #endregion
        }
    }
}
