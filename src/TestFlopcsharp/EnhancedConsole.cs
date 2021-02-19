using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TestFlopcsharp
{
    class EnhancedConsole
    {
        private TextWriter _tw;
        private ConsoleColor _DEFAULT;
        public EnhancedConsole()
        {
            _tw = Console.Out;
            _DEFAULT = Console.ForegroundColor;
        }
        public void WriteTest(bool b)
        {
            _tw.Write("[");
            if (b)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                _tw.Write("OK");

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                _tw.Write("Failed");
            }
            Console.ForegroundColor = _DEFAULT;
            _tw.WriteLine("]");
        }
    }
}
