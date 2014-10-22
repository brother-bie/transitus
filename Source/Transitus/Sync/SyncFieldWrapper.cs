using Sitecore.Data.Serialization.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transitus.Sync
{
	public class SyncFieldWrapper : IField
	{
		protected readonly SyncField _field;

		public SyncFieldWrapper(SyncField field)
		{
			_field = field;
		}

		public string Id
		{
			get { return _field.FieldID; }
		}

		public string Key
		{
			get { return _field.FieldKey; }
		}

		public string Name
		{
			get { return _field.FieldName; }
		}

		public string Value
		{
			get { return _field.FieldValue; }
		}
	}
}
