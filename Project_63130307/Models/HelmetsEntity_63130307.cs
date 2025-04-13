using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_63130307.Models
{
	public class HelmetsEntity_63130307
	{
		public long ID { get; set; }
		public string Name { get; set; }
		public Nullable<Decimal> Sellprice { get; set; }
		public Nullable<int> Quantity { get; set; }
		public string Picture { get; set; }
		public string Status { get; set; }
		public string Describe { get; set; }
		
		public string MenuID { get; set; }
	

	}
}