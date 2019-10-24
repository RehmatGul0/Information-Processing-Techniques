using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Xml;

namespace k163680_Q4
{
    public partial class RssFeedService: ServiceBase
    {
        private System.Timers.Timer time = new System.Timers.Timer();
        private RssFeed rssFeed;
        StreamWriter logs;

        public RssFeedService()
        {
            InitializeComponent();
            rssFeed = new RssFeed();
            
            logs = new StreamWriter(Path.Combine(ConfigurationManager.AppSettings.Get("Path"), "logs.txt"));
        }

        protected override void OnStart(string[] args)
        {
            logs.WriteLine("Service has been started");
            time.Start();
            time.Interval = 300000;
            time.Elapsed += creeateFeedXml;
        }

        protected override void OnStop()
        {
            logs.Dispose();
            logs.Close();
        }

        public void creeateFeedXml(object sender, ElapsedEventArgs e)
        {
            try
            {
                List<NewsItem> aryNewsFeed = rssFeed.generteRssfeed(ConfigurationManager.AppSettings.Get("NewsChannelAry"));
                List<NewsItem> bolNewsFeed = rssFeed.generteRssfeed(ConfigurationManager.AppSettings.Get("NewsChannelBol"));

                List<NewsItem> feed = NewsItem.mergeNewsItemsList(aryNewsFeed, bolNewsFeed);
                NewsItem.sortNewsItemsList(feed);
                GenerateXml(feed);

                logs.WriteLine("News Feed File has been created successfully");
            }
            catch
            {
                logs.WriteLine("Exception have occureed");
            }

        }

        public void GenerateXml(List<NewsItem> feed)
        {
            XmlWriter writer = XmlWriter.Create(ConfigurationManager.AppSettings.Get("Path") + "/" + "NewsFeed.xml");
            writer.WriteStartElement("NewsItemsList");
            foreach (NewsItem newsItem in feed)
            {
                writer.WriteStartElement("NewsItem");
                writer.WriteElementString("Title", newsItem.TITLE);
                writer.WriteElementString("Description", newsItem.DESCRIPTION);
                writer.WriteElementString("PublishedDate", newsItem.PUBLISHEDDATE.ToString());
                writer.WriteElementString("NewsChannel", newsItem.NEWSCHANNEL);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
    }
}
