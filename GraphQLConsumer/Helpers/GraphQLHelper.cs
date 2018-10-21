using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GraphQLConsumer
{
    public static class GraphQLHelper
    {
        public static GraphQLQuery GetGraphQLQuery(string QueryName)
        {
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@QueryName", QueryName);
            DataSet dsGraphQLQuery = SQLHelper.ExecuteDataset("dbo.GetGraphQLQuery", Parameters);
            GraphQLQuery GraphQLQuery = new GraphQLQuery();
            if (dsGraphQLQuery.Tables[0].Rows.Count > 0)
            {
                DataRow drGraphQLQuery = dsGraphQLQuery.Tables[0].Rows[0];
                GraphQLQuery.ID = drGraphQLQuery["ID"] != null ? Int32.Parse(drGraphQLQuery["ID"].ToString()) : 0;
                GraphQLQuery.Name = drGraphQLQuery["Name"] != null ? drGraphQLQuery["Name"].ToString() : null;
                GraphQLQuery.Body = drGraphQLQuery["Body"] != null ? drGraphQLQuery["Body"].ToString() : null;
                GraphQLQuery.Description = drGraphQLQuery["Description"] != null ? drGraphQLQuery["Description"].ToString() : null;
                GraphQLQuery.Notes = drGraphQLQuery["Notes"] != null ? drGraphQLQuery["Notes"].ToString() : null;
            }
            return GraphQLQuery;
        }
        
    }
}