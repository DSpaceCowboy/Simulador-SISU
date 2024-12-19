using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP__AEDS
{
    internal class FilaLinear
    {
        private Candidato[] array;
        private int primeiro;
        private int ultimo;

        public FilaLinear(int tamanho)
        {
            array = new Candidato[tamanho];
            primeiro = ultimo = 0;
        }

        public void Inserir(Candidato candidato)
        {
            if (((ultimo + 1) % array.Length) == 0) Console.WriteLine("Fila de espera cheia");
            array[ultimo] = candidato;
            ultimo = (ultimo + 1) % array.Length;
        }

        public void Mostrar()
        {
            int i = primeiro;

            Console.WriteLine("Fila de Espera:");
            while (i != ultimo)
            {
                Console.WriteLine($"{array[i].Nome} - {array[i].Media:F2}");
                i = (i + 1) % array.Length;
            }
        }

        public bool EstaCheia()
        {
            return (ultimo + 1) % array.Length == primeiro;
        }
    }
}
