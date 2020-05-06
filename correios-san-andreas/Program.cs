using correios_san_andreas.Models;
using correios_san_andreas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace correios_san_andreas
{
   public class Program
    {
        public static List<No> RetornaListaNos(string[] trechos)
        {
            var nos = new List<No>();
            if (trechos != null)
                foreach (var trecho in trechos)
                {
                    var valores = trecho.Split(" ");
                    if (valores.Length == 3)
                    {
                        //verifica se a origem e destino existem, caso contrario adiciona
                        if (!nos.Any(x => x.Nome == valores[0]))
                            nos.Add(new No(valores[0]));
                        if (!nos.Any(x => x.Nome == valores[1]))
                            nos.Add(new No(valores[1]));

                        var noOrgirem = nos.First(x => x.Nome == valores[0]);
                        var noDestino = nos.First(x => x.Nome == valores[1]);
                        int.TryParse(valores[2], out int peso);

                        //Criar aresta com o respectivo peso
                        noOrgirem.CriarAresta(noDestino, peso);
                    }
                }
            return nos;
        }
        static void Main(string[] args)
        {
            // recupera os trechos 
            var trechos = Util.IO.ReadLinesFileTxt("Data/trechos.txt");
            var encomendas = Util.IO.ReadLinesFileTxt("Data/encomendas.txt");



            var nos = RetornaListaNos(trechos);

            var rotas = new StringBuilder();
            foreach (var encomenda in encomendas)
            {
                var valores = encomenda.Split(" ");
                if (valores.Length == 2)
                {
                    var noOrgirem = nos.First(x => x.Nome == valores[0]);
                    var noDestino = nos.First(x => x.Nome == valores[1]);
                    var menorCaminho = Dijkstra.MenorCaminho(noOrgirem, noDestino);
                    var valorTotalRota = 0;
                    for (int i = 0; i < menorCaminho.Length; i++)
                    {
                        if (i + 1 < menorCaminho.Length)
                            valorTotalRota += menorCaminho[i].Arestas.Find(x => x.NoDeDestino == menorCaminho[i + 1]).Valor;
                        rotas.Append($"{menorCaminho[i].Nome} ");
                    }
                    rotas.Append($"{valorTotalRota}");
                    rotas.Append("\n");
                }
            }
            Console.WriteLine(rotas.ToString());

            var caminho = Util.IO.WriteFileTxt("Dados/rotas.txt", rotas.ToString());
            Console.WriteLine($"Arquivo gerado com sucesso!! {caminho}");

        }
    }
}
