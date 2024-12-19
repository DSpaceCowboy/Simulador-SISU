using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace TP__AEDS
{
   
    

    internal class Program
    {
        static bool CompararNotas(Candidato c1, Candidato c2)
        {
            if (c1.Media > c2.Media) return true; 
            if (c1.Media < c2.Media) return false;

            if (c1.NotaRedacao > c2.NotaRedacao) return true;
            if (c1.NotaRedacao < c2.NotaRedacao) return false;

            if (c1.NotaMatematica > c2.NotaMatematica) return true;
            if (c1.NotaMatematica < c2.NotaMatematica) return false;

            return false;
        }

        static void QuickSortMedia(List<Candidato> candidato, int esq, int dir)
        {
            int i = esq, j = dir;
            Candidato pivo = candidato[(esq + dir) / 2];

            while (i <= j)
            {
                while (CompararNotas(candidato[i], pivo))
                {
                    i++;
                }

                while (CompararNotas(pivo, candidato[j]))
                {
                    j--;
                }

                if (i <= j)
                {
                    Candidato temp = candidato[i];
                    candidato[i] = candidato[j];
                    candidato[j] = temp;
                    i++;
                    j--;
                }
            }

            if (esq < j) QuickSortMedia(candidato, esq, j);
            if (i < dir) QuickSortMedia(candidato, i, dir);
        }



        static void LeituraArquivo(string caminhoArquivo, out Dictionary<int, Curso> cursos, out List<Candidato> candidatos)
        {
            cursos = new Dictionary<int, Curso>();
            candidatos = new List<Candidato>();
            string[] linhas = File.ReadAllLines(caminhoArquivo);

            string[] primeiraLinha = linhas[0].Split(';');
            int tamanhoCursos = int.Parse(primeiraLinha[0]);
            int tamanhoCandidatos = int.Parse(primeiraLinha[1]);

            int linhaAtual = 1;
            for (int i = 0; i < tamanhoCursos; i++)
            {
                string[] dadosCurso = linhas[linhaAtual].Split(';');
                int cursoCod = int.Parse(dadosCurso[0]);
                string cursoNome = dadosCurso[1];
                int quantVagas = int.Parse(dadosCurso[2]);

                Curso curso = new Curso(cursoNome, quantVagas);
                cursos[cursoCod] = curso;
                linhaAtual++;
            }

            for (int i = 0; i < tamanhoCandidatos; i++)
            {
                string[] dadosCandidato = linhas[linhaAtual].Split(';');
                string nomeCandidato = dadosCandidato[0];
                double notaRedacao = double.Parse(dadosCandidato[1]);
                double notaMatematica = double.Parse(dadosCandidato[2]);
                double notaLinguagens = double.Parse(dadosCandidato[3]);
                int opcao1 = int.Parse(dadosCandidato[4]);
                int opcao2 = int.Parse(dadosCandidato[5]);

                Candidato candidato = new Candidato(nomeCandidato, notaRedacao, notaMatematica, notaLinguagens, opcao1, opcao2);
                candidatos.Add(candidato);
                linhaAtual++;
            }
        }

        static void ProcessarCandidatos(Dictionary<int, Curso> cursos, List<Candidato> candidatos)
        {
            QuickSortMedia(candidatos, 0, candidatos.Count - 1);

            foreach (Candidato candidato in candidatos)
            {
                Curso cursoPrimeiraOpcao = cursos[candidato.Opcao1];
                Curso cursoSegundaOpcao = cursos[candidato.Opcao2];

                if (cursoPrimeiraOpcao.Selecionados.Count < cursoPrimeiraOpcao.Vagas)
                {
                    cursoPrimeiraOpcao.Selecionados.Add(candidato);
                }
                else
                {
                    if (!cursoPrimeiraOpcao.FilaEspera.EstaCheia())
                    {
                        cursoPrimeiraOpcao.FilaEspera.Inserir(candidato);
                    }

                    if (cursoSegundaOpcao.Selecionados.Count < cursoSegundaOpcao.Vagas)
                    {
                        cursoSegundaOpcao.Selecionados.Add(candidato);
                    }
                    else
                    {
                        if (!cursoSegundaOpcao.FilaEspera.EstaCheia())
                        {
                            cursoSegundaOpcao.FilaEspera.Inserir(candidato);
                        }
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            Dictionary<int, Curso> cursos = new Dictionary<int, Curso>();
            List<Candidato> candidatos = new List<Candidato>();
            string arqPath = "entrada2.txt";
            LeituraArquivo(arqPath, out cursos, out candidatos);

            ProcessarCandidatos(cursos, candidatos);
            StreamWriter arqEsc = new StreamWriter("saida.txt", true, Encoding.UTF8);
            foreach (Curso curso in cursos.Values)
            {
                arqEsc.WriteLine($"{curso.Nome} {curso.MediaCurso}");
                Console.WriteLine($"Curso: {curso.Nome}");
                arqEsc.WriteLine("Selecionados:");
                Console.WriteLine("Selecionados:");
                foreach (Candidato candidato in curso.Selecionados)
                {
                    Console.WriteLine($"{candidato.Nome} - {candidato.Media:F2}");
                    arqEsc.WriteLine($"{candidato.Nome} {candidato.Media} {candidato.NotaRedacao} {candidato.NotaMatematica} {candidato.NotaLinguagens}");
                }
                arqEsc.WriteLine();
                arqEsc.WriteLine();
     
                curso.FilaEspera.Mostrar();
            }
            arqEsc.Close();
        }
    }
}
