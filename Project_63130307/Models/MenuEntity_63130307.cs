using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_63130307.Models
{
	public class MenuEntity_63130307
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public MenuEntity_63130307()
		{
			this.ItemTypes = new HashSet<ItemType>();
		}

		public long ID { get; set; }
		public string Name { get; set; }
		public string Link { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<ItemType> ItemTypes { get; set; }
	}
}