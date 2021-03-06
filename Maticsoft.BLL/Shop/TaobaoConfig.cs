﻿using Maticsoft.BLL.SysManage;
using Maticsoft.DALFactory;
using Maticsoft.Model.SysManage;
using Maticsoft.TaoBao;

namespace Maticsoft.BLL.Shop
{
    /// <summary>
    /// 淘宝配置
    /// </summary>
    public class TaoBaoConfig
    {

        public static ITopClient GetTopClient()
        {
            string taoBaoAppkey = SysManage.ConfigSystem.GetValue("OpenAPI_Shop_TaoBaoAppkey");
            string taobaoAppsecret = SysManage.ConfigSystem.GetValue("OpenAPI_Shop_TaobaoAppsecret");
            string taobaoApiUrl = SysManage.ConfigSystem.GetValue("OpenAPI_Shop_TaobaoApiUrl");
            if (string.IsNullOrWhiteSpace(taobaoApiUrl))
            {
                taobaoApiUrl = "http://gw.api.taobao.com/router/rest";
            }
            ITopClient client = new DefaultTopClient(taobaoApiUrl, taoBaoAppkey, taobaoAppsecret);
            return client;
        }

        /// <summary>
        /// /需要修改这部分
        /// </summary>
        //SysManage.ConfigSystem config = new ConfigSystem();
        //public static ITopClient GetTopClient()
        //{
        //    string TaoBaoAppkey = config.GetValue("OpenAPI_TaoBaoAppkey");
        //    string TaobaoAppsecret = config.GetValue("OpenAPI_TaobaoAppsecret");
        //    string TaobaoApiUrl = config.GetValue("OpenAPI_TaobaoApiUrl");
        //    ITopClient client = new DefaultTopClient(TaobaoApiUrl, TaoBaoAppkey, TaobaoAppsecret);
        //    return client;
        //}

        public ApplicationKeyType applicationKeyType = ApplicationKeyType.OpenAPI;

        public TaoBaoConfig(ApplicationKeyType keyType)
        {
            applicationKeyType = keyType;
        }

        public const string TAOBAO_APPKEY = "OpenAPI_Shop_TaoBaoAppkey";

        public string TaoBaoAppkey
        {
            get { return ConfigSystem.GetValueByCache(TaoBaoConfig.TAOBAO_APPKEY, applicationKeyType); }
            set { ConfigSystem.Modify(TaoBaoConfig.TAOBAO_APPKEY, value, "淘宝卖家接口APPKEY", applicationKeyType); }
        }

        public const string TAOBAO_APPSECRET = "OpenAPI_Shop_TaobaoAppsecret";

        public string TaobaoAppsecret
        {
            get { return ConfigSystem.GetValueByCache(TaoBaoConfig.TAOBAO_APPSECRET, applicationKeyType); }
            set { ConfigSystem.Modify(TaoBaoConfig.TAOBAO_APPSECRET, value, "淘宝卖家接口APPSECRET", applicationKeyType); }
        }

        public const string TAOBAO_APIURL = "OpenAPI_Shop_TaobaoApiUrl";

        public string TaobaoApiUrl
        {
            get { return ConfigSystem.GetValueByCache(TaoBaoConfig.TAOBAO_APIURL, applicationKeyType); }
            set { ConfigSystem.Modify(TaoBaoConfig.TAOBAO_APIURL, value, "淘宝卖家接口APIURL", applicationKeyType); }
        }
    }
}
