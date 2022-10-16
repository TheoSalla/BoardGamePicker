using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardGamePickerWeb
{
    public class testModel : PageModel
    {
        public void OnGet()
        {
            //name.Add("Jen");
            //name.Add("Ben");
            //name.Add("Ken");

        }


        public void OnPostFirst()
        {
            Console.WriteLine("duck");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void OnPostRemove()
        {
            Console.WriteLine("duck");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Green;


        }


        [BindProperty]
        public static List<string> name { get; set; } = new List<string>()
        {
            "Jen",
            "Ben",
            "Ken"
        };

        public void OnPost()
        {
            
        }


    }
}