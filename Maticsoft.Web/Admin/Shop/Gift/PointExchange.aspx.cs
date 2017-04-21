﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.Shop.Gift
{
    public partial class PointExchange : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 434; } } //Shop_礼品兑换管理_列表页
        // private User currentUser;
        Maticsoft.BLL.Shop.Gift.ExchangeDetail detailBll = new BLL.Shop.Gift.ExchangeDetail();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
            {
              
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }
                gridView.OnBind();
            }
        }

        #region gridView

        public void BindData()
        {

            string keyword = this.txtKeyword.Text;
            int type = Convert.ToInt32(DropDetailType.SelectedValue);
            DataSet ds = detailBll.GetListEX(type, keyword,1);
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
        }
        public int UserId
        {
            get
            {
                int userid = 0;
                if (Request.Params["userid"] != null && PageValidate.IsNumber(Request.Params["userid"]))
                {
                    userid = int.Parse(Request.Params["userid"]);
                }
                return userid;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }

        //返回处理
        public void btnReturn_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("/Admin/Accounts/Admin/UserAdmin.aspx");
        }
        /// <summary>
        /// 根据状态值获取状态名字
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetStatusName(int Status)
        {
            switch (Status)
            {
                case 0:
                    return "未审核";
                case 1:
                    return "已审核";
                default:
                    return "待处理";
            }

        }


        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region AjaxCallback
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "SetStatus":
                    writeText = SetStatus();
                    break;
            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string SetStatus()
        {
            if (!String.IsNullOrWhiteSpace(Request.Form["DetailID"]) && !String.IsNullOrWhiteSpace(Request.Form["Status"]))
            {
                int detailId = Common.Globals.SafeInt(Request.Form["DetailID"], 0);
                int status = Common.Globals.SafeInt(Request.Form["Status"], 0);

                JsonObject json = new JsonObject();
                if (detailBll.SetStatus(detailId, status))
                {
                    json.Put("STATUS", "OK");
                }
                else
                {
                    json.Put("STATUS", "NODATA");
                }
                return json.ToString();
            }
            return "Fail";

        }
        //批量处理状态
        protected void btnBatch_Click(object sender, EventArgs e)
        {
            string idlist = GetIdlist();
            int status = Convert.ToInt32(dropType.SelectedValue);
            if (idlist.Trim().Length == 0 || status == -1)
                return;

            if (status != -1)
            {
                if (detailBll.SetStatusList(idlist, status))
                {
                    gridView.OnBind();
                }
            }
        }

        private string GetIdlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Value != null)
                    {
                        //idlist += gridView.Rows[i].Cells[1].Text + ",";
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }
        #endregion
    }
}