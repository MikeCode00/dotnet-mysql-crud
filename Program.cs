using Dapper;
using MySql.Data.MySqlClient;
using System.Data;


WebApplication app = WebApplication.Create();

IDbConnection ConnectDB(){
  return new MySqlConnection("Server=localhost;Uid=root;Password=password;Database=api");
}

app.MapGet("/", ()=> "Hello World");
app.MapPost("/people", async(Person person)=>{
  var cnn = ConnectDB();
  try
  {
    cnn.Open();
    await cnn.QueryAsync("INSERT INTO person (name) VAlUES('" + person.Name + "');");
    cnn.Close();
    return "Person Added";
  }
  catch (System.Exception e)
  {
    Console.WriteLine(e.Message);
    throw;
  }
});
app.MapGet("/people", async() => {
  var cnn = ConnectDB();
  try
  {
    cnn.Open();
    var people = await cnn.QueryAsync("SELECT * FROM person;");
    cnn.Close();
    return people.ToList();
  }
  catch (System.Exception e)
  {
    Console.WriteLine(e.Message);
    throw;
  }
});
app.MapGet("/person/{id}", async(int id) => {
  var cnn = ConnectDB();
  try
  {
    cnn.Open();
    var person = await cnn.QueryAsync("SELECT * FROM person WHERE id='" + id + "';");
    cnn.Close();
    return person;
  }
  catch (System.Exception e)
  {
    Console.WriteLine(e.Message);
    throw;
  }
});
app.MapDelete("/person/{id}", async(int id) => {
  var cnn = ConnectDB();
  try
  {
    cnn.Open();
    await cnn.QueryAsync("DELETE FROM person WHERE id ='" + id + "';");
    cnn.Close();
    return "Person Deleted!";
  }
  catch (System.Exception e)
  {
    Console.WriteLine(e.Message);
    throw;
  }
});
app.MapPut("/person/{id}", async(int id, Person person) => {
  var cnn = ConnectDB();
  try
  {
    cnn.Open();
    await cnn.QueryAsync("UPDATE person SET name='" + person.Name + "' WHERE id='" + id + "';");
    cnn.Close();
    return "Person Updated!";
  }
  catch (System.Exception e)
  {
    Console.WriteLine(e.Message);
    throw;
  }
});
app.Run();

class Person {
  public int Id {get; set;}
  public string? Name {get;set;}
}
