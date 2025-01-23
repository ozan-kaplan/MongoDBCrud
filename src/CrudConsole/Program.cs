using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("TestDatabase");
var collection = database.GetCollection<Person>("People");

Console.WriteLine("MongoDB CRUD Example\n");

while (true)
{
    Console.WriteLine("Choose an operation:");
    Console.WriteLine("1. Create a new person");
    Console.WriteLine("2. Read all people");
    Console.WriteLine("3. Update a person");
    Console.WriteLine("4. Delete a person");
    Console.WriteLine("5. Exit");
    Console.Write("Enter your choice: ");

    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            await CreatePersonAsync(collection);
            break;
        case "2":
            await ReadPeopleAsync(collection);
            break;
        case "3":
            await UpdatePersonAsync(collection);
            break;
        case "4":
            await DeletePersonAsync(collection);
            break;
        case "5":
            return;
        default:
            Console.WriteLine("Invalid choice. Please try again.\n");
            break;
    }
}

async Task CreatePersonAsync(IMongoCollection<Person> collection)
{
    Console.Write("Enter name: ");
    var name = Console.ReadLine();

    Console.Write("Enter age: ");
    if (int.TryParse(Console.ReadLine(), out var age))
    {
        var person = new Person { Name = name, Age = age };
        await collection.InsertOneAsync(person);
        Console.WriteLine("Person created successfully!\n");
    }
    else
    {
        Console.WriteLine("Invalid age input.\n");
    }
}

async Task ReadPeopleAsync(IMongoCollection<Person> collection)
{
    var people = await collection.Find(new BsonDocument()).ToListAsync();

    if (people.Count > 0)
    {
        Console.WriteLine("People in the database:");
        foreach (var person in people)
        {
            Console.WriteLine($"ID: {person.Id}, Name: {person.Name}, Age: {person.Age}");
        }
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("No people found in the database.\n");
    }
}

async Task UpdatePersonAsync(IMongoCollection<Person> collection)
{
    Console.Write("Enter the ID of the person to update: ");
    var id = Console.ReadLine();

    Console.Write("Enter new name: ");
    var name = Console.ReadLine();

    Console.Write("Enter new age: ");
    if (int.TryParse(Console.ReadLine(), out var age))
    {
        var filter = Builders<Person>.Filter.Eq(p => p.Id, id);
        var update = Builders<Person>.Update.Set(p => p.Name, name).Set(p => p.Age, age);
        var result = await collection.UpdateOneAsync(filter, update);

        if (result.ModifiedCount > 0)
        {
            Console.WriteLine("Person updated successfully!\n");
        }
        else
        {
            Console.WriteLine("No person found with the given ID.\n");
        }
    }
    else
    {
        Console.WriteLine("Invalid age input.\n");
    }
}

async Task DeletePersonAsync(IMongoCollection<Person> collection)
{
    Console.Write("Enter the ID of the person to delete: ");
    var id = Console.ReadLine();

    var filter = Builders<Person>.Filter.Eq(p => p.Id, id);
    var result = await collection.DeleteOneAsync(filter);

    if (result.DeletedCount > 0)
    {
        Console.WriteLine("Person deleted successfully!\n");
    }
    else
    {
        Console.WriteLine("No person found with the given ID.\n");
    }
}

    public class Person
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("age")]
    public int Age { get; set; }
}