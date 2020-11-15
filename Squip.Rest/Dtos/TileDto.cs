using System;

namespace Squip.Rest.Dtos
{
    public class TileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // ReSharper disable once UnusedMember.Global
        public string Type { get; set; }
    }
}
