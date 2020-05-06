using correios_san_andreas;
using correios_san_andreas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MenorCaminhoTest
{
    [TestClass]
    public class MenorCaminhoTest
    {
        [TestMethod]
        public void TestExemploPDF ()
        {
            // recupera os trechos 
            var trechos = Util.IO.ReadLinesFileTxt("Data/trechos.txt");
            var encomendas = Util.IO.ReadLinesFileTxt("Data/encomendas.txt");

            var nos = Program.RetornaListaNos(trechos);


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
            var caminho = Util.IO.WriteFileTxt("Dados/rotas.txt", rotas.ToString());
            var resultado  = Util.IO.ReadFileTxt("Data/exresultado.txt");
            resultado = resultado.Replace("\r", "");
            Assert.AreEqual(rotas.ToString(), resultado);            
        }
    }
}
