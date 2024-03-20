using KIPServiceTestTask.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KIPServiceTestTask.Models
{
    public class Visit
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public DateOnly DateOfVisit { get; set; }
        public Visit(Guid Id, Guid UserId, DateOnly DateOfVisit)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.DateOfVisit = DateOfVisit;
        }
    }
}
