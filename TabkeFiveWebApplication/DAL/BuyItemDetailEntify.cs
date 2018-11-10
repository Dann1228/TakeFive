using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TabkeFiveWebApplication.Models.Cart;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using TabkeFiveWebApplication.Models.ViewModels;

namespace TabkeFiveWebApplication.DAL
{
    public class BuyItemDetailEntify
    {


        public IEnumerable<OrderDetailViewModel> Query(int payId)
        {

            IEnumerable<OrderDetailViewModel> vModel = Enumerable.Empty<OrderDetailViewModel>();

            using (DataTable dt = new DataTable())
            {

                string sqlTxt = 
                    @"select  p.name,p.kind,p.category,b.pqty,b.pprice 
                      from buyitemdetailtbl b,producttbl p 
                      where payid=@payid and b.pid=p.pid";

                string connStr = DBConnClass.GetDBConn();
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sqlTxt, sqlConn) { CommandType = CommandType.Text })
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sqlConn.Open();
                    cmd.Parameters.Add("@payid", SqlDbType.Int).Value = payId;
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {

                    var query = from r in dt.AsEnumerable()
                                select new OrderDetailViewModel
                                {                                   
                                    PName = (r.Field<String>("name") ?? "").Trim(),
                                    PKind = Convert.ToInt32(r.Field<int>("kind")),
                                    PCategory = Convert.ToInt32(r.Field<int>("category")),
                                    BQty = Convert.ToInt32(r.Field<int>("pqty")),
                                    BPrice = Convert.ToInt32(r.Field<int>("pprice")),
                                    
                                };
                    vModel = query;

                }

                return vModel;

            }


        }




            //新增一筆 ProductViewModels
        public Boolean Insert(List<CartItem> Items,string Mid,int payId)
        {

            using (SqlConnection cn = new SqlConnection() { ConnectionString = DBConnClass.GetDBConn() })
            {
                using (SqlCommand cmd = new SqlCommand() { Connection = cn })
                {
                    /* 
                     * Construct an INSERT statement with a SELECT which provides the new primary 
                     * key on a successful insert. 
                     *  
                     * INSERT/SELECT was generated via part of a code sample I wrote 
                     * https://code.msdn.microsoft.com/Working-with-SQL-Server-986fff9e 
                     *  
                     * where you select a database from a ComboBox, select a table from a ListBox 
                     * then select columns from a CheckedListBox which in turn create a INSERT/SELECT 
                     * statement. 
                     */
                    cmd.CommandText = @"INSERT INTO buyitemdetailtbl 
                                        (pid,pqty,pprice,mid,btime,state,starttime,endtime,payid) 
                                        VALUES 
                                        (@pid,@pqty,@pprice,@mid,@btime,@state,@starttime,@endtime,@payid); ";
                                      

                    /* 
                     * Setup parameters (one only inserting one row we would use cmd.Parameters.AddWithValue 
                     * yet in a for-each we can't unless we clear the parameter collection and set the values 
                     * on each iteration while doing the parameters as shown below is better. 
                     *  
                     * Usually what I see on forums is a new to data operations developer will not only  
                     * re-create parameters in a for-each but also they will be opening/closing a connection, 
                     * creating a new command, re-open a connection, run the query, not check for errors 
                     * or get the new id which with not many records might be acceptable to them yet when dealing 
                     * with many rows or many rows and many columns this most likely will slow things down and 
                     * make it harder to maintain the code. 
                     */
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@pid", DbType = DbType.Int32 });                    
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@pqty", DbType = DbType.Int32 });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@pprice", DbType = DbType.Int32 });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@mid", DbType = DbType.String });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@btime", DbType = DbType.DateTime });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@state", DbType = DbType.Int32 });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@starttime", DbType = DbType.DateTime });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@endtime", DbType = DbType.DateTime });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@payid", DbType = DbType.Int32 });

                    try
                    {

                        cn.Open();

                        foreach (CartItem player in Items)
                        {
                            /* 
                             * Since we setup parameters once above we simply index to the proper 
                             * parameter and set it's value 
                             */
                            cmd.Parameters["@pid"].Value = player.Id;
                            cmd.Parameters["@mid"].Value = player.Id;
                            cmd.Parameters["@pqty"].Value = player.Quantity;
                            cmd.Parameters["@pprice"].Value = player.Price;
                            cmd.Parameters["@mid"].Value = Mid;
                            cmd.Parameters["@btime"].Value = DateTime.Now;
                            cmd.Parameters["@state"].Value = SqlInt32.Null;
                            cmd.Parameters["@starttime"].Value = DateTime.Now;
                            cmd.Parameters["@endtime"].Value = SqlDateTime.Null;
                            cmd.Parameters["@payid"].Value = payId;

                            /* 
                             * As we have an insert and select the ExecuteScalar returns 
                             * the result of the select part of our CommandText. We get 
                             * back the identity/primary key for the newly added record 
                             * from ExecuteScalar cast from object to int. 
                             */
                            cmd.ExecuteScalar();

                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());                        
                    }
                }
            }

            return true;



        }


    }

}