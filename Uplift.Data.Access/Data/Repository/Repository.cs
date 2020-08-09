﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Uplift.Data.Access.Data.Repository.IRepository;

namespace Uplift.DataAccess.Data.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{

		protected readonly DbContext Context;
		internal DbSet<T> dbSet;

		public Repository(DbContext context)
		{
			Context = context;
			this.dbSet = context.Set<T>();
		}
		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public T Get(int id)
		{
			return dbSet.Find(id);
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);

			}


			//include proprties will be comma seperated
			if (includeProperties != null)
			{
				foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperties);
				}
			}

			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			return query.ToList();
		}

		public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (filter != null)
			{
				query = query.Where(filter);

			}


			//include proprties will be comma seperated
			if (includeProperties != null)
			{
				foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperties);
				}
			}

			return query.FirstOrDefault();
		}

		public void Remove(int id)
		{
			T enetityToRemove = dbSet.Find(id);
			Remove(enetityToRemove);
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}
	}
}
