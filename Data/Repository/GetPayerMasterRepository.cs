using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;
using Domain.Interfaces; 
using Dapper;
using System.Data.SqlClient;

namespace Data.Repository
{
    public class GetPayerMasterRepository : IPayerMasterRepository
    {
        private string _connectionString;
        public GetPayerMasterRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<PayerList> GetPayerList()
        {

            List<PayerList> payerList = new List<Domain.Model.PayerList>();
            string result = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT PayerId ,PayerId + ' - ' + PayerName as PayerName  \n");
            sql.Append("FROM   Avutox.dbo.PAYERS \n");
            sql.Append("/**where**/ \n");
            SqlBuilder sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate(sql.ToString()); 
            sqlBuilder.OrderBy("ID ");
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = template.RawSql;
                sqlConnection.Open();

                payerList = sqlConnection.Query<PayerList>(template.RawSql, template.Parameters).ToList();

            }
            return payerList;
        }
    }
}
