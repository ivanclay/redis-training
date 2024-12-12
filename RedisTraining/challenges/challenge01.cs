using RedisTraining.DbConections;
using StackExchange.Redis;

namespace RedisTraining.challenges;

public class challenge01
{
    private readonly RedisDatabaseConnection _redisDatabaseConnection;
    private readonly IDatabase _db;

    public challenge01(string connectionString = "localhost:6380")
    {
        _redisDatabaseConnection = new RedisDatabaseConnection(connectionString);
        _db = _redisDatabaseConnection.GetConection();
    }

    public void AddPost(int post_id, string title, string content, string[] tags)
    {
        // Definir valores em um Hash
        _db.HashSet($"post:{post_id}", new HashEntry[]
        {
            new HashEntry("title", title),
            new HashEntry("content", content),
        });

        AddPostIdToSet(post_id);
        AddTags(post_id, tags);
    }
    
    public void GetPost(string post_id)
    {
        // Recuperar todo o Hash
        var post = _db.HashGetAll($"post:{post_id}");
        Console.WriteLine($"Hash - Post: {string.Join(", ", post)}");
    }

    public List<Dictionary<string, string>> GetPosts()
    {
        // Obter todos os IDs dos posts
        var postIds = _db.SetMembers("post:ids");

        var posts = new List<Dictionary<string, string>>();

        foreach (var postId in postIds)
        {
            // Obter o hash do post
            var postHash = _db.HashGetAll($"post:{postId}");

            // Converter o hash para um dicionário
            var postDict = postHash.ToDictionary(
                entry => (string)entry.Name,
                entry => (string)entry.Value
            );

            postDict["post_id"] = postId.ToString(); // Adicionar o ID ao dicionário
            posts.Add(postDict);
        }

        return posts;
    }

    public void UpdatePost(int post_id, string? title=null, string? content=null, string[]? tags=null)
    {
        if (title != null) 
        {
            // Atualizar um campo específico
            _db.HashSet($"post:{post_id}", "title", title);
            
        }

        if (content != null)
        {
            // Atualizar um campo específico
            _db.HashSet($"post:{post_id}", "content", content);

        }

        if (tags != null)
        {
            RemoveTags(post_id);

            AddTags(post_id, tags);
        }
    }

    private void AddPostIdToSet(int post_id)
    {
        _db.SetAdd($"post:ids", post_id);
    }

    public void DeletePost(int post_id)
    {
        var removingFields = _db.HashKeys($"post:{post_id}");

        foreach (var field in removingFields)
        {
            _db.HashDelete($"post:{post_id}", field);
        }
    }

    private void AddTags(int post_id, string[] tags)
    {
        foreach (var tag in tags)
        {
            _db.SetAdd($"tags:{tag}", post_id);
        }
    }

    private void RemoveTags(int post_id)
    {
        var oldTags = _db.HashGetAll($"tags:{post_id}");

        foreach (var tag in oldTags)
        {
            _db.SetRemove($"tags:{tag}", post_id);
        }
    }
}
