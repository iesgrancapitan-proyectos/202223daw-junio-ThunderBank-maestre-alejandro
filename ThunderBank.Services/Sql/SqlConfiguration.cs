﻿namespace ThunderBank.Services.Sql
{
    public class SqlConfiguration
    {
        public SqlConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; set; }
    }
}
