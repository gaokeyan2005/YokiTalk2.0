using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.View
{
    public class ResourceHelper
    {
        private static System.Resources.ResourceManager _resourceManager = new System.Resources.ResourceManager("Yoki.View.Properties.Resources", typeof(Yoki.View.Properties.Resources).Assembly);
        public static System.Resources.ResourceManager Resourcemanager
        {
            get
            {
                return _resourceManager;
            }
        }
        private static Bitmap _settings = null;
        public static Bitmap Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = (Bitmap)Resourcemanager.GetObject("settings", null);
                }
                //Resourcemanager.ReleaseAllResources();
                return _settings;
            }
        }


        private static Bitmap _loadingAnimationBackground = null;
        public static Bitmap LoadingAnimationBackground
        {
            get
            {
                if (_loadingAnimationBackground == null)
                {
                    _loadingAnimationBackground = (Bitmap)Resourcemanager.GetObject("tooltipBackgound_168", null);
                }
                //Resourcemanager.ReleaseAllResources();
                return _loadingAnimationBackground;
            }
        }

        private static Bitmap[] _loadingAnimationFrames = null;
        public static Bitmap[] LoadingAnimationFrames
        {
            get
            {
                if (true)
                {
                    Queue<Bitmap> frameQueue = new Queue<Bitmap>();
                    for (int i = 0; i < 30; i++)
                    {
                        Bitmap frame = (Bitmap)Resourcemanager.GetObject("Preloader_8_" + i.ToString("00000"), null);
                        frameQueue.Enqueue(frame);
                    }
                    //Resourcemanager.ReleaseAllResources();
                    _loadingAnimationFrames = frameQueue.ToArray();
                }
                return _loadingAnimationFrames;
            }
        }


        private static Bitmap _noVideo = null;
        public static Bitmap NoVideo
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("noVideo");
                    //Resourcemanager.ReleaseAllResources();
                    _noVideo = bitmap;
                }
                return _noVideo;
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


        private static Bitmap _newOrder = null;
        public static Bitmap NewOrder
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("newOrder");
                    //Resourcemanager.ReleaseAllResources();
                    _newOrder = bitmap;
                }
                return _newOrder;
            }
        }

        private static Bitmap _closeWaring = null;
        public static Bitmap CloseWaring
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("closeWaring");
                    //Resourcemanager.ReleaseAllResources();
                    _closeWaring = bitmap;
                }
                return _closeWaring;
            }
        }

        private static Bitmap _evaluateStar = null;
        public static Bitmap EvaluateStar
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("evaluateStar");
                    //Resourcemanager.ReleaseAllResources();
                    _evaluateStar = bitmap;
                }
                return _evaluateStar;
            }
        }

        private static Bitmap _evaluateStarEmpty = null;
        public static Bitmap EvaluateStarEmpty
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("evaluateStar_empty");
                    //Resourcemanager.ReleaseAllResources();
                    _evaluateStarEmpty = bitmap;
                }
                return _evaluateStarEmpty;
            }
        }

        private static Bitmap _emptyHeader = null;
        public static Bitmap EmptyHeader
        {
            get
            {
                if (true)
                {
                    Bitmap bitmap = (Bitmap)Resourcemanager.GetObject("emptyHeader");
                    //Resourcemanager.ReleaseAllResources();
                    _emptyHeader = bitmap;
                }
                return _emptyHeader;
            }
        }


        private static System.IO.Stream _ringNewOrder = null;
        public static System.IO.Stream RingNewOrder
        {
            get
            {
                if (true)
                {
                    System.IO.Stream stream = Resourcemanager.GetStream("Ring_NewOrder");
                    //Resourcemanager.ReleaseAllResources();
                    _ringNewOrder = stream;
                }
                return _ringNewOrder;
            }
        }



    }
}
