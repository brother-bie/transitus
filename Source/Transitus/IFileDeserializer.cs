using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public interface IFileDeserializer
	{
		string ItemFileExtension { get; }
		IItem Deserialize(string filePath);
	}
}
