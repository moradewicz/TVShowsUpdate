using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;


namespace TVShowsUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            ResizeImage ri = new ResizeImage();
            Image img = new Bitmap(@"D:\\app\\tv\\tt2306299\\poster.jpg");

            Stream newstream = ri.resize(ri.ToStream(img, ImageFormat.Jpeg));
            Image newImg = System.Drawing.Image.FromStream(newstream);

            newImg.Save(@"D:\\app\\tv\\tt2306299\\poster2.jpg", ImageFormat.Jpeg);
            // UWAGA FAZA TESTOW dodano // ponizej 
            // UStawic dobra rzodzilczos dla cropa 

            // string[] dataID = new string[36] {"tt2879552", "tt2788432", "tt5189670", "tt4189022", "tt2707408", "tt3322312", "tt4158110","tt3032476", "tt2575988", "tt2802850", "tt2356777", "tt2861424", "tt2243973", "tt2234222", "tt2306299", "tt1856010", "tt2017109", "tt2085059", "tt1796960", "tt1632701", "tt0944947", "tt1520211", "tt1475582", "tt0903747", "tt0773262", "tt0898266", "tt0455275", "tt0412142", "tt0411008", "tt1628033", "tt0185906", "tt0121955", "tt0106179", "tt0096697", "tt3230454", "tt3749900" };
            // Console.WriteLine("     HI     ");
            // KickassKiler kiler = new KickassKiler();
            // Finder find = new Finder();
            // //find.ArrData("tt0096697");
            //// int akt = Int32.Parse(find.aktualny);
            // // if (akt >= 10)
            // // {
            // //     kiler.ArrData(find.nazwa + "%20S" + akt + "E01");
            // // }
            // // else {
            // //     kiler.ArrData(find.nazwa + "%20S0" + akt + "E01");
            // // }
            // // find.ArrData("tt0106179");
            // //  akt = Int32.Parse(find.aktualny);
            // // if (akt >= 10)
            // // {
            // //     kiler.ArrData(find.nazwa + "%20S" + akt + "E01");
            // // }
            // // else {
            // //     kiler.ArrData(find.nazwa + "%20S0" + akt + "E01");
            // // }
            // // find.ArrData("tt2575988");
            // //  akt = Int32.Parse(find.aktualny);
            // // if (akt >= 10)
            // // {
            // //     kiler.ArrData(find.nazwa + "%20S" + akt + "E01");
            // // }
            // // else {
            // //     kiler.ArrData(find.nazwa + "%20S0" + akt + "E01");
            // // }


            // foreach (var t in dataID)
            // {
            //     find.ArrData(t);


            // }

            //// id serialu (code) nie przechodzi do query w sql server



            // Console.WriteLine("     END    ");
            // Console.ReadKey();
            // // ogarnac date,
        }
    }
}
