using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace k163680_Q4
{
    public class NewsItem
    {
        private String title;
        private String description;
        private DateTime publishDate;
        private String newsChannel;
        public NewsItem(String title , String description , DateTime publishDate , String newsChannel)
        {
            this.title = title;
            this.description = description;
            this.publishDate = publishDate;
            this.newsChannel = newsChannel;
        }
        public String TITLE
        {
            get { return this.title; }
        }
        public String DESCRIPTION
        {
            get { return this.description; }
        }
        public DateTime PUBLISHEDDATE
        {
            get { return this.publishDate; }
        }
        public String NEWSCHANNEL
        {
            get { return this.newsChannel; }
        }
        public static List<NewsItem> mergeNewsItemsList(List<NewsItem> list1, List<NewsItem> list2)
        {
            List<NewsItem> temp = new List<NewsItem>(list1);
            temp = temp.Concat(list2).ToList();
            return temp;
        }

        public static DateTime getPublishDate(String publishDate)
        {
            return DateTime.Parse(publishDate.Substring(publishDate.IndexOf(',') + 2, publishDate.IndexOf('+') - publishDate.IndexOf(',') - 2));
        }

        public static void sortNewsItemsList(List<NewsItem> feed)
        {
            feed = feed.OrderByDescending(newsItem => newsItem.publishDate).ToList();
        }
    }
}
