namespace SharedServicesModule
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Task")]
    public partial class Task
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public int UserId { get; set; }

        public int ProjectId { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual Project Project { get; set; }
    }
}
