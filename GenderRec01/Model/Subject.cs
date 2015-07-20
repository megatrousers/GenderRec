using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GenderRec01.Model
{
    public enum GenderType { UNKNOWN, MALE, FEMALE };
    class Subject
    {
        private BitmapImage baseImage;
        private byte[] image1d;
        private string name;
        private string path;
        private GenderType gender;

        #region constructor(s)
        public Subject()
        {
        }
        #endregion

        #region Properties
        public BitmapImage BaseImage
        {
            get { return baseImage; }
            set { baseImage = value; }
        }
        public byte[] Image1d
        {
            get { return image1d; }
            set { image1d = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        public GenderType Gender
        {
            get { return gender; }
            set { gender = value; }
        }


        #endregion
    }
}
