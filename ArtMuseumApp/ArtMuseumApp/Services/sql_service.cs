using System.Threading.Tasks;
using System.Collections.Generic;


using SQLite;
using MovieApp.Models;

namespace MovieApp.Services
{
    public class sql_service
    {
        private readonly SQLiteAsyncConnection dbconnection;
        private async Task CreateTables()
        {
            await dbconnection.CreateTableAsync<Result>();
        }

        public async Task<List<T>> GetItems<T>() where T : new()
        {
            return await dbconnection.Table<T>().ToListAsync();
        }

        public async Task<bool> AddItem<T>(T item) where T : new()
        {
            int rows = await dbconnection.InsertAsync(item);
            return (rows == 1);
        }




        public async Task<bool> DeleteItem<T>(T item) where T : new()
        {
            int rows = await dbconnection.DeleteAsync(item);
            return (rows == 1);
        }


        public sql_service(string path)
        {
            dbconnection = new SQLiteAsyncConnection(path);
            Task.Run(() => CreateTables()).Wait();
        }
    }

}
