﻿/**
* VideoPreview.cs
*
* 功 能： 视频预览功能
* 类 名： VideoPreview
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/27 14:37:51  蒋海滨    初版
* V0.02  2012年6月8日 18:45:39  孙鹏    提示信息修改、using引用移除
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.Video
{
    public partial class VideoPreview : PageBaseAdmin
    {
        public string localVideoUrl = "";
        protected override int Act_PageLoad { get { return 272; } } 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        public int VideoID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.BLL.CMS.Video bll = new Maticsoft.BLL.CMS.Video();
            Maticsoft.Model.CMS.Video model = bll.GetModel(VideoID);
            if (null != model)
            {
                string videoUrl = model.VideoUrl;
                int urlType = model.UrlType;
                if (!string.IsNullOrWhiteSpace(videoUrl))
                {
                    if (urlType == 0)//本地视频
                    {
                        localVideoUrl = videoUrl;
                    }
                }
            }
        }
    }
}