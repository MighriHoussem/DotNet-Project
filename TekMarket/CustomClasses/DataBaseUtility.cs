using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using TekMarket.Models;


namespace TekMarket.CustomClasses
{
    public class DataBaseUtility
    {
        private String connectionString;
        private SqlConnection conn;
        private String command;
        private SqlCommand sqlCmd = new SqlCommand();
        private SqlDataReader reader;

        public DataBaseUtility()
        {
            connectionString = "workstation id=fia3glprojectdb.mssql.somee.com;packet size=4096;user id=fia3;pwd=Dotnetprojectfia3gl;data source=fia3glprojectdb.mssql.somee.com;persist security info=False;initial catalog=fia3glprojectdb";
            conn = new SqlConnection(connectionString);
        }

        public void init()
        {
            conn.Open();
        }

        public String[] categoryDescriptions()
        {
            DBModel db = new DBModel();
            List<Categorie> cat = db.Categories.ToList();
            String[] st = new string[cat.Count];
            for (int i = 0; i < cat.Count; i++)
            {
                st[i] = cat.ElementAt<Categorie>(i).description;
            }
            return st;
        }

        public String[] productLib()
        {
            DBModel db = new DBModel();
            List<Article> cat = db.Articles.ToList();
            String[] st = new string[cat.Count];
            for (int i = 0; i < cat.Count; i++)
            {
                st[i] = cat.ElementAt<Article>(i).libelle;
            }
            return st;
        }

        public String PriceByName(String name)
        {
            var selectString = $"SELECT a.prix FROM Article a WHERE a.libelle = '{name}';";
            sqlCmd.CommandText = selectString;
            sqlCmd.Connection = conn;
            reader = sqlCmd.ExecuteReader();
            String result = "<ul>";
            if (reader.Read())
            {
                result += "<li><p>" + name + ": " + reader["prix"] + " DT</p><br/>";
            }
            result += "</ul>";
            return result;
        }

        public String QuantityByName(String name)
        {
            var selectString = $"SELECT a.qtedisponible disp FROM Article a WHERE a.libelle = '{name}';";
            sqlCmd.CommandText = selectString;
            sqlCmd.Connection = conn;
            reader = sqlCmd.ExecuteReader();
            long q = -1;
            if (reader.Read())
            {
                q = (long)reader["disp"];
            }

            String res = "<p>";
            if (q > 0)
            {
                res += "we still have " + q + " pieces of " + name;
            }
            else
            {
                if (q == 0)
                {
                    res += "sorry, but we do not have " + name + " for now :( you can wait until the next week we will bring it for you!";
                }
                else
                {
                    res += "never heard of " + name + " sorry :(";
                }
            }
            res += "</p><br/>";


            return res;
        }
        
        public String ProductByCategory(String category)
        {
            var selectString = $"SELECT a.refarticle ref, a.libelle lib, a.description des, a.prix pr, c.description descat FROM Article a, Categorie c WHERE a.idcategorie=c.id and c.description like '%{category}%';";
            sqlCmd.CommandText = selectString;
            sqlCmd.Connection = conn;
            reader = sqlCmd.ExecuteReader();
            String img = "";
            String desc = "";
            String result = "<ul>";
            while (reader.Read())
            {
                desc = (String)reader["descat"];
                img = "../UploadedImage/" + reader["ref"] + ".jpg";
                result += "<li><p>" + reader["lib"] + ": " + reader["pr"] + " DT</p><br/>" +
                    "<img width=\"50px\" height=\"50px\" src=" + img + "></li><br/>";
            }
            result += "</ul>";
            result = "<p>" + desc + " list: </p><br/>" + result;
            return result;
        }

        public String ProductList()
        {
            var selectString = $"SELECT a.refarticle ref, a.libelle lib from article a ;";
            sqlCmd.CommandText = selectString;
            sqlCmd.Connection = conn;
            reader = sqlCmd.ExecuteReader();
            String result = "<ul> the list of products is:<br/>";
            if (reader.HasRows)
            {
               
                while (reader.Read())
                {
                    result += "<li>  &nbsp;" + reader.GetString(1) + "</li>";
                }
            }
            else
            {
                result = "No rows found.";
            }
            reader.Close();
            result += "</ul>";
            return result;
        }

        public String ColorListByName(String name)
        {
            var selectString = $"SELECT a.libelle, a.couleurdispo color from article a where a.libelle='{name}';";
            sqlCmd.CommandText = selectString;
            sqlCmd.Connection = conn;
            reader = sqlCmd.ExecuteReader();
            String result = "the list of colors of " + name + " is  &nbsp; : ";
            result += "<p></p>";
            if (reader.Read())
            {
                result += reader["Color"];
            }
            else
            {
                result = "not color.";
            }
            reader.Close();
            return result;
        }

        public String DescriptionByName(String name)
        {
            var selectString = $"SELECT a.libelle, a.description des from article a where a.libelle='{name}';";
            sqlCmd.CommandText = selectString;
            sqlCmd.Connection = conn;
            reader = sqlCmd.ExecuteReader();
            String result = "the description of " + name + " is  &nbsp; : ";
            result += "<p></p>";
            if (reader.Read())
            {
                result += reader["des"];
            }
            else
            {
                result = "I don't find the description";
            }
            reader.Close();
            return result;
        }
        

        public String MostWanted()
        {
            var selectString = $"SELECT a.refarticle ref, a.libelle lib, count(qc.refcom) cqc FROM Article a, qtecommande qc WHERE a.refarticle=qc.refarticle GROUP BY a.refarticle, a.libelle;";
            sqlCmd.CommandText = selectString;
            sqlCmd.Connection = conn;
            reader = sqlCmd.ExecuteReader();
            String result = "<ul>";
            while (reader.Read())
            {
                result += "<li><p>Libelle:" + reader["lib"] + "</p>" + "</li><br/>";
                result += "<li><p>Sold:" + reader["cqc"] + "</p>" + "</li><br/>";
            }
            result += "</ul>";
            return result;
        }


    }
}