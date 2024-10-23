using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrarInicializacao
{
    using Microsoft.Win32;

    class Program
    {
        static void Main(string[] args)
        {
            // Caminho da sua aplicação
            string appPath = @"C:\MeusArquivos\ServiceRestarter\ServiceRestarter.exe";

            // Adiciona a chave ao registro para rodar na inicialização
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            regKey.SetValue("ServiceRestarterApp", appPath);

            Console.WriteLine("Aplicação configurada para iniciar com o Windows.");
        }
    }
}
