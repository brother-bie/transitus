using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus
{
	public interface IField
	{
		string Id { get; }
		string Key { get; }
		string Name { get; }
		string Value { get; }
	}
}
