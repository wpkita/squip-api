using System;
using System.Collections.Generic;
using System.Text;

namespace SquipApi.Pocos
{
    public class User : BaseEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ThirdPartyId { get; set; }
        public string Picture { get; set; }
    }
}

