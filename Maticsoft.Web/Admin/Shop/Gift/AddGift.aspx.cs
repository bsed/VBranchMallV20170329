﻿/**
* AddGift.cs
*
* 功 能： [N/A]
* 类 名： AddGift
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/23 12:24:01  Administrator    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.Drawing;
using Maticsoft.Model.Settings;

namespace Maticsoft.Web.Admin.Shop.Gift
{
    public partial class AddGift : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 426; } } //Shop_礼品管理_添加页
        Maticsoft.BLL.Shop.Gift.Gifts GiftBll = new Maticsoft.BLL.Shop.Gift.Gifts();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                //ProductImagesThumbSize
                Size thumbSize = Maticsoft.Common.StringPlus.SplitToSize(
                    BLL.SysManage.ConfigSystem.GetValueByCache(SettingConstant.PRODUCT_NORMAL_SIZE_KEY),
                    '|', SettingConstant.ProductThumbSize.Width, SettingConstant.ProductThumbSize.Height);
                hfProductImagesThumbSize.Value = thumbSize.Width + "," + thumbSize.Height;
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            //MessageBox.Show(this, "添加商品功能完善中, 尽请期待!");
            //return;
            GetProdictInfo();
        }

        private void GetProdictInfo()
        {
            #region Get PageData

            //分类
            int categoryId = 0;
            if (!string.IsNullOrWhiteSpace(this.DropParentId.SelectedValue.Trim()))
            {
                categoryId = int.Parse(this.DropParentId.SelectedValue);
            }

            //基本信息
            string giftName = txtProductName.Text;
            string unit = txtUnit.Text;
            decimal? marketPrice = Globals.SafeDecimal(txtMarketPrice.Text, 0);
            decimal? costPrice = Globals.SafeDecimal(txtCostPrice.Text, 0);
            decimal? salePrice = Globals.SafeDecimal(txtSalePrice.Text, 0);
            int weight=Globals.SafeInt(txtWeight.Text,0);
            int stock = Globals.SafeInt(txtStock.Text, 0);
            int points = Globals.SafeInt(txtPoints.Text, 0);
            
            string shortDescription = txtShortDescription.Text;
            string description = txtDescription.Text;

            //SEO
            string metaTitle = txtMeta_Title.Text;
            string metaDescription = txtMeta_Description.Text;
            string metaKeywords = txtMeta_Keywords.Text;

            //主图片
            string inFocusImageUrl = hfImage.Value;
            //缩略图
            string thumbnailsUrl = thfImage.Value;
            #endregion Get PageData

            #region Data Proc

            string[] productImages = new string[0];
            //简介信息去除换行符号处理
            if (!string.IsNullOrWhiteSpace(shortDescription))
            {
                shortDescription = Globals.HtmlEncodeForSpaceWrap(shortDescription);
            }
            //if (!string.IsNullOrWhiteSpace(splitProductImages))
            //{
            //    productImages = splitProductImages.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //}
       
            #region Set ProductInfo

            Model.Shop.Gift.Gifts giftInfo = new Model.Shop.Gift.Gifts();
            //基本信息
            giftInfo.Name = giftName;
            giftInfo.CategoryID = categoryId;
            giftInfo.Unit = unit;
            giftInfo.CostPrice = costPrice;
            giftInfo.NeedPoint = points;
            giftInfo.SaleCounts = 0;
            giftInfo.SalePrice = salePrice;
            giftInfo.Stock = stock;
            giftInfo.Weight = weight;
            giftInfo.MarketPrice = marketPrice;
            giftInfo.CreateDate = DateTime.Now;
            giftInfo.Enabled = Globals.SafeBool(rblUpselling.SelectedValue, false);

            //描述
            giftInfo.ShortDescription = shortDescription;
            giftInfo.LongDescription = description;
            //SEO
            giftInfo.Title = metaTitle;
            giftInfo.Meta_Description = metaDescription;
            giftInfo.Meta_Keywords = metaKeywords;
            giftInfo.ThumbnailsUrl = string.Format(inFocusImageUrl, "");
            giftInfo.InFocusImageUrl = string.Format(thumbnailsUrl, "T_");
            //for (int i = 0; i < productImages.Length; i++)
            //{
            //    if (i == 0)
            //    {
            //        //主图片
            //        giftInfo.ThumbnailsUrl = string.Format(productImages[i], "");
            //        giftInfo.InFocusImageUrl = string.Format(productImages[i], "T_");
            //    }
            //    else
            //    {
            //        //附图片
            //        //giftInfo.ProductImages.Add(
            //        //    new ProductImage
            //        //    {
            //        //        ImageUrl = string.Format(productImages[i], ""),
            //        //        ThumbnailUrl1 = string.Format(productImages[i], "T_"),
            //        //        ThumbnailUrl2 = string.Format(productImages[i], "N_")
            //        //    }
            //        //    );
            //    }
            //}

            #endregion Set ProductInfo

            if (GiftBll.Add(giftInfo)>0)
            {
                Response.Redirect("GiftsList.aspx");
            }
            else
            {
                MessageBox.Show(this, "保存失败! 请重试.");
                return;
            }
            #endregion 
        }
        //取消
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            //return;
            Response.Redirect("GiftsList.aspx");
        }
    }
}