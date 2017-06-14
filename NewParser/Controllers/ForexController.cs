using CsQuery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewParser.Controllers
{

    public class ForexController : Controller
    {
        // GET: Forex
        public ActionResult Index()
        {
            ArrayList newsList = new ArrayList();
            CQ dom = CQ.CreateFromUrl("http://www.financemagnates.com/forex");
            CQ mainArticle = dom.Find("ul.featured_cats_wrapper").Eq(0);
            for ( int i = 0; i < mainArticle.Children().Length; i++)
            {
                CQ article = mainArticle.Children().Eq(i).Find("a").Eq(0);
                newsData nData = new newsData();
                nData.text = article.Text();
                nData.alt_url = article.Attr("href").ToString();
                nData.url = "";
                newsList.Add(nData);
            }
            CQ addArticle = dom.Find("div.sub-feature").Eq(0);
            newsData firstNews = new newsData();
            CQ first = addArticle.Children("article").Eq(0);
            firstNews.text = first.Find("a").Eq(0).Find("div").Eq(0).Text();
            firstNews.url = first.Find("a").Eq(0).Css("background-image");
            firstNews.alt_url = first.Find("a").Eq(0).Attr("href").ToString();
            newsList.Add(firstNews);

            newsData secondNews = new newsData();
            CQ second = addArticle.Children("article").Eq(1);
            secondNews.text = second.Find("a").Eq(0).Find("div").Eq(0).Text();
            secondNews.url = second.Find("a").Eq(0).Css("background-image");
            secondNews.alt_url = second.Find("a").Eq(0).Attr("href").ToString();
            newsList.Add(secondNews);

            ViewBag.newsList = newsList;
            return View();
        }
        public ActionResult getDetail(string url)
        {
            CQ dom = CQ.CreateFromUrl(url);
            CQ article = dom.Find("article.post").Eq(0);
            string aMainText = article.Children("header").Eq(0).Children("h1").Eq(0).Text();
            string aSubText = article.Children("header").Eq(0).Children("div").Eq(0).Text();

            CQ aMain = article.Children("div.entry-content").Eq(0);

            string hText = aMain.RenderSelection().ToString();

            ViewBag.aMainText = aMainText;
            ViewBag.aSubText = aSubText;
            ViewBag.hText = hText;
            return View();
        }
    }
}