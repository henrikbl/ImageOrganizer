using System.Collections.Generic;

namespace ImageOrganizer.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// Gets or sets the picture identifier.
        /// </summary>
        /// <value>
        /// The picture identifier.
        /// </value>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the file directory.
        /// </summary>
        /// <value>
        /// The file directory.
        /// </value>
        public string FileDirectory { get; set; }

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>
        /// The groups.
        /// </value>
        public virtual List<Group> Groups { get; set; }
    }
}
