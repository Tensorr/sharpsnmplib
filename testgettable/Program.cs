﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lextm.SharpSnmpLib;
using System.Net;
using System.IO;

namespace TestGetTable
{
    class Program
    {
        static StreamWriter writer = new StreamWriter(File.OpenWrite("result.txt"));

        static void Main(string[] args)
        {
            Manager manager = new Manager();
            try
            {
                Variable[,] table = manager.GetTable(IPAddress.Parse("127.0.0.1"), "public",
                    new uint[] { 1, 3, 6, 1, 2, 1, 2, 2 });
                for (int row = 0; row < table.GetUpperBound(0); row++)
                {
                    for (int col = 0; col < table.GetUpperBound(1); col++)
                    {
                        writer.Write(table[row, col].Data + ", ");
                    }
                    writer.WriteLine();
                }
                writer.Close(); 
            }
            catch (SharpSnmpException ex)
            {
                if (ex is SharpOperationException)
                {
                    Console.WriteLine((ex as SharpOperationException).Details);
                }
                else
                {
                    Console.WriteLine(ex);
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}