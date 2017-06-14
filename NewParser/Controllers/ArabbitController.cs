using CsQuery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewParser.Controllers
{
    public class ArabbitController : Controller
    {
        // GET: Arabbit
        public ActionResult Index()
        {
            ArrayList newsList = new ArrayList();

            CQ dom = CQ.CreateFromUrl("http://www.arabbit.net");
            CQ mainArticle = dom.Find("div.grid-wrapper").Eq(0);
            for (int i = 0; i < mainArticle.Children().Length; i++)
            {
                newsData nData = new newsData();

                CQ article = mainArticle.Children().Eq(i).Find("a").Eq(0);

                string aText = "";
                string aImg = "";
                string aAlt = "";

                if ( article.Length !=0)
                {
                    aText = article.Attr("title").ToString();
                    aAlt = article.Attr("href").ToString();
                }
                
                nData.text = aText;
                nData.alt_url = aAlt;
                nData.url = "";
                CQ img = article.Children("img").Eq(0);
                if ( img.Length != 0)
                {
                    aAlt = img.Attr("src").ToString();
                }
                nData.url = aAlt;
                newsList.Add(nData);
            }
            ViewBag.newsList = newsList;
            return View();
        }
        public ActionResult getDetail(string url)
        {
            CQ dom = CQ.CreateFromUrl(url);
            CQ article = dom.Find("article.post").Eq(0);
            CQ aMain = article.Children("div.entry-content").Eq(0);

            string hText = aMain.RenderSelection().ToString();

            CQ img = dom.Find("div.site-header-bg").Eq(0);
            string srcImg = img.Css("background-image").ToString();
            ViewBag.hText = hText;
            ViewBag.srcImg = srcImg;
            return View();
        }
    }
}