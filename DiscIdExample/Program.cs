namespace DiscIdExample
{
    using System;
    using DiscId;

    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine(Disc.DefaultDevice);
            
            using (var disc = Disc.Read("E:"))
            {
                Console.Out.WriteLine(disc.Id);
                Console.In.ReadLine();
            }
        }
    }
}
