using Backend.Models;

namespace Backend.Brains
{
    public interface IAddCategory
    {
        void CreateCategory(BackendProductCategory toCrate);
    }
}