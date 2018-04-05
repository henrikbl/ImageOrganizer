using System.Collections.Generic;

namespace ImageOrganizer.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        /// <value>
        /// The group identifier.
        /// </value>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the pictures.
        /// </summary>
        /// <value>
        /// The pictures.
        /// </value>
        public virtual List<Picture> Pictures { get; set; }
    }
}
