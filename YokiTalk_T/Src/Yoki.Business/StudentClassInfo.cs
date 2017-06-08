using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoki.Business
{
    // for start class notify View model
    public class StudentClassInfo
    {
        public Image HeaderImage
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Grade
        {
            get;
            set;
        }

        public Yoki.Core.Gender Gender
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public string ClassTitle
        {
            get;
            set;
        }



    }
}
