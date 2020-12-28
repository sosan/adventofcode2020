using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Day13Class
{

    public void Start()
    {
        // https://adventofcode.com/2020/day/13
        Console.WriteLine("****** DIA 13 ******");
        
        string[] lines = File.ReadAllLines(@"./inputs/inputs_dia13.txt");

        Console.WriteLine("****** FASE 1 ******");
        double estimatetimestamp = double.Parse(lines[0]);
        string[] rawdatabus = lines[1].Split(",");

        Dictionary<int, List<int>> salidasbus = new Dictionary<int, List<int>>();
        List<int> busids = new List<int>();

        //añadimos datos en las salidas
        for (ushort i = 0; i < rawdatabus.Length; i++ )
        {
            if (rawdatabus[i] != "x")
            {
                int value =  int.Parse(rawdatabus[i]);
                busids.Add(value);
            }

        }


        for (int i = 0; i < busids.Count; i++)
        {
            if (busids[i] == 0)
                continue;

            int amount = (int)Math.Ceiling((float)estimatetimestamp / busids[i]);
            
            int departureTime = amount * busids[i];

            if (salidasbus.ContainsKey(departureTime) == false)
            {
                salidasbus[departureTime] = new List<int>();
            }

            salidasbus[departureTime].Add(busids[i]);
            
        }

        int minTime = salidasbus.Keys.Min();
        int minBusId = salidasbus[minTime].First();

        double respuesta = (minTime - estimatetimestamp) * minBusId;

        Console.WriteLine("respuesta=" + respuesta);

        Console.WriteLine("****** FASE 2 ******");

        busids.Clear();

        for (ushort i = 0; i < rawdatabus.Length; i++)
        {
            if (rawdatabus[i] == "x")
            {
                busids.Add(0);
            }
            else
            {
                int value = int.Parse(rawdatabus[i]);
                busids.Add(value);
            }


        }

        long resultado = 0;
        long increment = busids[0];
        int offset = 1;

        while (offset < busids.Count)
        {
            if (busids[offset] == 0)
            {
                offset++;

                continue;
            }

            resultado += increment;

            if ((resultado + offset) % busids[offset] != 0)
            {
                continue;
            }

            increment *= busids[offset];

            offset++;
        }


        Console.WriteLine("resultado=" + resultado);


    }


    //Basado en: https://rosettacode.org/wiki/Chinese_remainder_theorem#C.23
    private long ChineseRemainderTheorem(long[] n, long[] a)
    {
        long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;

            for (int x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }

            return 1;
        }

        long prod = n.Aggregate(1, (long i, long j) => i * j);
        long sm = 0;

        for (int i = 0; i < n.Length; i++)
        {
            var p = prod / n[i];

            sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
        }

        return sm % prod;
    }



}
