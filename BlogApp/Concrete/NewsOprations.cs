using BlogApp.Common;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Concrete
{
    public class NewsOprations : IDisposable, INewsOprations
    {
        private readonly blogdbContext _context;
        private readonly IConfiguration _configuration;
        public NewsOprations(blogdbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public decimal NewsPost(ClsPost objclsPost)
        {
            try
            {
                    Newspost newsPost = new Newspost();
                    newsPost.EnglishTitle = objclsPost.EnglishTitle;
                    newsPost.OdiaTitle = objclsPost.OdiaTitle;
                    newsPost.EngShortDesc = objclsPost.EngShortDesc;
                    newsPost.OdshortDesc = objclsPost.ODShortDesc;
                    newsPost.SeoMeta = objclsPost.SeoMeta;
                    newsPost.Tags = objclsPost.Tags;
                    newsPost.CategoryId = objclsPost.CategoryId;
                    newsPost.HeaderImageName = objclsPost.ImageName;
                    newsPost.Odcontent = objclsPost.ODContent;
                    newsPost.IsActive = true;
                    newsPost.IsDeleted = false;
                    newsPost.IsReviewed = objclsPost.IsReviewed;
                    newsPost.CreatedBy = objclsPost.CreatedBy;
                    newsPost.ReviewedBy = objclsPost.ReviewedBy;
                    newsPost.PostedDate = objclsPost.PostedDate;
                    newsPost.PostedOn = objclsPost.PostedOn;
                    newsPost.PostedMonth = objclsPost.PostedMonth;
                    newsPost.PostedYear = objclsPost.PostedYear;
                    newsPost.SlugUrl = objclsPost.SlugUrl;
                    newsPost.Thumbnail86 = objclsPost.ImageName.Replace("Img", "Thumbnail_86x64");
                    newsPost.Thumbnail210 = objclsPost.ImageName.Replace("Img", "Thumbnail_210x136");
                    newsPost.Thumbnail279 = objclsPost.ImageName.Replace("Img", "Thumbnail_279x220");
                    newsPost.Frequency = 0;
                    _context.Newsposts.Add(newsPost);
                     _context.SaveChanges();
                    return newsPost.Id;
                
            }
            catch (Exception ex)
            {   
            }
            return 0;
        }

        public DataSet GetAllNews(decimal NewsId, int CreateId, int ReviewId, int PageIndex, DateTime? Stardt, DateTime? Enddt)
        {
                using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("AppConnection")))
                {
                    MySqlCommand cmd = new MySqlCommand("usp_GetAllNews", connection);
                    cmd.Parameters.Add("NewsId", MySqlDbType.Int32).Value = NewsId;
                    cmd.Parameters.Add("CreatedUserId", MySqlDbType.Int32).Value = CreateId;
                    cmd.Parameters.Add("ReviewdBy", MySqlDbType.Int32).Value = ReviewId;
                    cmd.Parameters.Add("PageIndex", MySqlDbType.Int32).Value = PageIndex;
                    cmd.Parameters.Add("StartDt", MySqlDbType.DateTime).Value = Stardt;
                    cmd.Parameters.Add("EndDt", MySqlDbType.DateTime).Value = Enddt;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 90;
                    try
                    {
                        connection.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;

                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                    }

                }
            return null;
        }
        public DataSet GetNews(int pageIndex, int pageSize, int catagoryId)
        {
            {
                using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("AppConnection")))
                {
                    MySqlCommand cmd = new MySqlCommand("usp_GetClientSideNews", connection);
                    cmd.Parameters.Add("PageIndex", MySqlDbType.Int32).Value = pageIndex + 1;
                    cmd.Parameters.Add("pageSize", MySqlDbType.Int32).Value = pageSize;
                    cmd.Parameters.Add("CategoryId", MySqlDbType.Int32).Value = catagoryId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 90;
                    try
                    {
                        connection.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;

                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                    }

                }
            }
            return null;
        }
        public DataTable GetSectionData(string SectionName, int PageIndex = 0, int catagoryId = 0)
        {
                using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("AppConnection")))
                {
                    MySqlCommand cmd = new MySqlCommand("usp_GetSetionData", connection);
                    cmd.Parameters.Add("PageIndex", MySqlDbType.Int32).Value = PageIndex + 1;
                    cmd.Parameters.Add("SectionName", MySqlDbType.VarChar).Value = SectionName;
                    cmd.Parameters.Add("CategoryId", MySqlDbType.Int32).Value = catagoryId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 90;
                    try
                    {
                        connection.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                    }

                }
            return null;
        }

        public DataSet GetClientCategory(int CategoryId)
        {
            try
            {
                    using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("AppConnection")))
                    {
                       MySqlCommand cmd = new MySqlCommand("usp_GetCatagoryData", connection);
                        cmd.Parameters.Add("CategoryId", MySqlDbType.Int32).Value = CategoryId;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 90;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            var dataSet = new DataSet();
                            dataAdapter.Fill(dataSet);
                            return dataSet;
                        };

                    }
            }
            catch (Exception ex)
            {   
            }
            return null;
        }
        public DataSet GetNewPost(decimal Id, int type)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("AppConnection")))
                {
                    var cmd = new MySqlCommand("usp_GetNewPost", connection);
                    cmd.Parameters.Add("Id", MySqlDbType.Decimal).Value = Id;
                    cmd.Parameters.Add("Type", MySqlDbType.Int32).Value = type;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 90;
                    using (var dataAdapter = new MySqlDataAdapter(cmd))
                    {
                        var dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        Task.Factory.StartNew(() => UpdateNewsCount(Id));
                        return dataSet;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public Newspost GetTopicData(int? Id)
        {
            try
            {
                var item = (from news in _context.Newsposts
                            where news.Id == Id
                            select news).FirstOrDefault();
                return item;
            }
            catch (Exception ex)
            {
            }
            return null;

        }
        public DataSet GetNewsBySearch(int pageIndex, string catagory)
        {
            var listPst = new List<ClsPost>();
            using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("AppConnection")))
            {
                var cmd = new MySqlCommand("usp_GetSearch", connection);
                cmd.Parameters.Add("PageIndex", MySqlDbType.Int32).Value = pageIndex;
                cmd.Parameters.Add("pageSize", MySqlDbType.Int32).Value = 10;
                cmd.Parameters.Add("SearchStr", MySqlDbType.VarChar).Value = catagory;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 90;
                try
                {
                    using (var dataAdapter = new MySqlDataAdapter(cmd))
                    {
                        var dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        return dataSet;
                    };
                }
                catch (Exception ex)
                {
                }
                return null;
            }

        }
        public DataSet GetHomePage()
        {
                using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("AppConnection")))
                {
                    var cmd = new MySqlCommand("usp_GetIndexPage", connection);
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 90;
                        connection.Open();
                        using (var da = new MySqlDataAdapter(cmd))
                        {
                            var ds = new DataSet();
                            da.Fill(ds);
                            return ds;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    return null;
                }
        }
        public void UpdateNewsCount(decimal NewsId)
        {
            try
            {
                    using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("AppConnection")))
                    {
                        MySqlCommand cmd = new MySqlCommand("usp_UpdateNewsCount", connection);
                        cmd.Parameters.Add("NewsId", MySqlDbType.Decimal).Value = NewsId;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 90;
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        int i = cmd.ExecuteNonQuery();
                        connection.Close();
                    }
            }
            catch (Exception ex)
            {
            }
        }
        public DataSet GetAnalytics(int userId)
        {
                using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("AppConnection")))
                {
                    MySqlCommand cmd = new MySqlCommand("usp_GetAnalytics", connection);
                    cmd.Parameters.Add("UserId", MySqlDbType.Int32).Value = userId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 90;
                    try
                    {
                        connection.Open();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataSet dt = new DataSet();
                        da.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {               
                    }

                }
            return null;
        }
        public clsUsers getCurrentUserId(int userId)
        {
            try
            {
                
                {
                    var item = _context.Set<clsUsers>().FromSqlRaw("CALL usp_GetUser({0})", userId).FirstOrDefault();
                      
                    return item;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public void UpdateData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("AppConnection")))
                {
                    MySqlCommand cmd = new MySqlCommand("usp_LatestPopular", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 90;
                     if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        int i = cmd.ExecuteNonQuery();
                        connection.Close();

                }

            }
            catch (Exception ex)
            {
            }
        }
        public void Send(Tblcontact tbl)
        {
           _context.Tblcontacts.Add(tbl);
            _context.SaveChanges();
        }
        public int getNewsNextId()
        {
            int  NewsId = _context.Newsposts.Where(x => x.PostedDate.Value.Year == DateTime.Now.Year && x.PostedDate.Value.Month == DateTime.Now.Month && x.PostedDate.Value.Day == DateTime.Now.Day).ToList().Count;
            NewsId = NewsId + 1;
            return NewsId;
        }
        public Newspost GetNewsposts(decimal id)
        {
           return _context.Newsposts.Where(x => x.Id == id).FirstOrDefault();
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~NewsOprations() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
