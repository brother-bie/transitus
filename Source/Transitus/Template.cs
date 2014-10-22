using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public class Template : ITemplate
	{
		protected readonly IItem _item;
		protected readonly IList<IItem> _items;
		private IList<IItem> _inheritedTemplates;
		private IList<TemplateField> _combinedFields;
		private IList<TemplateField> _localFields;

		public Template(IItem item, IList<IItem> items)
		{
			Sitecore.Diagnostics.Assert.IsTrue(new Sitecore.Data.ID(item.TemplateId).Equals(Sitecore.TemplateIDs.Template), "item must be template");

			_item = item;
			_items = items;
		}

		public string Path
		{
			get { return _item.ItemPath; }
		}

		public string Name
		{
			get { return _item.Name; }
		}

		public string Id
		{
			get { return _item.Id; }
		}

		public string ParentId
		{
			get { return _item.ParentId; }
		}

		public IEnumerable<ITemplateField> CombinedFields
		{
			get
			{
				if (_combinedFields == null)
				{
					var sections = _items.Where(template => CombinedTemplateItems.Select(i => i.Id).Contains(template.ParentId)).ToList();
					var fields = _items.Where(field => sections.Select(i => i.Id).Contains(field.ParentId)).ToList();

					_combinedFields = fields.Select(field => new TemplateField(field)).OrderBy(i => i.Name).ToList();
				}

				return _combinedFields;
			}
		}

		public IEnumerable<ITemplateField> LocalFields
		{
			get
			{
				if (_localFields == null)
				{
					var sections = _items.Where(template => template.ParentId == _item.Id).ToList();
					var fields = _items.Where(field => sections.Select(i => i.Id).Contains(field.ParentId)).ToList();

					_localFields = fields.Select(field => new TemplateField(field)).OrderBy(i => i.Name).ToList();
				}

				return _localFields;
			}
		}

		public IEnumerable<IItem> CombinedTemplateItems
		{
			get
			{
				if (_inheritedTemplates == null)
				{
					_inheritedTemplates = new List<IItem>();

					GetBaseTemplates(_item, _inheritedTemplates);
				}

				return _inheritedTemplates;
			}
		}

		public void GetBaseTemplates(IItem item, IList<IItem> inheritedTemplates)
		{
			if (item != null && !inheritedTemplates.Where(i => i.Id == item.Id).Any())
			{
				inheritedTemplates.Add(item);

				var baseField = item.SharedFields.Where(i => new Sitecore.Data.ID(i.Id) == Sitecore.FieldIDs.BaseTemplate).FirstOrDefault();

				if (baseField != null)
				{
					foreach(var value in baseField.Value.Split('|'))
					{
						var baseItem = _items.Where(i => i.Id == value).FirstOrDefault();

						GetBaseTemplates(baseItem, inheritedTemplates);
					}
				}
			}
		}
	}
}
