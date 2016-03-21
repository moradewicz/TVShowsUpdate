using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TVShowsUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("     HI     ");
            KickassKiler kiler = new KickassKiler();
            Finder find = new Finder();
           find.ArrData("tt0096697");
            int akt = Int32.Parse(find.aktualny);
            if (akt >= 10)
            {
                kiler.ArrData(find.nazwa + "%20S" + akt + "E01");
            }
            else {
                kiler.ArrData(find.nazwa + "%20S0" + akt + "E01");
            }
            find.ArrData("tt0106179");
             akt = Int32.Parse(find.aktualny);
            if (akt >= 10)
            {
                kiler.ArrData(find.nazwa + "%20S" + akt + "E01");
            }
            else {
                kiler.ArrData(find.nazwa + "%20S0" + akt + "E01");
            }
            find.ArrData("tt2575988");
             akt = Int32.Parse(find.aktualny);
            if (akt >= 10)
            {
                kiler.ArrData(find.nazwa + "%20S" + akt + "E01");
            }
            else {
                kiler.ArrData(find.nazwa + "%20S0" + akt + "E01");
            }
            find.ArrData("tt3749900");
             akt = Int32.Parse(find.aktualny);
            if (akt >= 10)
            {
              kiler.ArrData(find.nazwa + "%20S" + akt+"E01");
            }
            else {
              kiler.ArrData(find.nazwa + "%20S0" + akt +"E01");
            }
         
            Console.WriteLine("     END    ");
            Console.ReadKey();
            // ogarnac date,
        }
    }
}
