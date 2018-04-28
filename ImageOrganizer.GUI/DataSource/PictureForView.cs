
using ImageOrganizer.Model;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageOrganizer.GUI
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ImageOrganizer.Model.Picture" />
    public class PictureForView : Picture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PictureForView"/> class.
        /// </summary>
        /// <param name="picture">The picture.</param>
        /// <param name="image">The image.</param>
        public PictureForView(Picture picture, BitmapImage image)
            : base(picture.Title, picture.base64ImageString)
        {
            this.Image = image;
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public BitmapImage Image { get; set; }
    }
}
