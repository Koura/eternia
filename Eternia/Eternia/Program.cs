using System;

namespace Eternia
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Eternia game = new Eternia())
            {
                game.Run();
            }
        }
    }
#endif
}

