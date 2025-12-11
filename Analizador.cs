using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AnalizadorPascal
{
    public enum tipo { NumE, NumR, Lit, Esp, GnBajo, Asig, PC, C, P, PI, PF, CA, CC, Sum, Menos, Mul, Div, ComSL, ComMl, Igual, Me, Ma, Var, Key, Tab, SL, ERROR_a, ERROR_b, twoPuntos };

    public class DatoTabla
    {
        public string tokens { get; set; }
        public string caracter { get; set; }
        public string tipo { get; set; }
    }
    public class Analizador
    {
        private string CodePascal { get; set; }
        private int[][] Automatas =
        [
            // 0=Digito, 1=Comilla, 2=Mayus, 3=Minus, 4=Espacio, 5=G.Bajo, 6=':', 7='=', 8=';', 9=',', 10='.', 11='(', 12=')', 13='[', 14=']', 15='+', 16='-', 17='*', 18='/', 19='<', 20='>', 21='Tab', 22='SL/CR', 23=Otro
            [1,8,35,35,38,35,11,32,13,14,5,16,17,18,19,20,21,31,22,33,34,39,40,45],
            [1,15,15,15,2,15,15,15,2,15,3,2,2,2,2,2,2,2,2,2,2,2,2,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [3,15,15,15,4,15,15,15,4,15,15,4,4,4,4,4,4,4,4,4,4,4,4,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [3,15,15,15,7,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,7,7,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [9,10,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,15,45],
            [9,10,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,15,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0, 0, 12, 12, 12, 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [30,15,30,30,30,15,15,15,15,15,15,15,15,15,15,15,15,26,23,15,15,30,30,45],
            [24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,25,45],
            [24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,24,25,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,28,27,27,27,27,27,45],
            [27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,28,27,27,27,27,27,45],
            [27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,29,27,27,27,27,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [36,37,36,36,37,36,37,15,37,37, 37, 37,37,37,37,37,37,37,37,37,37,37,37,45],
            [36,37,36,36,37,36,37,15,37,37, 37, 37,37,37,37,37,37,37,37,37,37,37,37,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,45],
            [-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1],
        ];
        private List<string> alfabeto = [
            @"^[0-9]$",                    // 0: Dígitos
            @"^'$",                        // 1: Comilla simple
            @"^[A-Z]$",                    // 2: Letras mayúsculas
            @"^[a-z]$",                    // 3: Letras minúsculas
            @"^ $",                        // 4: Espacio
            @"^_$",                        // 5: Guion bajo
            @"^:$",                        // 6: Dos puntos
            @"^=$",                        // 7: Igual
            @"^;$",                        // 8: Punto y coma
            @"^,$",                        // 9: Coma
            @"^\.$",                       // 10: Punto (escapado)
            @"^\($",                       // 11: Paréntesis izquierdo (escapado)
            @"^\)$",                       // 12: Paréntesis derecho (escapado)
            @"^\[$",                       // 13: Corchete izquierdo (escapado)
            @"^\]$",                       // 14: Corchete derecho (escapado)
            @"^\+$",                       // 15: Suma (escapado)
            @"^-$",                        // 16: Menos/resta
            @"^\*$",                       // 17: Multiplicación (escapado)
            @"^/$",                        // 18: División (NO escapado, no es necesario)
            @"^<$",                        // 19: Menor que
            @"^>$",                        // 20: Mayor que
            @$"^{(char)9}$",                       // 21: Tabulación (usando \t)
            @$"^{(char)10}$",                       // 23: Salto de línea (agregado)
        ];

        private List<string> tt = [
            "Numero Entero", "Numero Real", "Literales", "Espacio", "Guion bajo", "Asignacion", "Punto y Coma", "Coma", "Punto",
            "Parentesis Inicio", "Parentesis Cerrado", "Corchete Abierto", "Corchete Cerrado", "Suma", "Menos",
            "Multiplicacion", "Division", "Comentario SL", "Comentario ML", "Igual", "Menor que", "Mayor que", "Identificador", "Keywords", "Tabulacion",
            "Salto de Linea", "Error Lexico", "Error Ahhh", "2 puntos"
            ];


        private int[][] AutomataOrdenProgram =
        [
            [1,0,0,-1,-1,0,6,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                        ,
            [-1,1,2,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                     ,
            [-1,2,2,3,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                      ,
            [-1,3,-1,-1,2,4,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                      ,
            [-1,4,5,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                     ,
            [-1,5,5,-1,-1,-1,6,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                      ,
            [-1,7,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                        ,
            [-1,7,-1,8,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                     ,
            [-1,8,-1,-1,-1,9,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                     ,
            [-1,9,10,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                        ,
            [-1,10,10,-1,-1,10,-1,11,-1,-1,18,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,11,12,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,12,12,13,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,13,-1,-1,-1,-1,-1,-1,14,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,14,-1,-1,-1,-1,-1,-1,-1,15,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,15,-1,-1,-1,16,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,16,17,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,17,17,13,-1,-1,-1,-1,-1,-1,18,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,18,19,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,19,19,20,-1,19,-1,-1,-1,-1,-1,-1,-1,23,23,-1,-1,26,-1,19]                                                       ,
            [-1,20,-1,-1,-1,-1,-1,-1,-1,-1,-1,21,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,21,-1,22,-1,-1,-1,-1,-1,-1,-1,-1,22,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,22,-1,-1,-1,18,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,24,-1,-1,-1,-1]                                                       ,
            [-1,24,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,25,-1,-1,-1,-1,-1,-1,-1]                                                       ,
            [-1,25,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,22,-1,-1,-1]                                                       ,
            [-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,27,-1]                                                       ,
            [-1,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27,27]

        ];

        private List<string> orden = [
            "uses", "$Espacio", "$Salto de Linea", "$Identificador",
            "$Coma", "$Punto y Coma", "program", "var", "2 puntos", "Tipo",
            "begin", "$Asignacion", "valor", "write", "writeln", "(", ")",
            "end", "$Punto", "$$"
            ];
        private List<string> keywords = [
            "and", "case", "file", "label", "packed", "then", "array", "const",
            "for", "library", "procedure", "to", "asm", "constructor", "forward",
            "mod", "program", "type", "begin", "destructor", "function", "nil",
            "record", "unit", "break", "div", "goto", "not", "repeat", "until",
            "do", "if", "object", "set", "uses", "downto", "implementation",
            "of", "shl", "var", "else", "in", "on", "shr", "while", "end",
            "inherited", "or", "string", "with", "exports", "inline", "xor",
            "interface", "interrupt", "is", "writeln", "write", "Integer", "Real", "String", "Char", "Boolean"
            ];
        private int[] lineAuto; 
        private string palabra;
        private int lastPosition;
        public bool isCorrectWrite = true;
        public bool isCorrectOrden = true;

        public Analizador(string codePascal)
        {
            CodePascal = codePascal+ $"{(char)13}";

            lineAuto = Automatas[0];
            palabra = "";
            lastPosition = -1;
        }
        public int GetIndex(string item)
        {
            char c = item[0];

            if (c >= '0' && c <= '9') return 0;
            if (c >= 'A' && c <= 'Z') return 2;
            if (c >= 'a' && c <= 'z') return 3;
            if (c == (char)10 || c == (char)13) return 22;

            return c switch
            {
                '\'' => 1,
                ' ' => 4,
                '_' => 5,
                ':' => 6,
                '=' => 7,
                ';' => 8,
                ',' => 9,
                '.' => 10,
                '(' => 11,
                ')' => 12,
                '[' => 13,
                ']' => 14,
                '+' => 15,
                '-' => 16,
                '*' => 17,
                '/' => 18,
                '<' => 19,
                '>' => 20,
                (char)9 => 21,
                _ => alfabeto.Count(),
            };
        
        }
        public int GetIndexM(DatoTabla item)
        {
            foreach (var fil in orden.Select((C, i) => new { C, i }))
            {
               // if(item.caracter == "end") Console.Write($"fil = {fil.C}; itemc = {item.caracter}, itemt {item.tipo} \t");

                if(fil.C == "valor")
                {
                    if (tt.Where((t, i) => i < 3).ToList().Contains(item.tipo))
                    {
                        return fil.i;
                    }
                }
                if (fil.C == item.caracter)
                {
                    return fil.i ;
                }
                if (fil.C[0] == '$')
                {
                    if(fil.C.Substring(1) == item.tipo)
                    {
                        return fil.i;
                    }
                    if (fil.C.Substring(1)[0] == '$')
                    {
                        if (!new List<string> { "program", "begin", "end" }.Contains(item.caracter))
                        {
                            return fil.i;
                        }
                    }
                }
            }
            return -1;
        }
        public void reload(ref int i, ref string item,  bool isretry = false)
        {
            
            Console.WriteLine($"<--{lastPosition}   {i}-->");
            lineAuto = Automatas[0];
            if(lastPosition != i)
            {
                if (isretry)
                {
                    palabra = palabra.Substring(0, palabra.Length - 1);
                    --i;
                }
            }
                lastPosition = i;
            
            Console.WriteLine($"--{palabra}--");
        }
        public List<DatoTabla> list = new List<DatoTabla>();
        public async Task<List<DatoTabla>> GetGeneratorDataT(string codePas = null)
        {
            list = new List<DatoTabla>();
            int index = 0;
            if (codePas != null) CodePascal = codePas + $"{(char)13}";

            isCorrectWrite = true;
            Console.WriteLine(CodePascal.Count());
            bool isfirst = true;
            for ( int i = 0; i< CodePascal.Count(); i++)
            {
                string item = CodePascal.Substring(i,1) ;
                var pos = GetIndex(item);
                index = lineAuto[pos];


                Console.WriteLine($"tokens {i} " + index + "  " + string.Join(",", lineAuto) + " " + Automatas.Count() + " " + item + " " + pos + " " + ((int)item.ToCharArray()[0]));
                lineAuto = Automatas[index];

                palabra += item.ToString();
               

                if (index == 2)
                {
                    reload(ref i, ref item, true);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.NumE)] });
                    palabra = "";
                    continue;
                }
                else if (index == 4)
                {//literales
                    reload(ref i, ref item, true);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.NumR)] });
                    palabra = "";
                    continue;
                }
                else if (index == 10)
                {//literales
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Lit)] });
                    palabra = "";
                    continue;
                }
                else if (index == 12)
                {// asignacion

                    var asig = palabra.Contains(":=");
                    reload(ref i, ref item, !asig);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[asig ? ((int)tipo.Asig) : ((int)tipo.twoPuntos)] });
                    palabra = "";
                    continue;
                }
                
                else if (index == 13)
                {//punto y goma
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.PC)] });
                    palabra = "";
                    continue;
                }
                else if (index == 14)
                {//goma
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.C)] });
                    palabra = "";
                    continue;
                }
                else if (index == 7)
                {//punto
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.P)] });
                    palabra = "";
                    continue;
                }
                else if (index == 16)
                {//parenteris Inicio
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.PI)] });
                    palabra = "";
                    continue;
                }
                else if (index == 17)
                {//parenteris Final
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.PF)] });
                    palabra = "";
                    continue;
                }
                else if (index == 18)
                {//Corchete Inicio
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.CA)] });
                    palabra = "";
                    continue;
                }
                else if (index == 19)
                {//Corchete Final
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.CC)] });
                    palabra = "";
                    continue;
                }
                else if (index == 20)
                {//mas
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Sum)] });
                    palabra = "";
                    continue;
                }
                else if (index == 19)
                {//menos
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Menos)] });
                    palabra = "";
                    continue;
                }
                else if (index == 25)
                {//ComSL
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.ComSL)] });
                    palabra = "";
                    continue;
                }
                else if (index == 29)
                {//ComML
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.ComMl)] });
                    palabra = "";
                    continue;
                }
                else if (index == 30)
                {//div
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Div)] });
                    palabra = "";
                    continue;
                }
                else if (index == 31)
                {//mul
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Mul)] });
                    palabra = "";
                    continue;
                }


                else if (index == 32)
                {//igual
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Igual)] });
                    palabra = "";
                    continue;
                }
                else if (index == 33)
                {//menorq
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Me)] });
                    palabra = "";
                    continue;
                }
                else if (index == 34)
                {//mayorq
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Ma)] });
                    palabra = "";
                    continue;
                }
                else if (index == 37)
                {//id o reserv
                    reload(ref i, ref item, true);
                    list.Add(new DatoTabla
                    {
                        tokens = $"{index}",
                        caracter = palabra,
                        tipo = tt[((int)(keywords.Contains(palabra) ? tipo.Key : tipo.Var))]
                    });
                    palabra = "";

                    continue;
                }
                else if (index == 38)
                {//espacio
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Esp)] });
                    palabra = "";

                    continue;
                }
                else if (index == 39)
                {//tab
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Tab)] });
                    palabra = "";

                    continue;
                }
                else if (index == 40)
                {//return
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.SL)] });
                    palabra = "";
                    continue;
                }
                else if (index == 15)
                {//error lexico
                    reload(ref i, ref item, false);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.ERROR_a)] });
                    palabra = "";
                    isCorrectWrite = false;
                    continue;
                }
                else if (index == 45)
                {//error de 
                    reload(ref i, ref item, false);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.ERROR_b)] });
                    palabra = "";
                    isCorrectWrite = false;
                    continue;
                }
                
 
            }



            lastPosition = 0;
            index = 0;
            var line = AutomataOrdenProgram[0];
            isCorrectOrden = false;
            foreach (var item in list)
            {
                lastPosition = GetIndexM(item);
                Console.Write($"line = {string.Join(",", line)}; item = {orden[lastPosition]}, j = {lastPosition}; {item.caracter} ");


               
                if (lastPosition == -1)
                {
                    lastPosition = list.IndexOf(item);
                    if (index == 0)
                    {
                        lastPosition = 0;
                    }
                    break;
                }
                index = line[lastPosition];
                if (index == 27) { isCorrectOrden = true; break; }

                Console.WriteLine(index);
                if (index == -1)
                {
                    lastPosition = list.IndexOf(item);
                    int columna = Array.IndexOf(AutomataOrdenProgram, line);
                    if (columna == 0)
                    {
                        lastPosition = 0;
                    }
                    break;
                } 
                
                line = AutomataOrdenProgram[index];
            }

            if (!isCorrectOrden )
            {
                // si el profesor varela no le llega gustar que todo este en rojo comenta este metodo asyncrono
                await seterror(lastPosition);
            }
            Console.WriteLine(isCorrectOrden);
            return list;
        }

        private async Task seterror(int v)
        {
            for (int i = v; i < list.Count(); i++)
            {
                var let = new DatoTabla { tokens = $"{list[i].tokens}", caracter = list[i].caracter, tipo = tt[(int)tipo.ERROR_a] };
               list[i] = let;
                
            }
        }
    }
}
