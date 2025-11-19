using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Threading;
using System.Media;
using System.Runtime.CompilerServices;

namespace GameProgramming_TBRPGFirstPlayable_LucasHardy
{
    internal class Program
    {

        static string map = "map.txt";

        static string[] mapLines = Array.Empty<string>();

        static int height;
        static int width;

        static char player = 'O';
        static int playerXPos;
        static int playerYPos;

        static int playerHealth = 2;
        static int playerDamage = 1;

        static char enemy = 'X';
        static string enemy2 = ">";
        static int enemyXPos;
        static int enemyYPos;
        static int enemy1Health = 1;
        static int enemyDamage = 1;
        static int enemy2Health = 2;


        static bool playersTurn = true;
        static bool gameRunning = true;

        static void Main(string[] args)
        {
            playerXPos = 0;
            playerYPos = 0;

            enemyXPos = 26;
            enemyYPos = 10;

            Console.CursorVisible = false;
            LoadMap();
            DisplayMap();
            DrawPlayer();
            DrawEnemy();

            if (playerHealth == 0)
            {
                
            }
            else
            {
                while (gameRunning)
                {
                    if (playersTurn)
                    {
                        Console.SetCursorPosition(0, 13);
                        Console.Write("Player's Turn     ");
                        Console.SetCursorPosition(0, 15);
                        Console.Write($"Players Health: {playerHealth}");
                        Thread.Sleep(250);
                        playerMovement();
                        playersTurn = false;
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 13);
                        Console.Write("Enemy's Turn     ");
                        Thread.Sleep(250);
                        EnemyMovement();
                        playersTurn = true;

                    }
                }
            }
            

        }

        static void LoadMap()
        {
            mapLines = File.ReadAllLines(map);
        }

        static void DisplayMap()
        {

            height = mapLines.Length;
            width = mapLines[0].Length;
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
            Console.Write(player);
        }

        static void DrawEnemy()
        {
            Console.SetCursorPosition(enemyXPos + 1, enemyYPos + 1);
            Console.ResetColor();
            Console.SetCursorPosition(enemyXPos + 1, enemyYPos + 1);
            Console.Write(enemy);


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
            if (newX >= width)
            {
                newX = oldX;

            }
            else if (newX < 0)
            {
                newX = oldX;
            }


            if (newY >= height)
            {
                newY = oldY;
            }
            else if (newY < 0)
            {
                newY = oldY;

            }

            if (!(mapLines[newY][newX] == '*' || mapLines[newY][newX] == '^' || mapLines[newY][newX] == '~'))
            {
                if (newX >= 0 && newX < mapLines[0].Length)
                {
                    playerXPos = newX;

                }

                if (newY >= 0 && newY < mapLines.Length)
                {
                    playerYPos = newY;

                }
                RestoreMapTile(oldX, oldY);

                }
                else
                {
                    newX = oldX;
                    newY = oldY;
                }

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

            if (!(mapLines[newEnemyY][newEnemyX] == '*' || mapLines[newEnemyY][newEnemyX] == '^' || mapLines[newEnemyY][newEnemyX] == '~'))
            {
               
                enemyXPos = newEnemyX;
                enemyYPos = newEnemyY;
                RestoreMapTile(oldEnemyX, oldEnemyY);

            }
            
            else if (mapLines[newEnemyY][newEnemyX] == '*' || mapLines[newEnemyY][newEnemyX] == '^' || mapLines[newEnemyY][newEnemyX] == '~')
            {
                newEnemyX = oldEnemyX;
                newEnemyY = oldEnemyY;
            }
            else if (mapLines[newEnemyY][newEnemyX] == mapLines[targetY][targetX])
            {
                newEnemyX = oldEnemyX;
                newEnemyY = oldEnemyY;
                AttackPlayer();
            }

            DrawEnemy();


        }
        static void AttackEnemy()
        {
            enemy1Health = enemy1Health - playerDamage;
        }
        static void AttackPlayer()
        {
            playerHealth = playerHealth - enemyDamage;
        }
        static void ResetGame()
        {
            playerXPos = 0;
            playerYPos = 0;

            playerHealth = 2;

            enemyXPos = 26;
            enemyYPos = 10;

            enemy1Health = 1;



        }

    }
}
