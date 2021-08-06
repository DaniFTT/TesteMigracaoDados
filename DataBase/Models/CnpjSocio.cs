using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Models
{
    public class CnpjSocio 
    {
        public string RazaoNomeSocial { get; set; }
        public string IdentificadorSocio { get; set; }
        public string CnpjCpfSocio { get; set; }
    }
}
