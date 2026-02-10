using System;
using System.Collections.Generic;
using System.Text;

namespace RentWise.Core.Entidades
{
    public abstract class EntidadeBase
    {

        public EntidadeBase()
        {
            CriadoEm = DateTime.Now;
            Ativo = true;
        }

        public int Id { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public bool Ativo {  get; private set; }

        public void Desativar() => Ativo = false;
    }
}
