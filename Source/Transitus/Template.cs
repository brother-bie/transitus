using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public class Template : ITemplate
	{
		public string Path { get; set; }
		public string Name { get; set; }
		public string Id { get; set; }
		public string ParentId { get; set; }
		public IEnumerable<ITemplateField> CombinedFields { get; set; }
		public IEnumerable<ITemplateField> LocalFields { get; set; }
		public IEnumerable<IItem> CombinedTemplateItems { get; set; }
	}
}
