using System.Collections.Generic;

namespace InstagramBot.Data.Accounts
{
    public class Follows
    {
        public int count { get; set; }
    }

    public class Dimensions
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Comments
    {
        public int count { get; set; }
    }

    public class Likes
    {
        public int count { get; set; }
    }

    public class Owner
    {
        public string id { get; set; }
    }

    public class Node
    {
        public int date { get; set; }
        public bool comments_disabled { get; set; }
        public Dimensions dimensions { get; set; }
        public string code { get; set; }
        public Comments comments { get; set; }
        public Likes likes { get; set; }
        public string id { get; set; }
        public string thumbnail_src { get; set; }
        public Owner owner { get; set; }
        public string caption { get; set; }
        public string display_src { get; set; }
        public bool is_video { get; set; }
    }

    public class PageInfo
    {
        public bool has_next_page { get; set; }
        public bool has_previous_page { get; set; }
        public string start_cursor { get; set; }
        public string end_cursor { get; set; }
    }

    public class Media
    {
        public int count { get; set; }
        public List<Node> nodes { get; set; }
        public PageInfo page_info { get; set; }
    }

    public class FollowedBy
    {
        public int count { get; set; }
    }

    public class User
    {
        public bool blocked_by_viewer { get; set; }
        public string profile_pic_url { get; set; }
        public bool has_blocked_viewer { get; set; }
        public bool follows_viewer { get; set; }
        public bool is_verified { get; set; }
        public object country_block { get; set; }
        public bool has_requested_viewer { get; set; }
        public string id { get; set; }
        public string full_name { get; set; }
        public Follows follows { get; set; }
        public object external_url { get; set; }
        public string profile_pic_url_hd { get; set; }
        public bool requested_by_viewer { get; set; }
        public string username { get; set; }
        public object external_url_linkshimmed { get; set; }
        public object connected_fb_page { get; set; }
        public Media media { get; set; }
        public bool followed_by_viewer { get; set; }
        public object biography { get; set; }
        public bool is_private { get; set; }
        public FollowedBy followed_by { get; set; }
    }

    public class RootObject
    {
        public User user { get; set; }
    }

}