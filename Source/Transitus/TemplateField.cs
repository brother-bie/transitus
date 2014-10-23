using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public class TemplateField : ITemplateField
	{
		public string Id { get; set; }
		public string TypeName { get; set; }
		public string TypeKey { get; set; }
		public string Name { get; set; }
		public string Key { get; set; }
	}
}
