using AgreementManagement.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace AgreementManagement.Web.Models
{
    public class AgreementViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductGroupId { get; set; }
        public int ProductId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? NewPrice { get; set; }
        public int Srn { get; set; }


        public string ProductDescription { get; set; }
        public Guid? ProductNumber { get; set; }
        public string ProductGroupDescription { get; set; }
        public Guid? GroupCode { get; set; }
        public string User { get; set; }
    }
}