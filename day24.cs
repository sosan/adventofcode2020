using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


class Day24Class
{


    public void Start()
    {
        // https://adventofcode.com/2020/day/24
        Console.WriteLine("****** DIA 24 ******");

        Console.WriteLine("****** FASE 1 ******");
        const decimal R = 1M;

        decimal x = 1.5M * R * (decimal)Math.Cos(Math.PI / 3);
        decimal y = 1.5M * R * (decimal)Math.Sin(Math.PI / 3);

        //https://catlikecoding.com/unity/tutorials/hex-map/part-1/
        Dictionary<string, (decimal x, decimal y)> deltaPositions = new Dictionary<string, (decimal x, decimal y)>()
            {

                {"e", (1.5M * R, 0)},
                {"w", (-1.5M * R, 0)},
                {"ne", (x, -y)},
                {"nw", (-x, -y)},
                {"se", (x, y)},
                {"sw", (-x, y)},

            };

        string[] inputs = File.ReadAllLines("./inputs/inputs_dia24.txt");

        Dictionary<(decimal x, decimal y), bool> tiles = new Dictionary<(decimal x, decimal y), bool>();
        List<string[]> instrucciones = GenerarInstrucciones(inputs);

        for (int i = 0; i < instrucciones.Count; i++)
        {

            (decimal x, decimal y) currentPos = (0, 0);

            for (int j = 0; j < instrucciones[i].Length; j++)
            {

                var deltaCurrentPos = deltaPositions[instrucciones[i][j]];
                currentPos.x += deltaCurrentPos.x;
                currentPos.y += deltaCurrentPos.y;

            }

            if (tiles.ContainsKey(currentPos) == true)
            {
                // true <-> false
                tiles[currentPos] = !tiles[currentPos];
            }
            else
            {
                tiles[currentPos] = false;
            }

        }

        ulong contador = 0;
        foreach(var valor in tiles)
        {
            if (valor.Value == false)
            {
                contador++;
            }

        }

        Console.WriteLine("Resultado=" + contador);

        

        Console.WriteLine("****** FASE 2 ******");

        tiles.Clear();

        instrucciones = GenerarInstrucciones(inputs);

        //format tiles
        for (int i = 0; i < instrucciones.Count; i++)
        {

            (decimal x, decimal y) currentPos = (0, 0);

            for (int j = 0; j < instrucciones[i].Length; j++)
            {

                var deltaCurrentPos = deltaPositions[instrucciones[i][j]];
                currentPos.x += deltaCurrentPos.x;
                currentPos.y += deltaCurrentPos.y;

            }

            if (tiles.ContainsKey(currentPos) == true)
            {
                // true <-> false
                tiles[currentPos] = !tiles[currentPos];
            }
            else
            {
                tiles[currentPos] = false;
            }

        }

        for (ushort i = 0; i < 100; i++)
        {
            var formatedTile = new Dictionary<(decimal x, decimal y), (bool coloreado, int numVecinosColoreados)>();

            foreach (var (posicionTile, tileColoreado) in tiles)
            {

                foreach (var (deltaPosicionNombre, (deltaPosicionX, deltaPosicionY)) in deltaPositions)
                {
                    var currentPositionTile = posicionTile;
                    currentPositionTile.x += deltaPosicionX;
                    currentPositionTile.y += deltaPosicionY;

                    //añadir
                    if (formatedTile.ContainsKey(currentPositionTile) == false)
                    {
                        bool isColoreado = true;
                        if (tiles.ContainsKey(currentPositionTile) == true)
                        {
                            isColoreado = tiles[currentPositionTile];

                        }
                        formatedTile[currentPositionTile] = (isColoreado, 0);
                    }

                    (bool coloreado, int numeroVecinosColoreados) valorTile = formatedTile[currentPositionTile];

                    if (tileColoreado == false)
                    {
                        valorTile.numeroVecinosColoreados++;
                    }

                    formatedTile[currentPositionTile] = valorTile;
                }
            }

            Dictionary<(decimal x, decimal y), bool> nuevoTile = new Dictionary<(decimal x, decimal y), bool>();

            foreach (var (positionTile, (isColoreado, numVecinosColoreados)) in formatedTile)
            {

                if (isColoreado == false )
                {

                    if (numVecinosColoreados == 0 || numVecinosColoreados > 2)
                    {

                    }
                    else
                    {
                        nuevoTile.Add(positionTile, false);

                    }

                }
                else
                {
                    if (numVecinosColoreados == 2)
                    {
                        nuevoTile.Add(positionTile, false);
                    }

                }

            }

            tiles = nuevoTile;
        }

        contador = 0;
        foreach (var valor in tiles)
        {
            if (valor.Value == false)
            {
                contador++;
            }

        }

        Console.WriteLine("Resultado=" + contador);


    }

    private List<string[]> GenerarInstrucciones(string[] inputs)
    {
        List<string[]> instrucciones = new List<string[]>();
        Regex regex = new Regex(@"e|se|sw|w|nw|ne");

        //format instrucciones
        for (ushort i = 0; i < inputs.Length; i++)
        {
            MatchCollection matches = regex.Matches(inputs[i]);

            List<string> templist = new List<string>();
            for (int j = 0; j < matches.Count; j++)
            {
                templist.Add(matches[j].Value);
            }

            instrucciones.Add(templist.ToArray());
        }

        return instrucciones;
    }

}