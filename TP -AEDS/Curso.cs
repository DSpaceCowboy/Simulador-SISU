using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP__AEDS
{
    internal class Curso
    {
        private string nome;
        private double mediaCurso;
        private int vagas;
        private List<Candidato> selecionados;
        private FilaLinear filaEspera;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public int Vagas
        {
            get { return vagas; }
            set { vagas = value; }
        }

        public List<Candidato> Selecionados
        {
            get { return selecionados; }
            set { selecionados = value; }
        }

        public FilaLinear FilaEspera
        {
            get { return filaEspera; }
            set { filaEspera = value; }
        }

        public double MediaCurso
        {
            get
            {
                if (!selecionados.Any())
                {
                    return 0;
                }

                double menorMedia = double.MaxValue;
                foreach (Candidato candidato in selecionados)
                {
                    if (candidato.Media < menorMedia)
                    {
                        menorMedia = candidato.Media;
                    }
                }

                mediaCurso = menorMedia;
                return mediaCurso;
            }
        }

        public Curso(string nome, int vagas)
        {
            this.nome = nome;
            this.vagas = vagas;
            this.selecionados = new List<Candidato>(10);
            this.filaEspera = new FilaLinear(10);
            this.mediaCurso = 0;
        }
    }
}