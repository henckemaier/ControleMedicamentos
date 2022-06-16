using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado
{
    public class BaseTest
    {
        public BaseTest()
        {

            string sql =
                @"DELETE FROM TBREQUISICAO;
                  DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)

                DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)

                DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)

                DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)

                 DELETE FROM TBFORNECEDOR;
                  DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)";
            Db.ExecutarSql(sql);
        }
    }
}
