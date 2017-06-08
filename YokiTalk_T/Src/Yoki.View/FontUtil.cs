using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Yoki.View
{
    public class FontUtil
    {
        private static System.Drawing.Font _defaultFont = null;
        public static System.Drawing.Font DefaultFont
        {
            get
            {
                if (_defaultFont == null)
                {
                    _defaultFont = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }

                return _defaultFont;
            }
        }
        private static System.Drawing.Font _defaultMicroFont = null;
        public static System.Drawing.Font DefaultMicroFont
        {
            get
            {
                if (_defaultMicroFont == null)
                {
                    _defaultMicroFont = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }

                return _defaultMicroFont;
            }
        }

        private static System.Drawing.Font _defaultMicroBoldFont = null;
        public static System.Drawing.Font DefaultMicroBoldFont
        {
            get
            {
                if (_defaultMicroBoldFont == null)
                {
                    _defaultMicroBoldFont = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                }
                return _defaultMicroBoldFont;
            }
        }


        private static System.Drawing.Font _defaultBoldFont = null;
        public static System.Drawing.Font DefaultBoldFont
        {
            get
            {
                if (_defaultBoldFont == null)
                {
                    _defaultBoldFont = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                }
                return _defaultBoldFont;
            }
        }


        private static System.Drawing.Font _titleFont = null;
        public static System.Drawing.Font TitleFont
        {
            get
            {
                if (_titleFont == null)
                {
                    _titleFont = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                }

                return _titleFont;
            }
        }



        private static System.Drawing.Font _heavyTitleFont = null;
        public static System.Drawing.Font HeavyTitleFont
        {
            get
            {
                if (_heavyTitleFont == null)
                {
                    _heavyTitleFont = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                }

                return _heavyTitleFont;
            }
        }

    }
}
