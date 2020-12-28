using System;
using System.IO;
using System.Collections.Generic;


public enum Direction { 
    Este, 
    Sur, 
    Oeste, 
    Norte,
    Length
}

class Day12Class
{

    public class Position
    {
        public int x;
        public int y;
        public Direction dire;

        public Position(int x, int y, Direction dire)
        {
            this.x = x;
            this.y = y;
            this.dire = dire;
        }


        
    }

    public void Start()
    {
        // https://adventofcode.com/2020/day/12
        Console.WriteLine("****** DIA 12 ******");
        
        string[] lines = File.ReadAllLines(@"./inputs/inputs_dia12.txt");

        Console.WriteLine("****** FASE 1 ******");

        Position currentPos = new Position(0, 0, Direction.Este);
        Position originalPos = new Position(0, 0, Direction.Este);

        for (ushort i = 0; i < lines.Length; i++)
        {

            string command = lines[i].Substring(0, 1);
            int value = int.Parse(lines[i].Substring(1));
            int numrotaciones = 0;
            int direccionIntEnum = 0;

            switch(command)
            {
                case "N":
                    currentPos.y += value;
                break;

                case "S":
                    currentPos.y -= value;
                break;

                case "E":
                    currentPos.x += value;
                break;

                case "W":
                    currentPos.x -= value;
                break;

                case "L":
                    numrotaciones = value / 90;
                    direccionIntEnum = (int)currentPos.dire - numrotaciones;
                    if (direccionIntEnum < 0)
                    {
                        direccionIntEnum += (int)Direction.Length;
                    }

                    currentPos.dire = (Direction)direccionIntEnum;

                break;

                case "R":
                    numrotaciones = value / 90;
                    direccionIntEnum = (int)currentPos.dire + numrotaciones;
                    if (direccionIntEnum > (int)Direction.Length - 1)
                    {
                        direccionIntEnum -= (int)Direction.Length;
                    }

                    currentPos.dire = (Direction)direccionIntEnum;

                break;

                case "F":

                    switch (currentPos.dire)
                    {
                        case Direction.Este:
                            currentPos.x += value;
                        break;

                        case Direction.Norte:
                            currentPos.y += value;
                        break;

                        case Direction.Oeste:
                            currentPos.x -= value;
                        break;

                        case Direction.Sur:
                            currentPos.y -= value;
                        break;

                        default:
                            Console.WriteLine("malll" + currentPos.dire);
                        break;
                    }

                break;

                default: Console.WriteLine("fallo commadn=" + command + " value=" + value); break;

            }

            // Console.WriteLine(lines[i] + " rotation=" + currentPos.rotation + " posX=" + currentPos.x + " posY=" + currentPos.y);

        }

        int resultadoX = Math.Abs(originalPos.x - currentPos.x);
        int resultadoY = Math.Abs(originalPos.y - currentPos.y);
        int suma = resultadoX + resultadoY;
        // Console.WriteLine("resultadoX=" + resultadoX + " resultadoY=" + resultadoY + " suma=" + suma);
        Console.WriteLine("El resultado es=" + suma);

        Console.WriteLine("****** FASE 2 ******");


        currentPos = new Position(0, 0, Direction.Este);
        originalPos = new Position(0, 0, Direction.Este);

        Position currentPosWaypoint = new Position(10, 1, Direction.Este);
        Position originalPosWaypoint = new Position(10, 1, Direction.Este);

        for(ushort i = 0; i < lines.Length; i++)
        {
            string command = lines[i].Substring(0, 1);
            int value = int.Parse(lines[i].Substring(1));

            switch (command)
            {
                case "N":
                    currentPosWaypoint.y += value;
                break;

                case "S":
                    currentPosWaypoint.y -= value;
                break;

                case "E":
                    currentPosWaypoint.x += value;
                break;

                case "W":
                    currentPosWaypoint.x -= value;
                break;

                case "L":
                    for (int j = 0; j < (value / 90); j++)
                    {
                        int oldWaypointX = currentPosWaypoint.x;
                        currentPosWaypoint.x = -currentPosWaypoint.y;
                        currentPosWaypoint.y = oldWaypointX;
                    }

                break;

                case "R":

                    for (int j = 0; j < value / 90; j++)
                    {
                        
                        int oldWaypointX = currentPosWaypoint.x;
                        currentPosWaypoint.x = currentPosWaypoint.y;
                        currentPosWaypoint.y = -oldWaypointX;
                    }

                    
                break;

                case "F":

                    currentPos.x += currentPosWaypoint.x * value;
                    currentPos.y += currentPosWaypoint.y * value;

                break;

                default: Console.WriteLine("fallo commadn=" + command + " value=" + value); break;

            }

        }

        resultadoX = Math.Abs(originalPos.x - currentPos.x);
        resultadoY = Math.Abs(originalPos.y - currentPos.y);
        suma = resultadoX + resultadoY;
        // Console.WriteLine("resultadoX=" + resultadoX + " resultadoY=" + resultadoY + " suma=" + suma);
        Console.WriteLine("El resultado es=" + suma);


    }



}
