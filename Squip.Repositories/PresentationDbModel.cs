using NodaTime;

namespace Squip.Repositories
{
    public class PresentationDbModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string SquipId { get; set; }
        public Instant InstantCreatedAt { get; set; }
    }
}
