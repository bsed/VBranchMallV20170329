﻿using System;
using System.Web.UI;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Admin.Members.Goodmanagement
{
    public partial class GoodMShow : PageBaseAdmin
    {
        public string strid = "";

        private Maticsoft.BLL.Members.SiteMessage bll = new BLL.Members.SiteMessage();
        private Maticsoft.Accounts.Bus.UserType UserType = new Maticsoft.Accounts.Bus.UserType();
        protected override int Act_PageLoad { get { return 287; } } //用户管理_会员信息管理_详细页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int ID = (Convert.ToInt32(strid));
                    ShowInfo(ID);
                }
            }
        }

        /// <summary>
        /// 详细信息
        /// </summary>
        /// <param name="ID"></param>
        private void ShowInfo(int ID)
        {
            AccountsPrincipal user = new AccountsPrincipal(ID);
            User manage = new Maticsoft.Accounts.Bus.User(user);

            BLL.Members.UsersExp expBll = new BLL.Members.UsersExp();
            Model.Members.UsersExpModel model = expBll.GetUsersExpModel(ID);
            if (manage != null && model != null)
            {
                this.lblUserName.Text = manage.UserName;
                this.lblTrueName.Text = manage.TrueName;
                this.lblPhone.Text = manage.Phone;
                this.lblNickName.Text = manage.NickName;
                this.lblEmail.Text = manage.Email;

                this.lblID.Text = model.UserID.ToString();
                if (model.UserAppType == 0)
                {
                    this.lblUserAppType.Text = "会员申请";
                }
                else if (model.UserAppType == 1)
                {
                    this.lblUserAppType.Text = "好粉申请";
                }
                else if (model.UserAppType == 2)
                {
                    this.lblUserAppType.Text = "好代申请";
                }
                else if (model.UserAppType == 3)
                {
                    this.lblUserAppType.Text = "分销店申请";
                }
                else if (model.UserAppType == 4)
                {
                    this.lblUserAppType.Text = "服务店申请";
                }

                if (model.UserStatus == 0)
                {
                    this.lblUserStatus.Text = "处理中";
                }
                else if (model.UserStatus == 1)
                {
                    this.lblUserStatus.Text = "通过审核";
                }
                else if (model.UserStatus == 2)
                {
                    this.lblUserStatus.Text = "未通过审核";
                }

                this.lblSex.Text = (!string.IsNullOrWhiteSpace(manage.Sex) && manage.Sex.Trim() == "0") ? "女" : "男";
                this.lblActivity.Text = manage.Activity ? "正常使用" : "已经冻结";
                this.lblCreTime.Text = manage.User_dateCreate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (model != null)
            {
                Maticsoft.BLL.Ms.Regions RegionBll = new BLL.Ms.Regions();
                this.imageGra.ImageUrl = string.Format("/Upload/User/Gravatar/{0}.jpg", model.UserID);
                string strAddress = RegionBll.GetRegionNameByRID(Common.Globals.SafeInt(model.RegionId, 0))+""+model.Address;
                if (strAddress.Contains("北京北京"))
                {
                    strAddress = strAddress.Replace("北京北京", "北京");
                }
                else if (strAddress.Contains("上海上海"))
                {
                    strAddress = strAddress.Replace("上海上海", "上海");
                }
                else if (strAddress.Contains("重庆重庆"))
                {
                    strAddress = strAddress.Replace("重庆重庆", "重庆");
                }
                else if (strAddress.Contains("天津天津"))
                {
                    strAddress = strAddress.Replace("天津天津", "天津");
                }
                this.lblAddress.Text = strAddress.ToString();
                this.lblPoints.Text = model.Points.ToString();
                this.lblBalance.Text = model.Balance.HasValue ? model.Balance.Value.ToString("F") : "0.00";
                this.lblLoginDate.Text = model.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 通过审核
        /// </summary>
        /// <param name="e"></param>
        protected void btnPssExamine_Click(object sender, EventArgs e)
        {
            int userId = Common.Globals.SafeInt(lblID.Text, -1);
            if (userId < 1)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "账户信息非法, 请联系系统管理员!");
                return;
            }

            Maticsoft.BLL.Members.UsersExp userEXPBll = new BLL.Members.UsersExp();

            Model.Members.UsersExpModel model = userEXPBll.GetUsersExpModel(userId);

            int? AppType = model.UserAppType;
            if (model.UserAppType == null || model.UserAppType.ToString() == "")
            {
                model.UserAppType = 0;
            }

            Maticsoft.Model.Members.UsersExpModel expModel = new Model.Members.UsersExpModel();
            expModel.UserStatus = 1;
            expModel.UserAppType = AppType;
            expModel.UserOldType = AppType;
            expModel.UserID = userId;

            if (userEXPBll.UpdateGoodUserStatus(expModel))
            {
                Common.MessageBox.ShowSuccessTip(this, "操作成功", "List.aspx");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败");
            }

            Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipOperateError);
        }

        /// <summary>
        /// 未通过审核
        /// </summary>
        /// <param name="e"></param>
        protected void btnNotExamine_Click(object sender, EventArgs e)
        {
            int userId = Common.Globals.SafeInt(lblID.Text, -1);
            if (userId < 1)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "账户信息非法, 请联系系统管理员!");
                return;
            }

            Maticsoft.BLL.Members.UsersExp userEXPBll = new BLL.Members.UsersExp();

            Model.Members.UsersExpModel model = userEXPBll.GetUsersExpModel(userId);

            Maticsoft.Model.Members.UsersExpModel expModel = new Model.Members.UsersExpModel();
            expModel.UserStatus = 2;
            expModel.UserID = userId;

            if (userEXPBll.UpdateGoodUserStatus(expModel))
            {
                Common.MessageBox.ShowSuccessTip(this, "操作成功", "List.aspx");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败");
            }

            Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipOperateError);
        }

    }
}