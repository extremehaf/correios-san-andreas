using correios_san_andreas.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace correios_san_andreas
{
    public class Dijkstra
    {

        private class PesoEstimativa
        {
            public No Origem { get; }
            public int Peso { get; }

            public PesoEstimativa(No origem, int peso)
            {
                Origem = origem;
                Peso = peso;
            }
        }

        private class VisitarVertice
        {
            private readonly List<No> _visitados;
            private readonly Dictionary<No, PesoEstimativa> _pesos;
            private readonly List<No> _aVisitar;
            public VisitarVertice()
            {
                _visitados = new List<No>();
                _pesos = new Dictionary<No, PesoEstimativa>();
                _aVisitar = new List<No>();
            }

            public void MarcarComoVisitado(No node)
            {
                if (!_visitados.Contains(node))
                    _visitados.Add((node));
            }

            public bool FoiVisitado(No node)
            {
                return _visitados.Contains(node);
            }

            public void AtualizarEstimativa(No node, PesoEstimativa novoPeso)
            {
                if (!_pesos.ContainsKey(node))
                {
                    _pesos.Add(node, novoPeso);
                }
                else
                {
                    _pesos[node] = novoPeso;
                }
            }

            public PesoEstimativa DescobrirPeso(No node)
            {
                PesoEstimativa result;
                if (!_pesos.ContainsKey(node))
                {
                    result = new PesoEstimativa(null, int.MaxValue);
                    _pesos.Add(node, result);
                }
                else
                {
                    result = _pesos[node];
                }
                return result;
            }

            public void AdicionarNoParaVisita(No node)
            {
                _aVisitar.Add(node);
            }

            public bool ExiteNosNaoVisitados => _aVisitar.Count > 0;

            public No RetornaNoAVisitar()
            {
                var result = _aVisitar.OrderBy(x => DescobrirPeso(x).Peso).First();
                _aVisitar.Remove(result);
                return result;
            }

            public bool HasComputedPathToOrigin(No node)
            {
                return DescobrirPeso(node).Origem != null;
            }

            public IEnumerable<No> ComputedPathToOrigin(No node)
            {
                var n = node;
                while (n != null)
                {
                    yield return n;
                    n = DescobrirPeso(n).Origem;
                }
            }
        }

        public static No[] MenorCaminho(No origem, No destino)
        {
            var controle = new VisitarVertice();

            //coloca o peso no vertice de origem como 0
            controle.AtualizarEstimativa(origem, new PesoEstimativa(null, 0));
            //marca como aberto para visitação
            controle.AdicionarNoParaVisita(origem);


            while (controle.ExiteNosNaoVisitados)
            {
                //pega o menor nó entre os a visitar
                var visitingNode = controle.RetornaNoAVisitar();
                //verifica o peso estimado para ele 
                var visitingNodeWeight = controle.DescobrirPeso(visitingNode);

                //marca o no como visitado
                controle.MarcarComoVisitado(visitingNode);

                //percore todos os adjacentes do no selecionado
                foreach (var adjacente in visitingNode.Adjacentes)
                {
                    //caso o no não estiver na lista para visitar adiciona
                    if (!controle.FoiVisitado(adjacente.No))
                    {
                        controle.AdicionarNoParaVisita(adjacente.No);
                    }

                    //verifica o peso do no adjacente
                    var pesoNoAdjacente = controle.DescobrirPeso(adjacente.No);

                    var estimativadePeso = (visitingNodeWeight.Peso + adjacente.PesoAteONo);
                    if (pesoNoAdjacente.Peso > estimativadePeso) // caso a estimativa de peso for menor subistitue
                    {
                        controle.AtualizarEstimativa(adjacente.No, new PesoEstimativa(visitingNode, estimativadePeso));
                    }
                }
            }
            //opos descobrir o menor caminho entre os nós, faz o caminho reverso do no de destino ate a origem
            return controle.HasComputedPathToOrigin(destino)
                ? controle.ComputedPathToOrigin(destino).Reverse().ToArray()
                : null;
        }
    }
}
