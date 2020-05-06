using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace correios_san_andreas.Models
{
    public class No
    {
        public string Nome { get; set; }

        public List<Aresta> Arestas { get; }

        public IEnumerable<Vizinho> Adjacentes => Arestas.Select(x => new Vizinho()
        {
            No = x.NoDeOrigem == this ? x.NoDeDestino : x.NoDeOrigem,
            PesoAteONo = x.Valor

        });

        public No()
        {
            Arestas = new List<Aresta>();
        }
        public No(string nome)
        {
            this.Nome = nome;
            Arestas = new List<Aresta>();
        }

        public void CriarAresta(No noDestino, int peso)
        {
            new Aresta(this, noDestino, peso);
        }
    }
}
