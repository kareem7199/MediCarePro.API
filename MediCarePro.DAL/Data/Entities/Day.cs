using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.DAL.Data.Entities
{
	public enum Day
	{
		[EnumMember(Value = "Saturday")]
		Saturday,
		[EnumMember(Value = "Sunday")]
		Sunday,
		[EnumMember(Value = "Monday")]
		Monday,
		[EnumMember(Value = "Tuesday")]
		Tuesday,
		[EnumMember(Value = "Wednesday")]
		Wednesday,
		[EnumMember(Value = "Thursday")]
		Thursday,
		[EnumMember(Value = "Friday")]
		Friday
	}
}
