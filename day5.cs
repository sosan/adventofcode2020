using System;
using System.Collections.Generic;


class Day5Class
{
    public void Start()
    {

        // https://adventofcode.com/2020/day/5
        Console.WriteLine("****** DIA 5 ******");
        Console.WriteLine("****** FASE 1 ******");

        string[] lines = System.IO.File.ReadAllLines(@"./inputs/inputs_dia5.txt");
        List<uint> seatids = new List<uint>();

        for (ushort i = 0; i < lines.Length; i++)
        {
            char[] seatpos = lines[i].ToCharArray();

            (uint min, uint max) verticalRange = (0, 127);
            uint verticalCurrentRange = verticalRange.max - verticalRange.min;

            (uint min, uint max) horizontalRange = (0, 7);
            uint horizontalCurrentRange = horizontalRange.max - horizontalRange.min;

            for (ushort j = 0; j < seatpos.Length; j++)
            {

                verticalCurrentRange = (verticalRange.max - verticalRange.min) / 2;
                horizontalCurrentRange = (horizontalRange.max - horizontalRange.min) / 2;

                switch(seatpos[j])
                {
                    case 'B':
                        verticalRange.min = (verticalRange.max - verticalCurrentRange);
                    break;

                    case 'F':
                        verticalRange.max = (verticalRange.min + verticalCurrentRange);
                    break;

                    case 'L':
                        horizontalRange.max = (horizontalRange.min + horizontalCurrentRange);
                    break;

                    case 'R':
                        horizontalRange.min = (horizontalRange.max - horizontalCurrentRange);
                    break;

                }

            }

            // Console.WriteLine(lines[i] + " vert min=" + verticalRange.min + " vert max=" + verticalRange.max + " row" + verticalRange.min 
            // + " hor min=" + horizontalRange.min + " horz max=" + horizontalRange.max + " column=" + horizontalRange.min);
            uint seatid = (verticalRange.min * 8) + horizontalRange.min;
            seatids.Add(seatid);
            // Console.WriteLine("row=" + verticalRange.min + " col=" + horizontalRange.min + " seatid=" + seatid);
            
        }

        seatids.Sort();
        Console.WriteLine("seat id min=" + seatids[0] + " seat id max=" + seatids[seatids.Count - 1]);

        Console.WriteLine("****** FASE 2 ******");
        uint contadorseats = seatids[0];
        for (ushort i = 0; i < seatids.Count; i++)
        {
            if (contadorseats != seatids[i])
            {
                Console.WriteLine("TU SEAT ID=" + contadorseats);
                break;
            }
            contadorseats++;
        }

    }

}
