﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace ProyectoCompilador.Recursos
{
    class Gramatica:Grammar
    {
        public Gramatica():base(caseSensitive:true) 
        {
            //Prueba

            #region er
            StringLiteral CADENA = new StringLiteral("cadena", "\"");
            RegexBasedTerminal letras = new RegexBasedTerminal("letras", "[(a-z)*(A-Z)*(0-9)*]+");
            var ENTERO = new RegexBasedTerminal("Entero", "[0-9]+");
            var DECIMAL = new RegexBasedTerminal("Decimal", "[0-9]+[.]+[0-9]+");
            IdentifierTerminal IDENTIFICADOR = new IdentifierTerminal("ID");

            //CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "/", "/");

            CommentTerminal comentarioLinea = new CommentTerminal("SingleLineComment", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
            CommentTerminal comentarioBloque = new CommentTerminal("DelimitedComment", "/*", "*/");
            //var whiteSpace = new 
            NonGrammarTerminals.Add(comentarioLinea);
            NonGrammarTerminals.Add(comentarioBloque);
            #endregion

            #region KeyWords de Puntuacion
            KeyTerm dosPuntos = ToTerm(":", "dosPuntos");
            KeyTerm puntoComa = ToTerm(";", "puntoComa");
            KeyTerm punto = ToTerm(".", "punto");
            KeyTerm coma = ToTerm(",", "coma");
            KeyTerm parentesisIzq = ToTerm("(", "parentesisIzq");
            KeyTerm parentesisDer = ToTerm(")", "parentesisDer");
            KeyTerm llaveIzq = ToTerm("{", "llaveIzq");
            KeyTerm llaveDer = ToTerm("}", "llaveDer");
            #endregion

            #region KeyWords Operadores, Comparadores, nombre variables, 
            KeyTerm plus = ToTerm("+", "plus");
            KeyTerm minus = ToTerm("-", "minus");
            KeyTerm por = ToTerm("*", "por");
            KeyTerm div = ToTerm("/", "div");
            KeyTerm plusPlus = ToTerm("++", "plusPlus");
            KeyTerm minusMinus = ToTerm("--", "menosMenos");

            //Comparadores
            KeyTerm mayorQue = ToTerm(">", "mayorQue");
            KeyTerm menorQue = ToTerm("<", "menorQue");
            KeyTerm igual = ToTerm("=", "igual");
            KeyTerm mayorIgual = ToTerm(">=", "mayorIgual");
            KeyTerm menorIgual = ToTerm("<=", "menorIgual");
            KeyTerm diferente = ToTerm("!=", "diferente");
            KeyTerm igualIgual = ToTerm("==", "igualIgual");

            //Variables
            KeyTerm varInt = ToTerm("int");
            KeyTerm varFloat = ToTerm("float");
            KeyTerm varString = ToTerm("string");
            KeyTerm varBoolean = ToTerm("bool");

            //Ciclos y condicionales
            KeyTerm condIf = ToTerm("if");
            KeyTerm condElif = ToTerm("elif");
            KeyTerm condElse = ToTerm("else");
            KeyTerm kwBreack = ToTerm("break");
            KeyTerm kwCase = ToTerm("case");
            KeyTerm kwClass = ToTerm("class");
            KeyTerm kwTry = ToTerm("Try");
            KeyTerm kwCatch = ToTerm("Catch");
            KeyTerm kwWhile = ToTerm("while");
            KeyTerm kwDowhile = ToTerm("do while");
            KeyTerm kwFor = ToTerm("for");
            KeyTerm kwPublic = ToTerm("public");
            KeyTerm kwThrow = ToTerm("Throw");
            KeyTerm kwVoid = ToTerm("Void");
            KeyTerm kwSwitch = ToTerm("Switch");
            KeyTerm kwTrue = ToTerm("true");
            KeyTerm kwFalse = ToTerm("false");
            #endregion

            #region Terminales
            NonTerminal ini = new NonTerminal("ini");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal expresion_numerica = new NonTerminal("expresion_numerica");
            NonTerminal expresion_cadena = new NonTerminal("expresion_cadena");
            NonTerminal expresion_logica = new NonTerminal("expresion_logica");

            //No terminales para declarar variables
            NonTerminal declararVar = new NonTerminal("decVar");
            NonTerminal decInt = new NonTerminal("decInt");
            NonTerminal decFloat = new NonTerminal("decFloat");
            NonTerminal decString = new NonTerminal("decString");
            NonTerminal decBool = new NonTerminal("decBool");

            var entradaID = new NonTerminal("entradaID");


            #endregion

            #region No terminales
            #endregion

            #region Gramatica
            ini.Rule = instrucciones;


            //Declarar varibles
            declararVar.Rule = decInt | decFloat | decString | decBool | declararVar;

            decInt.Rule = varInt + entradaID + igual + ENTERO + puntoComa;
            decFloat.Rule = (varFloat + entradaID + igual + DECIMAL + puntoComa);
            decString.Rule = (varString + entradaID + igual + (CADENA) + puntoComa);
            decBool.Rule = (varBoolean + entradaID + igual + (kwTrue | kwFalse) + puntoComa);

            entradaID.Rule = MakePlusRule(entradaID, ToTerm(","), IDENTIFICADOR);


            #endregion


            #region Jesus IF

            //----------------------IF------------------

            //En estos if solo se podran hacer 2 condiciones maximo por ejemplo if(a==2 && b=false){}
            //No terminales para condicional if
            NonTerminal condicionIF = new NonTerminal("condicionIF");
            NonTerminal condicionIFELSE = new NonTerminal("condicionIFELSE");
            NonTerminal condicionalIFELSEIF = new NonTerminal("condicionIFELSEIF");
            NonTerminal condicionELSE = new NonTerminal("condicionELSE");
            NonTerminal condicionELSEIF = new NonTerminal("condicionELSEIF");
            NonTerminal expComparacion = new NonTerminal("expComparacion");
            NonTerminal nuevaCondicion = new NonTerminal("nuevaCondicion");
            NonTerminal nuevaCondicion2 = new NonTerminal("nuevaCondicion2");
            NonTerminal compIgualDiferente = new NonTerminal("compIgualDiferente");
            NonTerminal compMayorMenor = new NonTerminal("comMayorMenor");
            NonTerminal compMayorMenorIgual = new NonTerminal("comMayorMenor");
            NonTerminal compBool = new NonTerminal("comBool");

            KeyTerm signoOR = ToTerm("|");
            KeyTerm signoAND = ToTerm("&&");

            //Nota: El Ciclo if que se presenta se modificara para poder poner mas codigo dentro del ciclo if

            //Declarar un if
            condicionIF.Rule = condIf + parentesisIzq + (expComparacion | (expComparacion + nuevaCondicion)) + parentesisDer + llaveIzq + llaveDer;
            expComparacion.Rule = (parentesisIzq + (compIgualDiferente | compMayorMenor | compMayorMenorIgual | compBool) + parentesisDer) | (compIgualDiferente | compMayorMenor | compMayorMenorIgual | compBool);
            compIgualDiferente.Rule = ((DECIMAL + igualIgual + DECIMAL) | (ENTERO + igualIgual + ENTERO) | (entradaID + igualIgual + (ENTERO | DECIMAL | CADENA))) |
                                      ((DECIMAL + diferente + DECIMAL) | (ENTERO + diferente + ENTERO) | (entradaID + diferente + (ENTERO | DECIMAL | CADENA)));
            compMayorMenor.Rule = ((DECIMAL + mayorQue + DECIMAL) | (ENTERO + mayorQue + ENTERO) | (entradaID + mayorQue + (ENTERO | DECIMAL | CADENA))) |
                                      ((DECIMAL + menorQue + DECIMAL) | (ENTERO + menorQue + ENTERO) | (entradaID + menorQue + (ENTERO | DECIMAL | CADENA)));
            compMayorMenorIgual.Rule = ((DECIMAL + mayorIgual + DECIMAL) | (ENTERO + mayorIgual + ENTERO) | (entradaID + mayorIgual + (ENTERO | DECIMAL | CADENA))) |
                                      ((DECIMAL + menorIgual + DECIMAL) | (ENTERO + menorIgual + ENTERO) | (entradaID + menorIgual + (ENTERO | DECIMAL | CADENA)));
            compBool.Rule = entradaID + igual + (kwTrue | kwFalse);
            nuevaCondicion.Rule = ((signoAND | signoOR) + expComparacion) | ((signoAND | signoOR) + expComparacion + nuevaCondicion2);
            nuevaCondicion2.Rule = nuevaCondicion;

            //Declarar un if else
            condicionIFELSE.Rule = condicionIF + condicionELSE;
            condicionELSE.Rule = condElse + llaveIzq + llaveDer;

            //Declarar un if else if
            condicionalIFELSEIF.Rule = condicionIF + condicionELSEIF;
            condicionELSEIF.Rule = condElse + (condicionIF | condicionELSE | condicionalIFELSEIF);

            #endregion

            #region ADRI SWITCH
            NonTerminal condicionSwitch = new NonTerminal("condicionSwitch");
            NonTerminal switchOpcion = new NonTerminal("switchOpcion");
            NonTerminal opcionCase = new NonTerminal("opcionCase");
            NonTerminal opcionDefault = new NonTerminal("opcionDefault");
            //poner con las keyterm
            KeyTerm kwDefault = ToTerm("default");
            opcionDefault.Rule = kwDefault + dosPuntos + kwBreack + puntoComa;
            opcionCase.Rule = kwCase + switchOpcion + dosPuntos + kwBreack + puntoComa + (opcionCase | opcionDefault);
            switchOpcion.Rule = DECIMAL | ENTERO | CADENA | entradaID;
            condicionSwitch.Rule = kwSwitch + parentesisIzq + switchOpcion + parentesisDer + llaveIzq
                                + opcionCase + llaveDer; //cierre de Switch
            #endregion

            #region Jesus Imprimir
            //-----------------------Print

            KeyTerm kwConsole = ToTerm("Console");
            KeyTerm kwWrite = ToTerm("Write");
            KeyTerm kwWriteLn = ToTerm("WriteLine");

            NonTerminal imprimir = new NonTerminal("imprimir");
            NonTerminal imprimirWrite = new NonTerminal("imprimirWrite");
            NonTerminal imprimirWriteLn = new NonTerminal("imprimirWriteLn");
            NonTerminal textoOVariable = new NonTerminal("textoOVariable");

            imprimir.Rule = kwConsole + punto + (imprimirWrite | imprimirWriteLn) + puntoComa;
            imprimirWrite.Rule = kwWrite + parentesisIzq + textoOVariable + parentesisDer;
            imprimirWriteLn.Rule = kwWriteLn + parentesisIzq + textoOVariable + parentesisDer;
            textoOVariable.Rule = (CADENA | entradaID) | ((CADENA | entradaID) + plus + textoOVariable);
            #endregion

            #region Jesus Operaciones
            //-------------------------Operaciones

            NonTerminal operaciones = new NonTerminal("operaciones");
            NonTerminal opeSuma = new NonTerminal("opeSuma");
            NonTerminal opeResta = new NonTerminal("opeResta");
            NonTerminal opeMultiplicacion = new NonTerminal("opeMultiplicacion");
            NonTerminal opeDivision = new NonTerminal("opeDivision");
            NonTerminal opeCompuesta = new NonTerminal("opeCompuesta");
            NonTerminal datoEntradaOpe = new NonTerminal("datoEntradaOpe");

            operaciones.Rule = ((opeSuma | opeResta | opeMultiplicacion | opeDivision) + puntoComa) | (opeCompuesta + puntoComa);
            opeSuma.Rule = datoEntradaOpe | (datoEntradaOpe + plus + opeSuma) | (parentesisIzq + opeSuma + parentesisDer) | (plus + datoEntradaOpe);
            opeResta.Rule = datoEntradaOpe | (datoEntradaOpe + minus + opeResta) | (parentesisIzq + opeResta + parentesisDer) | (minus + datoEntradaOpe);
            opeMultiplicacion.Rule = datoEntradaOpe | (datoEntradaOpe + por + opeMultiplicacion) | (parentesisIzq + opeMultiplicacion + parentesisDer) | (por + datoEntradaOpe) | (parentesisIzq + opeMultiplicacion + parentesisDer);
            opeDivision.Rule = datoEntradaOpe | (datoEntradaOpe + div + opeDivision) | (parentesisIzq + opeDivision + parentesisDer) | (div + datoEntradaOpe);
            opeCompuesta.Rule = (opeSuma | opeResta | opeMultiplicacion | opeDivision) | ((opeSuma | opeResta | opeMultiplicacion | opeDivision) + opeCompuesta);
            datoEntradaOpe.Rule = (ENTERO | DECIMAL | entradaID) | (parentesisIzq + datoEntradaOpe + parentesisDer) | (parentesisIzq + (opeSuma | opeResta | opeMultiplicacion | opeDivision) + parentesisDer);


            #endregion

            #region Jesus funciones y clases
            //------------------funciones de clases
            NonTerminal nameSpace = new NonTerminal("nameSpace");
            NonTerminal ntClass = new NonTerminal("ntClass");
            NonTerminal ntMain = new NonTerminal("ntMain");
            NonTerminal ntPublic = new NonTerminal("ntPublic");

            nameSpace.Rule = "namespace" + entradaID + llaveIzq + ntClass + llaveDer;
            ntClass.Rule = kwClass + entradaID + llaveIzq + (ntPublic | ntMain) + llaveDer;
            ntMain.Rule = "static void Main" + parentesisIzq + varString + "[] args" + parentesisDer + llaveIzq + llaveDer;
            ntPublic.Rule = kwPublic + entradaID + "()" + llaveIzq + llaveDer;
            #endregion

            #region Preferencias
            #endregion
        }
    }
}
