using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;

namespace Easy_Posting.Content{

    /// Uses XLINQ to create a List<see cref="YouTubeInfo">YouTubeInfo</see> using an Rss feed.
    /// 
    /// The following links are useful information regarding how the YouTube API works 
    /// 
    /// Example url
    ///
    /// http://gdata.youtube.com/feeds/api/videos?q=football+-soccer&alt=rss&orderby=published&start-index=11&max-results=10&v=1
    ///
    ///
    /// API Notes
    /// http://code.google.com/apis/youtube/2.0/developers_guide_protocol_api_query_parameters.html
    /// </summary>
    public class YouTubeProvider
    {
        #region Data
        private const string SEARCH = "http://gdata.youtube.com/feeds/api/videos?q={0}&alt=rss&&max-results=50&v=1";
        #endregion

        #region Load Videos From Feed
        /// <summary>
        /// Returns a List<see cref="YouTubeInfo">YouTubeInfo</see> which represent
        /// the YouTube videos that matched the keyWord input parameter
        /// </summary>
        /// 

        /// Call http://gdata.youtube.com/feeds/api/videos/ylLzyHk54Z0.
        /// In this XML file, read the value of the <title> tag.


        public static List<YouTubeInfo> LoadVideosKey(string keyWord)
        {
            try
            {
                var xraw = XElement.Load(string.Format(SEARCH, keyWord));
                var xroot = XElement.Parse(xraw.ToString());
                var links = (from item in xroot.Element("channel").Descendants("item")
                             select new YouTubeInfo
                             {
                                 EmbedUrl = GetEmbedUrlFromLink(item.Element("link").Value),
                                 ThumbNailUrl = GetThumbNailUrlFromLink(item),
                                 ViewerUrl = GetViewerUrlFromLink(item.Element("link").Value),
                                 title = item.Element("title").Value,
                                 description = item.Element("description").Value
                             }).Take(50);

                return links.ToList<YouTubeInfo>();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR");
            }
            return null;
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Simple helper methods that tunrs a link string into a embed string
        /// for a YouTube item. 
        /// turns 
        /// http://www.youtube.com/watch?v=hV6B7bGZ0_E
        /// into
        /// http://www.youtube.com/embed/hV6B7bGZ0_E
        /// </summary>
        private static string GetEmbedUrlFromLink(string link)
        {
            try
            {
                string embedUrl = link.Substring(0, link.IndexOf("&")).Replace("watch?v=", "embed/");
                return embedUrl;
            }
            catch
            {
                return link;
            }
        }

        private static string GetViewerUrlFromLink(string link)
        {
            try
            {
                string viewerUrl = link.Substring(0, link.IndexOf("&")).Replace("watch?v=", "v/");
                return viewerUrl;
            }
            catch
            {
                return link;
            }
        }


        private static string GetThumbNailUrlFromLink(XElement element)
        {

            XElement group = null;
            string thumbnailUrl = @"../Images/logo.png";

            foreach (XElement desc in element.Descendants())
            {
                if (desc.Name.LocalName == "group")
                {
                    group = desc;
                    break;
                }
            }

            if (group != null)
            {
                foreach (XElement desc in group.Descendants())
                {
                    if (desc.Name.LocalName == "thumbnail")
                    {
                        thumbnailUrl = desc.Attribute("url").Value.ToString();
                        break;
                    }
                }
            }

            return thumbnailUrl;
        }

        #endregion
    }
}
