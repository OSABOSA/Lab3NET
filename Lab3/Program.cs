namespace Lab3
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());


            //int n = 10;
            //Thread[] threads = new Thread[n];
            //for (int i = 0; i < n; i++)
            //{
            //    threads[i] = new Thread(Welcome);
            //    threads[i].Name = $"Thread {i + 1}";
            //}
            //foreach (Thread thread in threads)
            //{
            //    thread.Start();
            //}

        }

        //private static void Welcome()
        //{
        //     Console.WriteLine($"{Thread.CurrentThread.Name} : Hello !!");
        //}


    }
}