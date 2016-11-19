using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Let_s_Eat_Bee_Project.Models
{
    public class NewOrder
    {
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

    public class EditOrder
    {
        public int OrderID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input Address")]
        public string Address { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input Date in format dd:MM:yyyy")]
        public string Date { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input Time in format hh:mm")]
        public string Time { set; get; }
        public string Text { set; get; }
    }
    public class NewJoining
    {
        public int OrderID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input Your first name")]
        public string UserFirstName { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input Your last name")]
        public string UserLastName { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Input text of your order")]
        public string Text { set; get; }
    }

}