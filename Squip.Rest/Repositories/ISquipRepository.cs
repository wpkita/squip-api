using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Squip.Rest.Domain;

namespace Squip.Rest.Repositories
{
    public interface ISquipRepository
    {
        Task<Idea> GetRandomIdea();
        Task<Tuple<Idea, Idea>> GetRandomIdeaPair();
    }
}
