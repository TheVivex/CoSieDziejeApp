using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Maui.Storage;
using MySql.Data.MySqlClient;

namespace openstreetmapAPI
{
    class DataBaseConnection
    {
        private string connectionString = string.Format(@"server=51.83.131.143;" +
            "userid=hack_pr;password=hackpr123;database={0}", "hack_pr");
        class Marker
        {
            public int id;
            public string short_name;
            public string description;
            public int author_id;
            public int date;
            public string time;
            public int isOpen;
            public string price;
            public int category;
            public string adres;
            public string website;
            public string lat;
            public string lon;

            public Marker() { }

            public Marker(int id, string short_name, string description, int author_id, int date, string time, int isOpen, string price, int category, string adres, string website, string lat, string lon)
            {
                this.id = id;
                this.short_name = short_name;
                this.description = description;
                this.author_id = author_id;
                this.date = date;
                this.time = time;
                this.isOpen = isOpen;
                this.price = price;
                this.category = category;
                this.adres = adres;
                this.website = website;
                this.lat = lat;
                this.lon = lon;
            }
        }
        List<Marker> markers_list = new List<Marker>();
        Marker marker = new Marker();
        public List<string> Adres(string lat, string lon)
        {
            List<string> final = new List<string>();
            string GetJson()
            {
                string result = "";
                string url = string.Format("https://nominatim.openstreetmap.org/reverse?lat={0}&lon={1}&format=json", lat, lon);
                var req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
                req.UserAgent = ".NET Framework Test Client";
                using (var resp = req.GetResponse())
                {
                    var results = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                    result = results.ToString();
                }

                return result;
            }
            static string getBetween(string strSource, string strStart, string strEnd)
            {
                if (strSource.Contains(strStart) && strSource.Contains(strEnd))
                {
                    int Start, End;
                    Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    End = strSource.IndexOf(strEnd, Start);
                    return strSource.Substring(Start, End - Start);
                }

                return "";
            }
            //Legenda Indeksów
            //0 - numer domu
            //1 - ulica
            //2 - miasto
            //3 - kod pocztowy
            //4 - panstwo
            //5 - nazwa obiektu

            string json_code = GetJson();
            final.Add(getBetween(json_code, "\"house_number\":\"", "\","));
            final.Add(getBetween(json_code, "\"road\":\"", "\","));
            string miasto = getBetween(json_code, "\"town\":\"", "\",");
            if (miasto == "")
            {
                miasto = getBetween(json_code, "\"city\":\"", "\",");
            }
            final.Add(miasto);
            final.Add(getBetween(json_code, "\"postcode\":\"", "\","));
            final.Add(getBetween(json_code, "\"country\":\"", "\","));
            final.Add(getBetween(json_code, "\"name\":\"", "\","));
            return final;
        }

        public int AddNewMarker(int author_id, string short_name, int isOpen, string description, string date, string time_start, string time_stop, int category, string price, string website, string lat, string lon, string filename)
        {
            //legenda 
            // 0 - dodano pomyślnie
            // 1 - błąd

            int future_id = 0;
            try
            {
                using var con = new MySqlConnection(connectionString);
                con.Open();
                string query = string.Format("SHOW TABLE STATUS LIKE 'markers';");
                MySqlCommand command = new MySqlCommand(query, con);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        future_id = reader.GetInt32("Auto_increment") + 1;
                    }
                }
                if (filename != "")
                {
                    InsertPhoto(future_id, filename);
                }
                string time = string.Format("{0};{1}", time_start, time_stop);

