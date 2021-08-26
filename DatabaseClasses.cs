using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	public class DBProduct 
    {
		public string Name {get; set;}
		public int CategoryID {get; set;}
    }
	public class DBCategory 
    {
		public int ID {get; set;}
		public string Name {get; set;}
		
    }

	public class DBObjConstruction
	{
		private List<DBProduct> products;
		private List<DBCategory> categories;
		public DBObjConstruction()
		{
			this.categories = new List<DBCategory>()
			{
			new DBCategory(){ID =1, Name="Fruit"},
			new DBCategory(){ID =2, Name="Food"},
			new DBCategory(){ID =3, Name="Shoe"},
			new DBCategory(){ID =4, Name="Juice"},
			};
			this.products = new List<DBProduct>()
			{
			new DBProduct(){ Name="Strowberry",CategoryID =1},
			new DBProduct(){ Name="Apple",CategoryID =1},
			new DBProduct(){ Name="Meat",CategoryID =2},
			new DBProduct(){ Name="Orange Juice",CategoryID =4},
			new DBProduct(){ Name="Fish",CategoryID =2},
			new DBProduct(){ Name="Apple juice",CategoryID =4},
			new DBProduct(){ Name="Sandal",CategoryID =3},
			}; 
		}
		public List<DBCategory> Categories
		{
			get{return this.categories;}
			set{this.categories =value;}
		}
		public List<DBProduct> Products
		{
			get{return this.products;}
			set{this.products =value;}
		}
		
		
	}
}

