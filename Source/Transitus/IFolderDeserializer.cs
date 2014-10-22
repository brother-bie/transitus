using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public interface IFolderDeserializer
	{
		IEnumerable<IItem> Deserialize(string folderPath, bool recursive);
	}
}
