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
            [15,15,15,15,15,15,15,12,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,45],
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
            [36,37,36,36,27,36,37,15,37,37, 37, 37,37,37,37,37,37,37,37,37,37,37,37,45],
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
            @"[0-9]", @"'", @"[A-Z]", @"[a-z]", @" ", @"_", @":", @"=", @";", @",",
            @"\.", @"\(", @"\)", @"\[", @"\]", @"\+", @"\-", @"\*", @"\/", @"<", @">",
            $@"{(char)9}", $@"{(char)13}"
            ];
        private enum tipo { NumE, NumR, Lit, Esp, GnBajo, Asig, PC, C, P, PI, PF, CA, CC, Sum, Menos, Mul, Div, ComSL, ComMl, Igual, Me, Ma, Var, Key, Tab,SL, ERROR_a, ERROR_b };

        private List<string> tt = [
            "Numero Entero", "Numero Real", "Literales", "Espacio", "Guion bajo", "Asignacion", "Punto y Coma", "Coma", "Punto", 
            "Parentesis Inicio", "Parentesis Cerrado", "Corchete Abierto", "Corchete Cerrado", "Suma", "Menos", 
            "Multiplicacion", "Division", "Comentario SL", "Comentario ML", "Igual", "Menor que", "Mayor que", "Identificador", "Keywords", "Tabulacion", 
            "Salto de Linea", "Error Lexico", "Error Ahhh"
            ];
       

        private int[][] AutomataOrdenProgram =
        [
            [1,-1,-1,-1,-1,-1,-1,-1,-1,-1],
            [-1,1,2,-1,-1,-1,-1,-1,-1,-1],
            [-1,2,-1,3,-1,-1,-1,-1,-1,-1],
            [-1,3,3,4,4,-1,-1,-1,-1,-1],
            [-1,4,-1,5,4,5,-1,-1,-1,4],
            [-1,5,5,5,5,-1,5,6,-1,5],
            [-1,-1,-1,-1,-1,-1,-1,-1,7,-1],

        ];

        private List<string> orden = [
            "program", "$Espacio", "$Identificador", "$Punto y Coma", "$$", "begin", "$$",
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
               // if(item.caracter == "end") Console.Write($"fil = {fil.C}; itemc = {item.caracter}, itemt {item.tipo} \t");

                if (fil.C == item.caracter)
                {
                    return fil.i ;
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
                
                index = lineAuto[GetIndex(item)];

                if (index == 40 && lineAuto[GetIndex(CodePascal.Substring((i+1>= CodePascal.Count() ? CodePascal.Count()-1 : i+1 ), 1))] != 40)
                {

                    continue;
                }

                Console.WriteLine($"tokens{i} " + index + "  " + string.Join(",", lineAuto) + " " + Automatas.Count() + " " + item + " " + GetIndex(item));
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
                    reload(ref i, ref item);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Asig)] });
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
                    reload(ref i, ref item, true);
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.ERROR_a)] });
                    palabra = "";
                    isCorrectWrite = false;
                    continue;
                }
                else if (index == 45)
                {//error de 404
                    reload(ref i, ref item, true);
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
                Console.Write($"line = {string.Join(",", line)}; item = {orden[j]}, j = {j}; {item.caracter}  ");

                if (j == 7) { isCorrectOrden = true; break; }

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
