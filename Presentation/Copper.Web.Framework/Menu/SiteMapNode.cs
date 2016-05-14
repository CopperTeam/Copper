using System.Collections.Generic;

//code from Telerik MVC Extensions
namespace Copper.Web.Framework.Menu
{
    public class SiteMapNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteMapNode"/> class.
        /// </summary>
        public SiteMapNode()
        {
            ChildNodes = new List<SiteMapNode>();
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
        /// <summary>
        ///  Gets or sets the item mvc area
        /// </summary>
        public string AreaName { get; set; }
        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        /// <value>The name of the controller.</value>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the Action.
        /// </summary>
        /// <value>The name of the Action.</value>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the child nodes.
        /// </summary>
        /// <value>The child nodes.</value>
        public IList<SiteMapNode> ChildNodes { get; set; }

        /// <summary>
        /// Gets or sets the image path
        /// </summary>
        /// <value>The name of the image path.</value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the item is visible
        /// </summary>
        /// <value>A value indicating whether the item is visible</value>
        public bool Visible { get; set; }

        
    }
}
