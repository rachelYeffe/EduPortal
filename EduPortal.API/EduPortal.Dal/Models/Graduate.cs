using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Dal.Models
{
    public class Graduate
    {
        public string AccountNumber { get; set; }              // מספר חשבון
        public string LastName { get; set; }                   // שם משפחה
        public string FirstName { get; set; }                  // שם פרטי+שם אמצעי
        public string Institution { get; set; }                // מוסד

        [Key]
        public string IDNumber { get; set; }                   // מספר תעודת זהות

        public string MobilePhone { get; set; }                // טלפון נייד
        public string Passport { get; set; }                   // מספר דרכון

        public string Street { get; set; }                     // רחוב בית
        public string HouseNumber { get; set; }                // מספר בית בית
        public string Apartment { get; set; }                  // דירה בית
        public string City { get; set; }                       // ישוב בית
        public string Entrance { get; set; }                   // כניסה בית

        public string FatherCity { get; set; }                 // ישוב בית אב
        public string FatherStreet { get; set; }               // רחוב בית אב
        public string FatherHouseNumber { get; set; }          // מספר בית בית אב
        public string FatherEntrance { get; set; }             // כניסה בית אב
        public string FatherApartment { get; set; }            // דירה בית אב

        public string Mail { get; set; }                       // דואר ברירת מחדל
        public string Kind { get; set; }                       // סוג כרטיס
        public string Cycle { get; set; }                      // מחזור
        public string Age { get; set; }                        // גיל

        public string HomePhone { get; set; }                  // טלפון בית אב
        public string AddHomePhone { get; set; }               // טלפון בית נוסף אב
        public string FatherPhone { get; set; }                // טלפון נייד אב
        public string FatherBusinessPhone { get; set; }        // טלפון עסק אב
        public string AddFatherBusinessPhone { get; set; }     // טלפון עסק נוסף אב

        public string Status { get; set; }                     // מצב משפחתי
    }
}
