using Mine.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine.Services
{
 
    public class DatabaseService : IDataStore<ItemModel>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        /// <summary>
        /// Constructor for the DatabaseService
        /// </summary>
        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        /// <summary>
        /// Initialize
        /// </summary>
        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        /// <summary>
        /// Create a new record for the data passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ItemModel item)
        {
            var result = await Database.InsertAsync(item);
            return (result == 1);
        }

        /// <summary>
        /// Takes the ID and finds it in the data set
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Record if found else null</returns>
        public async Task<ItemModel> ReadAsync(string id)
        {
            return await Database.Table<ItemModel>().Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update the data with the information passed in
        /// </summary>
        /// <param name="item"></param>
        /// <returns>True for pass, else fail</returns>
        public async Task<bool> UpdateAsync(ItemModel item)
        {
            var data = await ReadAsync(item.Id);
            if (data == null)
            {
                return false;
            }

            var result = await Database.UpdateAsync(item);

            return (result == 1);
        }

        /// <summary>
        /// Deletes the Data passed in by
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True for pass, else fail</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var data = await ReadAsync(id);
            if (data == null)
            {
                return false;
            }

            var result = await Database.DeleteAsync(data);

            return (result == 1);
        }

        /// <summary>
        /// Get the full list of data
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns>List of records</returns>
        public async Task<List<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            return await Database.Table<ItemModel>().ToListAsync();
        }

        /// <summary>
        /// Wipe Data List
        /// </summary>
        /// <returns></returns>
        public void WipeDataList() 
        {
            Database.DropTableAsync<ItemModel>().GetAwaiter().GetResult();
            Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false).GetAwaiter().GetResult(); 
        }

    }
}
