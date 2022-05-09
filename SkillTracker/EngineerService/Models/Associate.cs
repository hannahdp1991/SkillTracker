using DataAnnotationsExtensions;

namespace EngineerService.Models
{
    public class Associate
    {
        [Max(30)]
        [Min(5)]
        public string Name { get; set; }

        [Max(30)]
        [Min(5)]
        public int AssociateId { get; set; }

        [Email]
        public string Email { get; set; }

        [Max(10)]
        [Min(10)]
        public int Mobile { get; set; }
    }
}
