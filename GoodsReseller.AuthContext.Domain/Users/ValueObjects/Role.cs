using GoodsReseller.SeedWork;

namespace GoodsReseller.AuthContext.Domain.Users.ValueObjects
{
    public sealed class Role : Enumeration
    {
        public Role(int id, string name) : base(id, name)
        {
        }

        public static readonly Role Admin = new Role(1, "Admin");
        public static readonly Role Customer = new Role(2, "Customer");
    }
}