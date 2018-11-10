using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using TabkeFiveWebApplication.Models.ViewModels;
using TabkeFiveWebApplication.Models.Cart;

namespace TabkeFiveWebApplication.DAL
{
    public class ProductEntity
    {


        

        //傳入ViewModel，修改資料
        public Boolean Update(ProductViewModels vModel)
        {

            string connStr = DBConnClass.GetDBConn();
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();

            try
            {

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText =
                @"update producttbl set 
                  kind=@kind,
                  category=@category,
                  mid=@mid,
                  name=@name,
                  intro=@intro,
                  price=@price,
                  state=@state,
                  score=@score,
                  picture=@picture
                  where pid=@pid";
                  //vidiourl=@vidiourl 
                cmd.Connection = sqlConnection;
                cmd.Parameters.Add("@pid", SqlDbType.Int).Value = vModel.PId;
                cmd.Parameters.Add("@kind", SqlDbType.Int).Value = vModel.SelectedKinds;
                cmd.Parameters.Add("@category", SqlDbType.Int).Value = vModel.SelectedCategories;
                cmd.Parameters.Add("@mid", SqlDbType.NVarChar).Value = vModel.MId;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = vModel.Name;
                cmd.Parameters.Add("@intro", SqlDbType.NVarChar).Value = vModel.Intro;
                cmd.Parameters.Add("@price", SqlDbType.Int).Value = vModel.Price;
                cmd.Parameters.Add("@state", SqlDbType.Int).Value = vModel.State;
                cmd.Parameters.Add("@score", SqlDbType.Int).Value = vModel.Score;
                cmd.Parameters.Add("@picture", SqlDbType.VarBinary).Value = vModel.Picture ?? null;
               // cmd.Parameters.Add("@vidiourl", SqlDbType.NVarChar).Value = vModel.VidioUrl ?? null;


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


        //傳入Pid(產品編號)，刪除資料
        public Boolean Delete(int Pid)
        {

            string connStr = DBConnClass.GetDBConn();
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();

            try
            {

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText =
                @"delete from producttbl where pid=@pid";

                cmd.Connection = sqlConnection;
                cmd.Parameters.Add("@pid", SqlDbType.Int).Value = Pid;
                
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


        //傳入Pid(產品編號)，取得照片檔案
        public byte[] GetPicture(String PId)
        {
            using (DataTable dt = new DataTable())
            {

                string sqlTxt = @"select picture from producttbl where pid=@pid";

                string connStr = DBConnClass.GetDBConn();
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sqlTxt, sqlConn) { CommandType = CommandType.Text })
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sqlConn.Open();
                    cmd.Parameters.Add("@pid", SqlDbType.Int).Value = PId;
                    da.Fill(dt);
                }


                if (dt.Rows.Count > 0)
                {

                    Byte[] data = new Byte[0];
                    data = (Byte[])(dt.Rows[0]["picture"]);

                    return data;
                }
                else
                {
                    return null;
                }


            }
        }

        public CartItem QueryByProductid(int? Pid)
        {

          
            using (DataTable dt = new DataTable())
            {

               

                string sqlTxt = @"select * from producttbl where pid=@pid";

                string connStr = DBConnClass.GetDBConn();
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sqlTxt, sqlConn) { CommandType = CommandType.Text })
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sqlConn.Open();
                    cmd.Parameters.Add("@pid", SqlDbType.Int).Value = Pid;
                    da.Fill(dt);
                }
               

