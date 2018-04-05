using ImageOrganizer.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ImageOrganizer.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class ImageOrganizerContext : DbContext
    {
        /// <summary>
        /// Gets or sets the pictures.
        /// </summary>
        /// <value>
        /// The pictures.
        /// </value>
        public virtual DbSet<Picture> Pictures { get; set; }

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>
        /// The groups.
        /// </value>
        public virtual DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageOrganizerContext"/> class.
        /// </summary>
        public ImageOrganizerContext()
        {
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new ImageOrganizerDBInitializer());
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Group>()
                .HasMany(a => a.Pictures)
                .WithMany(b => b.Groups)
                .Map(m =>
                {
                    m.ToTable("PictureGroup");
                    m.MapLeftKey("PictureId");
                    m.MapRightKey("GroupId");
                });
        }
    }
}
