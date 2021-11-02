using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.ModelLayer.Alexa
{
    [Table("SkillTokens")]
   public class SkillToken:SharedTableModelNoUserKey
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
