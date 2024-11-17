using sh_rt.Models;

namespace sh_rt.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Shirt> FavoritesShirts { get; set; }
    }
}
