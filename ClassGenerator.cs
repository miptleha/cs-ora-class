using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OraClass
{
    class ClassGenerator
    {
        public string Generate(string tab, string conStr, string ns)
        {
            var S1 = new string(' ', 4);
            var S2 = S1 + S1;
            var S3 = S1 + S1 + S1;
            var sb = new StringBuilder();
            sb.Append(
@"using Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Misc;

namespace " + ns + "\n{" + "\n");
            sb.Append(S1 + "class " + tab + " : IRow\n" + S1 + "{\n");

            using (var con = new OracleConnection(conStr))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "select * from " + tab + " where 1=0";
                using (var r = cmd.ExecuteReader())
                {
                    var t = r.GetSchemaTable();
                    var sb2 = new StringBuilder();
                    foreach (DataRow i in t.Rows)
                    {
                        string name = i[0].ToString();

                        var ty = (Type)i[11];
                        string type = ty.ToString();
                        if (ty == typeof(string))
                            type = "string";
                        else if (ty == typeof(short))
                            type = "short?";
                        else if (ty == typeof(long))
                            type = "long?";
                        else if (ty == typeof(decimal))
                            type = "decimal?";
                        else if (ty == typeof(DateTime))
                            type = "DateTime?";

                        string read = "?";
                        if (type == "string")
                            read = "Util.ToStr(r[\"" + name + "\"])";
                        else if (type == "short?")
                            read = "Util.ToShortNull(r[\"" + name + "\"])";
                        else if (type == "long?")
                            read = "Util.ToLongNull(r[\"" + name + "\"])";
                        else if (type == "decimal?")
                            read = "Util.ToDecimalNull(r[\"" + name + "\"])";
                        else if (type == "DateTime?")
                            read = "Util.ToDateNull(r[\"" + name + "\"])";
                        sb2.Append(S3 + name + " = " + read + ";\n");

                        sb.Append(S2 + "public " + type + " " + name + " { get; set; }\n");
                    }

                    sb.Append("\n" + S2 + "public void Init(DbDataReader r, Dictionary<string, int> columns)\n" + S2 + "{\n" + sb2.ToString() + S2 + "}\n");
                }
            }


            sb.Append(S1 + "}\n}");
            return sb.ToString();
        }
    }
}
