using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TritonGymProject.Utilities;

namespace Farmacia1.DbHelpers
{
    public class SqlService
    {
        private string connectionString;

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public SqlService()
        {
            connectionString = AESHelper.Desencriptar(Utils.ConsultaParametro("CS"), Constants.key, Constants.iv);
        }

        public DataSet ExecuteSP(string sp, List<string> paramList)
        {
            var objSQL = new InterfazSQL.IntSQL(connectionString);
            return objSQL.StoreProcedure(sp, paramList);
        }
    }
}
