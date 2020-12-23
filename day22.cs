using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;



class Day22Class
{

    public void Start()
    {
        // https://adventofcode.com/2020/day/22
        Console.WriteLine("****** DIA 22 ******");
        var lines = File.ReadAllLines(@"./inputs/inputs_dia22.txt");
        // var lines = File.ReadAllLines(@"./inputs/inputs_dia22corto.txt");
        Console.WriteLine("****** FASE 1 ******");

        List<int> player1 = new List<int>();
        List<int> player2 = new List<int>();

        bool isplayer1 = true;
        for(ushort i = 0; i < lines.Length; i++)
        {

            if (lines[i] == "") continue;

            if (lines[i] == "Player 1:")
            {
                isplayer1 = true;
                continue;
            }
            else if (lines[i] == "Player 2:")
            {
                isplayer1 = false;
                continue;
            }

            if (isplayer1 == true)
            {
                player1.Add(int.Parse(lines[i]));
            }
            else
            {
                player2.Add(int.Parse(lines[i]));
            }


        }

        while(player1.Count > 0 && player2.Count > 0 )
        {
            
            if (player1[0] > player2[0])
            {
                int tempnum = player1[0];
                player1.RemoveAt(0);
                player1.Add(tempnum);
                player1.Add(player2[0]);
                player2.RemoveAt(0);
            }
            else if (player2[0] > player1[0])
            {

                int tempnum = player2[0];
                player2.RemoveAt(0);
                player2.Add(tempnum);
                player2.Add(player1[0]);
                
                player1.RemoveAt(0);
            }


        }

        List<int> ganador = new List<int>();
        string ganadortxt = "";
        if (player1.Count > 0)
        {
            ganador = player1;
            ganadortxt = "player1";
        }
        else if (player2.Count > 0)
        {
            ganador = player2;
            ganadortxt = "player2";
        }

        long total = 0;
        for(int i = ganador.Count - 1; i >= 0; i--)
        {
            total += ganador[i] * (ganador.Count() - i);


        }

        Console.WriteLine("el ganador es " + ganadortxt + " con puntos de=" +total );

        Console.WriteLine("****** FASE 2 ******");

        player1.Clear();
        player2.Clear();
        ganador.Clear();

        isplayer1 = true;

        for (ushort i = 0; i < lines.Length; i++)
        {

            if (lines[i] == "") continue;

            if (lines[i] == "Player 1:")
            {
                isplayer1 = true;
                continue;
            }
            else if (lines[i] == "Player 2:")
            {
                isplayer1 = false;
                continue;
            }

            if (isplayer1 == true)
            {
                player1.Add(int.Parse(lines[i]));
            }
            else
            {
                player2.Add(int.Parse(lines[i]));
            }


        }

        List<int> ganadorFase2 = new List<int>();
        (List<int> player1, List<int> player2) resultado = RunGame(new List<int> (player1), new List<int>(player2));

        if (resultado.player1.Count > 0)
        {
            ganadorFase2 = resultado.player1.ToList();
            ganadortxt = "player1";
        }
        else
        {
            ganadorFase2 = resultado.player2.ToList();
            ganadortxt = "player2";
        }

        total = 0;
        for (int i = ganadorFase2.Count - 1; i >= 0; i--)
        {
            total += ganadorFase2[i] * (ganadorFase2.Count() - i);
        }

        Console.WriteLine("el ganador es " + ganadortxt + " con puntos de=" + total);



    }


    public (List<int> player1, List<int> player2) RunGame(List<int> player1, List<int> player2)
    {

        var anteriorRoundPlayer1 = new HashSet<string>();
        var anteriorRoundPlayer2 = new HashSet<string>();

        while (player1.Count > 0 && player2.Count > 0)
        {

            string cadPlayer1 = GenerarCadena(player1);
            string cadPlayer2 = GenerarCadena(player2);

            if (anteriorRoundPlayer1.Contains(cadPlayer1) || anteriorRoundPlayer2.Contains(cadPlayer2))
            {
                return (player1, new List<int>());

            }
            else
            {
                anteriorRoundPlayer1.Add(cadPlayer1);
                anteriorRoundPlayer2.Add(cadPlayer2);
            }

            int ganadorPlayer = 0;

            int currentCartaPlayer1 = player1[0];
            int currentCartaPlayer2 = player2[0];
            player1.RemoveAt(0);
            player2.RemoveAt(0);

            if (currentCartaPlayer1 <= player1.Count && currentCartaPlayer2 <= player2.Count)
            {

                var nuevascartasplayer1 = GenerarNuevasCartas(currentCartaPlayer1, player1);
                var nuevascartasplayer2 = GenerarNuevasCartas(currentCartaPlayer2, player2);

                var resultado = RunGame(nuevascartasplayer1, nuevascartasplayer2);
                
                
                if (resultado.player1.Count > resultado.player2.Count)
                {
                    ganadorPlayer = 1;
                }
                else
                {
                    ganadorPlayer = 2;
                }


            }
            else
            {
                if (currentCartaPlayer1 > currentCartaPlayer2)
                {
                    ganadorPlayer = 1;
                }
                else
                {
                    ganadorPlayer = 2;
                }

            }


            if (ganadorPlayer == 1)
            {
                player1.Add(currentCartaPlayer1);
                player1.Add(currentCartaPlayer2);
            }
            else
            {
                player2.Add(currentCartaPlayer2);
                player2.Add(currentCartaPlayer1);

            }

        }

        return (player1, player2);




    }

    public string GenerarCadena(List<int> player)
    {

        string cadena = "";
        for(ushort i = 0; i < player.Count; i++)
        {
            cadena += player[i].ToString() + ",";

        }

        return cadena.Substring(0, cadena.Length - 1);
    }



    public ushort EncontrarListado(List<int> player, HashSet<List<int>> anteriorRoundPlayer)
    {

        if (anteriorRoundPlayer.Count == 0) return 0;

        ushort numeroEncontrados = 0;

        for (ushort i = 0; i < player.Count; i++)
        {

            bool encontrado = false;
            for (ushort y = 0; y < anteriorRoundPlayer.Count; y++)
            {
                List<int> currentAntriorRoundPlayer = anteriorRoundPlayer.ElementAt(y);
                for (ushort j = 0; j < currentAntriorRoundPlayer.Count; j++)
                {
                    if (player[i] == currentAntriorRoundPlayer[j])
                    {
                        encontrado = true;
                        break;
                    }

                }

                if (encontrado == true)
                {
                    break;
                }
            }

            if (encontrado == true)
            {
                numeroEncontrados++;
                continue;

            }
            else
            {
                break;
            }

        }

        return numeroEncontrados;

    }


    public List<int> GenerarNuevasCartas(int cantidadcartas, List<int> player)
    {

        List<int> nuevalista = new List<int>();

        for(ushort i = 0; i < cantidadcartas; i++)
        {
            nuevalista.Add(
                player[i]
            );

        }

        return nuevalista;

    }







}