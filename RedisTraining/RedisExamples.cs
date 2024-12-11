using RedisTraining.DbConections;
using StackExchange.Redis;
using System.Text;

namespace RedisTraining;

public class RedisExamples
{
    private readonly IDatabase _db;
    private readonly RedisDatabaseConnection _redisDatabaseConnection;

    public RedisExamples(string connectionString = "localhost:6380")
    {
        _redisDatabaseConnection = new RedisDatabaseConnection(connectionString);
        _db = _redisDatabaseConnection.GetConection();
    }

    public void HashExample()
    {
        // Definir valores em um Hash
        _db.HashSet("user:1000", new HashEntry[]
        {
            new HashEntry("name", "Alice"),
            new HashEntry("age", 30),
            new HashEntry("email", "alice@exemplo.com.br")
        });

        // Recuperar todo o Hash
        var user = _db.HashGetAll("user:1000");
        Console.WriteLine($"Hash - User: {string.Join(", ", user)}");

        // Atualizar um campo específico
        _db.HashSet("user:1000", "age", 31);

        // Recuperar um campo específico
        var age = _db.HashGet("user:1000", "age");
        Console.WriteLine($"Updated Age: {age}");
    }

    public void ListExample()
    {
        // Adicionar itens na lista
        _db.ListRightPush("tasks", "task1");
        _db.ListRightPush("tasks", "task2");
        _db.ListRightPush("tasks", "task3");

        // Recuperar todos os itens da lista
        var tasks = _db.ListRange("tasks");
        Console.WriteLine($"Exemplo Lista - Tasks: [{string.Join(", ", tasks)}]");

        // Remover e retornar o primeiro item
        var task = _db.ListLeftPop("tasks");
        Console.WriteLine($"Popped Task: {task}");

        // Tamanho da lista
        var size = _db.ListLength("tasks");
        Console.WriteLine($"List Size: {size}");
    }

    public void SetExample()
    {
        // Adicionar itens em um Set
        _db.SetAdd("tags", "python");
        _db.SetAdd("tags", "redis");
        _db.SetAdd("tags", "database");

        // Recuperar todos os membros do Set
        var tags = _db.SetMembers("tags");
        Console.WriteLine($"Set Example - Tags: [{string.Join(", ", tags)}]");

        // Verificar se um item existe no Set
        var isMember = _db.SetContains("tags", "python");
        Console.WriteLine($"Is 'python' a member of tags? {isMember}");

        // Remover um item do Set
        _db.SetRemove("tags", "database");
    }

    public void SortedSetExample()
    {
        // Adicionar itens em um Sorted Set
        _db.SortedSetAdd("leaderboard", new SortedSetEntry[]
        {
            new SortedSetEntry("Alice", 100),
            new SortedSetEntry("Bob", 200),
            new SortedSetEntry("Charlie", 150)
        });

        // Recuperar todo o Sorted Set
        var leaderboard = _db.SortedSetRangeByRankWithScores("leaderboard");
        Console.WriteLine($"Sorted Set Example - Leaderboard: {FormatSortedSet(leaderboard)}");

        // Incrementar o score de um membro
        _db.SortedSetIncrement("leaderboard", "Alice", 50);

        // Recuperar o score de um membro
        var score = _db.SortedSetScore("leaderboard", "Alice");
        Console.WriteLine($"Alice's Updated Score: {score}");

        // Recuperar membros dentro de um range de score
        var topPlayers = _db.SortedSetRangeByScoreWithScores("leaderboard", 100, 200);
        Console.WriteLine($"Top Players: {FormatSortedSet(topPlayers)}");
    }

    private string FormatSortedSet(SortedSetEntry[] entries)
    {
        var sb = new StringBuilder("[");
        foreach (var entry in entries)
        {
            sb.Append($"({entry.Element}, {entry.Score}) ");
        }
        sb.Append("]");
        return sb.ToString();
    }
}

