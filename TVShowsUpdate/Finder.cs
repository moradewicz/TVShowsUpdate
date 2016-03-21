using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TVShowsUpdate
{


    class Finder
    {
        

        public String nazwa { get; set; }
        public String aktualny { get; set; }



        public String[,] ArrData(String code)
        {
          

                // use the connection here
            


            Console.WriteLine("-----Lets roll-oll-----");

            String[,]
            data = new String[7, 150];


            WebClient web = new WebClient();
           
            String html = web.DownloadString("http://www.imdb.com/title/"+code);
          //  Console.WriteLine(html);
            Match title = Regex.Match(html, @"originalTitle"">\s*(.*?)\s*<span", RegexOptions.Singleline);
            if (title.Length <= 4)
            { 
            title = Regex.Match(html, @"<h1 itemprop=""name"" class="""">(.*?)&nbsp;", RegexOptions.Singleline);
           
            }
          Console.WriteLine(title.Groups[1]);
            String TitleTXT = title.Groups[1].ToString();
            nazwa = TitleTXT;
            data[0, 0] = TitleTXT;

            Match description = Regex.Match(html, @"description"" content=""(.*?)""", RegexOptions.Multiline);
           Console.WriteLine(description.Groups[1]);
            String DescriptionTXT = @description.Groups[1].ToString();
            data[1, 0] = DescriptionTXT;

            Match rate = Regex.Match(html, @"<strong title=""(.*?) based on", RegexOptions.Multiline);
          Console.WriteLine(rate.Groups[1]);
            String RateTXT = rate.Groups[1].ToString();
            data[2, 0] = RateTXT;

            Match votes = Regex.Match(html, @"based on (.*?)user ratings", RegexOptions.Multiline);
            Console.WriteLine(votes.Groups[1]);
            String VotesTXT = votes.Groups[1].ToString();
            data[3, 0] = VotesTXT;

            Match poster = Regex.Match(html, @"<div class=""poster"">\n<a href=""(.*?)""\n(.*?)\nsrc=""(.*?)""", RegexOptions.Multiline);
            Console.WriteLine(poster.Groups[1]);
            String posterhtml = web.DownloadString("http://www.imdb.com/" + poster.Groups[1]);
            Match poster2 = Regex.Match(posterhtml, @"cursor-default(.*?)src=""(.*?)""", RegexOptions.Multiline);
            Console.WriteLine(poster2.Groups[2]);
            string subPath = @"D:\\app\\tv\\"+code;

            bool exists = System.IO.Directory.Exists(subPath);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(subPath);
            }
            web.DownloadFile(poster2.Groups[2].Value, @"D:\\app\\tv\\" + code+"\\poster.jpg");



            String PosterTXT = poster2.Groups[2].ToString();
            data[4, 0] = PosterTXT;

            Match cat = Regex.Match(html, @"><span class=""itemprop"" itemprop=""genre"">(.*?)</span>(.*?)\n(.*?)\n><span class=""itemprop"" itemprop=""genre"">(.*?)</span>", RegexOptions.Multiline);
            if (cat.Length <= 4)
            {
             cat = Regex.Match(html, @"><span class=""itemprop"" itemprop=""genre"">(.*?)</span>", RegexOptions.Multiline);
            }
          //  Console.WriteLine(cat.Groups[1]);
          // Console.WriteLine(cat.Groups[4]);
            String CatTXT = cat.Groups[1].ToString();
            data[5, 0] = CatTXT;

            String htmlS = web.DownloadString("http://www.imdb.com/title/"+code+"/episodes");

            Match actualSeasone = Regex.Match(htmlS, @"nbsp;<strong>Season (.*?)</strong>", RegexOptions.Multiline);
          //  Console.WriteLine(actualSeasone.Groups[1]);
            String ActualSeasoneTXT = actualSeasone.Groups[1].ToString();
            aktualny = ActualSeasoneTXT;
            data[6, 0] = ActualSeasoneTXT;
            int t = 0;

            string connectionString = @"Data Source=HPDV6\;Initial Catalog=TV;Integrated Security=True";
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
             
                
                SqlCommand cmd = new SqlCommand
                {

                    CommandType = CommandType.Text,
                    CommandText =
                        @"INSERT INTO  Show(Id,Name,Description,Rate,Votes,Poster,Cat,ActualSeasone) VALUES (@Id,@Name,@Description,@Rate,@Votes,@Poster,@Cat,@ActualSeasone);"
                };
                
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Id", code);
                cmd.Parameters.AddWithValue("@Name", TitleTXT);
                cmd.Parameters.AddWithValue("@Description", DescriptionTXT);
                cmd.Parameters.AddWithValue("@Rate", RateTXT);
                cmd.Parameters.AddWithValue("@Votes", VotesTXT);
                cmd.Parameters.AddWithValue("@Poster", PosterTXT);
                cmd.Parameters.AddWithValue("@Cat", CatTXT);
                cmd.Parameters.AddWithValue("@ActualSeasone", ActualSeasoneTXT);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
   
            MatchCollection episodesTitle = Regex.Matches(htmlS, @" itemprop=""name"">(.*?)</a></strong>", RegexOptions.Multiline);
            int espidoesTitleCount = episodesTitle.Count;
            foreach (Match m in episodesTitle)
            {
              Console.WriteLine(m.Groups[1].Value + t);
                data[0, t] = m.Groups[1].Value;
                t++;
            }

            MatchCollection episodesDescriptions = Regex.Matches(htmlS, @" <div class=""item_description"" itemprop=""description"">\n(.*?)</div>", RegexOptions.Multiline);
            int episodesDescriptionsCount = episodesDescriptions.Count;
            foreach (Match m in episodesDescriptions)
            {
                Console.WriteLine(m.Groups[1].Value + t);
                data[0, t] = m.Groups[1].Value;
                t++;
            }

            MatchCollection episodesAir = Regex.Matches(htmlS, @"<div class=""airdate"">\n(.*?)\n", RegexOptions.Multiline);
            int episodesAirCount = episodesAir.Count;
            foreach (Match m in episodesAir)
            {
               Console.WriteLine(m.Groups[1].Value+t);
                Console.WriteLine(m.Groups[1].Value);
                DateTime myDate = DateTime.Parse(m.Groups[1].Value);
                Console.WriteLine(myDate.ToString());
                data[0, t] = m.Groups[1].Value;
                t++;
            }
            int ss = 0;
            MatchCollection episodesCover = Regex.Matches(htmlS, @"<img width=""200"" height=""112"" class=""zero-z-index"" alt=""(.*?)"" src=""(.*?)"">\n<div>S(.*?), Ep(.*?)</div>", RegexOptions.Multiline);
            int episodesCoverCount = episodesCover.Count;
            foreach (Match m in episodesCover)
            {
                int s = Int32.Parse(m.Groups[3].Value);
                int e = Int32.Parse(m.Groups[4].Value);
                string sTXT;
                string eTXT;
                if (s < 10)
                {
                    sTXT = "S0" + s;
                }
                else
                {
                    sTXT = "S" + s;
                }
                if (e < 10)
                {
                    eTXT = "E0" + e;
                }
                else
                {
                    eTXT = "E" + e;
                }
               
            //    Console.WriteLine(sTXT+"    "+eTXT);
               
                data[0, t] = m.Groups[2].Value;
                web.DownloadFile(m.Groups[2].Value, @"D:\\app\\tv\\" + code + "\\"+sTXT+""+eTXT+".jpg");
                //   Console.WriteLine(m.Groups[2].Value);
                t++;
                data[0, t] = sTXT+""+eTXT;

              
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                   

                    SqlCommand cmd = new SqlCommand
                    {

                        CommandType = CommandType.Text,
                        CommandText =
                            @"INSERT INTO  Episode(Id,EpisodesTitle,EpisodesDescriptions,EpisodesAir,EpisodesCover,IdEpisode) VALUES (@Id,@EpisodesTitle,@EpisodesDescriptions,@EpisodesAir,@EpisodesCover,@IdEpisode);"
                    };
                  
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@Id", code);
                    cmd.Parameters.AddWithValue("@EpisodesTitle", data[0, ss]);
                    cmd.Parameters.AddWithValue("@EpisodesDescriptions", data[0, espidoesTitleCount+ss]);
                    cmd.Parameters.AddWithValue("@EpisodesAir", data[0, espidoesTitleCount+episodesDescriptionsCount + ss ]);
                    cmd.Parameters.AddWithValue("@EpisodesCover", data[0, espidoesTitleCount + episodesDescriptionsCount+episodesAirCount + ss]);
                    cmd.Parameters.AddWithValue("@IdEpisode", sTXT + "" + eTXT);
                    ss++;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
               

            }


            Console.WriteLine("title"+espidoesTitleCount+"des" + episodesDescriptionsCount+"air"+episodesAirCount);
        

            Console.WriteLine("-----Data RECIVED ------");
            return data;
        }
    }
}
