﻿/**  版本信息模板在安装目录下，可自行修改。
* LinkLog.cs
*
* 功 能： N/A
* 类 名： LinkLog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/9 18:22:16   N/A    初版
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
using Maticsoft.WeChat.Model.Core;
using Maticsoft.WeChat.IDAL.Core;
namespace Maticsoft.WeChat.BLL.Core
{
	/// <summary>
	/// LinkLog
	/// </summary>
	public partial class LinkLog
	{
        private readonly ILinkLog dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (ILinkLog)new WeChat.SQLServerDAL.Core.LinkLog() : null;//暂时预留
		public LinkLog()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string WeChatLink)
		{
			return dal.Exists(WeChatLink);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.WeChat.Model.Core.LinkLog model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.WeChat.Model.Core.LinkLog model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string WeChatLink)
		{
			
			return dal.Delete(WeChatLink);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string WeChatLinklist )
		{
			return dal.DeleteList(WeChatLinklist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.WeChat.Model.Core.LinkLog GetModel(string WeChatLink)
		{
			
			return dal.GetModel(WeChatLink);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.WeChat.Model.Core.LinkLog GetModelByCache(string WeChatLink)
		{
			
			string CacheKey = "LinkLogModel-" + WeChatLink;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(WeChatLink);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.WeChat.Model.Core.LinkLog)objModel;
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
		public List<Maticsoft.WeChat.Model.Core.LinkLog> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.WeChat.Model.Core.LinkLog> DataTableToList(DataTable dt)
		{
			List<Maticsoft.WeChat.Model.Core.LinkLog> modelList = new List<Maticsoft.WeChat.Model.Core.LinkLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.WeChat.Model.Core.LinkLog model;
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
        public static bool Add(string link)
        {
            Maticsoft.WeChat.BLL.Core.LinkLog linkBll = new LinkLog();
            Maticsoft.WeChat.Model.Core.LinkLog linkModel = new Model.Core.LinkLog();
            linkBll.Delete(link);
            linkModel.CreatedDate = DateTime.Now;
            linkModel.WeChatLink = link;
            return linkBll.Add(linkModel);
        }

        public static bool DeleteEx(string link)
        {
            Maticsoft.WeChat.BLL.Core.LinkLog linkBll = new LinkLog();
            return linkBll.Delete(link);
        }

        public static bool ExistsEx(string link)
        {
            Maticsoft.WeChat.BLL.Core.LinkLog linkBll = new LinkLog();
            return linkBll.Exists(link);
        }
		#endregion  ExtensionMethod
	}
}

