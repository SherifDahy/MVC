using DAL;
using Data;
using Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;


		public UnitOfWork(ApplicationDbContext _context
			, UserManager<ApplicationUser> UserManager
			, RoleManager<IdentityRole<int>> RoleManager
			, SignInManager<ApplicationUser> SignInManager)
			
		{
			this._context = _context;
			Categories = new Repository<Category>(_context);
			Orders = new Repository<Order>(_context);
			Products = new Repository<Product>(_context);
			ShoppingCarts = new Repository<ShoppingCart>(_context);
			Reviews = new Repository<Review>(_context);
			OrderDets = new Repository<OrderDet>(_context);

			this.UserManager = UserManager;
			this.RoleManager = RoleManager;
			this.SignInManager = SignInManager;

		}

		public IRepository<Category> Categories { get; }
		public IRepository<Order> Orders { get; }
		public IRepository<Product> Products { get; }
		public IRepository<ShoppingCart> ShoppingCarts { get; }
		public IRepository<Review> Reviews { get; }


        public UserManager<ApplicationUser> UserManager {  get; }
        public RoleManager<IdentityRole<int>> RoleManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }

        public IRepository<OrderDet> OrderDets {  get; }

        public void Dispose()
		{
			_context.Dispose();
		}

		public int Save()
		{
            return _context.SaveChanges();
        }
	}

}
