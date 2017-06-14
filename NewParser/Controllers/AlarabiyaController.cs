using CsQuery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewParser.Controllers
{
    public class AlarabiyaController : Controller
    {
        // GET: Alarabiya
        public ActionResult Index()
        {
            ArrayList newsList = new ArrayList();
            CQ dom = CQ.CreateFromUrl("http://www.alhurra.com/p/349.html");

            newsData topNews = new newsData();
            CQ tNews = dom.Find(".img-overlay").Eq(0).Find("a").Eq(0);
            if ( tNews.Length != 0)
            {
                topNews.alt_url = tNews.Attr("href").ToString();
                topNews.text = tNews.Attr("title").ToString();
            }
            CQ cImg = tNews.Find("img").Eq(0);
            if ( cImg.Length != 0)
            {
                topNews.url = cImg.Attr("data-src").ToString();
            }
            newsList.Add(topNews);

            CQ mainArticle = dom.Find("#wrowblock-145_12").Eq(0).Find("li");
            for (int i = 0; i < mainArticle.Length; i++)
            {
                CQ article = mainArticle.Eq(i).Find("a").Eq(0);
                newsData nData = new newsData();
                nData.text = article.Attr("title").ToString();
                nData.alt_url = article.Attr("href").ToString();
                nData.url = "";
                CQ img = article.Find("img").Eq(0);
                if (img.Length != 0) nData.url = img.Attr("data-src").ToString();
                newsList.Add(nData);
            }
            ViewBag.newsList = newsList;
            return View();
        }
        public ActionResult getDetail(string url)
        {
            CQ dom = CQ.CreateFromUrl("http://www.alhurra.com" + url);
            string aTitle = "";
            string aImg_url = "";
            string aText = "";

            CQ cTitle = dom.Find("h1.pg-title").Eq(0);
            if ( cTitle.Length !=0)
            {
                aTitle = cTitle.Text();
            }

            CQ cImg = dom.Find(".cover-media").Eq(0).Find("img").Eq(0);
            if ( cImg.Length != 0)
            {
                aImg_url = cImg.Attr("src").ToString();
            }

            CQ cText = dom.Find(".wsw").Eq(0);
            if ( cText.Length !=0)
            {
                aText = cText.RenderSelection().ToString();
            }

            ViewBag.aTitle = aTitle;
            ViewBag.aImg = aImg_url.Replace("_w250_r1","_w1023_r1");
            ViewBag.aText = aText;
            return View();
        }
    }
}