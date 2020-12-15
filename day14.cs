
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Day14Class
{

    public void Start()
    {
        // https://adventofcode.com/2020/day/14
        Console.WriteLine("****** DIA 14 ******");
        
        string[] lines = File.ReadAllLines(@"./inputs/inputs_dia14.txt");

        Console.WriteLine("****** FASE 1 ******");

        Dictionary<string, string> inputs = new Dictionary<string, string>();
        string maskcadena = "";
        double resultado = 0;

        //añadir en el dict
        for (ushort i = 0; i < lines.Length; i++)
        {
            string command = lines[i].Substring(0, 4);
            switch(command)
            {
                case "mem[":
                    string[] stringsplitted = lines[i].Split("] = ");
                    string key = stringsplitted[0].Substring(4);
                    string value = stringsplitted[1];

                    uint decimalvalue = uint.Parse(value);

                    //101
                    //"000000000000000000000000000001100101"
                    //                              1100101
                    string binary = Convert.ToString(decimalvalue, 2).PadLeft(36, '0');
                    //en teoria misma longitud
                    if (maskcadena.Length != binary.Length)
                    {
                        Console.WriteLine("no tiene la misma longitued" + stringsplitted[0] + " " + stringsplitted[1]);
                    }

                    char[] cadena = binary.ToCharArray();
                    for (ushort j = 0; j < maskcadena.Length; j++)
                    {
                        if (maskcadena[j] == 'X')
                        {
                            continue;
                        }

                        if (maskcadena[j] != cadena[j])
                        {
                            cadena[j] = maskcadena[j];
                        }
                    }

                    binary = new string(cadena);

                    if (inputs.ContainsKey(key) == true)
                    {
                        // Console.WriteLine("ya existe"); //66
                        inputs[key] = binary;
                    }
                    else
                    {
                        inputs.Add(key, binary);

                    }

                break;

                case "mask":
                    maskcadena = lines[i].Split(" = ")[1];
                break;
                default: 
                    Console.WriteLine("noop");
                break;
            }
            
        }

        resultado = 0;
        foreach(var value in inputs.Values)
        {
            double conversion = Convert.ToInt64(value, 2);
            resultado += conversion;
        }

        Console.WriteLine("resultado=" + resultado);

        Console.WriteLine("****** FASE 2 ******");

        resultado = 0;
        maskcadena = "";
        // Dictionary<string, List<string>> inputsfase2 = new Dictionary<string, List<string>>();
        Dictionary<long, long> inputsfase2 = new Dictionary<long, long>();
        long numeroCombinaciones = 0;

        //añadir en el dict
        for (ushort i = 0; i < lines.Length; i++)
        {
            string command = lines[i].Substring(0, 4);
            switch(command)
            {
                case "mem[":
                    string[] stringsplitted = lines[i].Split("] = ");
                    long direccionmemoria = long.Parse(stringsplitted[0].Substring(4));
                    long decimalvalue = long.Parse(stringsplitted[1]);

                    for (long o = 0; o < numeroCombinaciones; o++)
                    {

                        int offset = 0;
                        long index = direccionmemoria;
                        for (ushort j = 0; j < maskcadena.Length; j++)
                        {

                            //empezamos por atras
                            char c = maskcadena[maskcadena.Length - 1 - j];
                            if (c == '1')
                            {
                                index |= (1L << j);
                            }
                            else if (c == 'X')
                            {
                                //alternancia entre 0 y 1
                                var ceroOUno = (o >> offset) & 1;

                                if (ceroOUno == 0)
                                {
                                    index &= ~(1L << j);
                                }
                                else
                                {
                                    index |= (1L << j);
                                }

                                offset++;
                            }

                        }


                        if (inputsfase2.ContainsKey(index) == false)
                        {
                            inputsfase2.Add(index, 0);
                        }

                        inputsfase2[index] = decimalvalue;

                    }




                break;

                case "mask":
                    maskcadena = lines[i].Split(" = ")[1];
                    int contadorx = maskcadena.Split('X').Length - 1;
                    numeroCombinaciones = (long)BigInteger.Pow(2, contadorx);

                break;
                default: 
                    Console.WriteLine("noop");
                break;
            }
            
        }


        resultado = inputsfase2.Values.Sum();
        Console.WriteLine("resultado=" + resultado);



    }



}