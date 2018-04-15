using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramBot.Data.Accounts
{
    public class Info
    {
        public class ActivityCounts
        {
            public int comment_likes { get; set; }
            public int comments { get; set; }
            public int likes { get; set; }
            public int relationships { get; set; }
            public int usertags { get; set; }
        }

        public class Viewer
        {
            public string biography { get; set; }
            public object external_url { get; set; }
            public string full_name { get; set; }
            public bool has_profile_pic { get; set; }
            public string id { get; set; }
            public string profile_pic_url { get; set; }
            public string profile_pic_url_hd { get; set; }
            public string username { get; set; }
        }

        public class Config
        {
            public string csrf_token { get; set; }
            public Viewer viewer { get; set; }
        }

        public class EdgeFollowedBy
        {
            public int count { get; set; }
        }

        public class EdgeFollow
        {
            public int count { get; set; }
        }

        public class MutualFollowers
        {
            public int additional_count { get; set; }
            public List<object> usernames { get; set; }
        }

        public class PageInfo
        {
            public bool has_next_page { get; set; }
            public string end_cursor { get; set; }
        }

        public class Node2
        {
            public string text { get; set; }
        }

        public class Edge2
        {
            public Node2 node { get; set; }
        }

        public class EdgeMediaToCaption
        {
            public List<Edge2> edges { get; set; }
        }

        public class EdgeMediaToComment
        {
            public int count { get; set; }
        }

        public class Dimensions
        {
            public int height { get; set; }
            public int width { get; set; }
        }

        public class EdgeLikedBy
        {
            public int count { get; set; }
        }

        public class EdgeMediaPreviewLike
        {
            public int count { get; set; }
        }

        public class Owner
        {
            public string id { get; set; }
        }

        public class ThumbnailResource
        {
            public string src { get; set; }
            public int config_width { get; set; }
            public int config_height { get; set; }
        }

        public class Node
        {
            public string __typename { get; set; }
            public string id { get; set; }
            public EdgeMediaToCaption edge_media_to_caption { get; set; }
            public string shortcode { get; set; }
            public EdgeMediaToComment edge_media_to_comment { get; set; }
            public bool comments_disabled { get; set; }
            public int taken_at_timestamp { get; set; }
            public Dimensions dimensions { get; set; }
            public string display_url { get; set; }
            public EdgeLikedBy edge_liked_by { get; set; }
            public EdgeMediaPreviewLike edge_media_preview_like { get; set; }
            public object gating_info { get; set; }
            public string media_preview { get; set; }
            public Owner owner { get; set; }
            public string thumbnail_src { get; set; }
            public List<ThumbnailResource> thumbnail_resources { get; set; }
            public bool is_video { get; set; }
        }

        public class Edge
        {
            public Node node { get; set; }
        }

        public class EdgeOwnerToTimelineMedia
        {
            public int count { get; set; }
            public PageInfo page_info { get; set; }
            public List<Edge> edges { get; set; }
        }

        public class PageInfo2
        {
            public bool has_next_page { get; set; }
            public object end_cursor { get; set; }
        }

        public class EdgeSavedMedia
        {
            public int count { get; set; }
            public PageInfo2 page_info { get; set; }
            public List<object> edges { get; set; }
        }

        public class PageInfo3
        {
            public bool has_next_page { get; set; }
            public object end_cursor { get; set; }
        }

        public class EdgeMediaCollections
        {
            public int count { get; set; }
            public PageInfo3 page_info { get; set; }
            public List<object> edges { get; set; }
        }

        public class User
        {
            public string biography { get; set; }
            public bool blocked_by_viewer { get; set; }
            public bool country_block { get; set; }
            public string external_url { get; set; }
            public string external_url_linkshimmed { get; set; }
            public EdgeFollowedBy edge_followed_by { get; set; }
            public bool followed_by_viewer { get; set; }
            public EdgeFollow edge_follow { get; set; }
            public bool follows_viewer { get; set; }
            public string full_name { get; set; }
            public bool has_blocked_viewer { get; set; }
            public bool has_requested_viewer { get; set; }
            public string id { get; set; }
            public bool is_private { get; set; }
            public bool is_verified { get; set; }
            public MutualFollowers mutual_followers { get; set; }
            public string profile_pic_url { get; set; }
            public string profile_pic_url_hd { get; set; }
            public bool requested_by_viewer { get; set; }
            public string username { get; set; }
            public object connected_fb_page { get; set; }
            public EdgeOwnerToTimelineMedia edge_owner_to_timeline_media { get; set; }
            public EdgeSavedMedia edge_saved_media { get; set; }
            public EdgeMediaCollections edge_media_collections { get; set; }
        }

        public class Graphql
        {
            public User user { get; set; }
        }

        public class ProfilePage
        {
            public string logging_page_id { get; set; }
            public bool show_suggested_profiles { get; set; }
            public Graphql graphql { get; set; }
        }

        public class EntryData
        {
            public List<ProfilePage> ProfilePage { get; set; }
        }

        public class Gatekeepers
        {
            public bool ld { get; set; }
            public bool vl { get; set; }
            public bool seo { get; set; }
            public bool seoht { get; set; }
        }

        public class Knobs
        {
          
        }

        public class P
        {
        }

        public class DashForVod
        {
            public string g { get; set; }
            public P p { get; set; }
        }

        public class P2
        {
        }

        public class Aysf
        {
            public string g { get; set; }
            public P2 p { get; set; }
        }

        public class P3
        {
            public string threeline { get; set; }
        }

        public class Bc3l
        {
            public string g { get; set; }
            public P3 p { get; set; }
        }

        public class P4
        {
        }

        public class CommentReporting
        {
            public string g { get; set; }
            public P4 p { get; set; }
        }

        public class P5
        {
        }

        public class DirectReporting
        {
            public string g { get; set; }
            public P5 p { get; set; }
        }

        public class P6
        {
        }

        public class Reporting
        {
            public string g { get; set; }
            public P6 p { get; set; }
        }

        public class P7
        {
        }

        public class MediaReporting
        {
            public string g { get; set; }
            public P7 p { get; set; }
        }

        public class P8
        {
        }

        public class AccRecoveryLink
        {
            public string g { get; set; }
            public P8 p { get; set; }
        }

        public class P9
        {
        }

        public class Notif
        {
            public string g { get; set; }
            public P9 p { get; set; }
        }

        public class P10
        {
        }

        public class DrctNav
        {
            public string g { get; set; }
            public P10 p { get; set; }
        }

        public class P11
        {
        }

        public class PlPivotLi
        {
            public string g { get; set; }
            public P11 p { get; set; }
        }

        public class P12
        {
            public string show_pivot { get; set; }
        }

        public class PlPivotLo
        {
            public string g { get; set; }
            public P12 p { get; set; }
        }

        public class P13
        {
        }

        public class __invalid_type__404AsReact
        {
            public string g { get; set; }
            public P13 p { get; set; }
        }

        public class P14
        {
        }

        public class AccRecovery
        {
            public string g { get; set; }
            public P14 p { get; set; }
        }

        public class P15
        {
            public string is_enabled { get; set; }
        }

        public class ClientGql
        {
            public string g { get; set; }
            public P15 p { get; set; }
        }

        public class P16
        {
        }

        public class Collections
        {
            public string g { get; set; }
            public P16 p { get; set; }
        }

        public class P17
        {
        }

        public class CommentTa
        {
            public string g { get; set; }
            public P17 p { get; set; }
        }

        public class P18
        {
            public string has_suggestion_context_in_feed { get; set; }
        }

        public class Connections
        {
            public string g { get; set; }
            public P18 p { get; set; }
        }

        public class P19
        {
            public string has_follow_all_button { get; set; }
            public string has_pagination { get; set; }
        }

        public class DiscPpl
        {
            public string g { get; set; }
            public P19 p { get; set; }
        }

        public class P20
        {
        }

        public class Embeds
        {
            public string g { get; set; }
            public P20 p { get; set; }
        }

        public class P21
        {
        }

        public class EbdsimLi
        {
            public string g { get; set; }
            public P21 p { get; set; }
        }

        public class P22
        {
        }

        public class EbdsimLo
        {
            public string g { get; set; }
            public P22 p { get; set; }
        }

        public class P23
        {
        }

        public class Es6
        {
            public string g { get; set; }
            public P23 p { get; set; }
        }

        public class P24
        {
        }

        public class ExitStoryCreation
        {
            public string g { get; set; }
            public P24 p { get; set; }
        }

        public class P25
        {
        }

        public class Fs
        {
            public string g { get; set; }
            public P25 p { get; set; }
        }

        public class P26
        {
        }

        public class GdprLoggedOut
        {
            public string g { get; set; }
            public P26 p { get; set; }
        }

        public class P27
        {
        }

        public class Appsell
        {
            public string g { get; set; }
            public P27 p { get; set; }
        }

        public class P28
        {
        }

        public class Imgopt
        {
            public string g { get; set; }
            public P28 p { get; set; }
        }

        public class P29
        {
            public string is_inline { get; set; }
        }

        public class FollowButton
        {
            public string g { get; set; }
            public P29 p { get; set; }
        }

        public class P30
        {
            public string new_cta { get; set; }
            public string remove_upsell_banner { get; set; }
            public string update_nav { get; set; }
        }

        public class Loggedout
        {
            public string g { get; set; }
            public P30 p { get; set; }
        }

        public class P31
        {
            public string has_new_loggedout_upsell_content { get; set; }
        }

        public class LoggedoutUpsell
        {
            public string g { get; set; }
            public P31 p { get; set; }
        }

        public class P32
        {
        }

        public class UsLi
        {
            public string g { get; set; }
            public P32 p { get; set; }
        }

        public class P33
        {
        }

        public class Msisdn
        {
            public string g { get; set; }
            public P33 p { get; set; }
        }

        public class P34
        {
        }

        public class BgSync
        {
            public string g { get; set; }
            public P34 p { get; set; }
        }

        public class P35
        {
        }

        public class Onetaplogin
        {
            public string g { get; set; }
            public P35 p { get; set; }
        }

        public class P36
        {
        }

        public class OnetaploginUserbased
        {
            public string g { get; set; }
            public P36 p { get; set; }
        }

        public class P37
        {
        }

        public class LoginPoe
        {
            public string g { get; set; }
            public P37 p { get; set; }
        }

        public class P38
        {
        }

        public class PrvcyTggl
        {
            public string g { get; set; }
            public P38 p { get; set; }
        }

        public class P39
        {
            public string show_lock_ui { get; set; }
        }

        public class PrivateLo
        {
            public string g { get; set; }
            public P39 p { get; set; }
        }

        public class P40
        {
            public string prefill_photo { get; set; }
            public string skip_nux { get; set; }
        }

        public class ProfilePhotoNuxFbcV2
        {
            public string g { get; set; }
            public P40 p { get; set; }
        }

        public class P41
        {
        }

        public class PushNotifications
        {
            public string g { get; set; }
            public P41 p { get; set; }
        }

        public class P42
        {
        }

        public class Reg
        {
            public string g { get; set; }
            public P42 p { get; set; }
        }

        public class P43
        {
            public string hide_value_prop { get; set; }
        }

        public class RegVp
        {
            public string g { get; set; }
            public P43 p { get; set; }
        }

        public class P44
        {
            public string is_hidden { get; set; }
        }

        public class FeedVp
        {
            public string g { get; set; }
            public P44 p { get; set; }
        }

        public class P45
        {
        }

        public class ReportHaf
        {
            public string g { get; set; }
            public P45 p { get; set; }
        }

        public class P46
        {
            public string is_enabled { get; set; }
        }

        public class ReportMedia
        {
            public string g { get; set; }
            public P46 p { get; set; }
        }

        public class P47
        {
        }

        public class ReportProfile
        {
            public string g { get; set; }
            public P47 p { get; set; }
        }

        public class P48
        {
            public string is_enabled { get; set; }
        }

        public class Save
        {
            public string g { get; set; }
            public P48 p { get; set; }
        }

        public class P49
        {
        }

        public class Sidecar
        {
            public string g { get; set; }
            public P49 p { get; set; }
        }

        public class P50
        {
            public string use_autocomplete_login { get; set; }
        }

        public class SuUniverse
        {
            public string g { get; set; }
            public P50 p { get; set; }
        }

        public class P51
        {
        }

        public class Stale
        {
            public string g { get; set; }
            public P51 p { get; set; }
        }

        public class P52
        {
        }

        public class StoriesLo
        {
            public string g { get; set; }
            public P52 p { get; set; }
        }

        public class P53
        {
            public string stories_permalink { get; set; }
        }

        public class Stories
        {
            public string g { get; set; }
            public P53 p { get; set; }
        }

        public class P54
        {
        }

        public class TpPblshr
        {
            public string g { get; set; }
            public P54 p { get; set; }
        }

        public class P55
        {
        }

        public class Video
        {
            public string g { get; set; }
            public P55 p { get; set; }
        }

        public class P56
        {
        }

        public class GdprSettings
        {
            public string g { get; set; }
            public P56 p { get; set; }
        }

        public class P57
        {
        }

        public class GdprEuTos
        {
            public string g { get; set; }
            public P57 p { get; set; }
        }

        public class P58
        {
        }

        public class GdprRowTos
        {
            public string g { get; set; }
            public P58 p { get; set; }
        }

        public class Qe
        {
            public DashForVod dash_for_vod { get; set; }
            public Aysf aysf { get; set; }
            public Bc3l bc3l { get; set; }
            public CommentReporting comment_reporting { get; set; }
            public DirectReporting direct_reporting { get; set; }
            public Reporting reporting { get; set; }
            public MediaReporting media_reporting { get; set; }
            public AccRecoveryLink acc_recovery_link { get; set; }
            public Notif notif { get; set; }
            public DrctNav drct_nav { get; set; }
            public PlPivotLi pl_pivot_li { get; set; }
            public PlPivotLo pl_pivot_lo { get; set; }
            public __invalid_type__404AsReact __invalid_name__404_as_react { get; set; }
            public AccRecovery acc_recovery { get; set; }
            public ClientGql client_gql { get; set; }
            public Collections collections { get; set; }
            public CommentTa comment_ta { get; set; }
            public Connections connections { get; set; }
            public DiscPpl disc_ppl { get; set; }
            public Embeds embeds { get; set; }
            public EbdsimLi ebdsim_li { get; set; }
            public EbdsimLo ebdsim_lo { get; set; }
            public Es6 es6 { get; set; }
            public ExitStoryCreation exit_story_creation { get; set; }
            public Fs fs { get; set; }
            public GdprLoggedOut gdpr_logged_out { get; set; }
            public Appsell appsell { get; set; }
            public Imgopt imgopt { get; set; }
            public FollowButton follow_button { get; set; }
            public Loggedout loggedout { get; set; }
            public LoggedoutUpsell loggedout_upsell { get; set; }
            public UsLi us_li { get; set; }
            public Msisdn msisdn { get; set; }
            public BgSync bg_sync { get; set; }
            public Onetaplogin onetaplogin { get; set; }
            public OnetaploginUserbased onetaplogin_userbased { get; set; }
            public LoginPoe login_poe { get; set; }
            public PrvcyTggl prvcy_tggl { get; set; }
            public PrivateLo private_lo { get; set; }
            public ProfilePhotoNuxFbcV2 profile_photo_nux_fbc_v2 { get; set; }
            public PushNotifications push_notifications { get; set; }
            public Reg reg { get; set; }
            public RegVp reg_vp { get; set; }
            public FeedVp feed_vp { get; set; }
            public ReportHaf report_haf { get; set; }
            public ReportMedia report_media { get; set; }
            public ReportProfile report_profile { get; set; }
            public Save save { get; set; }
            public Sidecar sidecar { get; set; }
            public SuUniverse su_universe { get; set; }
            public Stale stale { get; set; }
            public StoriesLo stories_lo { get; set; }
            public Stories stories { get; set; }
            public TpPblshr tp_pblshr { get; set; }
            public Video video { get; set; }
            public GdprSettings gdpr_settings { get; set; }
            public GdprEuTos gdpr_eu_tos { get; set; }
            public GdprRowTos gdpr_row_tos { get; set; }
        }

        public class DisplayPropertiesServerGuess
        {
            public double pixel_ratio { get; set; }
            public int viewport_width { get; set; }
            public int viewport_height { get; set; }
            public string orientation { get; set; }
        }

        public class ZeroData
        {
        }

        public class NewInfo
        {
            public ActivityCounts activity_counts { get; set; }
            public Config config { get; set; }
            public bool supports_es6 { get; set; }
            public string country_code { get; set; }
            public string language_code { get; set; }
            public string locale { get; set; }
            public EntryData entry_data { get; set; }
            public Gatekeepers gatekeepers { get; set; }
            public Knobs knobs { get; set; }
            public Qe qe { get; set; }
            public string hostname { get; set; }
            public DisplayPropertiesServerGuess display_properties_server_guess { get; set; }
            public bool environment_switcher_visible_server_guess { get; set; }
            public string platform { get; set; }
            public string rhx_gis { get; set; }
            public string nonce { get; set; }
            public bool is_bot { get; set; }
            public ZeroData zero_data { get; set; }
            public string rollout_hash { get; set; }
            public string bundle_variant { get; set; }
            public bool probably_has_app { get; set; }
            public bool show_app_install { get; set; }
        }
    }
}
