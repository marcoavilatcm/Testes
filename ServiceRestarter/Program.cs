using System;
using System.ServiceProcess;
using System.IO;

namespace ServiceRestarter
{
    internal class Program
    {


        static void Main(string[] args)
        {
            string serviceName = "Serviços de Área de Trabalho Remota";

            ServiceController[] services = ServiceController.GetServices();

            Console.WriteLine("Serviços em execução:");

            foreach (ServiceController service in services)
            {
                // Verifica se o serviço está em execução
                if (service.DisplayName == serviceName)
                {
                    reiniciaservico(service);
                    
                }
            }

            //Console.WriteLine("\nPressione qualquer tecla para sair...");
            //Console.ReadKey();

        }

        private static void reiniciaservico(ServiceController servico)
        {
            string NomeArquivoLog = DefineConfiguracoesConformeAmbiente();
            try
            {

                Logar($"Reiniciando o serviço: {servico.DisplayName}", NomeArquivoLog);

                if (servico.Status != ServiceControllerStatus.Stopped)
                {
                    servico.Stop();
                    servico.WaitForStatus(ServiceControllerStatus.Stopped);
                }

                servico.Start();
                servico.WaitForStatus(ServiceControllerStatus.Running);

                Logar($"Serviço {servico.DisplayName} reiniciado com sucesso.", NomeArquivoLog);
            }
            catch (Exception ex)
            {
                Logar($"Erro ao reiniciar o serviço: {ex.Message}", NomeArquivoLog);
            }
        }
        private static void listarservicos()
        {

                // Obtém todos os serviços instalados no sistema
                ServiceController[] services = ServiceController.GetServices();

                Console.WriteLine("Serviços em execução:");

                foreach (ServiceController service in services)
                {
                    // Verifica se o serviço está em execução
                    if (service.Status == ServiceControllerStatus.Running)
                    {
                        Console.WriteLine(service.DisplayName); // Nome amigável do serviço
                    }
                }

                Console.WriteLine("\nPressione qualquer tecla para sair...");
                Console.ReadKey();

        }


        private static string DefineConfiguracoesConformeAmbiente()
        {
            string NomeArquivo;
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");

            // Cria a pasta "log" se ela não existir
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            // Nome do arquivo de log com data e hora
            NomeArquivo = Path.Combine(logDir, $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            return NomeArquivo;
        }

        private static void Logar(string mensagem, string nomearquivo)
        {
            // Escreve a mensagem no console
            Console.WriteLine(mensagem);

                // Adiciona a mensagem no arquivo de log
                using (StreamWriter writer = new StreamWriter(nomearquivo, true))
                {
                    // "true" para anexar ao arquivo, se existir
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}");
                }

        }
    }
}
