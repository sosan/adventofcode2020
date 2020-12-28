using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Day16Class
{

    public class Campo
    {
        public string nombre;
        public int[] rango1;
        public int[] rango2;
        public List<int> indexPosibles = new List<int>();
        public Campo(string nombre, int[] rango1, int[] rango2)
        {
            this.nombre = nombre;
            this.rango1 = rango1;
            this.rango2 = rango2;
            for (ushort i = 0; i < 20; i++)
            {
                indexPosibles.Add(i);
            }
        }
    }

    public void Start()
    {
        // https://adventofcode.com/2020/day/16
        Console.WriteLine("****** DIA 16 ******");
        var lines = File.ReadAllLines(@"./inputs/inputs_dia16.txt");
        // var lines = File.ReadAllLines(@"./inputs/inputs_dia16corto.txt");

        ushort rulesRowStart = 0;
        ushort rulesRowStop = 19;

        ushort nerbyticketsStart = 22;
        ushort nerbyticketsStop = 259;


        //corto
        // ushort rulesRowStart = 0;
        // ushort rulesRowStop = 2;

        // ushort nerbyticketsStart = 8;
        // ushort nerbyticketsStop = 10;

        List<Dictionary<int, int>> rules = new List<Dictionary<int, int>>();
        HashSet<int> numerosValidos = new HashSet<int>();

        //formating inputs
        for (ushort i = rulesRowStart; i <= rulesRowStop; i++ )
        {

            string[] cleansplittedtext = lines[i].Split( new string[] { "-", ": ", " or " }, StringSplitOptions.RemoveEmptyEntries );

            int min = int.Parse(cleansplittedtext[1]);
            int max = int.Parse(cleansplittedtext[2]);

            Dictionary<int, int> temp = new Dictionary<int, int>();

            for (int j = min; j <= max; j++ )
            {
                temp.Add(j, j);
                if (numerosValidos.Contains(j) == false)
                {
                    numerosValidos.Add(j);
                }

            }

            min = int.Parse(cleansplittedtext[3]);
            max = int.Parse(cleansplittedtext[4]);

            for (int j = min; j <= max; j++)
            {
                temp.Add(j, j);
                if (numerosValidos.Contains(j) == false)
                {
                    numerosValidos.Add(j);
                }

            }

            rules.Add( temp);

        }


        
        Console.WriteLine("****** FASE 1 ******");
        List<List<int>> inputs = new List<List<int>>();

        for ( ushort i = nerbyticketsStart; i <= nerbyticketsStop; i++)
        {

            if (i >= 25 || i == 22)
            {
                string[] splittedNumsStr = lines[i].Split(",");
                List<int> ticketTemp = new List<int>();

                for (ushort j = 0; j < splittedNumsStr.Length; j++)
                {
                    ticketTemp.Add(int.Parse(splittedNumsStr[j]));
                }

                inputs.Add(ticketTemp);

            }
        }

        List<int> ticketScanningErrorRate = new List<int>();
        
        for (ushort i = 0; i < inputs.Count; i++)
        {

            List<int> listadoelementos = inputs[i];
            int currentelement = 0;
            
            bool encontrado = false;
            for (ushort j = 0; j < listadoelementos.Count; j++)
            {
                currentelement = listadoelementos[j];
                encontrado = false;
                for (ushort o = 0; o < rules.Count; o++)
                {

                    if (rules[o].ContainsKey(currentelement) == true)
                    {
                        encontrado = true;
                        break;
                    }
                    else
                    {
                        encontrado = false;
                        
                    }

                }

                if (encontrado == false)
                {
                    ticketScanningErrorRate.Add(currentelement);
                }

            }

        }

        Console.WriteLine("ticket scanning error rate=" + ticketScanningErrorRate.Sum()) ;
        Console.WriteLine("****** FASE 2 ******");

        //con oop
        List<Campo> campos= new List<Campo>();
        List<int[]> ticketsValids = new List<int[]>();

        //relleno 
        for (ushort i = rulesRowStart; i <= rulesRowStop; i++)
        {

            string[] cleansplittedtext = lines[i].Split(new string[] { "-", ": ", " or " }, StringSplitOptions.RemoveEmptyEntries);

            int range1Min = int.Parse(cleansplittedtext[1]);
            int range1Max = int.Parse(cleansplittedtext[2]);
            
            int range2Min = int.Parse(cleansplittedtext[3]);
            int range2Max = int.Parse(cleansplittedtext[4]);

            Campo nuevoCampo = new Campo(
                cleansplittedtext[0],
                new int[] { range1Min, range1Max },
                new int[] { range2Min, range2Max }
            );

            campos.Add(nuevoCampo);

        }

        //comprobar tickets validos
        bool isValid = false;
        for (ushort i = 0; i < inputs.Count; i++)
        {
            isValid = true;
            List<int> elementos = inputs[i];
            for(ushort x = 0; x < elementos.Count; x++)
            {
                if (numerosValidos.Contains(elementos[x]) == false)
                {
                    isValid = false;
                }

            }

            if (isValid == true)
            {
                ticketsValids.Add(inputs[i].ToArray());
            }

        }

        for (int i = 0; i < campos.Count; i++)
        {

            for (int x = 0; x < 20; x++)
            {

                for (int y = 0; y < ticketsValids.Count; y++)
                {
                    if ((ticketsValids[y][x] >= campos[i].rango1[0] && ticketsValids[y][x] <= campos[i].rango1[1]) ||
                        (ticketsValids[y][x] >= campos[i].rango2[0] && ticketsValids[y][x] <= campos[i].rango2[1]))
                    {
                        continue;
                    }
                    else
                    {
                        campos[i].indexPosibles.Remove(x);
                        break;
                    }

                }
            }

        }

        int cantidadCampos = campos.Count;
        int contador = 0;

        do
        {
            contador = 0;

            for(ushort i = 0; i < campos.Count; i++)
            {

                if (campos[i].indexPosibles.Count == 1)
                {
                    for (ushort y = 0; y < campos.Count; y++)
                    {
                        if (campos[y] != campos[i])
                        {
                            campos[y].indexPosibles.Remove(campos[i].indexPosibles[0]);
                        }

                    }
                }
                contador += campos[i].indexPosibles.Count();
            }

        }
        while(contador != cantidadCampos);

        long resultadoparte2 = 1;
        for (ushort i = 0; i < campos.Count; i++)
        {
            if (campos[i].nombre.Contains("departure") == true)
            {
                resultadoparte2 *= ticketsValids[0][campos[i].indexPosibles[0]];
            }
        }

        Console.WriteLine("resultado=" + resultadoparte2);

    }

}