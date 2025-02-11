using NuGet.DependencyResolver;
using System.ComponentModel.DataAnnotations;

namespace MamunTutorial.Models
{
    public class Packages
    {
            [Key]
            public Guid PackageId { get; set; } // Unique identifier for the pricing record.

            public Guid ProgramName { get; set; } // ID of the program this pricing is associated with.

            public int TotalClasses { get; set; } // Total number of classes included in the pricing package.

            public decimal TotalBill { get; set; } // Total bill amount for the pricing package.

            public DateTime StartDate { get; set; } // Start date of the pricing period.

            public DateTime EndDate { get; set; } // End date of the pricing period.

            public string Grade { get; set; } // Grade or level for which this pricing applies (e.g., Beginner, Advanced).

           
            public string PackageName { get; set; } // Name of the package (e.g., "Starter Plan", "Pro Plan").

            public bool IsActive
            {
                get
                {
                    var today = DateTime.Now;
                    return StartDate <= today && EndDate >= today;
                }
            } // Indicates if the pricing is currently active.

            public string Description { get; set; } // Description of the pricing package (optional, for additional details).

            public bool isDisCount { get; set; } 

            public decimal discount { get; set; }

            public List<Teachers> AssignedTeachers { get; set; }


    }
}
