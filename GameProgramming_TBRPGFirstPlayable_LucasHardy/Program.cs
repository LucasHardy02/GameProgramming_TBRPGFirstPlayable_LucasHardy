using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace GameProgramming_TBRPGFirstPlayable_LucasHardy
{
    internal class Program
    {

        static string map = "map.txt";

        static string[] mapLines = Array.Empty<string>();
        

        static string player = "O";
        static int playerXPos;
        static int playerYPos;

        static string enemy = "X";
        static int enemyXPos;
        static int enemyYPos;

        static bool playersTurn = true;
        static bool gameRunning = true;

        static void Main(string[] args)
        {
            playerXPos = 5;
            playerYPos = 2;

            Console.CursorVisible = false;
            LoadMap();
            DisplayMap();
            DrawPlayer();
            DrawEnemy();

            while(gameRunning)
            {
                if (playersTurn)
                {
                    playerMovement();
                    playersTurn = false;
                }
                else
                {
                    EnemyMovement();
                    playersTurn = true;
                }
            }
            



        }

        void Update()
        {
            
        }

        static void LoadMap()
        {
            mapLines = File.ReadAllLines(map);
        }

        static void DisplayMap()
        {

            int height = mapLines.Length;
            int width = mapLines[0].Length;
            string topBottomBorder = "═";

            

            for (int i = 0; i < width + 2; i++)
            {
                Console.ResetColor();
                Console.Write(topBottomBorder);
            }

            Console.Write("\n");
            for (int y = 0; y < height; y++)

            {
                Console.ResetColor();
                Console.Write('║');
                for (int x = 0; x < mapLines[y].Length; x++)
                {
                   char tile = mapLines[y][x];
                    SetForegroundColor(tile);
                   Console.Write(tile);
                   

                }
                Console.ResetColor();
                Console.Write('║');
                Console.Write("\n");


            }
            for (int i = 0; i < width + 2; i++)
            {
                Console.ResetColor();
                Console.Write(topBottomBorder);
            }
        }
        static void SetForegroundColor(char mapCharacter)
        {
            if (mapCharacter == '^')
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else if (mapCharacter == '`')
            {
                Console.ForegroundColor = ConsoleColor.Green;

            }
            else if (mapCharacter == '~')
            {
                Console.ForegroundColor = ConsoleColor.Blue;

            }
            else if (mapCharacter == '*')
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;

            }
            else if (mapCharacter == '"')
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ResetColor();
            }  
        }

        static void RestoreMapTile(int x, int y)
        {
            char tile = mapLines[y][x];
            Console.SetCursorPosition(x + 1, y + 1);
            SetForegroundColor(tile);
            Console.Write(tile);
        }

        static void DrawPlayer()
        {

            Console.SetCursorPosition(playerXPos + 1, playerYPos + 1);
            Console.ResetColor();

            Console.SetCursorPosition(playerXPos + 1, playerYPos + 1);
            Console.Write("O");
        }

        static void DrawEnemy()
        {
            Console.SetCursorPosition(enemyXPos + 1, enemyYPos + 1);
            Console.ResetColor();
            Console.SetCursorPosition(enemyXPos + 1, enemyYPos + 1);
            Console.Write("X");


        }
        static void playerMovement()
        {
           
            int newX = playerXPos;
            int newY = playerYPos;

            int oldX = playerXPos;
            int oldY = playerYPos;

            ConsoleKeyInfo Direction = Console.ReadKey(true);

            if (Direction.Key == ConsoleKey.W)
            {
                newY = newY - 1;
            }
            if (Direction.Key == ConsoleKey.S)
            {
                newY = newY + 1;

            }
            if (Direction.Key == ConsoleKey.D)
            {
                newX = newX + 1;
            }
            if (Direction.Key == ConsoleKey.A)
            {
                newX = newX - 1;

            }

            if (newX >= 0 && newX < mapLines[0].Length)
            {
                playerXPos = newX;

            }

            if (newY >= 0 && newY < mapLines.Length)
            {
                playerYPos = newY;

            }
            RestoreMapTile(oldX, oldY);
            DrawPlayer();


        }
        static void EnemyMovement()
        {

            int targetX = playerXPos - enemyXPos;
            int targetY = playerYPos - enemyYPos;
            
            int newEnemyX = enemyXPos;
            int newEnemyY = enemyYPos;

            int oldEnemyX = enemyXPos;
            int oldEnemyY = enemyYPos;

            if (Math.Abs(targetX) > Math.Abs(targetY))
            {
                if (targetX > 0)
                {
                    newEnemyX = oldEnemyX + 1;
                }
                else
                {
                    newEnemyX = oldEnemyX - 1;
                }
            }
            else
            {
                if (targetY > 0)
                {
                    newEnemyY = oldEnemyY + 1;
                }
                else
                {
                    newEnemyY = oldEnemyY - 1;

                }
            }


           
            




            RestoreMapTile(oldEnemyX, oldEnemyY);
            enemyXPos = newEnemyX;
            enemyYPos = newEnemyY;
            DrawEnemy();


        }
    }
}
