using System;
using System.Collections.Generic;
using System.Text;

namespace correios_san_andreas.Models
{
    public class Aresta
    {
        public int Valor { get; set; }
        public No NoDeOrigem { get; set; }
        public No NoDeDestino { get; set; }

        public Aresta()
        {

        }
        public Aresta(No origem, No destino, int valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor precisa ser maior que 0");
            }
            Valor = valor;
            NoDeOrigem = origem;
            NoDeDestino = destino;

            origem.Arestas.Add(this);
            destino.Arestas.Add(this);
        }
    }
}
