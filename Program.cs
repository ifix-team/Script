namespace Executor
{
    using System;
    using System.Windows.Forms;
    /// <summary>
    /// Defines the <see cref="Program" />.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main()
        {
           Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            Application.Run(new Executor());
        }
    }
}
