using System;
using System.Collections.Generic;

#nullable disable

namespace AgreementManagement.Data.Entities
{
    public partial class ProductGroup
    {
        public ProductGroup()
        {
            Agreements = new HashSet<Agreement>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string GroupDescription { get; set; }
        public Guid? GroupCode { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Agreement> Agreements { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
