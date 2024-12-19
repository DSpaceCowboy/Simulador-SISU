using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP__AEDS
{
    internal class Candidato
    {
        private string nome;
        private double notaRedacao;
        private double notaMatematica;
        private double notaLinguagens;
        private int opcao1;
        private int opcao2;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public double NotaRedacao
        {
            get { return notaRedacao; }
            set { notaRedacao = value; }
        }

        public double NotaMatematica
        {
            get { return notaMatematica; }
            set { notaMatematica = value; }
        }

        public double NotaLinguagens
        {
            get { return notaLinguagens; }
            set { notaLinguagens = value; }
        }

        public double Media
        {
            get { return ((notaRedacao + notaMatematica + notaLinguagens) / 3); }
        }

        public int Opcao1
        {
            get { return opcao1; }
            set { opcao1 = value; }
        }
        public int Opcao2
        {
            get { return opcao2; }
            set { opcao2 = value; }
        }
        public override string ToString()
        {
            return $"{Nome} {Media} {NotaRedacao} {NotaMatematica} {NotaLinguagens}";
        }

        public Candidato(string nome, double notaRedacao, double notaMatematica, double notaLinguagens, int opcao1, int opcao2)
        {
            this.nome = nome;
            this.notaRedacao = notaRedacao;
            this.notaMatematica = notaMatematica;
            this.notaLinguagens = notaLinguagens;
            this.opcao1 = opcao1;
            this.opcao2 = opcao2;
        }
    }
}
