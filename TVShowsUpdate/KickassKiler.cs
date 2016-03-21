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
        public String[,] ArrData(String name)
        {
            String[,] data = new String[10, 20];


          var client = new WebClient();
          GZipStream responseStream = new GZipStream(client.OpenRead("https://kat.cr/usearch/" + name + "/?field=seeders&sorder=desc"), CompressionMode.Decompress);
          var reader = new StreamReader(responseStream);
          var textResponse = reader.ReadToEnd();



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

                //string connectionString = @"Data Source=HPDV6\;Initial Catalog=TV;Integrated Security=True";
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{


                //    SqlCommand cmd = new SqlCommand
                //    {

                //        CommandType = CommandType.Text,
                //        CommandText =
                //            @"INSERT INTO  Torrent(IdEpisode,Rozmiar,Format,Seed,Peer,Magnet,Nazwa) VALUES (@IdEpisode,@Rozmiar,@Format,@Seed,@Peer,@Magnet,@Nazwa);"
                //    };

                //    cmd.Connection = connection;
                //    cmd.Parameters.AddWithValue("@IdEpisode", m.Groups[2].Value);
                //    cmd.Parameters.AddWithValue("@Rozmiar", m.Groups[10].Value );
                //    cmd.Parameters.AddWithValue("@Format", m.Groups[11].Value);
                //    cmd.Parameters.AddWithValue("@Seed", m.Groups[15].Value);
                //    cmd.Parameters.AddWithValue("@Peer", m.Groups[17].Value);
                //    cmd.Parameters.AddWithValue("@Magnet", m.Groups[24].Value);
                //    cmd.Parameters.AddWithValue("@Nazwa", name);
                //    connection.Open();
                //    cmd.ExecuteNonQuery();
                //    connection.Close();
                //}

            }
            return data;
        }
    }
}
