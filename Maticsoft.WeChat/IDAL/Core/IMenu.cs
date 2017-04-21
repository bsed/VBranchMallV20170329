﻿/**
* Menu.cs
*
* 功 能： N/A
* 类 名： Menu
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/17 12:25:28   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.WeChat.IDAL.Core
{
	/// <summary>
	/// 接口层Menu
	/// </summary>
	public interface IMenu
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int MenuId);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add( Maticsoft.WeChat.Model.Core.Menu model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update( Maticsoft.WeChat.Model.Core.Menu model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int MenuId);
		bool DeleteList(string MenuIdlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		 Maticsoft.WeChat.Model.Core.Menu GetModel(int MenuId);
		 Maticsoft.WeChat.Model.Core.Menu DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

	    /// <summary>
	    /// 根据分页获得数据列表
	    /// </summary>
	    //DataSet GetList(int PageSize,int PageIndex,string strWhere);

	    #endregion  成员方法

	    #region  MethodEx
	    bool UpdateSeq(int seq, int menuId);

        int GetSequence(string openId);

	    bool AddEx(Maticsoft.WeChat.Model.Core.Menu model);
	    bool DeleteEx(int menuId);
        Maticsoft.WeChat.Model.Core.Menu GetMenu(string key);
	    #endregion  MethodEx
	} 
}
