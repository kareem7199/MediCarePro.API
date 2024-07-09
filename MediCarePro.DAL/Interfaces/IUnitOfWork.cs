﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.DAL.Interfaces
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IGenericRepository<T> Repository<T>() where T : BaseEntity;
		Task<int> CompleteAsync();
	}
}
