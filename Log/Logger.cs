using System;
using System.Collections.Generic;

namespace Log
{
    /// <summary>
    /// Logging all actions for application.
    /// Add code for logging to database, console, etc.
    /// Log4net requires initialization, see App.config.
    /// </summary>
    public class Logger : ILog
    {
        public Logger(Type type)
        {
            _type = type;
        }

        Type _type;

        public void Error(Exception ex)
        {
            Error("!!!Error", ex);
        }

        public void Error(string message, Exception ex)
        {
            Console.WriteLine("{0}\n{1}", message, ex.ToString());
        }

        public void Debug(string message)
        {
            Console.WriteLine(message);
        }
    }
}
