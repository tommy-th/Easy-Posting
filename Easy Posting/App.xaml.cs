using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using Easy_Posting.Content;

namespace Easy_Posting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //id 콤보리스트 갱신
        public bool list_refresh = false;
        //첫 콤보리스트
        public bool first_list = false;
        //스킨 설정
        public bool first_skin = false;
        //구글 맵
        public string textStreet = "";
        public bool check_mapuse = false;
        //계정관리
        public bool check_first = false;
        public string first_id = "";
        public string blog_ad = "";        
        //파일 암호키
        public string sSecretKey = "";     
        //로그인된 계정 정보
        public string now_select_id_item = "";
        public string now_blog_ad = "";
        public string now_id = "";
        public string now_password = "";
        public string now_api_id = "";
        public string now_api_key = "";
        public string now_blog_service = "";
        public bool check_account_select = false;
        //코드 하일라이터 입력
        public string coderesult = "";
        //유투브 관련 변수
        public string you_url = "";
        public string thum_url = "";
        public bool check_video_insert = false;
        public List<YouTubeInfo> MyList = null;
        public List<YouTubeInfo> reMyList = null;
        //레이아웃
        public string layout = "";
        public bool check_layout_insert = false;
        //이미지 검색
        public string image_url = "";
    }
}
