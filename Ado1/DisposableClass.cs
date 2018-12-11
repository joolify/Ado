using System;
using System.Collections.Generic;
using System.Text;

namespace Ado1
{
    class DisposableClass : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Lämnar tillbaka omanagerade resurser");
        }
    }
}
