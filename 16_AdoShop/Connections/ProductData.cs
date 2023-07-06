

using System.ComponentModel.DataAnnotations.Schema;

namespace Connections
{
    [Table("Shopping")]
    class ProductData
    {                
        public int ProductCode { get;}
        public string ProductName { get;}
        public ProductData(int productCode, string productName) 
        {           
            ProductCode = productCode;
            ProductName = productName;
        }
    }
}
