using CsQuery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewParser.Controllers
{
    public class CnbcArabiaController : Controller
    {
        // GET: CnbcArabia
        public ActionResult Index()
        {
            ArrayList newsList = new ArrayList();
            CQ dom = CQ.CreateFromUrl("http://www.cnbcarabia.com/news/latest");
            CQ mainArticle = dom.Find("div.blog-news.clearfix");
            for (int i = 0; i < mainArticle.Length; i++)
            {
                CQ article = mainArticle.Children().Eq(i);
                CQ data = article.Find(".blog-box-title").Eq(0).Children("a").Eq(0);
                newsData nData = new newsData();
                nData.text = data.Text().ToString();
                nData.alt_url = "http://www.cnbcarabia.com" + data.Attr("href").ToString();
                nData.url = "";
                CQ img = article.Find("img").Eq(0);
                nData.url ="http://www.cnbcarabia.com" + img.Attr("src");
                newsList.Add(nData);
            }
            ViewBag.newsList = newsList;
            return View();
        }
        public ActionResult getDetail(string url)
        {
            CQ dom = CQ.CreateFromUrl(url);
            CQ article = dom.Find(".col-xs-12.col-md-8").Eq(0);
            ViewBag.aTitle = article.Find(".article-title").Text();
            ViewBag.aImg = "http://cnbcarabia.com/" + article.Find("img").Eq(0).Attr("src").ToString();
            ViewBag.aText = article.Find(".article-content").Eq(0).RenderSelection().ToString();
            return View();
        }
    }
}