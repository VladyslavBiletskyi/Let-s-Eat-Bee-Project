//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Let_s_Eat_Bee_Project
{
    using System;
    using System.Collections.Generic;
    
    public partial class Joining
    {
        public int Id { get; set; }
        public string TextOfOrder { get; set; }
        public int OrderId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
    }
}
