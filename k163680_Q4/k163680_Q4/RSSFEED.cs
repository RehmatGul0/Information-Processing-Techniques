using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace k163680_Q4
{
    internal class RssFeed
    {
        private WebClient client;
        private HtmlDocument html;
        public RssFeed()
        {
            client = new WebClient();
            html = new HtmlDocument();
        }

        public List<NewsItem> generteRssfeed(String newsSite)
        {
            if (newsSite == ConfigurationManager.AppSettings.Get("NewsChannelAry"))
            {
                return getNewsItemsAry(ConfigurationManager.AppSettings.Get("AryNewsUrl"));
            }
            else if (newsSite == ConfigurationManager.AppSettings.Get("NewsChannelBol"))
            {
                return getNewsItemsBol(ConfigurationManager.AppSettings.Get("BolNewsUrl"));
            }
            return new List<NewsItem>();
        }

        private List<NewsItem> getNewsItemsAry(String newsChannelUrl)
        {
            List<NewsItem> newsItems = new List<NewsItem>();
            html.LoadHtml(new StreamReader(client.OpenRead(newsChannelUrl)).ReadToEnd());
            HtmlNodeCollection nodes = html.DocumentNode.SelectNodes("//item");

            foreach (HtmlNode item in nodes)
            {
                Regex regexPattern1 = new Regex("(&.*?;)");
                Regex regexPattern2 = new Regex("(]]>)");

                String title = item.Element("title").InnerHtml;
                String pubdate = item.Element("pubdate").InnerHtml;
                DateTime publishDate = NewsItem.getPublishDate(pubdate);
                String description = regexPattern2.Replace(regexPattern1.Replace(item.Element("content:encoded").InnerText, ""), "");

                newsItems.Add(new NewsItem(title, description, publishDate, "Ary News"));
            }

            return newsItems;
        }

        private List<NewsItem> getNewsItemsBol(String newsChannelUrl)
        {
            List<NewsItem> newsItems = new List<NewsItem>();
            html.LoadHtml(new StreamReader(client.OpenRead(newsChannelUrl)).ReadToEnd());
            HtmlNodeCollection nodes = html.DocumentNode.SelectNodes("//item");

            foreach (HtmlNode item in nodes)
            {
                Regex regexPattern1 = new Regex("(&.*?;)");
                Regex regexPattern2 = new Regex("(]]>)");

                String title = item.Element("title").InnerHtml;
                String pubdate = item.Element("pubdate").InnerHtml;
                DateTime publishDate = NewsItem.getPublishDate(pubdate);
                String description = regexPattern2.Replace(regexPattern1.Replace(item.Element("description").InnerText.Replace("Continue reading", ""), ""), "");

                newsItems.Add(new NewsItem(title, description, publishDate, "Bol News"));
            }

            return newsItems;
        }
    }
}
