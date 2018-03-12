using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumHypercube
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("SVP, entrez le nombre de sommets du graphe");
            int numberOfProcessors=0;
            int d ;
            bool isHyper = false;
            //string s_numberofprocessors;
            do
            {
                Int32.TryParse(Console.ReadLine(), out numberOfProcessors);
                d = 0;
                do
                {
                    if (numberOfProcessors == Math.Pow(2,d))
                    {
                        isHyper = true;
                        Console.WriteLine("c'est un hypergraphe de degré " + d);
                    }

                    d++;
                } while (numberOfProcessors >= Math.Pow(2,d));
                if (!isHyper)Console.WriteLine("ceci n'est pas un hypergraphe; Le plus proche est de degré "+d+" de nombre de processeurs"+Math.Pow(2,d));
            } while ( !isHyper);

            int[] value = new int[numberOfProcessors];
            bool[] Actif = new bool[numberOfProcessors];
            long[] partialSum = new long[numberOfProcessors]; 
            Random r = new Random();
            for (int i = 0; i <numberOfProcessors; i++)
            {
                value[i] = r.Next(0,50);
                Actif[i] = true;
                partialSum[i] = value[i];
                Console.WriteLine(value[i]);
            }

            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < numberOfProcessors; j++)
                {
                
                        int vois = voisin(j, i, d);
                        if (j!= vois&& Actif[vois] && Actif[j])
                        {

                        
                        partialSum[voisin(j, i, d)] += partialSum[j];
                            Actif[j] = false;
                            Console.WriteLine("Prcessor "+j+" sent to its neighbor "+ voisin(j, i, d) + " the partial sum "+ partialSum[j]+" à l'itération "+ (i+1));
                        Console.WriteLine("the existing sum is "+partialSum[vois]);
                    }
                      
                }
            }
            
            Console.ReadLine();
        }

        private static int voisin(int j, int i, int d)
        {
            int VOISIN=0;
            byte[] J = new byte[d];
            for (int k = d-1; k >= 0; k--)
            {
                J[k] = Convert.ToByte(j % 2);
                
                    j = (j / 2);
               
                
            }
            J[i] = Convert.ToByte(1-J[i]);
            for (int k = 0; k < d-1; k++)
            {
                VOISIN += J[d-k-1]*Convert.ToInt32(Math.Pow(2,k));
            }
            return VOISIN;
        }
    }
}
