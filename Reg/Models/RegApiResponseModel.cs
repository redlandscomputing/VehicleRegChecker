using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reg.Models
{
    public class CarDetails
    {
        public string HttpStatus { get; set; }
        public string ErrorMessage { get; set; }
        public string AwsRequestId { get; set; }
        public string Registration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string PrimaryColour { get; set; }
        public string MotTestExpiryDate { get; set; } = null;
        public Mottest[] MotTests { get; set; }

        public string NumberOfFailedTests
        {
            get
            {
                if (this.MotTests == null && Registration != null)
                    return "0";
                else
                    return this.MotTests?.Where(motTest => motTest.testResult != "PASSED")?.Count().ToString() ?? null;
            }
        }

        public string MOTExpiry
        {
            get
            {
                return this.MotTests?
                    .Where(motTest => motTest.testResult == "PASSED")?
                    .OrderByDescending(motToOrder => DateTimeOffset.Parse(motToOrder.completedDate))?
                    .FirstOrDefault()?.expiryDate ?? this.MotTestExpiryDate;
            }
        }
    }

    public class Mottest
    {
        public string completedDate { get; set; }
        public string testResult { get; set; }
        public string expiryDate { get; set; }
    }

}
