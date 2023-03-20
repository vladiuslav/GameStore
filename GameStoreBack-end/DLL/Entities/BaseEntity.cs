
using System.ComponentModel.DataAnnotations;

namespace DLL.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
