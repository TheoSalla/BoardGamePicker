using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razerBoard.Models;
using razerBoard.Models.Board;

namespace razerBoard
{
    public class BoardModel : PageModel
    {
        public void OnGet() // Inbyggd metod i C#
        {

        }


        [BindProperty]  // det här sätter man för att kunna spara saker i variabeln när man postar
        public static string showGame { get; set; }
        public static string showPic { get; set; }
        public string removePlayer { get; set; }
        public string myMin { get; set; }
        public string myMax { get; set; }
        public int index { get; set; }
        public static string showAccount { get; set; } = "a";

        public static List<string> showMinplayer { get; set; }
        public static List<string> showMaxplayer { get; set; }
        public static string showMin { get; set; }
        public static string showMax { get; set; }

        public Microsoft.Extensions.Primitives.StringValues myButton { get; set; }
        public Microsoft.Extensions.Primitives.StringValues myRem { get; set; }

        //public List<string> correctPic = new List<string>();
        //public List<int> minPly = new List<int>();
        //public List<int> maxPly = new List<int>();
        //public List<string> gamesWithCorrectValues = new List<string>();
        //public List<string> correctUser = new List<string>();


        public void OnPost()
        {
            myButton = Request.Form["clicked"];
            //var myButton = Request.Form["clicked"];
            myRem = Request.Form["rem"];

            myMin = Request.Form["min"];
            myMax = Request.Form["max"];
            string newMyMax = "1000";

            Console.WriteLine(myRem.GetType());
            if (Request.Form["friendAccount"] != "")
            {
                var getLength = "https://www.boardgamegeek.com/xmlapi/collection/" + Request.Form["friendAccount"];

                WebClient client = new WebClient();
                string downloadString = client.DownloadString(getLength);



                //Console.WriteLine(downloadString.Length);
                //Console.WriteLine(downloadString);
                if (AddedPlayers.players.Contains(Request.Form["friendAccount"]) | // Säkerställa att man inte lägger samma person fler gånger eller en tom sträng
                    (downloadString.Length == 187 |
                    downloadString.Length == 142 |
                    downloadString.Length == 189))
                {

                }
                else
                {
                    AddedPlayers.players.Add(Request.Form["friendAccount"]);
                }

            }



            if (myRem.Count() == 1)
            {
                if (AddedPlayers.players.Count != 0)
                {

                    AddedPlayers.players.RemoveAt(AddedPlayers.players.Count - 1);
                }
            }

            List<string> correctPic = new List<string>();
            List<int> minPly = new List<int>();
            List<int> maxPly = new List<int>();
            List<string> gamesWithCorrectValues = new List<string>();
            List<string> correctUser = new List<string>();


            if (myButton.Count() == 1)
            {

                foreach (var item in AddedPlayers.players)
                {
                    UserCreation.createUser(item);
                    UserCreation.getInfoToLists();

                }

                // <=
                for (int i = 0; i < UserCreation.minPlay.Count; i++)
                {
                    if (int.Parse(myMin) >= int.Parse(UserCreation.minPlay[i]))
                    {
                        minPly.Add(i);
                    }
                }

                // >=
                for (int i = 0; i < UserCreation.maxPlay.Count; i++)
                {
                    if (int.Parse(newMyMax) >= int.Parse(UserCreation.maxPlay[i]) && int.Parse(myMin) <= int.Parse(UserCreation.maxPlay[i]))
                    {
                        maxPly.Add(i);
                    }
                }

                showMinplayer = new List<string>();
                showMaxplayer = new List<string>();

                foreach (var item in maxPly)
                {
                    if (minPly.Contains(item))
                    {
                        showMaxplayer.Add(UserCreation.maxPlay[item]);
                        showMinplayer.Add(UserCreation.minPlay[item]);
                        gamesWithCorrectValues.Add(UserCreation.listOfGam[item]);
                        correctPic.Add(UserCreation.listOfPic[item]);
                        correctUser.Add(UserCreation.user[item]);

                    }
                }



                if (gamesWithCorrectValues.Count == 0)
                {
                    showGame = "Didn't find game";
                    UserCreation.listOfPic.Clear();
                    UserCreation.listOfGam.Clear();
                    UserCreation.minPlay.Clear();
                    UserCreation.maxPlay.Clear();
                    AddedPlayers.players.Clear();
                    UserCreation.user.Clear();
                    showMinplayer.Clear();

                }
                else
                {
                    Random rand = new Random();

                    index = rand.Next(gamesWithCorrectValues.Count);

                    string show = gamesWithCorrectValues[index];
                    showGame = gamesWithCorrectValues[index];
                    showPic = correctPic[index];
                    showAccount = correctUser[index];
                    //showMin = UserCreation.minplayer;
                    //showMax = UserCreation.maxplayer;
         
                    showMin = showMinplayer[index];
                    showMax = showMaxplayer[index];


                    //watch.Stop();
                    //object howMuchTime = watch.ElapsedMilliseconds / 1000;
                    //ViewData["timer"] = howMuchTime;

                    //UserCreation.listOfPic.Clear();
                    //UserCreation.listOfGam.Clear();
                    //UserCreation.minPlay.Clear();
                    //UserCreation.maxPlay.Clear();
                    //AddedPlayers.players.Clear();
                    //UserCreation.user.Clear();

                }
            }

        }
            

        //System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
    }
}