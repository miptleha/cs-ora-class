# Generate class for oracle table

Console application that selects table structure and generates cs-files.<br/>
Generated classes have methods to initialize object from db [Database layer](https://github.com/miptleha/cs-ora-dblayer).


## How to use
-   clone or download project
-   open in Visual Studio
-   edit Program.cs: set connection string and list of tables
-   run application
-   see content of bin/debug/code folder for classes
-   all required helper classes, used in generated classes, included in project

## Sample


```cs
string[] tables = { "dual" };
string conStr = "...";
```

generated dual.cs:
```cs
using Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Misc;

namespace AistService.AF
{
    class dual : IRow
    {
        public string DUMMY { get; set; }

        public void Init(DbDataReader r, Dictionary<string, int> columns)
        {
            DUMMY = Util.ToStr(r["DUMMY"]);
        }
    }
}```
