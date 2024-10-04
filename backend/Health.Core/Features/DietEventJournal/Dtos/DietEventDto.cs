namespace Health.Core.Features.DietEventJournal.Dtos;

public class DietEventDto
{
    public long Id { get; set; }
    public string Date { get; set; }
    public double Calories { get; set; }
    public double Fats { get; set; }
    public double Proteins { get; set; }
    public double Carbohydrates { get; set; }
    public long DietId { get; set; }
}

