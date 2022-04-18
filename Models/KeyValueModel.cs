using System.ComponentModel.DataAnnotations;

namespace KeyValueIoT.Models
{
    public class KeyValueModel
    {
        [Key]
        public string Key { get; set; }
        
        public string Value { get; set; }
    }
}
