using BlogApp.Common;
using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Concrete
{
    public interface INewsOprations
    {
        decimal NewsPost(ClsPost objclsPost);
        DataSet GetAllNews(decimal NewsId, int CreateId, int ReviewId, int PageIndex, DateTime? Stardt, DateTime? Enddt);
        DataSet GetNews(int pageIndex, int pageSize, int catagoryId);
        DataTable GetSectionData(string SectionName, int PageIndex = 0, int catagoryId = 0);
        DataSet GetClientCategory(int CategoryId);
        DataSet GetNewPost(decimal Id, int type);
        DataSet GetNewsBySearch(int pageIndex, string catagory);
        DataSet GetHomePage();
        void UpdateNewsCount(decimal NewsId);
        DataSet GetAnalytics(int userId);
        clsUsers getCurrentUserId(int userId);
        void UpdateData();
        Newspost GetTopicData(int? Id);
        void Send(Tblcontact tbl);
        int getNewsNextId();
        Newspost GetNewsposts(decimal id);
    }
}
