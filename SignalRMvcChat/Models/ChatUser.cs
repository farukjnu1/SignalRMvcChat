namespace SignalRMvcChat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChatUser")]
    public partial class ChatUser
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; }
    }
}
