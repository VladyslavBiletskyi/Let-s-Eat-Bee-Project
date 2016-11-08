using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Let_s_Eat_Bee_Project.Models
{
    public class NewOrder
    {
        public int UserId { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input First Name")]
        public string FirstName { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input Last Name")]
        public string LastName { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input Address")]
        public string Address { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input Date in format dd:MM:yyyy")]
        public string Date { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input Time in format hh:mm")]
        public string Time { set; get; }
        public string TextOfOrder { set; get; }
    }
}