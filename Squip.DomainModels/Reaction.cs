namespace Squip.Domain
{
    public class Reaction : DomainModelBase
    {
        public string UserId { get; set; }
        public string PresentationId { get; set; }
        public string ReactionCategory { get; set; }
    }
}
