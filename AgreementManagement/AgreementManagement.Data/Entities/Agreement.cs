using System;
using System.Collections.Generic;

#nullable disable

namespace AgreementManagement.Data.Entities
{
    public partial class Agreement
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? ProductGroupId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? NewPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
