using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


class Day25Class
{


    public void Start()
    {
        // https://adventofcode.com/2020/day/25
        Console.WriteLine("****** DIA 25 ******");
        Console.WriteLine("****** FELIZ NAVIDAD!! ******");

        // string[] lines = File.ReadAllLines(@"./inputs/inputs_dia25corto.txt");
        string[] lines = File.ReadAllLines(@"./inputs/inputs_dia25.txt");

        Console.WriteLine("****** FASE 1 ******");
        List<ulong> publickey = new List<ulong>();
        for(ushort i = 0; i < lines.Length; i++)
        {

            publickey.Add(ulong.Parse(lines[i]));
        }

        ulong currentvalue = 1;
        ulong subjectnumber = 7;
        ulong loopsize = 1;

        for(; ; loopsize++)
        {
            currentvalue = currentvalue * subjectnumber % 20201227;
            if (publickey[1] == currentvalue)
            {
                break;
            }
        }

        currentvalue = 1;
        subjectnumber = publickey[0];

        for (ulong i = 1; i <= loopsize; i++)
        {
            currentvalue = currentvalue * subjectnumber % 20201227;
        }
        
        Console.WriteLine("El resultado es = " + currentvalue);
        Console.WriteLine("****** FASE 2 ******");
        Console.WriteLine("****** Fin ******");
    }


}