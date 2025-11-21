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

        static List<(int x, int y)> goldList;
        static char gold = '0';
        static int goldCollected;
       

        static char player = 'O';
        static int playerXPos;
        static int playerYPos;

        static int playerHealth = 2;
        static int playerDamage = 1;

        static int lavaDamage = 1;

        static char enemy = 'X';

        static int enemyXPos;
        static int enemyYPos;


        static int enemy1Health = 1;
        static int enemyDamage = 1;
        static int enemy2Health = 2;

        static bool GameOver = false;
        static bool Winstate = false;

        static bool playersTurn = true;
        static bool gameRunning = true;

        static void Main(string[] args)
        {

            goldList = new List<(int x, int y)>();
            goldList.Add((1, 5));
            goldList.Add((2, 10));
            goldList.Add((11, 6));
            goldList.Add((22, 5));
            goldList.Add((24, 0));



            playerXPos = 0;
            playerYPos = 0;

            enemyXPos = 26;
            enemyYPos = 10;

            Console.CursorVisible = false;
            LoadMap();
            DisplayMap();
            DrawPlayer();
            DrawEnemy();
            DrawGold();


            
            while (GameOver == false)
            {

                if (Winstate == true)
                {
                    Console.ResetColor();
                    Console.SetCursorPosition(0, 19);
                    Console.Write("You win!");
                    break;

                }
                else if (playersTurn)
                {
                    Console.ResetColor();
                    Console.SetCursorPosition(0, 13);
                    Console.Write("Player's Turn     ");
                    Console.SetCursorPosition(0, 15);
                    Console.Write($"Player's Health: {playerHealth}");
                    Thread.Sleep(250);
                    playerMovement();
                    playersTurn = false;
                }
                else
                {
                    if (enemy1Health > 0)
                    {
                        Console.ResetColor();

                        Console.SetCursorPosition(0, 13);
                        Console.Write("Enemy's Turn     ");
                        Thread.Sleep(250);
                        EnemyMovement();
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 13);
                        Console.Write("                   ");

                    }

                    playersTurn = true;

                }
                if (GameOver == true)
                {
                    Console.ResetColor();
                    Console.SetCursorPosition(0, 19);
                    Console.Write("Game Over");
                    break;
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
            else if (mapCharacter == '░')
            {
                Console.ForegroundColor = ConsoleColor.Red;
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

        static void DrawGold()
        {
            foreach (var g in goldList)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(g.x + 1, g.y + 1);
                Console.Write(gold);
            }

            Console.ResetColor();

            
        }

        static void DrawPlayer()
        {
            if(playerHealth > 0)
            {
                Console.SetCursorPosition(playerXPos + 1, playerYPos + 1);
                Console.ResetColor();

                Console.SetCursorPosition(playerXPos + 1, playerYPos + 1);
                Console.Write(player);
            }
            else
            {

            }
            
        }

        static void DrawEnemy()
        {
            if(enemy1Health > 0)
            {
                Console.SetCursorPosition(enemyXPos + 1, enemyYPos + 1);
                Console.ResetColor();
                Console.SetCursorPosition(enemyXPos + 1, enemyYPos + 1);
                Console.Write(enemy);
            }
            else
            {
                if (enemyXPos >= 0 && enemyYPos >= 0)
                {
                    RestoreMapTile(enemyXPos, enemyYPos);

                }
            }
            


        }
        static void playerMovement()
        {
            if (GameOver || Winstate) return;

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

            if (newY == enemyYPos && newX == enemyXPos)
            {
                playerAttack();
                DrawEnemy();
                return;
            }



            else if (mapLines[newY][newX] == '░')
            {
                playerHealth = playerHealth - lavaDamage;

                if (playerHealth <= 0)
                {
                    GameOver = true;
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

            }
            else if (!(mapLines[newY][newX] == '*' || mapLines[newY][newX] == '^' || mapLines[newY][newX] == '~'))
            {
                

                for (int i = 0; i < goldList.Count; i++)
                {
                    if (newX == goldList[i].x && newY == goldList[i].y)
                    {
                        goldCollected = goldCollected + 1;
                        goldList.RemoveAt(i);

                        Console.SetCursorPosition(0, 17);
                        Console.Write($"Player's Gold: {goldCollected}");

                        if (goldCollected == 5 && enemy1Health <= 0)
                        {
                            Winstate = true;
                        }
                        break;
                    }
                }

                playerXPos = newX;
                playerYPos = newY;
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
            if (GameOver || Winstate)
            {
                return;
            }

            if (enemy1Health <= 0)
            {
                return;
            }

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

            if (newEnemyX == playerXPos && newEnemyY == playerYPos)
            {
                enemyAttack();
                DrawPlayer();
                return;
                
            }
            else if (mapLines[newEnemyY][newEnemyX] == '░')
            {
                enemy1Health = enemy1Health - lavaDamage;

                if(enemy1Health <= 0)
                {
                    RestoreMapTile(enemyXPos, enemyYPos);

                    if (goldCollected == 5)
                    {
                        Winstate = true;
                    }
                    return;
                }


                RestoreMapTile(oldEnemyX, oldEnemyY);
                enemyXPos = newEnemyX;
                enemyYPos = newEnemyY;
                DrawEnemy();
                return;
                

            }
            else if (!(mapLines[newEnemyY][newEnemyX] == '*' || mapLines[newEnemyY][newEnemyX] == '^' || mapLines[newEnemyY][newEnemyX] == '~'))
            {
                RestoreMapTile(oldEnemyX, oldEnemyY);
                enemyXPos = newEnemyX;
                enemyYPos = newEnemyY;
                DrawEnemy();
                return;

            }
           
            return;


        }
        static void playerAttack()
        {
            enemy1Health = enemy1Health - playerDamage;

            if (enemy1Health <= 0)
            {
                RestoreMapTile(enemyXPos, enemyYPos);

                enemyXPos = -1;
                enemyYPos = -1;

                if (goldCollected == 5)
                {
                    Winstate = true;
                }
                return;
            }

        }
        static void enemyAttack()
        {
            playerHealth = playerHealth - enemyDamage;

            if (playerHealth <= 0)
            {
                RestoreMapTile(playerXPos, playerYPos);
                GameOver = true;
                return;
            }

            
            
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
