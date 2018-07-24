namespace SquipApi.Pocos
{
    public class SquipTag : BaseEntity
    {
        public long SquipId { get; set; }
        public Squip Squip { get; set; }

        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
