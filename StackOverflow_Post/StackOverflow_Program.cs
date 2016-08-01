using System;
using System.Collections.Generic;
using static System.String;

namespace StackOverflow
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Initialize Main() variables
            var posts = new List<Post>();
            var input = "";

            //Initialize StackOverflow
            const string header = "===================  StackOverflow  ======================";
            while (IsNullOrWhiteSpace(input) || !string.Equals(input, "y"))
            {
                Console.Clear();
                Console.WriteLine(header);
                Console.WriteLine("\nWelcome to StackOverflow!\n\n");
                Console.WriteLine("There are currently no posts. Be the first? [Y]es or [N]o.");
                input = Console.ReadLine();
                if (string.Equals(input, "n"))
                    break;
            }
            //Creates the first (current) post
            var currentPost = new Post();
            var charInput = new[] { Convert.ToChar(input[0]) };
            var i = 0;
            var selecting = false;

            while (charInput[0] != 'x')
            {
                Console.Clear();
                Console.WriteLine(header);
                Console.WriteLine($"                  {DateTime.Now}                \n\n");
                if (posts.Count == 0)
                {
                    Console.WriteLine("There are currently no posts. Be the first? [C]reate one!");
                }

                foreach (var post in posts)
                {
                    if (post.Equals(currentPost))
                    {
                        post.Totals(post, header,true);
                    }
                    else
                    {
                        post.Totals(post, header);
                    }
                    Console.WriteLine("\n");

                }
                Console.WriteLine("\n");
                if (posts.Count == 0)
                {
                    Console.WriteLine("There are currently no posts.");
                }
                else
                {
                    Console.WriteLine($"There are currently [{posts.Count}] post(s).");
                }
                Console.WriteLine("\nPress 'L' to like, 'D' to dislike, 'S' to select a post or 'C' to create a new post. 'X' exits.");
                input = Console.ReadLine();
                input += " ";
                charInput[0] = input.ToLower()[0];
                if (selecting)
                {
                    i++;
                    selecting = false;
                }
                if (i == posts.Count)
                {
                    i = 0;
                }
                if (true)
                    switch (charInput[0])
                    {
                        case 'l':
                        {
                            if (posts.Count != 0)
                                currentPost.Upvote(currentPost, header);
                            break;
                        }
                        case 'd':
                        {
                            currentPost.Downvote(currentPost, header);
                            break;
                        }
                        case 'c':
                        {
                            new Post().Create(posts, header);
                                currentPost = posts[i];
                                break;
                        }
                        case 's':
                        {
                            if (posts.Count == 0)
                            {
                                break;
                            }
                            selecting = true;
                            if (i < posts.Count-1)
                            {
                                currentPost = posts[i+1];
                                break;
                            }
                            currentPost = posts[0];
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }
            }

            Console.Clear();
            Console.WriteLine(header);
            Console.WriteLine("\nThanks for visiting StackOverflow!\n\n\n");
        }


        // ==================================  POST CLASS  =====================================


        public class Post
        {
            private List<int> UpVotes { get; } = new List<int>();
            private List<int> DownVotes { get; } = new List<int>();
            public string Title { get; }
            public string Descrip { get; }
            private readonly DateTime _datetime;

            public Post()
            {

            }

            public Post(string title, string descrip)
            {
                if (title.Length > 1)
                {
                    var formatTitle = title.ToUpper().Remove(1) + title.ToLower().Substring(1);
                    Title = formatTitle;
                }
                else
                {
                    var formatTitle = title.ToUpper();
                    Title = formatTitle;
                }
                if (descrip.Length > 1)
                {
                    var formatDescrip = descrip.ToUpper().Remove(1) + descrip.ToLower().Substring(1);
                    Descrip = formatDescrip;
                }
                else
                {
                    var formatDescrip = descrip.ToUpper();
                    Descrip = formatDescrip;
                }
                _datetime = DateTime.Now;
            }



            public void Create(List<Post> post, string header)
            {
                Console.Clear();

                Console.WriteLine(header);
                Console.WriteLine("\nPlease enter a title: ");
                var title = Console.ReadLine();
                while (IsNullOrWhiteSpace(title))
                {
                    Console.Clear();
                    Console.WriteLine(header);
                    Console.WriteLine("\nPlease enter a title: ");
                    title = Console.ReadLine();
                }

                Console.WriteLine("\nand a brief description... ");
                var descrip = Console.ReadLine();
                    while (IsNullOrWhiteSpace(descrip))
                {
                    Console.Clear();
                    Console.WriteLine(header);
                    Console.WriteLine("\nand a brief description... ");
                    descrip = Console.ReadLine();
                }    

                post.Add(new Post(title, descrip));
                Console.Clear();
                Console.WriteLine(header);
                Console.WriteLine($"{post[post.Count-1].Title} created at {DateTime.Now}!\n");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            public void Upvote(Post post, string header)
            {
                post.UpVotes.Add(0);
                var currentTime = DateTime.Now;
                var waitTime = DateTime.Now.AddSeconds(1);
                Console.Clear();
                Console.WriteLine(header);
                Console.WriteLine($"\n{post.Title} Liked!");
                while (currentTime < waitTime)
                {
                    currentTime = DateTime.Now;
                }

            }

            public void Downvote(Post post, string header)
            {
                post.DownVotes.Add(0);
                Console.WriteLine($"{post.Title} Disliked!");
            }

            public void Totals(Post post, string header)
            {
                Totals(post, header, false);
            }

            public void Totals(Post post, string header, bool isCurrent)
            {
                if (isCurrent)
                {
                    Console.WriteLine($"  ->   {post.Title}, created at {_datetime} has {post.UpVotes.Count} likes and {post.DownVotes.Count} dislikes.");
                    Console.WriteLine($"      \"{post.Descrip}...  \"");

                }
                else
                {
                    Console.WriteLine($"  {post.Title}, created at {_datetime} has {post.UpVotes.Count} likes and {post.DownVotes.Count} dislikes.");
                    Console.WriteLine($"  \"{post.Descrip}...  \"");
                }

            }
        }

        
    }
}
