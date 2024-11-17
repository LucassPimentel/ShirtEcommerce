using Microsoft.EntityFrameworkCore;
using sh_rt.Context;
using sh_rt.Interfaces;
using sh_rt.Models;

namespace sh_rt.Repositories
{
    public class ShirtRepository : IShirtRepository
    {
        private readonly AppDbContext _context;

        public ShirtRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Shirt> Shirts => _context.Shirts.Include(s => s.Category);

        public IEnumerable<Shirt> PreferredShirts => Shirts.Where(p => p.IsFavoriteShirt == true);

        public Shirt? GetShirtById(int id)
        {
            return _context.Shirts.FirstOrDefault(s => s.Id == id);
        }
    }
}
