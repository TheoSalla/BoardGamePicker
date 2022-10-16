using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace razerBoard.Models.Board
{
    public class UserCreation
    {

        public static string game;
        public static string pic;
        public static string minplayer;
        public static string maxplayer;

        public static List<string> games = new List<string>();
        public static void getInfoToLists() // Det här kan man effektivisera
        {


            string newItem = "hello";

            for (int i = 0; i < games.Count; i++)
            {
                newItem = games[i].Substring(0, games[i].IndexOf("ä2"));
                games[i] = games[i].Replace(newItem, "");
                newItem = Regex.Replace(newItem, "ä1", "");
                listOfGam.Add(newItem);

            }



            for (int i = 0; i < games.Count; i++)
            {
                newItem = games[i].Substring(0, games[i].IndexOf("ä3"));
                games[i] = games[i].Replace(newItem, "");
                newItem = Regex.Replace(newItem, "ä2", "");
                listOfPic.Add(newItem);
            }



            for (int i = 0; i < games.Count; i++)
            {
                newItem = games[i].Substring(0, games[i].IndexOf("ä4"));
                games[i] = games[i].Replace(newItem, "");
                newItem = Regex.Replace(newItem, "ä3", "");
                minPlay.Add(newItem);
            }



            for (int i = 0; i < games.Count; i++)
            {
                newItem = games[i].Substring(0, games[i].IndexOf("ä5"));
                games[i] = games[i].Replace(newItem, "");
                newItem = Regex.Replace(newItem, "ä4", "");
                maxPlay.Add(newItem);
            }

            for (int i = 0; i < games.Count; i++)
            {
                newItem = games[i].Substring(0, games[i].IndexOf("ä6"));
                games[i] = games[i].Replace(newItem, "");
                newItem = Regex.Replace(newItem, "ä5", "");
                user.Add(newItem);
            }




            games.Clear();


        }
        public static List<string> listOfGam = new List<string>();
        public static List<string> listOfPic = new List<string>();
        public static List<string> minPlay = new List<string>();
        public static List<string> maxPlay = new List<string>();
        public static List<string> user = new List<string>();




        public static void createUser(string username)
        {

            Again:
            try
            {
            XmlReader reader = new XmlTextReader("https://www.boardgamegeek.com/xmlapi/collection/" + username);
            //string game;

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element && reader.Name == "status")
                    || (reader.NodeType == XmlNodeType.Element && reader.Name == "name")
                    || (reader.NodeType == XmlNodeType.Element && reader.Name == "image")
                    || (reader.NodeType == XmlNodeType.Element && reader.Name == "stats")

                    )  // Status om man äger, name är namnet på spelet
                {

                    string ElementName = reader.Name;

                    string own = "0";


                    if (ElementName == "name")
                    {
                        string GetGameName = reader.ReadElementString();
                        game = GetGameName;
                    }

                    if (ElementName == "status")
                    {
                        string GetOwnGame = reader.GetAttribute("own");
                        own = GetOwnGame;
                    }

                    if (ElementName == "image")
                    {
                        string GetImage = reader.ReadElementString();
                        pic = GetImage;
                    }

                    if (ElementName == "stats")
                    {
                        string GetMinPlayer = reader.GetAttribute("minplayers");
                        minplayer = GetMinPlayer;

                        string GetMaxPlayer = reader.GetAttribute("maxplayers");
                        maxplayer = GetMaxPlayer;


                    }


                    if (own == "1")
                    {
                            if (string.IsNullOrEmpty(minplayer) || string.IsNullOrEmpty(maxplayer))
                            {
                                continue;
                            }
                           
                        games.Add($"{game}ä2{pic}ä3{minplayer}ä4{maxplayer}ä5{username}ä6");
                      
                  
                    }

                }

            }

            }
            catch (Exception)
            {

                goto Again;
            }


        }
    }
}
