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
        ///Will take a List of movies and find each unique category and will return a List<string> with each unique category
        public List<string> GetListCategories(List<Movie> listOfMovies)
        {
            List<string> listOfCategories = new List<string>();
            foreach (Movie movie in listOfMovies)
            {
                if (listOfCategories.Contains(movie.category) == false)
                {
                    listOfCategories.Add(movie.category);
                }   
            }
            
            if (listOfCategories.Count == 0)
            {
                Console.WriteLine("Movies list is not populated or not found. No categories could be listed.");
                List<string> emptyString = new List<string>() {""};
                return emptyString;
            }
            return listOfCategories;
        }
    }
}


