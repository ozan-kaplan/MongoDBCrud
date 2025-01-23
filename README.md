# MongoDB CRUD Console Application

This project demonstrates a simple **CRUD (Create, Read, Update, Delete)** application using **MongoDB** with a console-based interface. The application connects to a MongoDB database and performs operations on a `People` collection.

---

## Prerequisites

1. **.NET 8 SDK** installed.
2. **MongoDB** instance running locally or remotely.
   - To start MongoDB using Docker:
     ```bash
     docker run -d --name mongodb -p 27017:27017 mongo
     ```
3. **NuGet Packages**: Ensure the following packages are installed:
   ```bash
   dotnet add package MongoDB.Driver
   ```

---

## Running the Application

### Step 1: Configure MongoDB Connection

The connection string is set to `mongodb://localhost:27017` in the application. If your MongoDB instance runs on a different host or port, modify the connection string in the `Program.cs` file:
```csharp
var client = new MongoClient("mongodb://<host>:<port>");
```

### Step 2: Run the Application

1. Navigate to the project directory.
2. Run the application:
   ```bash
   dotnet run
   ```
3. Follow the on-screen instructions to perform CRUD operations.

---

## Features

### 1. Create a New Person
- Prompts the user to enter a name and age.
- Inserts the person into the MongoDB collection.

### 2. Read All People
- Fetches and displays all records from the `People` collection in the database.

### 3. Update an Existing Person
- Prompts the user to enter the ID of a person to update.
- Allows updating the name and age of the person.

### 4. Delete a Person
- Prompts the user to enter the ID of a person to delete.
- Deletes the person with the specified ID from the database.

---

## Example Output

### Main Menu:
```
MongoDB CRUD Example

Choose an operation:
1. Create a new person
2. Read all people
3. Update a person
4. Delete a person
5. Exit
Enter your choice:
```

### Create a Person:
```
Enter name: Ozan Kaplan
Enter age: 30
Person created successfully!
```

### Read All People:
```
People in the database:
ID: 63b2f4e9c7d2ab1c4e7a2d89, Name: Ozan Kaplan, Age: 30
ID: 63b2f4e9c7d2ab1c4e7a2d90, Name: Test Person, Age: 25
```

### Update a Person:
```
Enter the ID of the person to update: 63b2f4e9c7d2ab1c4e7a2d89
Enter new name: Ozan Updated
Enter new age: 35
Person updated successfully!
```

### Delete a Person:
```
Enter the ID of the person to delete: 63b2f4e9c7d2ab1c4e7a2d90
Person deleted successfully!
```

---

## Project Structure

- **Program.cs**: Main entry point for the application.
- **Person.cs**: Defines the `Person` class, which maps to the MongoDB `People` collection.

### Person Class
```csharp
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
```

---

## Configuration

### MongoDB Settings
Default MongoDB connection is configured as:
- Host: `localhost`
- Port: `27017`

To modify these settings, update the connection string in the `Program.cs` file.

---

## License
This project is licensed under the MIT License. Feel free to use, modify, and distribute as needed.

