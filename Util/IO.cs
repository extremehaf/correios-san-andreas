using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Util
{
    public static class IO
    {
        public static string ReadFileTxt(string arquivo)
        {
            if (File.Exists(arquivo) && Path.HasExtension(".txt"))
            {
                // Read entire text file content in one string    
                return File.ReadAllText(arquivo);
            }

            return null;
        }
        public static string[] ReadLinesFileTxt(string arquivo)
        {
            if (File.Exists(arquivo) && Path.HasExtension(".txt"))
            {
                // Read entire text file content in one string    
                return File.ReadAllLines(arquivo);
            }

            return null;
        }

        public static string WriteFileTxt(string caminho, string conteudo)
        {
            var path = @"Data/rotas.txt";

            File.WriteAllText(path, conteudo, Encoding.UTF8);

            return Path.GetFullPath(path);
        }
    }  
}
