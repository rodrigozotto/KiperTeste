using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class MoradorModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> data_nascimento { get; set; }
        public string telefone { get; set; }
        public Nullable<int> id_apartamento { get; set; }
    }
}