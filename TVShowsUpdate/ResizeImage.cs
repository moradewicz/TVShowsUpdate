using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Imaging;


namespace TVShowsUpdate
{
    class ResizeImage
    {
        public  Stream ToStream( Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }
        public Stream resize(Stream stream)
        {
            ImageFactory imageFactory = new ImageFactory();
           
            imageFactory.Load(stream);
            imageFactory.Resize(new ResizeLayer(new Size(160, 200), ResizeMode.Crop, AnchorPosition.Center, false));
            imageFactory.Save(stream);
            return stream;
        }

    }
}
