using Microsoft.AspNetCore.SignalR;
using System;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
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
        public string CodePascal { get; set; }
        public int[][] Automatas = 
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
        public List<string> alfabeto = [
            @"[0-9]", @"'", @"[A-Z]", @"[a-z]", @" ", @"_", @":", @"=", @";", @",",
            @"\.", @"\(", @"\)", @"\[", @"\]", @"\+", @"\-", @"\*", @"/", @"<", @">",
            $@"{(char)9}", $@"{(char)13}"
            ];
        enum tipo { Num, Lit, Esp, GnBajo, Asig, PC, C, P, PI, PF, CA, CC, Sum, Menos, Mul, Div, Igual, Me, Ma, Var, Key, Tab,SL, ERROR_a, ERROR_b };

        public List<string> tt = [
            "Numero", "Literales", "Espacio", "Guion bajo", "Asignacion", "Punto y Coma", "Coma", "Punto", 
            "Parentesis Inicio", "Parentesis Cerrado", "Corchete Abierto", "Corchete Cerrado", "Suma", "Menos", 
            "Multiplicacion", "Division", "Igual", "Menor que", "Mayor que", "Var", "Keywords", "Tabulacion", 
            "Salto de Linea", "Error Lexico", "Error Ahhh"
            ];
        public List<string> keywords = [
            "and", "case", "file", "label", "packed", "then", "array", "const",
            "for", "library", "procedure", "to", "asm", "constructor", "forward",
            "mod", "program", "type", "begin", "destructor", "function", "nil",
            "record", "unit", "break", "div", "goto", "not", "repeat", "until",
            "do", "if", "object", "set", "uses", "downto", "implementation",
            "of", "shl", "var", "else", "in", "on", "shr", "while", "end",
            "inherited", "or", "string", "with", "exports", "inline", "xor",
            "interface", "interrupt", "is"
            ];
        public Analizador(string codePascal)
        {
            CodePascal = codePascal;
        }
        public async Task<int> GetIndex(string item)
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
        public void reload(ref int[]? lineAuto, ref int j, ref int i, ref string palabra, ref string item)
        {
            lineAuto = Automatas[0];
            if (j != i)
            {
                j = i;
                --i;
                palabra = "";
            }
            else
            {
                palabra = item;
            }
        }
        public async Task<List<DatoTabla>> GetGeneratorDataT()
        {
            var list = new List<DatoTabla>();
            int index = 0;
            var lineAuto = Automatas[index];
            string palabra = "";
            int j = 0;
            for ( int i = 0; i< CodePascal.Count(); i++)
            {
                string item = CodePascal.Substring(i,1) ;
                if (await GetIndex(item) >= lineAuto.Count())
                {
                    Debug.WriteLine("line " + lineAuto.Length + " " + alfabeto.Count() + " " + item);
                }
                index = lineAuto[await GetIndex(item)];
                Console.WriteLine("tokens " + index + "  " + string.Join(",", lineAuto) + " " + Automatas.Count() + " " + item + " " + await GetIndex(item));
                lineAuto = Automatas[index];
                if (index == 4)
                {
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Num)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                } 
                else if (index == 8)
                {//literales
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Lit)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 10)
                {// asignacion
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Asig)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 11)
                {//punto y goma
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.PC)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 12)
                {//goma
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.C)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 13)
                {//
                 //
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.P)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 14)
                {//parenteris Inicio
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.PI)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 15)
                {//parenteris Final
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.PF)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 16)
                {//Corchete Inicio
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.CA)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 17)
                {//Corchete Final
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.CC)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 18)
                {//mas
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Sum)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 19)
                {//menos
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Menos)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 20)
                {//mul
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Mul)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 21)
                {//div
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Div)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 22)
                {//igual
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Igual)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 23)
                {//menorq
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Me)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 24)
                {//mayorq
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Ma)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 27)
                {//id o reserv
                    
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, 
                        tipo = tt[((int)(keywords.Contains(palabra) ? tipo.Key : tipo.Var))] });

                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 28)
                {//espacio
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Esp)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 29)
                {//tab
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.Tab)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 30)
                {//return
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.SL)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 5)
                {//error lexico
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.ERROR_a)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else if (index == 31)
                {//error de 404
                    list.Add(new DatoTabla { tokens = $"{index}", caracter = palabra, tipo = tt[((int)tipo.ERROR_b)] });
                    reload(ref lineAuto, ref j, ref i, ref palabra, ref item);
                    continue;
                }
                else {
                    palabra += item.ToString();
                }

            }
            return list;
        }
    }
}
