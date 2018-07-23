namespace SquipApi.WebApi.Models
{
    public class SquipTag
    {
        public long SquipId { get; set; }
        public Squip Squip { get; set; }

        public string TagName { get; set; }
        public Tag Tag { get; set; }
    }
}