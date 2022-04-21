using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace col_transposition
{
    class Program
    {
        static void Main(string[] args)
        {
            int action = 0;

            while (action != 3)
            {

                Console.WriteLine("Seleccionar accion");
                Console.WriteLine("1: Encriptar");
                Console.WriteLine("2: Desencriptar");
                Console.WriteLine("3: Salir");
                action = int.Parse(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        {
                            Console.WriteLine("\nIngresar mensaje en claro:");
                            string plainText = Console.ReadLine();

                            string key = "";
                            bool valid = false;
                            while (!valid)
                            {
                                Console.WriteLine("\nIngresar llave sin caracteres duplicados");
                                key = Console.ReadLine().ToUpper();

                                if (key.Distinct().Count() != key.Length)
                                {
                                    key = "";
                                    Console.WriteLine("\nLa llave contiene caracteres duplicados");
                                }
                                else
                                {
                                    valid = true;
                                }
                            }

                            int counter = 0;
                            while (plainText.Length % key.Length != 0) //Complete explicit text with "_" until the number of characters is divisible by the remainder of the number of characters in the key
                            {
                                counter++;
                                plainText += '_';
                            }
                            if (counter > 0)
                            {
                                Console.WriteLine($"\nThe explicit message was supplemented by {counter} symbol \"_\".");
                            }

                            #region setting priorities

                            int[] priorities = new int[key.Length];

                            for (int i = 0; i < priorities.Length; i++) //fill in -1 to indicate which items have already been filled in and which
                            {
                                    priorities[i] = -1;
                            }

                            int minIndex = 0;

                            for (int i = 0; i < priorities.Length; i++)
                            {
                                int min = int.MaxValue;
                                for (int j = 0; j < priorities.Length; j++)
                                {
                                    if (key[j] <= min && priorities[j] == -1)
                                    {
                                        min = key[j];
                                        minIndex = j;
                                    }
                                }
                                priorities[minIndex] = i;
                            }

                            #endregion


                            #region getting cipherMatrix


                            char[,] cipherMatrix = new char[plainText.Length / key.Length, key.Length];
                            for (int i = 0; i < cipherMatrix.GetLength(0); i++)
                            {
                                for (int j = 0; j < cipherMatrix.GetLength(1); j++)
                                {
                                    cipherMatrix[i, priorities[j]] = plainText[i * key.Length + j];
                                }
                            }

                            #endregion
                            int actionType = 1;


                            string cipherText = "";
                            if (actionType == 1)
                            {
                                for (int i = 0; i < cipherMatrix.GetLength(1); i++)
                                {
                                    for (int j = 0; j < cipherMatrix.GetLength(0); j++)
                                    {
                                        cipherText += cipherMatrix[j, i];
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cipherMatrix.GetLength(0); i++)
                                {
                                    for (int j = 0; j < cipherMatrix.GetLength(1); j++)
                                    {
                                        cipherText += cipherMatrix[i, j];
                                    }
                                }
                            }
                            Console.WriteLine("\nCryptograma:");
                            Console.WriteLine(cipherText + "\n");
                            break;
                        }

                    case 2:
                        {
                            Console.WriteLine("\nIngresar criptograma");
                            string cipherText = Console.ReadLine();

                            string key = "";
                            bool valid = false;
                            while (!valid)
                            {
                                Console.WriteLine("\nIntroducir llave sin caracteres duplicados");
                                key = Console.ReadLine().ToUpper();

                                if (key.Distinct().Count() != key.Length)
                                {
                                    key = "";
                                    Console.WriteLine("\n¡La clave contiene caracteres duplicados!");
                                }
                                else
                                {
                                    valid = true;
                                }
                            }

                            #region setting priorities

                            int[] priorities = new int[key.Length];

                            for (int i = 0; i < priorities.Length; i++) //filling priorities with -1 so we know which indexes we've already assigned a new value to
                            {
                                priorities[i] = -1;
                            }

                            int minIndex = 0;

                            for (int i = 0; i < priorities.Length; i++)
                            {
                                int min = int.MaxValue;
                                for (int j = 0; j < priorities.Length; j++)
                                {
                                    if (key[j] <= min && priorities[j] == -1)
                                    {
                                        min = key[j];
                                        minIndex = j;
                                    }
                                }
                                priorities[minIndex] = i;
                            }

                            #endregion
                            int actionType = 1;
                            
                            char[,] cipherMatrix = new char[cipherText.Length / key.Length, key.Length];

                            if (actionType == 1)
                            {

                                for (int i = 0; i < cipherMatrix.GetLength(1); i++)
                                {
                                    for (int j = 0; j < cipherMatrix.GetLength(0); j++)
                                    {
                                        cipherMatrix[j, i] = cipherText[i * cipherText.Length / key.Length + j];
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < cipherMatrix.GetLength(0); i++)
                                {
                                    for (int j = 0; j < cipherMatrix.GetLength(1); j++)
                                    {
                                        cipherMatrix[i, j] = cipherText[i * key.Length + j];
                                    }
                                }
                            }

                            char[,] plainTextMatrix = new char[cipherMatrix.GetLength(0), cipherMatrix.GetLength(1)];

                            for (int i = 0; i < cipherMatrix.GetLength(1); i++)
                            {
                                for (int j = 0; j < cipherMatrix.GetLength(0); j++)
                                {
                                    plainTextMatrix[j, i] = cipherMatrix[j, priorities[i]];
                                }
                            }

                            string plainText = "";
                            for (int i = 0; i < plainTextMatrix.GetLength(0); i++)
                            {
                                for (int j = 0; j < plainTextMatrix.GetLength(1); j++)
                                {
                                    plainText += plainTextMatrix[i, j];
                                }
                            }

                            Console.WriteLine("\nMensaje en claro:");
                            Console.WriteLine(plainText + "\n");
                            break;
                        }
                }
            }
        }
    }
}
