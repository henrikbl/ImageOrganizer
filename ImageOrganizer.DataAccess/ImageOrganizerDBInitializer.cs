using System.Collections.Generic;
using System.Data.Entity;
using ImageOrganizer.Model;

namespace ImageOrganizer.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.Entity.DropCreateDatabaseIfModelChanges{ImageOrganizer.DataAccess.ImageOrganizerContext}" />
    public class ImageOrganizerDBInitializer : DropCreateDatabaseIfModelChanges<ImageOrganizerContext>
    {
        /// <summary>
        /// A method that should be overridden to actually add data to the context for seeding.
        /// The default implementation does nothing.
        /// </summary>
        /// <param name="context">The context to seed.</param>
        protected override void Seed(ImageOrganizerContext context)
        {
            var Picture1 = context.Pictures.Add(new Picture() { Title = "Vacation", FilePath = @"C:\documents\test1" });
            var Picture2 = context.Pictures.Add(new Picture() { Title = "My friend", FilePath = @"C:\images" });
            var Picture3 = context.Pictures.Add(new Picture() { Title = "Test1", FilePath = @"C:\Users\hen_b\Pictures\EveOnlineWallpaper.jpg" });

            context.Groups.Add(new Group() { Name = "Places", Pictures = new List<Picture>() { Picture1 } });
            context.Groups.Add(new Group() { Name = "People", Pictures = new List<Picture>() { Picture2 } });

            base.Seed(context);
        }
    }
}
