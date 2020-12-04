using System;
using System.Collections.Generic;


class Day4Class
{
    public void Start()
    {

        Console.WriteLine("****** DIA 4 ******");

        string[] lines = System.IO.File.ReadAllLines(@"./inputs_dia4.txt");
        var t = lines.ToString();

        List<string> inputs = new List<string>();
        string cadena = "";

        for(ushort i = 0; i < lines.Length; i++)
        {
            
            if (lines[i] != "")
            {

                string[] splitedlines = lines[i].Split(" ");
                cadena += string.Join(",", splitedlines) + ",";

            }
            
            if (lines[i] == "" || i == lines.Length - 1)
            {
                
                cadena = cadena.Remove(cadena.Length - 1);
                inputs.Add(cadena);
                cadena = "";
            }

        }

        // System.IO.File.WriteAllLines(@"./sana_inputs_dia4.txt", inputs);

        //fase 1
        Console.WriteLine("******** FASE 1 ********************");
        {
            ushort contador = 0;
            for (ushort i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].Contains("byr:") && inputs[i].Contains("iyr:") && 
                    inputs[i].Contains("eyr:") && inputs[i].Contains("hgt:") &&
                    inputs[i].Contains("hcl:") && inputs[i].Contains("ecl:") &&
                    inputs[i].Contains("pid:"))
                {

                    contador++;
                    // Console.WriteLine("bueno es=" + inputs[i]);
                }

            }

            Console.WriteLine("contador=" + contador);

        }

        //fase 2
        Console.WriteLine("******** FASE 2 ********************");
        {
            ushort contador = 0;
            for (ushort i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].Contains("byr:") && inputs[i].Contains("iyr:") &&
                    inputs[i].Contains("eyr:") && inputs[i].Contains("hgt:") &&
                    inputs[i].Contains("hcl:#") && inputs[i].Contains("ecl:") &&
                    inputs[i].Contains("pid:"))
                {

                    bool resultado = Checkcampos(inputs[i].Split(","));
                    if (resultado == true)
                    {
                        contador++;
                        // Console.WriteLine("bueno es=" + inputs[i]);

                    }

                    
                }

            }

            Console.WriteLine("contador=" + contador);

        }

        
    }


    public bool Checkcampos(string[] campos)
    {

        for (ushort i = 0; i < campos.Length; i++)
        {
            if (campos[i].Contains("byr:") == true)
            {

                bool valido = NumeroValido(campos[i].Split("byr:")[1], 4, 1920, 2002);

                if (valido == false)
                    return false;
            }

            


            if (campos[i].Contains("iyr:") == true)
            {

                bool valido = NumeroValido(campos[i].Split("iyr:")[1], 4, 2010, 2020);

                if (valido == false)
                    return false;
            }


            if (campos[i].Contains("eyr:") == true)
            {

                bool valido = NumeroValido(campos[i].Split("eyr:")[1], 4, 2020, 2030);

                if (valido == false)
                    return false;
            }


            if (campos[i].Contains("hgt:") == true)
            {
                bool valido = false;

                if (campos[i].Split("hgt:")[1].Contains("cm") == true)
                {
                    string campo = campos[i].Split("hgt:")[1].Split("cm")[0];
                    valido = NumeroValido(campo, 3, 150, 193);
                }
                else if (campos[i].Split("hgt:")[1].Contains("in") == true)
                {
                    string campo = campos[i].Split("hgt:")[1].Split("in")[0];
                    valido = NumeroValido(campo, 2, 59, 76);

                }


                if (valido == false)
                    return false;
            }


            if (campos[i].Contains("hcl:#") == true)
            {
                string campo = campos[i].Split("hcl:#")[1];
                bool valido = HexadecimalColor(campo);

                if (valido == false)
                    return false;
                
            }

            if (campos[i].Contains("ecl:") == true)
            {
                string campo = campos[i].Split("ecl:")[1];
                string[] colores = new string[]
                {
                    "amb", "blu","brn","gry","grn","hzl","oth"
                };

                bool valido = false;
                for (ushort j = 0; j < colores.Length; j++)
                {
                    if (campo == colores[j])
                    {
                        valido = true;
                        break;
                    }
                }

                if (valido == false)
                {
                    return false;

                }

            }

            if (campos[i].Contains("pid:") == true)
            {

                string campo = campos[i].Split("pid:")[1];
                if (campo.Length > 9 || campo.Length < 9)
                {
                    return false;
                }

                int resultado = 0;
                bool valido = int.TryParse(campo, out resultado);
                if (valido == false)
                {
                    return false;
                }
                

            }

        }


        return true;

    }

    public bool NumeroValido(string numerostr, int numerodigitos, int inicio, int max)
    {
        
        if (numerostr.Length > numerodigitos || numerostr.Length < numerodigitos)
        {
            return false;

        }

        int numero = int.Parse(numerostr);

        if (numero >= inicio && numero <= max)
        {
            return true;
        }

        return false;

    }


    public bool HexadecimalColor(string campo)
    {
        if (campo.Length > 6)
        {
            return false;
        }

        int resultado = 0;
        bool valido = int.TryParse(campo, System.Globalization.NumberStyles.HexNumber, null, out resultado);

        return valido;
    }
}
