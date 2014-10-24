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
			var templateItems = GetTemplates(items);
			var templates = new List<ITemplate>();

			foreach (var templateItem in templateItems)
			{
				var combinedTemplateItems = GetCombinedBaseTemplates(templateItem, items);
				var combinedSections = GetCombinedSections(combinedTemplateItems, items);
				var combinedFields = GetFields(combinedSections, items);
				var localSections = GetLocalSections(templateItem, items);
				var localFields = GetFields(localSections, items);
				var baseTemplateIds = GetBaseTemplatesIds(templateItem, combinedTemplateItems);
				var template = new Template
				{
					Id = templateItem.Id,
					Name = templateItem.Name,
					ParentId = templateItem.ParentId,
					Path = templateItem.ItemPath,
					CombinedFields = combinedFields,
					LocalFields = localFields,
					BaseTemplateIds = baseTemplateIds
				};

				templates.Add(template);
			}

			return templates;
		}

		public IEnumerable<IItem> GetCombinedBaseTemplates(IItem item, IEnumerable<IItem> items)
		{
			var baseTemplates = new List<IItem>();

			GetCombinedBaseTemplates(item, baseTemplates, items);

			return baseTemplates;
		}

		public void GetCombinedBaseTemplates(IItem item, IList<IItem> baseTemplates, IEnumerable<IItem> items)
		{
			if (item != null && !baseTemplates.Where(i => i.Id == item.Id).Any())
			{
				baseTemplates.Add(item);

				var baseTemplateField = item.SharedFields.Where(i => IsBaseTemplateField(i.Id)).FirstOrDefault();

				if (baseTemplateField != null)
				{
					foreach (var value in baseTemplateField.Value.Split('|'))
					{
						var baseTemplateItem = items.Where(i => i.Id == value).FirstOrDefault();

						GetCombinedBaseTemplates(baseTemplateItem, baseTemplates, items);
					}
				}
			}
		}

		public IEnumerable<string> GetBaseTemplatesIds(IItem templateItem, IEnumerable<IItem> combinedTemplateItems)
		{
			return combinedTemplateItems.Where(item => !string.Equals(item.Id, templateItem.Id)).Select(item => item.Id);
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

		public IEnumerable<IItem> GetCombinedSections(IEnumerable<IItem> combinedTemplateItems, IEnumerable<IItem> items)
		{
			return items.Where(item => combinedTemplateItems.Select(i => i.Id).Contains(item.ParentId)).ToList();
		}

		public IEnumerable<IItem> GetLocalSections(IItem templateItem, IEnumerable<IItem> items)
		{
			return items.Where(item => item.ParentId == templateItem.Id).ToList();
		}

		public IEnumerable<IItem> GetTemplates(IEnumerable<IItem> items)
		{
			return items.Where(i => IsTemplate(i.TemplateId));
		}

		public bool IsBaseTemplateField(string id)
		{
			return new ID(id) == Sitecore.FieldIDs.BaseTemplate;
		}

		public bool IsTemplate(string id)
		{
			return new ID(id) == Sitecore.TemplateIDs.Template;
		}
	}
}
