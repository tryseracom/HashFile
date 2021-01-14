using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace HashFile
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Usage: HashFile [FileToHash]");
                return;
            }

            string fileName = Path.GetFileNameWithoutExtension(args[0]);
            string extension = Path.GetExtension(args[0]);
            string directory = Path.GetDirectoryName(args[0]);
            string hashed = Path.Combine(directory, string.Format("{0}.hashed.{1}", fileName, extension));
            StreamReader reader = File.OpenText(args[0]);
            StreamWriter writer = new StreamWriter(hashed);
            string line = null;
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            while((line = reader.ReadLine()) != null)
            {
                byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(line));
                StringBuilder hashBuilder = new StringBuilder();
                foreach(byte b in hash)
                {
                    hashBuilder.AppendFormat("{0:x2}", b);
                }
                writer.WriteLine(hashBuilder.ToString());
            }
            writer.Flush();
            writer.Close();
            reader.Close();
        }
    }
}
