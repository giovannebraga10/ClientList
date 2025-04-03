﻿using System.Collections.Generic;

namespace ClientAPI.Domain.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int DDD { get; set; }
        public decimal Telefone { get; set; }
    }
}
