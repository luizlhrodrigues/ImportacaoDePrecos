using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;
using ImportacaoDePrecos.Models;
using System.Linq;

namespace ImportacaoDePrecos.Data
{
    public class DataBase
    {
        public DataBase() { }

        private static SQLiteConnection sqliteConnection;

        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=ImportacaoDeAcoes.sqlite3");
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public void CreateDataBase()
        {
            try
            {
                if (!File.Exists("ImportacaoDeAcoes.sqlite3"))
                {
                    using (var cmd = DbConnection().CreateCommand())
                    {
                        //SQLiteConnection.CreateFile("./ImportacaoDeAcoes.sqlite3");

                        string query = $@"CREATE TABLE tbAcao (IdAcao     INTEGER PRIMARY KEY AUTOINCREMENT, 
                                                               Nome       VARCHAR(30) NULL,
                                                               Habilitado INTEGER);
                                  
                                          CREATE TABLE tbFechamento(IdFechamento INTEGER PRIMARY KEY AUTOINCREMENT,
                                                                    IdAcao       INTEGER,
                                                                    Data         DATETIME,
                                                                    Valor        REAL,
                                                                    Ativo        INT,
                                                                    FOREIGN KEY (IdAcao) REFERENCES tbAcao(IdAcao));
                                          
                                          INSERT INTO tbAcao (Nome) VALUES( 'B3SA3.SAO');
                                          INSERT INTO tbAcao (Nome) VALUES( 'PETR4.SAO');";
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ConsultaFechamentoAcao(string nomeAcao, DateTime data)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $@"SELECT a.IdAcao, b.Valor 
                                           FROM tbAcao a 
                                          INNER JOIN tbFechamento b ON b.IdAcao = a.IdAcao 
                                          WHERE a.Nome = '{nomeAcao}' 
                                            AND b.Data = '{data.Date.ToString("yyyy-MM-dd")}'";

                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAcao(string nomeAcao)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $@"SELECT a.IdAcao
                                           FROM tbAcao a 
                                          WHERE a.Nome = '{nomeAcao}'";

                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddFechamentoAcao(Acao acao)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO tbFechamento(IdAcao, Data, Valor, Ativo) values (@IdAcao, @Data, @Valor, @Ativo)";
                    cmd.Parameters.AddWithValue("@IdAcao", acao.IdAcao);
                    cmd.Parameters.AddWithValue("@Data", acao.Fechamentos.FirstOrDefault().Data.Date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Valor", acao.Fechamentos.FirstOrDefault().Valor);
                    cmd.Parameters.AddWithValue("@Ativo", 1);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateFechamto(Acao acao)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (acao.IdAcao != null)
                    {
                        cmd.CommandText = "UPDATE tbFechamento SET Valor=@Valor WHERE IdAcao=@IdAcao AND Data = @Data";
                        cmd.Parameters.AddWithValue("@IdAcao", acao.IdAcao);
                        cmd.Parameters.AddWithValue("@Data", acao.Fechamentos.FirstOrDefault().Data);
                        cmd.Parameters.AddWithValue("@Valor", acao.Fechamentos.FirstOrDefault().Valor);
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
