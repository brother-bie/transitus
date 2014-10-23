using Sitecore.Data;
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
			var templateItems = items.Where(i => IsTemplate(i.TemplateId));
			var templates = new List<ITemplate>();

			foreach (var templateItem in templateItems)
			{
				var combinedTemplateItems = GetBaseTemplates(templateItem, items);
				var combinedSections = items.Where(item => combinedTemplateItems.Select(i => i.Id).Contains(item.ParentId)).ToList();
				var combinedFields = GetFields(combinedSections, items);
				var localSections = items.Where(item => item.ParentId == templateItem.Id).ToList();
				var localFields = GetFields(localSections, items);
				var template = new Template
				{
					Id = templateItem.Id,
					Name = templateItem.Name,
					ParentId = templateItem.ParentId,
					Path = templateItem.ItemPath
				};

				template.CombinedTemplateItems = combinedTemplateItems;
				template.CombinedFields = combinedFields;
				template.LocalFields = localFields;

				templates.Add(template);
			}

			return templates;
		}

		public IEnumerable<IItem> GetBaseTemplates(IItem item, IEnumerable<IItem> items)
		{
			var baseTemplates = new List<IItem>();

			GetBaseTemplates(item, baseTemplates, items);

			return baseTemplates;
		}

		public void GetBaseTemplates(IItem item, IList<IItem> inheritedTemplates, IEnumerable<IItem> items)
		{
			if (item != null && !inheritedTemplates.Where(i => i.Id == item.Id).Any())
			{
				inheritedTemplates.Add(item);

				var baseField = item.SharedFields.Where(i => IsBaseTemplate(i.Id)).FirstOrDefault();

				if (baseField != null)
				{
					foreach (var value in baseField.Value.Split('|'))
					{
						var baseItem = items.Where(i => i.Id == value).FirstOrDefault();

						GetBaseTemplates(baseItem, inheritedTemplates, items);
					}
				}
			}
		}

		public IEnumerable<ITemplateField> GetFields(IEnumerable<IItem> sections, IEnumerable<IItem> items)
		{
			var fieldItems = items.Where(field => sections.Select(i => i.Id).Contains(field.ParentId)).ToList();
			var fields = new List<ITemplateField>();

			foreach (var fieldItem in fieldItems)
			{
				var typeName = fieldItem.SharedFields
										.Where(f => string.Equals(f.Key, "type"))
										.Select(i => i.Value)
										.FirstOrDefault();

				var field = new TemplateField
				{
					Id = fieldItem.Id,
					Name = fieldItem.Name,
					Key = fieldItem.Name.ToLower(),
					TypeName = typeName,
					TypeKey = typeName.ToLower()
				};

				fields.Add(field);
			}

			return fields.OrderBy(field => field.Name).ToList();
		}

		public bool IsBaseTemplate(string id)
		{
			return new ID(id) == Sitecore.FieldIDs.BaseTemplate;
		}

		public bool IsTemplate(string id)
		{
			return new ID(id) == Sitecore.TemplateIDs.Template;
		}
	}
}
