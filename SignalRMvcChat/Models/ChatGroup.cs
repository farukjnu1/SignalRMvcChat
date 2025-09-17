namespace SignalRMvcChat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChatGroup")]
    public partial class ChatGroup
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }
    }
}
