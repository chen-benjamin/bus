using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace bus.Model
{
    public class DBManager
    {
        private SQLiteAsyncConnection _connection;
        public DBManager()
        {
            _connection = DependencyService.Get<ISOLiteDb>().GetConnection();
        }

        public async Task<ObservableCollection<Stop>> CreateTable()
        {
            await _connection.CreateTableAsync<Stop>();
            var stops = await _connection.Table<Stop>().ToListAsync();
            var _stops = new ObservableCollection<Stop>(stops);
            return _stops;
        }

        public void InsertOrUpdateStop(Stop stop)
        {
            var rowsAffected = _connection.UpdateAsync(stop);
            if (rowsAffected.Result == 0) {
                _connection.InsertAsync(stop);
            }
        }

        public async void DeleteStop(Stop stop)
        {
            await _connection.DeleteAsync(stop);
        }

        public bool CheckIfStopExist(Stop stop)
        {
            var query = _connection.FindAsync<Stop>(s => s.Id == stop.Id);
            return query.Result != null;
        }
    }
}
