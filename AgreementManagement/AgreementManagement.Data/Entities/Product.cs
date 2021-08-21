using System;
using System.Collections.Generic;

#nullable disable

namespace AgreementManagement.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            Agreements = new HashSet<Agreement>();
        }

        public int Id { get; set; }
        public int? ProductGroupId { get; set; }
        public string ProductDescription { get; set; }
        public Guid? ProductNumber { get; set; }
        public decimal? Price { get; set; }
        public bool? Active { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
        public virtual ICollection<Agreement> Agreements { get; set; }
    }
}
