﻿/**
* Show.cs
*
* 功 能： N/A
* 类 名： Show
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01						   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
namespace Maticsoft.Web.Admin.SNS.StarType
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 616; } } //SNS_达人类型管理_详细页   
        		public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					strid = Request.Params["id"];
					int TypeID=(Convert.ToInt32(strid));
					ShowInfo(TypeID);
				}
			}
		}
		
	private void ShowInfo(int TypeID)
	{
		Maticsoft.BLL.SNS.StarType bll=new Maticsoft.BLL.SNS.StarType();
		Maticsoft.Model.SNS.StarType model=bll.GetModel(TypeID);
		this.lblTypeID.Text=model.TypeID.ToString();
		this.lblTypeName.Text=model.TypeName;
		this.lblCheckRule.Text=model.CheckRule;
		this.lblRemark.Text=model.Remark;
		this.lblStatus.Text=model.Status.ToString();

	}



        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("StarTypeList.aspx");
        }
    }

}