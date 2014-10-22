using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public class TemplateProvider : ITemplateProvider
	{
		public IEnumerable<ITemplate> Create(IEnumerable<IItem> items)
		{
			throw new NotImplementedException();
		}
	}
}
