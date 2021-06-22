using System;
using SQLite;

namespace bus
{
    public interface ISOLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
