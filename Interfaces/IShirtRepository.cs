using sh_rt.Models;

namespace sh_rt.Interfaces
{
    public interface IShirtRepository
    {
        IEnumerable<Shirt> Shirts { get; }
        IEnumerable<Shirt> PreferredShirts { get; }

        Shirt GetShirtById(int id);
    }
}
