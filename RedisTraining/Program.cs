using RedisTraining;
using RedisTraining.challenges;

Console.WriteLine("Using Redis with DotNet 8");

var challenge = new challenge01();
challenge.AddPost(1, "Primeiro Post", "Conteúdo do primeiro post", ["python", "redis"]);
challenge.AddPost(2, "Segundo Post", "Conteúdo do segundo post", ["python", "flask"]);

Console.WriteLine("=============================");
Console.WriteLine("TODOS OS POSTS");
var posts = challenge.GetPosts();
foreach (var post in posts)
{
    Console.WriteLine($"{post["title"].ToString()}: {post["content"].ToString()}");
}
Console.WriteLine("=============================");

//var redisExamples = new RedisExamples();

//Console.WriteLine("Hash Example:");
//redisExamples.HashExample();

//Console.WriteLine("\nList Example:");
//redisExamples.ListExample();

//Console.WriteLine("\nSet Example:");
//redisExamples.SetExample();

//Console.WriteLine("\nSorted Set Example:");
//redisExamples.SortedSetExample();