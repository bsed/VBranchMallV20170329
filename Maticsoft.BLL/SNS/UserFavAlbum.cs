﻿/**
* UserFavAlbum.cs
*
* 功 能： N/A
* 类 名： UserFavAlbum
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:03   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.SNS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
namespace Maticsoft.BLL.SNS
{
	/// <summary>
	/// 用户关注专辑
	/// </summary>
	public partial class UserFavAlbum
	{
		private readonly IUserFavAlbum dal=DASNS.CreateUserFavAlbum();
		public UserFavAlbum()
		{}
		#region  BasicMethod



		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AlbumID,int UserID)
		{
			return dal.Exists(AlbumID,UserID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.SNS.UserFavAlbum model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.UserFavAlbum model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int AlbumID,int UserID)
		{
			
			return dal.Delete(AlbumID,UserID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(IDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SNS.UserFavAlbum GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.UserFavAlbum GetModelByCache(int ID)
		{
			
			string CacheKey = "UserFavAlbumModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.SNS.UserFavAlbum)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.UserFavAlbum> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.UserFavAlbum> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.UserFavAlbum> modelList = new List<Maticsoft.Model.SNS.UserFavAlbum>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.UserFavAlbum model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 收藏专辑
        /// </summary>
        /// <param name="AlbumId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int FavAlbum(int AlbumId, int UserId)
        {
            return dal.FavAlbum(AlbumId,UserId);
        
        }

        /// <summary>
        /// 取消收藏专辑
        /// </summary>
        /// <param name="AlbumId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int UnFavAlbum(int AlbumId, int UserId)
        {
            return dal.UnFavAlbum(AlbumId,UserId);
        }

        /// <summary>
        ///检测是否关注此专辑
        /// </summary>
        /// <param name="AlbumId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool CheckIsFav(int AlbumId, int UserId)
        {
            if (GetRecordCount("AlbumID=" + AlbumId + " and UserID=" + UserId + "") > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetSearchList(string Keywords)
        {
            return dal.GetList(0,string.Format(" Tags like '%{0}%'",Keywords) ,"");
        }
		#endregion  ExtensionMethod
	}
}

