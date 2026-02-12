using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Museu_do_Expedicionário
{
    public static class DatabaseHelper
    {
        private static string dbPath = GetDatabasePath();
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        private static bool _databaseInitialized = false;
        private static readonly object _lockObject = new object();

        private static string GetDatabasePath()
        {
            string dbName = ConfigurationManager.AppSettings["DatabaseName"] ?? "MuseuExpedicionario.db";
            string dbPathConfig = ConfigurationManager.AppSettings["DatabasePath"];

            if (string.IsNullOrWhiteSpace(dbPathConfig))
            {
                dbPathConfig = AppDomain.CurrentDomain.BaseDirectory;
            }

            // ✅ Criar pasta se não existir
            if (!Directory.Exists(dbPathConfig))
            {
                Directory.CreateDirectory(dbPathConfig);
                Console.WriteLine($"✅ Pasta criada: {dbPathConfig}");
            }

            string fullPath = Path.Combine(dbPathConfig, dbName);
            Console.WriteLine($"✅ Caminho do banco: {fullPath}");

            return fullPath;
        }

        public static void EnsureDatabaseExists()
        {
            if (_databaseInitialized) return;

            lock (_lockObject)
            {
                if (_databaseInitialized) return;

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine($"✅ Banco de dados localizado em: {dbPath}");

                    // ✅ CORREÇÃO: Remover UNIQUE de CPFResponsavel
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Usuarios (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nome TEXT NOT NULL,
                            Email TEXT UNIQUE NOT NULL,
                            Senha TEXT NOT NULL,
                            DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP
                        );
                        
                        CREATE TABLE IF NOT EXISTS Agendamentos (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Data TEXT NOT NULL,
                            Hora TEXT NOT NULL,
                            CPFResponsavel TEXT NOT NULL,
                            FoneResponsavel TEXT,
                            FoneInstituicao TEXT,
                            TipoVisitante TEXT,
                            UFGrupo TEXT,
                            CidadeGrupo TEXT,
                            TurmaFaixaEtaria TEXT,
                            GrauEscolaridade TEXT,
                            NumeroVisitantes TEXT,
                            FinalidadeVisita TEXT,
                            PaisGrupo TEXT,
                            DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP
                        );
                        
                        CREATE UNIQUE INDEX IF NOT EXISTS idx_agendamento_data_hora 
                            ON Agendamentos(Data, Hora);";

                    using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("✅ Tabelas verificadas/criadas com sucesso.");
                        }
                        catch (SQLiteException ex)
                        {
                            Console.WriteLine($"❌ Erro ao criar tabelas: {ex.Message}");
                            throw;
                        }
                    }
                }

                _databaseInitialized = true;
            }
        }

        public static int ExecuteNonQuery(string query, params SQLiteParameter[] parameters)
        {
            EnsureDatabaseExists();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"❌ Erro ao executar query: {ex.Message}");
                throw new Exception($"Erro ao acessar o banco de dados: {ex.Message}", ex);
            }
        }

        public static DataTable ExecuteQuery(string query, params SQLiteParameter[] parameters)
        {
            EnsureDatabaseExists();
            DataTable dt = new DataTable();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"❌ Erro ao executar query SELECT: {ex.Message}");
                throw new Exception($"Erro ao acessar o banco de dados: {ex.Message}", ex);
            }
            return dt;
        }
    }
}