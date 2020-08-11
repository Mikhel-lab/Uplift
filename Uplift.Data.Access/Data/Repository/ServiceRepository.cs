using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
	public class ServiceRepository : Repository<Service>, IServiceRepository
	{
		private readonly ApplicationDbContext _db;

		public ServiceRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		//public IEnumerable<SelectListItem> GetCategoryListForDropDown()
		//{
		//	return _db.Category.Select(i => new SelectListItem()
		//	{
		//		Text = i.Name,
		//		Value = i.Id.ToString()
		//	}); ;
			
		//}

		public void Update(Category category)
		{
			var objFromdb = _db.Category.FirstOrDefault(s => s.Id == category.Id);

			objFromdb.Name = category.Name;
			objFromdb.DisplayOrder = category.DisplayOrder;

			_db.SaveChanges();
		}

		public void Update(Service service)
		{
			var objFromdb = _db.Service.FirstOrDefault(s => s.Id == service.Id);

			objFromdb.Name = service.Name;
			objFromdb.LongDesc = service.LongDesc;
			objFromdb.Price = service.Price;
			objFromdb.ImageUrl = service.ImageUrl;
			objFromdb.FrequencyId = service.FrequencyId;
			objFromdb.CategoryId = service.CategoryId;

			_db.SaveChanges();
		}
	}
}
