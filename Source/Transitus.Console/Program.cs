using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Console.SetWindowSize(150, 40);

			var folderDeserializer = TransitusProvider.FolderDeserializer;
			var templateFactory = TransitusProvider.TemplateFactory;
			var items = folderDeserializer.Deserialize(@"C:\Work\Git\Transitus\TestSerialisedFiles\sitecore");
			var templates = templateFactory.Create(items);

			foreach (var template in templates)
			{
				var templateOutput = new StringBuilder();

				templateOutput.AppendLine(string.Format("{0} | {1}", template.Name, template.Path));

				System.Console.Write(templateOutput);
			}
		}
	}
}
