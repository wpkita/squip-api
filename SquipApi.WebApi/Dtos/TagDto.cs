using System.Collections.Generic;
using SquipApi.Pocos;

namespace SquipApi.WebApi.Dtos
{
    public class TagDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<Squip> Squips { get; set; }
    }
}