                if (dt.Rows.Count == 1)
                {


                    CartItem item = new CartItem
                    {
                        Id = dt.Rows[0].Field<int>("pid"),
                        Name = (dt.Rows[0].Field<String>("Name") ?? "").Trim(),
                        Price = dt.Rows[0].Field<int>("price"),
                        Quantity = 1,
                        PhotoUrl = (dt.Rows[0].Field<String>("vidiourl") ?? "").Trim(),
                    };

                    return item;

                }
                else
                {
                    return null;
                }
                

            }

        }




        //利用 Pid(產品編號) 回傳，該筆(一筆) ProductViewModels
        public IEnumerable<ProductViewModels> QueryByProductId(String Pid)
        {


            IEnumerable<ProductViewModels> vModel = Enumerable.Empty<ProductViewModels>();

            using (DataTable dt = new DataTable())
            {

                string sqlTxt = @"select * from producttbl where pid=@pid";

                string connStr = DBConnClass.GetDBConn();
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sqlTxt, sqlConn) { CommandType = CommandType.Text })
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sqlConn.Open();
                    cmd.Parameters.Add("@pid", SqlDbType.Int).Value = Pid;
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 1)
                {

                    var query = from r in dt.AsEnumerable()
                                select new ProductViewModels
                                {
                                    PId = Convert.ToInt32(r.Field<int>("pid")),
                                    SelectedKinds = Convert.ToInt32(r.Field<int>("kind")),
                                    SelectedCategories = Convert.ToInt32(r.Field<int>("category")),
                                    MId = (r.Field<String>("mid") ?? "").Trim(),
                                    Name = (r.Field<String>("name") ?? "").Trim(),
                                    Intro = (r.Field<String>("intro") ?? "").Trim(),
                                    Price = Convert.ToInt32(r.Field<int>("price")),
                                    State = Convert.ToInt32(r.Field<int>("state")),
                                    Score = Convert.ToInt32(r.Field<int>("score")),
                                    Picture = (r.Field<Byte[]>("picture")).ToArray(),
                                    VidioUrl = (r.Field<String>("vidiourl") ?? "").Trim(),
                                };
                    vModel = query;


                }
                else
                {
                    vModel = null;
                }

                return vModel;

            }
        }
        public IEnumerable<ProductViewModels> QueryByCatgoryId(int? categoryId)
        {
            IEnumerable<ProductViewModels> vModel = Enumerable.Empty<ProductViewModels>();

            using (DataTable dt = new DataTable())
            {

                string sqlTxt = @"select * from producttbl where category=@category";
                string connStr = DBConnClass.GetDBConn();
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sqlTxt, sqlConn) { CommandType = CommandType.Text })
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sqlConn.Open();
                    cmd.Parameters.Add("@category", SqlDbType.Int).Value = categoryId;
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {

                    var query = from r in dt.AsEnumerable()
                                select new ProductViewModels
                                {
                                    PId = Convert.ToInt32(r.Field<int>("pid")),
                                    SelectedKinds = Convert.ToInt32(r.Field<int>("kind")),
                                    SelectedCategories = Convert.ToInt32(r.Field<int>("category")),
                                    MId = (r.Field<String>("mid") ?? "").Trim(),
                                    Name = (r.Field<String>("name") ?? "").Trim(),
                                    Intro = (r.Field<String>("intro") ?? "").Trim(),
                                    Price = Convert.ToInt32(r.Field<int>("price")),
                                    State = Convert.ToInt32(r.Field<int>("state")),
                                    Score = Convert.ToInt32(r.Field<int>("score")),
                                    Picture = (r.Field<Byte[]>("picture")).ToArray(),
                                    VidioUrl = (r.Field<String>("vidiourl") ?? "").Trim(),
                                };
                    vModel = query;

                }

                return vModel;

            }
        }

        //利用 kindId 種類編號回傳 ，kindId 的 ProductViewModels(一筆或多筆)
        public IEnumerable<ProductViewModels> QueryByAllKind()
        {


            IEnumerable<ProductViewModels> vModel = Enumerable.Empty<ProductViewModels>();

            using (DataTable dt = new DataTable())
            {

                string sqlTxt = @"select * from producttbl";

                string connStr = DBConnClass.GetDBConn();
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sqlTxt, sqlConn) { CommandType = CommandType.Text })
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sqlConn.Open();                    
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {

                    var query = from r in dt.AsEnumerable()
                                select new ProductViewModels
                                {
                                    PId = Convert.ToInt32(r.Field<int>("pid")),
                                    SelectedKinds = Convert.ToInt32(r.Field<int>("kind")),
                                    SelectedCategories = Convert.ToInt32(r.Field<int>("category")),
                                    MId = (r.Field<String>("mid") ?? "").Trim(),
                                    Name = (r.Field<String>("name") ?? "").Trim(),
                                    Intro = (r.Field<String>("intro") ?? "").Trim(),
                                    Price = Convert.ToInt32(r.Field<int>("price")),
                                    State = Convert.ToInt32(r.Field<int>("state")),
                                    Score = Convert.ToInt32(r.Field<int>("score")),
                                    Picture = (r.Field<Byte[]>("picture")).ToArray(),
                                    VidioUrl = (r.Field<String>("vidiourl") ?? "").Trim(),
                                };
                    vModel = query;

                }

                return vModel;

            }
        }



        //利用 kindId 種類編號回傳 ，kindId 的 ProductViewModels(一筆或多筆)
        public IEnumerable<ProductViewModels> QueryByKindId(int? kindId,int? catelogId)
        {


            IEnumerable<ProductViewModels> vModel = Enumerable.Empty<ProductViewModels>();

            using (DataTable dt = new DataTable())
            {

                string sqlTxt = @"select * from producttbl where kind=@kind and category=@category";
                //string sqlTxt = @"select * from producttbl where kind=@kind";
                string connStr = DBConnClass.GetDBConn();
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sqlTxt, sqlConn) { CommandType = CommandType.Text })
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sqlConn.Open();
                    cmd.Parameters.Add("@kind", SqlDbType.Int).Value = kindId;
                    cmd.Parameters.Add("@category", SqlDbType.Int).Value = catelogId;
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {

                    var query = from r in dt.AsEnumerable()
                                select new ProductViewModels
                                {
                                    PId = Convert.ToInt32(r.Field<int>("pid")),
                                    SelectedKinds = Convert.ToInt32(r.Field<int>("kind")),
                                    SelectedCategories = Convert.ToInt32(r.Field<int>("category")),
                                    MId = (r.Field<String>("mid") ?? "").Trim(),
                                    Name = (r.Field<String>("name") ?? "").Trim(),
                                    Intro = (r.Field<String>("intro") ?? "").Trim(),
                                    Price = Convert.ToInt32(r.Field<int>("price")),
                                    State = Convert.ToInt32(r.Field<int>("state")),
                                    Score = Convert.ToInt32(r.Field<int>("score")),
                                    Picture = (r.Field<Byte[]>("picture")).ToArray(),
                                    VidioUrl = (r.Field<String>("vidiourl") ?? "").Trim(),
                                };
                    vModel = query;

                }

                return vModel;

            }
        }



        //利用 kindId 種類編號回傳 ，kindId 的 ProductViewModels(一筆或多筆)
        public IEnumerable<ProductViewModels> QueryByKindId(int? kindId)
        {


            IEnumerable<ProductViewModels> vModel = Enumerable.Empty<ProductViewModels>();

            using (DataTable dt = new DataTable())
            {

                string sqlTxt = @"select * from producttbl where kind=@kind";

                string connStr = DBConnClass.GetDBConn();
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sqlTxt, sqlConn) { CommandType = CommandType.Text })
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sqlConn.Open();
                    cmd.Parameters.Add("@kind", SqlDbType.Int).Value = kindId;
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {

                    var query = from r in dt.AsEnumerable()
                                select new ProductViewModels
                                {
                                    PId = Convert.ToInt32(r.Field<int>("pid")),
                                    SelectedKinds = Convert.ToInt32(r.Field<int>("kind")),
                                    SelectedCategories = Convert.ToInt32(r.Field<int>("category")),
                                    MId = (r.Field<String>("mid") ?? "").Trim(),
                                    Name = (r.Field<String>("name") ?? "").Trim(),
                                    Intro = (r.Field<String>("intro") ?? "").Trim(),
                                    Price = Convert.ToInt32(r.Field<int>("price")),
                                    State = Convert.ToInt32(r.Field<int>("state")),
                                    Score = Convert.ToInt32(r.Field<int>("score")),
                                    Picture = (r.Field<Byte[]>("picture")).ToArray(),
                                    VidioUrl = (r.Field<String>("vidiourl") ?? "").Trim(),
                                };
                    vModel = query;

                }

                return vModel;

            }
        }

        //利用 MemberId 會員編號回傳 ，符合MemberId 的 ProductViewModels(一筆或多筆)
        public IEnumerable<ProductViewModels> QueryByMemberId(String memberId)
        {


            IEnumerable<ProductViewModels> vModel = Enumerable.Empty<ProductViewModels>();

            using (DataTable dt = new DataTable())
            {

                string sqlTxt = @"select * from producttbl where mid=@mid";

                string connStr = DBConnClass.GetDBConn();
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(sqlTxt, sqlConn) { CommandType = CommandType.Text })
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sqlConn.Open();
                    cmd.Parameters.Add("@mid", SqlDbType.NVarChar).Value = memberId;
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {

                    var query = from r in dt.AsEnumerable()
                                select new ProductViewModels
                                {
                                    PId = Convert.ToInt32(r.Field<int>("pid")),
                                    SelectedKinds = Convert.ToInt32(r.Field<int>("kind")),
                                    SelectedCategories = Convert.ToInt32(r.Field<int>("category")),
                                    MId = (r.Field<String>("mid") ?? "").Trim(),
                                    Name = (r.Field<String>("name") ?? "").Trim(),
                                    Intro = (r.Field<String>("intro") ?? "").Trim(),
                                    Price = Convert.ToInt32(r.Field<int>("price")),
                                    State = Convert.ToInt32(r.Field<int>("state")),
                                    Score = Convert.ToInt32(r.Field<int>("score")),
                                    Picture = (r.Field<Byte[]>("picture")).ToArray(),
                                    VidioUrl = (r.Field<String>("vidiourl") ?? "").Trim(),
                                };
                    vModel = query;

                }

                return vModel;

            }
        }

        //新增一筆 ProductViewModels
        public Boolean Insert(ProductViewModels vModel)
        {


            string connStr = DBConnClass.GetDBConn();
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();

            try
            {

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText =
                @"insert producttbl(kind,category,mid,name,intro,price,state,score,picture,vidiourl) 
                 VALUES (@kind,@category,@mid,@name,@intro,@price,@state,@score,@picture,@vidiourl)";

                cmd.Connection = sqlConnection;
                cmd.Parameters.Add("@kind", SqlDbType.Int).Value = vModel.SelectedKinds;
                cmd.Parameters.Add("@category", SqlDbType.Int).Value = vModel.SelectedCategories;
                cmd.Parameters.Add("@mid", SqlDbType.NVarChar).Value = vModel.MId;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = vModel.Name;
                cmd.Parameters.Add("@intro", SqlDbType.NVarChar).Value = vModel.Intro;
                cmd.Parameters.Add("@price", SqlDbType.Int).Value = vModel.Price;
                cmd.Parameters.Add("@state", SqlDbType.Int).Value = vModel.State;
                cmd.Parameters.Add("@score", SqlDbType.Int).Value = vModel.Score;
                cmd.Parameters.Add("@picture", SqlDbType.VarBinary).Value = vModel.Picture;
                cmd.Parameters.Add("@vidiourl", SqlDbType.NVarChar).Value = vModel.VidioUrl ?? "";


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
    }

}