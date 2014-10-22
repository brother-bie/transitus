using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public class TemplateFactory : ITemplateFactory
	{
		public IEnumerable<ITemplate> Create(IEnumerable<IItem> items)
		{
			var templates = items
								.Where(i => new Sitecore.Data.ID(i.TemplateId) == Sitecore.TemplateIDs.Template)
								.Select(i => new Template(i, items))
								.ToList();

			return templates;
		}
	}
}
