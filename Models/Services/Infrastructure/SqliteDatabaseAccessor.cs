//COME ESTRARRE

using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        //senza asynch e solo DataSet Task<tipo restituito> o solo task se siamo im una void
        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            //var conn = new SqliteConnection("Data Source=Data/MyCourse.db");
            //conn.Open();
            //var cmd = new SqliteCommand(query, conn);
            ////cmd.ExecuteNonQuery  (per query che non restituiscono risultati)
            ////cmd.ExecuteScalar()  (per query con una sola riga e una sola colonna tipo count)            
            //var reader=cmd.ExecuteReader();
            ////reader.Read() (restituisce un true se presente il dato
            //while (reader.Read())
            //{
            //    string title = (string) reader["Title"];
            //}
            //conn.Dispose();
            ////throw new NotImplementedException();


            //---snippet non molto intuitivo
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for (var i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();

            using (var conn = new SqliteConnection("Data Source=Data/MyCourse.db"))
            {
                await conn.OpenAsync();
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqliteParameters);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var dataSet = new DataSet(); //collection di datatable
                        dataSet.EnforceConstraints = false; //patch per non far andarlo nel bug

                        //var dataTable = new DataTable();
                        //dataSet.Tables.Add(dataTable);
                        //dataTable.Load(reader); //recupera e contiene tutte le righe
                        //return dataSet;
                        ////while (reader.Read())
                        ////{
                        ////    string title = (string)reader["Title"];
                        ////}

                        //carica più tabelle
                        do
                        {
                            var dataTable = new DataTable();
                            dataSet.Tables.Add(dataTable);
                            dataTable.Load(reader); //recupera e contiene tutte le righe
                        } while (!reader.IsClosed);

                        return dataSet;
                    }
                }
                //throw new NotImplementedException();
            }
        }
    }
}
