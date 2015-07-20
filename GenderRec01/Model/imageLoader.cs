using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GenderRec01.Model
{
    class ImageLoader
    {
        List<BitmapImage> images;

        public ImageLoader()
        {
            images = new List<BitmapImage>();
        }

        public List<BitmapImage> Load(string path, string[] fileNames)
        {
            try 
	        {
                foreach (string fileName in fileNames)
                {
                    images.Add(new BitmapImage(new Uri(string.Format(@"{0}\{1}", path, fileName))));
                }
	        }
	        catch (Exception exc)
	        {
		        string msg = "problem loading image(s).";
                
	        }
            return images;
        }
       
    }
}
