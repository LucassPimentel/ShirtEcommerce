using sh_rt.Models;

namespace sh_rt.ViewModels
{
    public class GetShirtListViewModel
    {
        public IEnumerable<Shirt> Shirts { get; set; }

        public string CurrentCategory { get; set; }
    }
}
