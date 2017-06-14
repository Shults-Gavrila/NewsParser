using CsQuery;
using System.Collections;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;

namespace NewParser.Controllers
{
    public class newsData
    {
        public string url;
        public string text;
        public string alt_url;
    }

    public class NewsParseController : Controller
    {
        // GET: NewsParse
        public ActionResult Index()
        {
            ArrayList newsList = new ArrayList();
        
            WebClient webClient = new WebClient();
            const string strUrl = "http://www.edition.cnn.com/world";
            string pageContent = webClient.DownloadString(strUrl);
            CQ dom = pageContent;
            CQ mainStory = dom["ul.cn.cn-list-hierarchical-xs.cn--idx-0"].Eq(0);

            for ( int i =0; i< mainStory.Children().Length; i++)
            {
                CQ news = mainStory.Children().Eq(i).Children().Eq(0);
                string url = "";
                string alt_url = "";
                if (news.Find("a").Length != 0)
                {
                    alt_url = news.Find("a").Attr("href").ToString();
                }
                if (news.Find("img").Length != 0)
                {
                    url = news.Find("img").Attr("data-src-large").ToString();
                }

                string text = news.Find("span.cd__headline-text").Text();
                newsData nData = new newsData();
                nData.url = url;
                nData.text = text;
                nData.alt_url = alt_url;
                newsList.Add(nData);
            }
            CQ lastStory = dom["ul.cn.cn-list-hierarchical-xs.cn--idx-0"].Eq(1);
            for (int i = 0; i < lastStory.Children().Length; i++)
            {
                CQ news = lastStory.Children("article").Eq(i).Children().Eq(0);
                string alt_url = "";
                if (news.Find("a").Length != 0)
                {
                    alt_url = news.Find("a").Attr("href").ToString();
                }
                string url = "";
                if (news.Find("img").Length != 0)
                {
                    url = news.Find("img").Attr("data-src-large").ToString();
                }

                string text = news.Find("span.cd__headline-text").Text();
                newsData nData = new newsData();
                nData.url = url;
                nData.alt_url = alt_url;
                nData.text = text;
                newsList.Add(nData);
            }
            ViewBag.newsList = newsList;
            return View();
        }
        public ActionResult getDetail(string url)
        {
            string str_url = "http://edition.cnn.com" + url;
            CQ dom = CQ.CreateFromUrl(str_url);
            dom = dom.Find("article.pg-rail-tall").Eq(0);
            CQ article = dom.Children("div.l-container").Eq(0);
            article = article.Remove("script");
            string aTitle = article.Children("h1").Eq(0).Text();
            string aText = article.Find("div.pg-rail-tall__body").Eq(0).Children("section").Eq(0).Children("div").Eq(0).RenderSelection().ToString();
            CQ mImg = article.Find("div#large-media_0--thumbnail").Eq(0);
            string aImg = "";
            if ( mImg.Length != 0)
            {
                aImg = mImg.Children("img").Eq(0).Attr("src").ToString();
            }
            ViewBag.aTitle = aTitle;
            ViewBag.aText = aText;
            ViewBag.aImg = aImg;
            return View();
        }
    }
}