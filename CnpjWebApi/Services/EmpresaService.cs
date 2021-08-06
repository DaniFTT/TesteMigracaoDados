using CnpjWebApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CnpjWebApi.Services
{
    public class EmpresaService
    {
        private readonly IMongoCollection<CnpjEmpresa> _empresas;

        public EmpresaService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _empresas = database.GetCollection<CnpjEmpresa>(settings.EmpresasCollectionName);
        }

        public async Task<CnpjEmpresa> Get(string cnpjEmpresa)
        {
            var empresa = await _empresas.Find(empresa => empresa.Cnpj == cnpjEmpresa).FirstOrDefaultAsync();

            return empresa;
        }
        public async Task<List<CnpjEmpresa>> Get()
        {
            return await _empresas.Find(new BsonDocument()).Limit(5).ToListAsync(); 
        }

    }
}
