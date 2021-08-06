using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoboDownload.Models
{
    public class AcessoMongoDb
    {
        public const string STRING_DE_CONEXAO = "mongodb://localhost:27017";
        public const string NOME_DA_BASE = "DadosEmpresariais";
        public const string COLECAO_EMPRESA = "Empresas";

        private static readonly IMongoClient _cliente;
        private static readonly IMongoDatabase _BaseDeDados;

        static AcessoMongoDb()
        {
            _cliente = new MongoClient(STRING_DE_CONEXAO);
            _BaseDeDados = _cliente.GetDatabase(NOME_DA_BASE);
        }

        public IMongoClient Cliente => _cliente;

        public IMongoCollection<CnpjEmpresa> Empresas => _BaseDeDados.GetCollection<CnpjEmpresa>(COLECAO_EMPRESA);
    }
}
