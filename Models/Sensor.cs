using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensorList.Models;

public class Sensor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Category { get; set; } = string.Empty;
    
    [Range(0, int.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
    public int Amount { get; set; } = 0;
}
