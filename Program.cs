using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OraClass
{
    class Program
    {
        static void Main(string[] args)
        {
            string ns = "AistService.AF";
            string[] tables = { "dual" };
            string conStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.226)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl12)));Persist Security Info=True;User ID=FTS5_CRSVED_CONNECT;Password=FTS5_CRSVED_CONNECT";

            var g = new ClassGenerator();
            var res = new Dictionary<string, string>();
            foreach (var t in tables)
                res.Add(t, g.Generate(t, conStr, ns));

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "code");
            if (Directory.Exists(path))
                Directory.Delete(path, true);
            Directory.CreateDirectory(path);

            foreach (var r in res)
            {
                string path1 = Path.Combine(path, r.Key + ".cs");
                using (var sr = new StreamWriter(path1, false, Encoding.UTF8))
                {
                    sr.WriteLine(r.Value);
                    sr.Flush();
                }
            }

            Console.WriteLine("Created " + res.Count + " classes in bind/Debug/code folder");
        }
    }
}
