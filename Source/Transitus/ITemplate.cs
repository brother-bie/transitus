using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public interface ITemplate
	{
		string Path { get; }
		string Name { get; }
		string Id { get; }
		string ParentId { get; }
		IEnumerable<ITemplateField> CombinedFields { get; }
		IEnumerable<ITemplateField> LocalFields { get; }
	}
}
