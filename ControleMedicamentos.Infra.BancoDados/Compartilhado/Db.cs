﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Compartilhado
{
    public static class Db
    {
        private const string enderecoBanco =
            "Data Source=(LocalDb)\\MSSQLLocalDB;" +
            "Initial Catalog=ControleMedimentoDb;" +
            "Integrated Security=True;" +
            "Pooling=False";

        public static void ExecutarSql(string sql)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comando = new SqlCommand(sql, conexaoComBanco);

            conexaoComBanco.Open();
            comando.ExecuteNonQuery();
            conexaoComBanco.Close();
        }
    }
}
