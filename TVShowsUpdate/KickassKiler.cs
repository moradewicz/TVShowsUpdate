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
using System.IO.Compression;


namespace TVShowsUpdate
{
    class KickassKiler
    {
        public String[,] ArrData(String name,string idS ,string idE)
        {
            String[,] data = new String[10, 20];
            Console.WriteLine("Downloading  data from Kickass ..........");


            var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            Stream stream = client.OpenRead("https://kat.cr/usearch/" + name + "/?field=seeders&sorder=desc");

            
            GZipStream responseStream = new GZipStream(stream , CompressionMode.Decompress);

          var reader = new StreamReader(responseStream);
           
          var textResponse = reader.ReadToEnd();
           
            responseStream.Dispose();
            reader.Dispose();
            Console.WriteLine("Downloaded data from Kickass -- OK");

            MatchCollection torrents = Regex.Matches(textResponse, @"<strong class=""red"">(.*?)</a>\n(.*?)\n(.*?)\n(.*?)\n(.*?)\n(.*?)\n(.*?)""nobr center"">(.*?) <span>(.*?)</span></td>\n(.*?)\n(.*?)\n(.*?)green center"">(.*?)</td>\n(.*?)red lasttd center"">(.*?)</td>\n(.*?)\n(.*?)\n(.*?)\n(.*?)\n(.*?)\n(.*?)Torrent magnet link"" href=""(.*?)""(.*?)\n(.*?)torrent file"" href=""//(.*?)"" class(.*?)\n(.*?)\n", RegexOptions.Multiline);



            foreach (Match m in torrents)
            {
                Console.WriteLine("Nazwa: " + m.Groups[1].Value.Replace(@"<strong class=""red"">", "").Replace("</strong>", "")); // nazwa
                Console.WriteLine("Rozmiar: " + m.Groups[8].Value); // mb
                Console.WriteLine("Rozmiar: " + m.Groups[9].Value); // mb
                Console.WriteLine("Seed: " + m.Groups[13].Value); //seed
                Console.WriteLine("Peer: " + m.Groups[15].Value); //peer
                Console.WriteLine("Magnet link: " + m.Groups[22].Value); //magnet
                Console.WriteLine("Torrent link: " + m.Groups[25].Value);

                string connectionString = @"Data Source=HPDV6\;Initial Catalog=TV;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    SqlCommand cmd = new SqlCommand
                    {

                        CommandType = CommandType.Text,
                        CommandText =
                            @"INSERT INTO  Torrent(IdEpisode,Rozmiar,Format,Seed,Peer,Magnet,Nazwa) VALUES (@IdEpisode,@Rozmiar,@Format,@Seed,@Peer,@Magnet,@Nazwa);"
                    };

                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@IdShow", idS);
                    cmd.Parameters.AddWithValue("@IdEpisode", idE);
                    cmd.Parameters.AddWithValue("@Rozmiar", m.Groups[8].Value);
                    cmd.Parameters.AddWithValue("@Format", m.Groups[9].Value);
                    cmd.Parameters.AddWithValue("@Seed", m.Groups[13].Value);
                    cmd.Parameters.AddWithValue("@Peer", m.Groups[15].Value);
                    cmd.Parameters.AddWithValue("@Magnet", m.Groups[22].Value);
                    cmd.Parameters.AddWithValue("@Nazwa", m.Groups[1].Value.Replace(@"<strong class=""red"">", "").Replace("</strong>", ""));
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

            }
            return data;
        }
    }
}
