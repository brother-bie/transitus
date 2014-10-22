using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public interface ITemplateField
	{
		string Id { get; }
		string TypeName { get; }
		string TypeKey { get; }
		string Name { get; }
		string Key { get; }
	}
}
