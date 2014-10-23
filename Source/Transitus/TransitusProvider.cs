using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transitus.Sync;

namespace Transitus
{
	public static class TransitusProvider
	{
		public static IFileDeserializer FileDeserializer = new SyncItemDeserializer();
		public static IFolderDeserializer FolderDeserializer = new FolderDeserializer(FileDeserializer);
		public static ITemplateFactory TemplateFactory = new TemplateFactory();
	}
}
