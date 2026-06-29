namespace ToDoSommarProjekt.Data
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }
}
