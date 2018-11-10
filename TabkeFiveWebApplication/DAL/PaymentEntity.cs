using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TabkeFiveWebApplication.Models.ViewModels;
using System.Data.SqlClient;
using System.Data;

namespace TabkeFiveWebApplication.DAL
{
    public class PaymentEntity
    {

        //傳入ViewModel，修改資料
        public Boolean CancelOrder(string pId,string cancelReason)
        {

            string connStr = DBConnClass.GetDBConn();
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();

            try
            {

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText =
                @"update paymenttbl set 
                  status=@status,
                  cancelreason=@cancelreason,
                  canceldate=@canceldate 
                  where payid=@payid";

                cmd.Connection = sqlConnection;
                cmd.Parameters.Add("@payid", SqlDbType.Int).Value = pId;
                cmd.Parameters.Add("@status", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@cancelreason", SqlDbType.NChar).Value = cancelReason;
                cmd.Parameters.Add("@canceldate", SqlDbType.DateTime).Value = DateTime.Now;

                sqlConnection.Open();
                int effectrows;
                effectrows = cmd.ExecuteNonQuery();

                if (effectrows <= 0)
                {
                    return false;
                }
                else
                {
                    return true;

                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }



        }


            public IEnumerable<OrderViewModel>  Query(string userId)
        {

            IEnumerable<OrderViewModel> vModel = Enumerable.Empty<OrderViewModel>();

            using (DataTable dt = new DataTable())
            {

                string sqlTxt = @"select 
                                  *
                                  from paymenttbl where mid=@mid";

                string connStr = DBConnClass.GetDBConn();
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sqlTxt, sqlConn) { CommandType = CommandType.Text })
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sqlConn.Open();
                    cmd.Parameters.Add("@mid", SqlDbType.NVarChar).Value = userId;
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {

                    var query = from r in dt.AsEnumerable()
                                select new OrderViewModel
                                {
                                    PayId = Convert.ToInt32(r.Field<int>("payid")),
                                    MName = (r.Field<String>("name") ?? "").Trim(),
                                    MPhone = (r.Field<String>("phone") ?? "").Trim(),
                                    MAddr = (r.Field<String>("addr") ?? "").Trim(),
                                    TotalPrice = Convert.ToInt32(r.Field<int>("pay")),
                                    BuyDate = Convert.ToDateTime(r.Field<DateTime>("paydate")),
                                    Memo = (r.Field<String>("memo") ?? "").Trim(),
                                    //Status = Convert.ToInt32(r.Field<int>("status")),
                                };
                    vModel = query;

                }

                return vModel;

            }



        }


        //新增一筆 ProductViewModels
        public int Insert(OrderViewModel vModel,
                          string userId,
                          Decimal totalPay,
                          int payKind)
        {


            string connStr = DBConnClass.GetDBConn();
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();
            int insertedPid=-1;

            try
            {

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText =
                @"insert paymenttbl
                 (mid,name,phone,addr,pay,paydate,paykind,
                  creditkind,creaditno,vaildmonth,vaildyear,vaildcode,
                  memo) 
                 output inserted.payid 
                 VALUES 
                 (@mid,@name,@phone,@addr,@pay,@paydate,@paykind,
                  @creditkind,@creaditno,@vaildmonth,@vaildyear,@vaildcode,
                  @memo)";

                cmd.Connection = sqlConnection;
                cmd.Parameters.Add("@mid", SqlDbType.NVarChar).Value= userId;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = vModel.MName;
                cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = vModel.MPhone;
                cmd.Parameters.Add("@addr", SqlDbType.NVarChar).Value = vModel.MAddr;
                cmd.Parameters.Add("@pay", SqlDbType.Int).Value = totalPay;
                cmd.Parameters.Add("@paydate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@paykind", SqlDbType.Int).Value = vModel.CreditKind;
                cmd.Parameters.Add("@creditkind", SqlDbType.Int).Value = payKind;
                cmd.Parameters.Add("@creaditno", SqlDbType.NVarChar).Value = 
                    vModel.CreditNumber1 + vModel.CreditNumber2 + vModel.CreditNumber3 + vModel.CreditNumber4;

                cmd.Parameters.Add("@vaildmonth", SqlDbType.NVarChar).Value = vModel.VaildMonth;
                cmd.Parameters.Add("@vaildyear", SqlDbType.NVarChar).Value = vModel.VaildYear;
                cmd.Parameters.Add("@vaildcode", SqlDbType.NVarChar).Value = vModel.VaildCode;
                cmd.Parameters.Add("@memo", SqlDbType.NText).Value = vModel.Memo ?? "";


                sqlConnection.Open();               
                insertedPid = (int)cmd.ExecuteScalar();

                return insertedPid;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }


        }
    }
}