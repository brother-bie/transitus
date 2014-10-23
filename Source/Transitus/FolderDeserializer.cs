using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public class FolderDeserializer : IFolderDeserializer
	{
		protected readonly IFileDeserializer _deserializer;

		public FolderDeserializer(IFileDeserializer deserializer)
		{
			_deserializer = deserializer;
		}

		public IEnumerable<IItem> Deserialize(string folderPath, bool recursive = true)
		{
			var items = new List<IItem>();

			ReadItems(folderPath, recursive, items);

			return items;
		}

		public void ReadItems(string folderPath, bool recursive, IList<IItem> items)
		{
			var files = Directory.GetFiles(folderPath);

			foreach (var file in files)
			{
				if (file.EndsWith(_deserializer.ItemFileExtension))
				{
					var item = _deserializer.Deserialize(file);

					if (item != null)
					{
						items.Add(item);

						if (recursive)
						{
							var folderName = file.Substring(0, file.Length - _deserializer.ItemFileExtension.Length);

							if (IsValidDirectory(folderName))
							{
								ReadItems(folderName, recursive, items);
							}
						}
					}
				}
			}
		}

		public bool IsValidDirectory(string folderPath)
		{
			return Directory.Exists(folderPath) && !IsDirectoryHidden(folderPath);
		}

		public bool IsDirectoryHidden(string folderPath)
		{
			return IsHidden(new DirectoryInfo(folderPath));
		}

		public bool IsHidden(FileSystemInfo info)
		{
			return (bool)((info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden);
		}
	}
}
