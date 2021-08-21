using AgreementManagement.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace AgreementManagement.Web.Models
{
    public class ProductGroupViewModel
    {
        public int Id { get; set; }
        public string GroupDescription { get; set; }
        public Guid? GroupCode { get; set; }
        public bool? Active { get; set; }
    }
}