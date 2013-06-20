namespace DiscIdExample
{
    using System;
    using DiscId;

    class Program
    {
        static void Main(string[] args)
        {
			Console.Out.WriteLine("Using device   : {0}", Disc.DefaultDevice);

            using (var disc = Disc.Read())
            {
                Console.Out.WriteLine("DiscId         : {0}", disc.Id);
                Console.Out.WriteLine("FreeDB ID      : {0}", disc.FreedbId);
                Console.Out.WriteLine("MCN            : {0}", disc.Mcn);
                Console.Out.WriteLine("First track no.: {0}", disc.FirstTrackNumber);
                Console.Out.WriteLine("Last track no. : {0}", disc.LastTrackNumber);
                Console.Out.WriteLine("Sectors        : {0}", disc.Sectors);
                Console.Out.WriteLine("Submission URL : {0}", disc.SubmissionUrl);
                Console.In.ReadLine();
            }
        }
    }
}
