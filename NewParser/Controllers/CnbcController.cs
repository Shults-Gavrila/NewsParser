using CsQuery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewParser.Controllers
{
    public class CnbcController : Controller
    {
        // GET: Cnbc
        public ActionResult Index()
        {
            ArrayList newsList = new ArrayList();
            CQ dom = CQ.CreateFromUrl("http://www.cnbc.com/world/?region=world");
            CQ mainArticle = dom.Find("ul.stories_assetlist").Eq(0);
            for (int i = 0; i < mainArticle.Children(".card,.last-card").Length; i++)
            {
                CQ article = mainArticle.Children(".card").Eq(i);
                CQ data = article.Find("div.headline").Eq(0).Children("a").Eq(0);
                newsData nData = new newsData();
                nData.text = data.Text().ToString();
                nData.alt_url = data.Attr("href").ToString();
                nData.url = "";
                CQ img = article.Find("img").Eq(0);
                nData.url = img.Attr("data-img-src");
                newsList.Add(nData);
            }
            CQ smallArticle = dom.Find("ul.stories_assetlist").Eq(1);
            for ( int i = 0; i< smallArticle.Children("li").Length; i++)
            {
                CQ article = smallArticle.Children("li").Eq(i);
                CQ data = article.Find("div.headline").Eq(0).Children("a").Eq(0);
                if (data.Length == 0) continue;
                newsData nData = new newsData();
                nData.text = data.Text().ToString();
                nData.alt_url = data.Attr("href").ToString();
                nData.url = "";
                CQ img = article.Find("img").Eq(0);
                nData.url = img.Attr("data-img-src");
                newsList.Add(nData);
            }
            ViewBag.newsList = newsList;
            return View();
        }

        public ActionResult getDetail(string url)
        {
            CQ dom = CQ.CreateFromUrl("http://cnbc.com/"+url );
            ViewBag.title = dom.Find("h1").Eq(0).Text();

            string article = dom.Find("#article_body").Eq(0).RenderSelection().ToString();
            if ( article.Length == 0)
            {
                article = dom.Find(".cnbc-body").Eq(0).RenderSelection().ToString();
            }
            ViewBag.text = article;
            return View();
        }
    }
}