using CsQuery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewParser.Controllers
{
    public class CryptoCoinController : Controller
    {
        // GET: CryptoCoin
        public ActionResult Index()
        {
            ArrayList newsList = new ArrayList();
            CQ dom = CQ.CreateFromUrl("http://www.cryptocoinsnews.com");
            CQ mainArticle = dom.Find("div.grid-wrapper").Eq(0);
            for (int i = 0; i < mainArticle.Children(".post").Length; i++)
            {
                CQ article = mainArticle.Children(".post").Eq(i).Find("a").Eq(0);
                newsData nData = new newsData();
                nData.text = article.Attr("title").ToString();
                nData.alt_url = article.Attr("href").ToString();
                nData.url = "";
                CQ img = article.Children("img").Eq(0);
                nData.url = img.Attr("src").ToString();
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
            string srcImg = "";
            if ( img.Length !=0)
            {
                srcImg = img.Css("background-image").ToString();

            }
            //string srcImg = "";
            ViewBag.hText = hText;
            ViewBag.srcImg = srcImg;
            ViewBag.aTitle = dom.Find("div.hero-text").Eq(0).Children("h1").Eq(0).Text();
            return View();
        }
    }
}