namespace TeamCitySharp.DomainEntities
{
    public class NewProjectDescription
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public ParentProjectWrapper ParentProject { get; set; }
    }
}
