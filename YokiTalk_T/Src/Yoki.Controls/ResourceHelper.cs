using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Controls
{
    public class ResourceHelper
    {
        private static System.Resources.ResourceManager _resourceManager = new System.Resources.ResourceManager("Yoki.Controls.Properties.Resources", typeof(Yoki.Controls.Properties.Resources).Assembly);
        public static System.Resources.ResourceManager Resourcemanager
        {
            get
            {
                return _resourceManager;
            }
        }


        private static Bitmap _arrowLeft = null;
        public static Bitmap ArrowLeft
        {
            get
            {
                if (_arrowLeft == null)
                {
                    _arrowLeft = (Bitmap)Resourcemanager.GetObject("arrowLeft", null);
                }
                //Resourcemanager.ReleaseAllResources();
                return _arrowLeft;
            }
        }


        private static Bitmap _arrowRight = null;
        public static Bitmap ArrowRight
        {
            get
            {
                if (_arrowRight == null)
                {
                    _arrowRight = (Bitmap)Resourcemanager.GetObject("arrowRight", null);
                }
                //Resourcemanager.ReleaseAllResources();
                return _arrowRight;
            }
        }



        private static Bitmap _expansion = null;
        public static Bitmap Expansion
        {
            get
            {
                if (_expansion == null)
                {
                    _expansion = (Bitmap)Resourcemanager.GetObject("expansion", null);
                }
                //Resourcemanager.ReleaseAllResources();
                return _expansion;
            }
        }

        private static Bitmap _noPendingTopic = null;
        public static Bitmap NoPendingTopic
        {
            get
            {
                if (_noPendingTopic == null)
                {
                    _noPendingTopic = (Bitmap)Resourcemanager.GetObject("noPendingTopic", null);
                }
                //Resourcemanager.ReleaseAllResources();
                return _noPendingTopic;
            }
        }


        private static Bitmap _loadingGIF = null;
        public static Bitmap LoadingGIF
        {
            get
            {
                if (_loadingGIF == null)
                {
                    _loadingGIF = (Bitmap)Resourcemanager.GetObject("loadingGIF", null);
                }
                //Resourcemanager.ReleaseAllResources();
                return _loadingGIF;
            }
        }


        private static Bitmap _orderIsCommingGIF = null;
        public static Bitmap OrderIsCommingGIF
        {
            get
            {
                if (_orderIsCommingGIF == null)
                {
                    _orderIsCommingGIF = (Bitmap)Resourcemanager.GetObject("orderIsComing", null);
                }
                //Resourcemanager.ReleaseAllResources();
                return _orderIsCommingGIF;
            }
        }


        private static Bitmap _orderPuase = null;
        public static Bitmap OrderPuase
        {
            get
            {
                if (_orderPuase == null)
                {
                    _orderPuase = (Bitmap)Resourcemanager.GetObject("orderPuase", null);
                }
                //Resourcemanager.ReleaseAllResources();
                return _orderPuase;
            }
        }

        private static Bitmap _switchONMsg = null;
        public static Bitmap SwitchONMsg
        {
            get
            {
                if (_switchONMsg == null)
                {
                    _switchONMsg = (Bitmap)Resourcemanager.GetObject("switchONMsg", null);
                }
                //Resourcemanager.ReleaseAllResources();
                return _switchONMsg;
            }
        }


        private static Bitmap _genderGirl = null;
        public static Bitmap GenderGirl
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("gender_girl");
                    //Resourcemanager.ReleaseAllResources();
                    _genderGirl = bitmap;
                }
                return _genderGirl;
            }
        }

        private static Bitmap _genderBoy = null;
        public static Bitmap GenderBoy
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("gender_boy");
                    //Resourcemanager.ReleaseAllResources();
                    _genderBoy = bitmap;
                }
                return _genderBoy;
            }
        }

        private static Bitmap _genderUnknown = null;
        public static Bitmap GenderUnknown
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("gender_unknown");
                    //Resourcemanager.ReleaseAllResources();
                    _genderUnknown = bitmap;
                }
                return _genderUnknown;
            }
        }
        
        private static Bitmap _classRoomTemp = null;
        public static Bitmap ClassRoomTemp
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("classRoom_temp");
                    //Resourcemanager.ReleaseAllResources();
                    _classRoomTemp = bitmap;
                }
                return _classRoomTemp;
            }
        }
        private static Bitmap _classRoomSelected = null;
        /// <summary>
        /// 菜单按钮1选中图片
        /// </summary>
        public static Bitmap ClassRoomSelected
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("classRoom_temp");
                    //Resourcemanager.ReleaseAllResources();
                    _classRoomSelected = bitmap;
                }
                return _classRoomSelected;
            }
        }
        private static Bitmap _classRoomNo = null;
        /// <summary>
        /// 菜单按钮1未选图片
        /// </summary>
        public static Bitmap ClassRoomNo
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("classRoom");
                    //Resourcemanager.ReleaseAllResources();
                    _classRoomNo = bitmap;
                }
                return _classRoomNo;
            }
        }
        private static Bitmap _menuSettings = null;
        /// <summary>
        /// 菜单按钮设置图片
        /// </summary>
        public static Bitmap MenuSettings
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("menuSettings");
                    //Resourcemanager.ReleaseAllResources();
                    _menuSettings = bitmap;
                }
                return _menuSettings;
            }
        }

    }
}
