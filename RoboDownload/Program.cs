using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace RoboDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            //var downloadUrl = "https://drive.google.com/u/0/uc?export=download&confirm=Tx6V&id=11JEE8WKSD9_FBAfGfiFq_z-ZtS1bmGeR"
            string downloadUrl = "https://drive.google.com/file/d/11JEE8WKSD9_FBAfGfiFq_z-ZtS1bmGeR/view";

            var pathArquivosZipados = Environment.CurrentDirectory + "\\arquivos-cnpj-zipados";
            var pathArquivosExtraidos = Environment.CurrentDirectory + "\\arquivos-cnpj-extraidos";

            if (!File.Exists(pathArquivosZipados))
            {
                Directory.CreateDirectory(pathArquivosZipados);
            }
            if (!File.Exists(pathArquivosExtraidos))
            {
                Directory.CreateDirectory(pathArquivosExtraidos);
            }

            using (FileDownloader downloader = new FileDownloader())
            {
                var nomeArquivo = "DADOS_CNPJ_EMPRESAS_1.zip";
                var caminhoArquivo = $"{pathArquivosZipados}\\{nomeArquivo}";

                if (File.Exists(caminhoArquivo))
                {
                    Console.WriteLine($"\nArquivo {nomeArquivo} já existe na base e nao pode ser adicionado novamente!");
                }
                else
                {
                    Console.WriteLine($"Download do arquivo: {nomeArquivo} iniciado... \nLink do Download: {downloadUrl}\nBaixando...\n\n");
                    downloader.DownloadFile(downloadUrl, caminhoArquivo);
                    Console.WriteLine($"Download do {nomeArquivo} Completo!\n\n");
                }
               
            }


            Console.WriteLine("Extração dos Arquivos iniciada!\n\n");

            bool jaExiste = false;
            var arquivosExtraidos = Directory.GetFiles(pathArquivosExtraidos);
            if (arquivosExtraidos.Length == 1)
            {
                jaExiste = true;
            }


            foreach (var file in Directory.GetFiles(pathArquivosZipados))
            {
                if (!jaExiste)
                {
                    Console.WriteLine($"Extraindo arquivo {file}...\n");
                    ZipFile.ExtractToDirectory(file, pathArquivosExtraidos, true);
                    Console.WriteLine($"Arquivo {file} foi extraido com sucesso para a pasta: {pathArquivosExtraidos}\n");
                }
            }

            Console.WriteLine("\nTodos os Aquivos foram extraidos!\n\n");


            Console.WriteLine("Os Arquivos agora serão adicionados na base de Dados MongoDB");
            Console.WriteLine("Adicionando....\n\n");


            MetodosMongoDB.AdicionaArquivosNaBase(Directory.GetFiles(pathArquivosExtraidos), jaExiste);

            Console.WriteLine("Finalizado!");
        }
    }
}
