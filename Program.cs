using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordenadas
{
    class Program
    {
        static void Main(string[] args)
        {

            //string[] campos = "MA|CAXIAS|05|20".Split('|');

            /*
            foreach (string line in File.ReadAllLines(@"D:\pessoal\itamar\Coordenadas\cidades_brasileiras.txt"))
            { 
            }
            */

            Encoding cp = Encoding.GetEncoding("Windows-1252");

            // Encoding.GetEncoding("CP850")
            string[] lines = File.ReadAllLines(@"D:\pessoal\itamar\Coordenadas\cidades_brasileiras.txt",cp);

            /*
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(' ')
                orderby x[2]
                select x[2] + ", " + (x[1] + " " + x[0]);
            */

            // Pre filtragem
            // Apenas cidades
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(' ')
                //orderby x[2]
                where (line.Length > 30) && (
                (line.ToLowerInvariant().Contains("cidade")) ||
                (line.ToLowerInvariant().Contains("capital")) 
                )// x.Count() > 2
                select line;

            // Execute the query and write out the new file. Note that WriteAllLines
            // takes a string[], so ToArray is called on the query.
            
            /*
            System.IO.File.WriteAllLines(@"../../../spreadsheet2.csv", query.ToArray());
            Console.WriteLine("Spreadsheet2.csv written to disk. Press any key to exit");
            */

            System.IO.StreamWriter fileout = new System.IO.StreamWriter(@"D:\pessoal\itamar\Coordenadas\cidades_coordenadas.txt", false, Encoding.UTF8);

            //query.ToArray().ToList<String>().

            foreach (string line in query.ToArray()) {
                string[] x = line.Split(' ');
                //string saida;

                string uf = x[0];

                string alt   = x[x.Length - 1];
                string lon_s = x[x.Length - 2];
                string lon_m = x[x.Length - 3];
                string lon_g = x[x.Length - 4];

                string lat_s = x[x.Length - 5];
                string lat_m = x[x.Length - 6];
                string lat_g = x[x.Length - 7];

                // Cidade começa no 1
                // Mínimo = 8

                int qtd = x.Length - 9;
                string cidade = "";
                for (int i = 0; i < qtd; i++ )
                {
                    cidade += x[1 + i] + " ";
                }
                cidade = cidade.TrimEnd();

                string latitude = String.Format("{0}.{1}.{2}", lat_g, lat_m, lat_s);
                string longitude = String.Format("-{0}.{1}.{2}", lon_g, lon_m, lon_s);

                fileout.WriteLine("{0}|{1}|{2}|{3}",uf,cidade,latitude,longitude);
            }
            
            fileout.Close();

            ////System.IO.File.WriteAllLines(@"D:\pessoal\itamar\Coordenadas\teste.txt", query.ToArray(),Encoding.UTF8);
            //string[] lines = File.ReadAllLines(@"D:\pessoal\itamar\Coordenadas\cidades_brasileiras.txt");

            Console.WriteLine("teste...");
            Console.ReadKey();

        }
    }
}
