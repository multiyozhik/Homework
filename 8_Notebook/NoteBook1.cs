using System;

namespace Notebook
{
	struct NoteBook
	{
		public string Name { get; set; }
		public string Street { get; set; }
		public string HouseNumber { get; set; }
		public string FlatNumber { get; set; }
		public string MobilePhone { get; set; }
		public string FlatPhone { get; set; }

		public NoteBook(string name, string street, string houseNumber, string flatNumber, string mobilePhone, string flatPhone)
		{
			Name = name;
			Street = street;
			HouseNumber = houseNumber;
			FlatNumber = flatNumber;
			MobilePhone = mobilePhone;
			FlatPhone = flatPhone;
		}
	}
}