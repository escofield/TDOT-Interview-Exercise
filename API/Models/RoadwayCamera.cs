namespace IO.Swagger.Models
{
    /// roadway camera
    public class RoadwayCamera
    {
        /// thumbnail url
        public string thumbnailUrl { get; set; }
        /// jurisdiction
        public string jurisdiction { get; set; }
        /// video url
        public string httpVideoUrl { get; set; }
        /// active
        public bool active { get; set; }
        /// id
        public int id { get; set; }
        /// route
        public string route { get; set; }
        /// title
        public string title { get; set; }
    }
}