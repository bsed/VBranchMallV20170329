﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.User
{
    public partial class RequestMsg : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 653; } } //移动营销_微信用户消息管理_列表页
        protected new int Act_DelData = 654;   //移动营销_微信用户消息管理_删除数据
        Maticsoft.WeChat.BLL.Core.RequestMsg bll = new Maticsoft.WeChat.BLL.Core.RequestMsg();
        Maticsoft.WeChat.BLL.Core.User userBll=new Maticsoft.WeChat.BLL.Core.User();
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDeleteMsg.Visible = false;
                }
                //绑定用户分组
              
                if (!String.IsNullOrWhiteSpace(UserName))
                {
                    string name = GetNickName(UserName);
                    this.lblTitle.Text = "您正在查看用户【" + name + "】的消息记录";
                }
               
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }

                List<Maticsoft.WeChat.Model.Core.Action> actionList = Maticsoft.WeChat.BLL.Core.Action.GetAllAction();
                this.ddlAction.DataSource = actionList;
                this.ddlAction.DataTextField = "Name";
                this.ddlAction.DataValueField = "ActionId";
                this.ddlAction.DataBind();
                this.ddlAction.Items.Insert(0, new ListItem("请选择", "0"));

                //默认显示一周的用户消息
                this.txtFrom.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                this.txtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
         
        /// <summary>
        /// 编号
        /// </summary>
        protected string UserName
        {
            get
            {
                string  user = "";
                if (!string.IsNullOrWhiteSpace(Request.Params["user"]))
                {
                    user = Request.Params["user"];
                }
                return user;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView


        public void BindData()
        {
       
            StringBuilder strWhere = new StringBuilder();
            //用户
            if (!String.IsNullOrWhiteSpace(UserName))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("UserName='{0}'", UserName);
            }
            if (!String.IsNullOrWhiteSpace(this.txtFrom.Text) && Common.PageValidate.IsDateTime(this.txtFrom.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" CreateTime >='" + this.txtFrom.Text + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(this.txtTo.Text) && Common.PageValidate.IsDateTime(this.txtTo.Text))
            {
                string endTime = Common.Globals.SafeDateTime(this.txtTo.Text, DateTime.Now).AddDays(1).ToString("yyyy-MM-dd");
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  CreateTime< dateadd(day,1,'{0}')", txtTo.Text.Trim());
                //strWhere.AppendFormat(" CreateTime <='" + this.txtTo.Text + "' ");
            }
            //关键字
            if (!String.IsNullOrWhiteSpace(this.txtKeyword.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" Description like '%{0}%' ", this.txtKeyword.Text);
            }
             //过滤事件消息
            if (!chkShowEvent.Checked)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.Append(" MsgType <> 'event' ");
            }
            //选择事件
            if (!String.IsNullOrWhiteSpace(ddlEvent.SelectedValue))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" Event = '{0}' ", ddlEvent.SelectedValue);
            }
            //自定义事件
            int actionId = Common.Globals.SafeInt(ddlAction.SelectedValue, 0);
            if (actionId>0)
            {
                string key = "Action_" + actionId;
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                if (actionId == 1)
                {
                    strWhere.AppendFormat(" EventKey like '{0}_%' ", key);
                }
                else
                {
                    strWhere.AppendFormat(" EventKey = '{0}' ", key);
                }
              
            }
            gridView.DataSetSource = bll.GetList(-1, strWhere.ToString(), "CreateTime desc");
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

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (bll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
            {
                gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
        }
        #endregion

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetNickName(object target)
        {
            //0:取消关注、1:关注、
            string str = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
               Maticsoft.WeChat.BLL.Core.User userBll=new Maticsoft.WeChat.BLL.Core.User();
               str = userBll.GetNickName(target.ToString());
               if (String.IsNullOrWhiteSpace(str))
               {
                   str = target.ToString();
               }
            }
            return str;
        }


                /// <summary>
        ///消息类型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetMsgType(object target)
        {
            //0:取消关注、1:关注、
            string str = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
              //  地理位置:location,文本消息:text,消息类型:image，链接信息：link，事件信息：event
                string msgType = target.ToString();
                switch (msgType)
                {
                    case "text":
                        str = "文本消息";
                        break;
                    case "location":
                        str = "地理位置";
                        break;
                    case "image":
                        str = "图片消息";
                        break;
                    case "link":
                        str = "链接信息";
                        break;
                    case "event":
                        str = "事件信息";
                        break;
                    default:
                        str = "文本消息";
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取消息ID 的集合
        /// </summary>
        /// <returns></returns>
        private string GetMsgIDlist()
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

        protected void btnDeleteMsg_Click(object sender, EventArgs e)
        {
            string idlist = GetMsgIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (bll.DeleteList(idlist))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK, "RequestMsg.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind(); 
        }
       

    }
}