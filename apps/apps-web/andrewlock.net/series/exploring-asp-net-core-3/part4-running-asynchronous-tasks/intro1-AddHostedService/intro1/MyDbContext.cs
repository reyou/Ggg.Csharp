namespace intro1
{
    public class MyDbContext
    {
        public MyDatabase Database { get; set; }

        public MyDbContext()
        {
            Database = new MyDatabase();
        }
    }
}