                List<string> adress = Adres(lat.ToString(), lon.ToString());
                string adres = string.Format("{0} {1};{2};{3}", adress[1], adress[0], adress[2], adress[3]);
                query = string.Format("INSERT INTO `markers` (`id`, `short_name`, `description`, `author_id`, `date`, `time`, `isOpen`, `price`, `category`, `adres`, `website`, `lat`, `lon`) VALUES (NULL, '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}');",
                    short_name, description, author_id, date, time, isOpen, price, category, adres, website, lat, lon);
                command = new MySqlCommand(query, con);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                    }
                }

                con.Close();
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public List<string> GetMarkerInfo(int id)
        {
            //Legenda indeksów
            //0 - nagłówek
            //1 - opis
            //2 - data
            //3 - zakres godzin
            //4 - czy impreza jest otwarta
            //5 - cena
            //6 - adres
            //7 - strona int
            //8 - Nazwa organizatora
            //9 - nazwa kategorii
            List<string> render = new List<string>();
            int author_id = 1;
            int categorie_id = 2115;
            using var con = new MySqlConnection(connectionString);
            con.Open();
            string query = string.Format("SELECT * FROM markers WHERE id = '{0}';", id);
            MySqlCommand command = new MySqlCommand(query, con);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    author_id = reader.GetInt32("author_id");
                    categorie_id = reader.GetInt32("category");
                    render.Add(reader.GetString("short_name"));
                    render.Add(reader.GetString("description"));
                    render.Add(reader.GetString("date"));
                    render.Add(reader.GetString("time"));
                    render.Add(reader.GetInt32("isOpen").ToString());
                    render.Add(reader.GetString("price"));
                    render.Add(reader.GetString("adres"));
                    render.Add(reader.GetString("website"));
                }
            }


            query = string.Format("SELECT full_name FROM authors WHERE id = '{0}';", author_id);
            command = new MySqlCommand(query, con);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    render.Add(reader.GetString("full_name"));
                }
            }

            query = string.Format("SELECT name FROM categories WHERE id = '{0}';", author_id);
            command = new MySqlCommand(query, con);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    render.Add(reader.GetString("name"));
                }
            }

            con.Close();
            return render;
        }

        public class listaMarkerow
        {
            public int id { get; set; }
            public int cat_id { get; set; }
            public string name { get; set; }
            public string lat { get; set; }
            public string lon { get; set; }

            public listaMarkerow() { }

            public listaMarkerow(int id, int cat_id, string name, string lat, string lon)
            {
                this.id = id;
                this.cat_id = cat_id;
                this.name = name;
                this.lat = lat;
                this.lon = lon;
            }
        }

        public List<listaMarkerow> Markers()
        {
            //Legenda
            //0 - id markera
            //1 - id kategorii
            //2 - nazwa markera
            //3 - lat
            //4 - long
            List<listaMarkerow> render = new List<listaMarkerow>();
            using var con = new MySqlConnection(connectionString);
            con.Open();
            string query = string.Format("SELECT id, category, short_name, lat, lon FROM markers");
            MySqlCommand command = new MySqlCommand(query, con);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    render.Add(new listaMarkerow(reader.GetInt32("id"), reader.GetInt32("category"), reader.GetString("short_name"), reader.GetString("lat"), reader.GetString("lon")));
                }
            }
            con.Close();
            return render;
        }
        public async void test(int m_id, string filename)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO images (id, markes_id, img) VALUES (NULL, @markes_id, @ImageData)", connection))
                {

                    byte[] imageData = File.ReadAllBytes(filename);

                    cmd.Parameters.Add("@markes_id", MySqlDbType.Int32).Value = m_id;
                    cmd.Parameters.Add("@ImageData", MySqlDbType.Blob).Value = imageData;

                    int rowsAffected = cmd.ExecuteNonQuery();

                }
            }
        }
        public int InsertPhoto(int m_id, string filename)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.OpenAsync();

                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO images (id, markes_id, img) VALUES (NULL, @markes_id, @ImageData)", connection))
                {

                    byte[] imageData = File.ReadAllBytes(filename);

                    cmd.Parameters.Add("@markes_id", MySqlDbType.Int32).Value = m_id;
                    cmd.Parameters.Add("@ImageData", MySqlDbType.Blob).Value = imageData;

                    int rowsAffected = cmd.ExecuteNonQuery();

                }
            }
            return 0;
        }
        public byte[] streamFile;
        public void GetPhoto(int m_id)
        {
            //Co przy zdj dodać!
            //BitmapImage bitmap = new BitmapImage();
            //using (MemoryStream stream = new MemoryStream(connection.streamFile))
            //{
            //    bitmap.BeginInit();
            //    bitmap.CacheOption = BitmapCacheOption.OnLoad;
            //    bitmap.StreamSource = stream;
            //    bitmap.EndInit();
            //}
            //img_img.Source = bitmap;



            using var con = new MySqlConnection(connectionString);
            con.Open();
            string query = string.Format("SELECT img FROM images WHERE markes_id = '{0}'", m_id);
            MySqlCommand command = new MySqlCommand(query, con);
            byte[] imageData;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    imageData = (byte[])reader["img"];
                    streamFile = imageData;
                }
            }

            con.Close();
        }

    }
}
