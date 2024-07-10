using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediCarePro.BLL.Dtos;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.Resolvers
{
	internal class FullNameResolver : IValueResolver<Account, PhysicianDto, string>
	{
		public string Resolve(Account source, PhysicianDto destination, string destMember, ResolutionContext context)
		{
			return $"{source.FirstName} {source.SecondName}";
		}
	}
}
