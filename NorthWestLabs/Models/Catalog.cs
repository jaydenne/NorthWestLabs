using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    //[Table("Client")]
    public class Catalog
    {
        //[DisplayName("Company Name")]
        //public int CompanyName { get; set; }
        
        //[DisplayName("First Name")]
        //public string ContactFirstName { get; set; }
        
        //[DisplayName("Last Name")]
        //public string ContactLastName { get; set; }
                  
        ////This requires that there are 5 numbers in the zip
        //[RegularExpression("\\d{5}", ErrorMessage = "Zipcode must have only 5 digits")]
        //public int Zip { get; set; }
        
        //[EmailAddress]
        //public string Email { get; set; }
        
        ////This Regular Expression makes the phone format of (XXX) XXX-XXXX necessary
        //[RegularExpression("^ ?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$", ErrorMessage = "Phone Number must be in (XXX) XXX-XXX format")]
        //public string Phone { get; set; }

        //[DisplayName("Modified By")]
        //public string ModifiedBy { get; set; }

        //[DisplayName("Modified Date")]
        //public string ModifiedDate { get; set; }

        //[DisplayName("Created By")]
        //public string CreatedBy { get; set; }

        //[DisplayName("Created  Date")]
        //public string CreatedDate { get; set; }
    }
}