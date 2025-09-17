namespace SignalRMvcChat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChatMessage")]
    public partial class ChatMessage
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Sender { get; set; }

        [StringLength(50)]
        public string Receiver { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        [StringLength(250)]
        public string Text { get; set; }

        [StringLength(250)]
        public string FilePath { get; set; }

        public DateTime? MessageTime { get; set; }
    }
}
