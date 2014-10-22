using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public interface ITemplateFactory
	{
		IEnumerable<ITemplate> Create(IEnumerable<IItem> items);
	}
}
