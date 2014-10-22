using Sitecore.Data.Serialization.ObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus.Sync
{
	public class SyncItemDeserializer : IFileDeserializer
	{
		protected const string _itemFileExtension = ".item";

		public IItem Deserialize(string filePath)
		{
			var syncItem = ReadItem(filePath);
			var wrappedItem = new SyncItemWrapper(syncItem);

			return wrappedItem;
		}

		public string ItemFileExtension
		{
			get { return _itemFileExtension; }
		}

		public SyncItem ReadItem(string filePath)
		{
			using (TextReader reader = (TextReader)new StreamReader((Stream)File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
			{
				var tokenizer = new Tokenizer(reader);
				var syncItem = SyncItem.ReadItem(tokenizer);

				return syncItem;
			}
		}
	}
}
