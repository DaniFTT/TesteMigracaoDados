using MongoDB.Bson;
using MongoDB.Driver;
using RoboDownload.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RoboDownload
{
    public static class MetodosMongoDB
    {

        public static void AdicionaArquivosNaBase(string[]  filesPath, bool jaExiste)
        {

            AcessoMongoDb acesso = new AcessoMongoDb();

            if (!jaExiste)
            {
                foreach (var file in filesPath)
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                       
                        do
                        {
                            string linha = sr.ReadLine();

                            if (linha == null)
                                break;


                            if (linha.IndexOf('1') == 0)
                            {
                                var empresa = new CnpjEmpresa
                                {
                                    Cnpj = linha.Substring(3, 14),
                                    RazaoSocial = linha.Substring(18, 150).Trim(' '),
                                    IdMatrizFilial = linha.Substring(17, 1),
                                    NomeFantasia = linha.Substring(168, 55).Trim(' '),
                                    SituacaoCadastral = linha.Substring(223, 2),
                                    CapitalSocial = Convert.ToDouble(linha.Substring(891, 14).Trim(' ')),
                                    DataSituacaoCadastral = DateTime.ParseExact(linha.Substring(225, 8), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                                    Cep = linha.Substring(674, 8),
                                    Socios = new List<CnpjSocio>()
                                };


                                acesso.Empresas.InsertOne(empresa);

                            }
                            else if(linha.IndexOf('2') == 0)
                            {
                                var cnpj = linha.Substring(3, 14);

                                var socio = new CnpjSocio
                                {
                                    RazaoNomeSocial = linha.Substring(18, 150).Trim(' '),
                                    IdentificadorSocio = linha.Substring(17, 1),
                                    CnpjCpfSocio = linha.Substring(168, 14),
                                };

                                var construtor = Builders<CnpjEmpresa>.Update;
                                var condicaoAlteracao = construtor.Push(x => x.Socios, socio);

                                acesso.Empresas.UpdateOne(x => x.Cnpj == cnpj, condicaoAlteracao);
                            }
                            else
                            {
                                continue;
                            }



                        } while (true);
                                               
                    }

                }
            }
            else
            {
                Console.WriteLine("\n\nOs Aquivos ja foram adicionados a base de dados!");
            }
        }
    }
}
