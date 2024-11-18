using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUnitOfWork: IDisposable
	{
		public IRepository<Category> Categories { get; }
		public IRepository<Order> Orders { get; }
		public IRepository<Product> Products { get; }
		public IRepository<ShoppingCart> ShoppingCarts { get; }
		public IRepository<Review> Reviews { get; }

		public IRepository<OrderDet> OrderDets { get; }

		public UserManager<ApplicationUser> UserManager { get; }
		public RoleManager<IdentityRole<int>> RoleManager { get; }
		public SignInManager<ApplicationUser> SignInManager { get; }

		int Save();
		public void Dispose();


	}
}
