//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace demoTraining.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            this.TraineeEnrollments = new HashSet<TraineeEnrollment>();
        }
    
        public int CourseID { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string CourseDescription { get; set; }
        [Required]
        public Nullable<int> TopicID { get; set; }
        [Required]
        public Nullable<int> CategoryID { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Topic Topic { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TraineeEnrollment> TraineeEnrollments { get; set; }
    }
}
