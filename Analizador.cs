using Microsoft.AspNetCore.SignalR;
using System;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AnalizadorPascal
{
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
            [1, 6, 25, 25, 28, 31, 9, 22, 11, 12, 3, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24, 29, 30, 31],
            [1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5],
            [2, 5, 5, 5, 5, 5, 5, 5, 4, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 31],
            [2, 5, 5, 5, 13, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 13, 13, 5],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31],
            [7, 8, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 5, 31],
            [7, 8, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 5, 31],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [31, 31, 31, 31, 31, 31, 31, 10, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [26, 5, 26, 26, 27, 26, 9, 5, 27, 5, 27, 27, 5, 5, 5, 5, 5, 5, 5, 5, 5, 27, 27, 5],
            [26, 5, 26, 26, 27, 26, 9, 5, 27, 5, 27, 27, 5, 5, 5, 5, 5, 5, 5, 5, 5, 27, 27, 5],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31, 31]
        ];
        private List<string> alfabeto = [
            @"[0-9]", @"'", @"[A-Z]", @"[a-z]", @" ", @"_", @":", @"=", @";", @",",
            @"\.", @"\(", @"\)", @"\[", @"\]", @"\+", @"\-", @"\*", @"/", @"<", @">",
            $@"{(char)9}", $@"{(char)13}"
            ];
        private enum tipo { Num, Lit, Esp, GnBajo, Asig, PC, C, P, PI, PF, CA, CC, Sum, Menos, Mul, Div, Igual, Me, Ma, Var, Key, Tab,SL, ERROR_a, ERROR_b };

        private List<string> tt = [
            "Numero", "Literales", "Espacio", "Guion bajo", "Asignacion", "Punto y Coma", "Coma", "Punto", 
            "Parentesis Inicio", "Parentesis Cerrado", "Corchete Abierto", "Corchete Cerrado", "Suma", "Menos", 
            "Multiplicacion", "Division", "Igual", "Menor que", "Mayor que", "Identificador", "Keywords", "Tabulacion", 
            "Salto de Linea", "Error Lexico", "Error Ahhh"
            ];
       

        private int[][] AutomataOrdenProgram =
        [
            [1,-1,-1,-1,-1,-1,-1,-1,-1],
            [-1,1,2,-1,-1,-1,-1,-1,-1],
            [-1,2,-1,3,-1,-1,-1,-1,-1],
            [-1,3,-1,4,4,-1,-1,-1,-1],
            [-1,4,-1,5,4,5,-1,-1,-1],
            [-1,5,-1,6,5,-1,5,6,-1],
            [-1,6,-1,7,6,-1,-1,-1,7]
        ];

        private List<string> orden = [
            "program", "$Espacio", "$Identificador", "$Punto y Coma", "$Salto de Linea", "begin", "$$",
            "end", "$Punto"
            ];
        private List<string> keywords = [
            "and", "case", "file", "label", "packed", "then", "array", "const",
            "for", "library", "procedure", "to", "asm", "constructor", "forward",
            "mod", "program", "type", "begin", "destructor", "function", "nil",
            "record", "unit", "break", "div", "goto", "not", "repeat", "until",
            "do", "if", "object", "set", "uses", "downto", "implementation",
            "of", "shl", "var", "else", "in", "on", "shr", "while", "end",
            "inherited", "or", "string", "with", "exports", "inline", "xor",
            "interface", "interrupt", "is", "writeln", "write"
            ];
        private int[] lineAuto; 
        private string palabra; 
        private int j;
        public bool isCorrectWrite = true;
        public bool isCorrectOrden = true;

        public Analizador(string codePascal)
        {
            CodePascal = codePascal+ $"{(char)13}";

            lineAuto = Automatas[0];
            palabra = "";
            j = -1;
        }
        public int GetIndex(string item)
        {
            foreach (var fil in alfabeto.Select((C, i) => new { C, i }))
            {
                if (Regex.IsMatch(item, fil.C))
                {
                    Debug.WriteLine($"  -> {item} == {(int)char.Parse(item)}  || {fil.i}");

                    return fil.i;
                }
            }
            return alfabeto.Count()-1;
        }
        public int GetIndexM(DatoTabla item)
        {
            foreach (var fil in orden.Select((C, i) => new { C, i }))
            {
                if(item.caracter == "end") Console.Write($"fil = {fil.C}; itemc = {item.caracter}, itemt {item.tipo} \t");

                if (fil.C == item.caracter)
                {
                    return fil.i;
                }
                if (fil.C[0] == '$')
                {
                    if(fil.C.Substring(1, fil.C.Length -1) == item.tipo)
                    {
                        return fil.i;
                    }
                    if (fil.C[1] == '$')
                    {
                        if (!new List<string> { "program", "begin", "end" }.Contains(item.caracter))
                        return fil.i;
                        
                    }
                }
            }
            return -1;
        }
        public void reload(ref int i, ref string item,  bool isretry = false)
        {
            
            Console.WriteLine($"<--{j}   {i}-->");
            lineAuto = Automatas[0];
            if(j != i)
            {
                if (isretry)
                {
                    j = i;

                    palabra = palabra.Substring(0, palabra.Length - 1);
                    --i;
                }
                else
                {
                    j = i;

                }
            }
            else
            {

                j = i;
            }

            Console.WriteLine($"--{palabra}--");


            /*
                        lineAuto = Automatas[0];
                        if (j == i)
                        {
                            palabra = "";

                        }
                        else
                        {
                            

                        }*/
        }
        public async Task<List<DatoTabla>> GetGeneratorDataT()
        {
            var list = new List<DatoTabla>();
            int index = 0;
            isCorrectWrite = true;
            Console.WriteLine(CodePascal.Count());
            for ( int i = 0; i< CodePascal.Count(); i++)
            {
                string item = CodePascal.Substring(i,1) ;

                index = lineAuto[GetIndex(item)];
                Console.WriteLine($"tokens{i} " + index + "  " + string.Join(",", lineAuto) + " " + Automatas.Count() + " " + item + " " + GetIndex(item));
                lineAuto = Automatas[index];

                palabra += item.ToString();
                if (index == 4)
                {
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Num)] });
                    palabra = "";
                    continue;
                } 
                else if (index == 8)
                {//literales
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Lit)]});
                    palabra = "";

                    continue;
                }
                else if (index == 10)
                {// asignacion
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Asig)] });
                    palabra = "";
                    continue;
                }
                else if (index == 11)
                {//punto y goma
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.PC)] });
                    palabra = "";
                    continue;
                }
                else if (index == 12)
                {//goma
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.C)] });
                    palabra = "";
                    continue;
                }
                else if (index == 13)
                {//punto
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.P)] });
                    palabra = "";
                    continue;
                }
                else if (index == 14)
                {//parenteris Inicio
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.PI)] });
                    palabra = "";
                    continue;
                }
                else if (index == 15)
                {//parenteris Final
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.PF)] });
                    palabra = "";
                    continue;
                }
                else if (index == 16)
                {//Corchete Inicio
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.CA)] });
                    palabra = "";
                    continue;
                }
                else if (index == 17)
                {//Corchete Final
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.CC)] });
                    palabra = "";
                    continue;
                }
                else if (index == 18)
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
                else if (index == 20)
                {//mul
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Mul)] });
                    palabra = "";
                    continue;
                }
                else if (index == 21)
                {//div
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Div)] });
                    palabra = "";
                    continue;
                }
                else if (index == 22)
                {//igual
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Igual)] });
                    palabra = "";
                    continue;
                }
                else if (index == 23)
                {//menorq
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Me)] });
                    palabra = "";
                    continue;
                }
                else if (index == 24)
                {//mayorq
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Ma)] });
                    palabra = "";
                    continue;
                }
                else if (index == 27)
                {//id o reserv
                    reload(ref i, ref item, true);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, 
                        tipo = tt[((int)(keywords.Contains(palabra) ? tipo.Key : tipo.Var))] });
                    palabra = "";

                    continue;
                }
                else if (index == 28)
                {//espacio
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Esp)] });
                    palabra = "";

                    continue;
                }
                else if (index == 29)
                {//tab
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Tab)] });
                    palabra = "";

                    continue;
                }
                else if (index == 30)
                {//return
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.SL)] });
                    palabra = "";
                    continue;
                }
                else if (index == 5)
                {//error lexico
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.ERROR_a)] });
                    palabra = "";
                    isCorrectWrite = false;
                    continue;
                }
                else if (index == 31)
                {//error de 404
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.ERROR_b)] });
                    palabra = "";
                    isCorrectWrite = false;
                    continue;
                }
                
                

            }
            
            
            
            j = 0;
            index = 0;
            var line = AutomataOrdenProgram[j];
            isCorrectOrden = false;
            foreach (var item in list)
            {
                j = GetIndexM(item);
                if (j == 7) { isCorrectOrden = true; break; }
                Console.Write($"line = {string.Join(",", line)}; item = {orden[j]}, j = {j}; {item.caracter}  ");

                if (j == -1)
                {
                    break;
                }
                index = line[j];

                Console.WriteLine(index);
                if (index == -1)
                {
                    break;
                }
                
                line = AutomataOrdenProgram[index];
            }
            
            Console.WriteLine(isCorrectOrden);
            return list;
        }
    }
}
