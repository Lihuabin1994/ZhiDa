using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZhiDaCore.Entity.Identity
{
    public class User
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "please enter user name")]
        public string Account { get; set; }
        [Required(ErrorMessage = "please enter password")]
        public string Password { get; set; }
        public string Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModityTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
