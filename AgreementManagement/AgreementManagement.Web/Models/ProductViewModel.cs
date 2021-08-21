using AgreementManagement.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace AgreementManagement.Web.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int? ProductGroupId { get; set; }
        public string ProductDescription { get; set; }
        public Guid? ProductNumber { get; set; }
        public decimal? Price { get; set; }
        public bool? Active { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
    }
}