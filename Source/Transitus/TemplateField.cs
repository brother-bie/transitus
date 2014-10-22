using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public class TemplateField : ITemplateField
	{
		protected IItem _fieldItem;

		public TemplateField(IItem fieldItem)
		{
			_fieldItem = fieldItem;
		}

		public string Id
		{
			get { return _fieldItem.Id; }
		}

		public string TypeName
		{
			get
			{
				return _fieldItem.SharedFields
									.Where(f => string.Equals(f.Key, "type"))
									.Select(i => i.Value)
									.FirstOrDefault();
			}
		}

		public string TypeKey
		{
			get { return TypeName.ToLower(); }
		}

		public string Name
		{
			get { return _fieldItem.Name; }
		}

		public string Key
		{
			get { return Name.ToLower(); }
		}
	}
}
