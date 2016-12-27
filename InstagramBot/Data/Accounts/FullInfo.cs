using System.Collections.Generic;

namespace InstagramBot.Data.Accounts
{
   public class FullInfo
    {
        public class P
        {
        }

        public class Ebd
        {
            public P p { get; set; }
            public string g { get; set; }
        }

        public class P2
        {
            public string use_continue_text { get; set; }
        }

        public class Us
        {
            public P2 p { get; set; }
            public string g { get; set; }
        }

        public class P3
        {
        }

        public class Feed
        {
            public P3 p { get; set; }
            public string g { get; set; }
        }

        public class P4
        {
            public string new_chaining_endpoint { get; set; }
        }

        public class Gql
        {
            public P4 p { get; set; }
            public string g { get; set; }
        }

        public class P5
        {
        }

        public class Freq
        {
            public P5 p { get; set; }
            public string g { get; set; }
        }

        public class P6
        {
        }

        public class Discovery
        {
            public P6 p { get; set; }
            public string g { get; set; }
        }

        public class P7
        {
            public string new_interstitial_design { get; set; }
        }

        public class UsLi
        {
            public P7 p { get; set; }
            public string g { get; set; }
        }

        public class P8
        {
        }

        public class Profile
        {
            public P8 p { get; set; }
            public string g { get; set; }
        }

        public class P9
        {
            public string can_phone { get; set; }
        }

        public class SuUniverse
        {
            public P9 p { get; set; }
            public string g { get; set; }
        }

        public class P10
        {
        }

        public class ActivityStories
        {
            public P10 p { get; set; }
            public string g { get; set; }
        }

        public class Qe
        {
            public Ebd ebd { get; set; }
            public Us us { get; set; }
            public Feed feed { get; set; }
            public Gql gql { get; set; }
            public Freq freq { get; set; }
            public Discovery discovery { get; set; }
            public UsLi us_li { get; set; }
            public Profile profile { get; set; }
            public SuUniverse su_universe { get; set; }
            public ActivityStories activity_stories { get; set; }
        }

        public class DisplayPropertiesServerGuess
        {
            public int viewport_width { get; set; }
            public double pixel_ratio { get; set; }
        }

        public class Follows
        {
            public int count { get; set; }
        }

        public class PageInfo
        {
            public bool has_previous_page { get; set; }
            public string end_cursor { get; set; }
            public bool has_next_page { get; set; }
            public string start_cursor { get; set; }
        }

        public class Owner
        {
            public string id { get; set; }
        }

        public class Comments
        {
            public int count { get; set; }
        }

        public class Likes
        {
            public int count { get; set; }
        }

        public class Dimensions
        {
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Node
        {
            public bool is_video { get; set; }
            public int date { get; set; }
            public string display_src { get; set; }
            public Owner owner { get; set; }
            public string caption { get; set; }
            public string code { get; set; }
            public string thumbnail_src { get; set; }
            public bool comments_disabled { get; set; }
            public Comments comments { get; set; }
            public string id { get; set; }
            public Likes likes { get; set; }
            public Dimensions dimensions { get; set; }
        }

        public class Media
        {
            public int count { get; set; }
            public PageInfo page_info { get; set; }
            public List<Node> nodes { get; set; }
        }

        public class FollowedBy
        {
            public int count { get; set; }
        }

        public class User
        {
            public string profile_pic_url { get; set; }
            public bool is_verified { get; set; }
            public string full_name { get; set; }
            public Follows follows { get; set; }
            public object connected_fb_page { get; set; }
            public bool is_private { get; set; }
            public object country_block { get; set; }
            public object external_url { get; set; }
            public bool followed_by_viewer { get; set; }
            public bool requested_by_viewer { get; set; }
            public object external_url_linkshimmed { get; set; }
            public string id { get; set; }
            public Media media { get; set; }
            public FollowedBy followed_by { get; set; }
            public string username { get; set; }
            public bool has_requested_viewer { get; set; }
            public string profile_pic_url_hd { get; set; }
            public bool has_blocked_viewer { get; set; }
            public bool follows_viewer { get; set; }
            public object biography { get; set; }
            public bool blocked_by_viewer { get; set; }
        }

        public class ProfilePage
        {
            public User user { get; set; }
        }

        public class EntryData
        {
            public List<ProfilePage> ProfilePage { get; set; }
        }

        public class ActivityCounts
        {
            public int comment_likes { get; set; }
            public int comments { get; set; }
            public int likes { get; set; }
            public int relationships { get; set; }
            public int usertags { get; set; }
        }

        public class Gatekeepers
        {
            public bool sms { get; set; }
        }

        public class Viewer
        {
            public string profile_pic_url { get; set; }
            public object external_url { get; set; }
            public bool has_profile_pic { get; set; }
            public string id { get; set; }
            public string username { get; set; }
            public string full_name { get; set; }
            public object biography { get; set; }
            public string profile_pic_url_hd { get; set; }
        }

        public class Config
        {
            public string csrf_token { get; set; }
            public Viewer viewer { get; set; }
        }

        public class RootObject
        {
            public Qe qe { get; set; }
            public string platform { get; set; }
            public string hostname { get; set; }
            public DisplayPropertiesServerGuess display_properties_server_guess { get; set; }
            public bool environment_switcher_visible_server_guess { get; set; }
            public EntryData entry_data { get; set; }
            public string country_code { get; set; }
            public ActivityCounts activity_counts { get; set; }
            public string static_root { get; set; }
            public Gatekeepers gatekeepers { get; set; }
            public Config config { get; set; }
            public bool show_app_install { get; set; }
            public string language_code { get; set; }
        }

    }
}
