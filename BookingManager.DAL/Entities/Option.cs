using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManager.DAL.Entities
{
    [Table("Option")]
    public class Option
    {
        public int OptionId { get; set; }
        public string Name { get; set; } = null!;
        public List<Room> Rooms { get; set; } = null!;
    }
}
