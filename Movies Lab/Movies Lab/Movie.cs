using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Movies_Lab
{
    public class Movie
    {
        public string title;
        public string category;


        public Movie(string title, string category)
        {
            this.title = title;
            this.category = category;


        }

        public string GetCategory(Movie movieObject)
        {
            return this.category;
        }
        
        
    }
}


