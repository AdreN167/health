namespace Health.Core.Features.WorkoutEventJournal.Dtos;

public class WorkoutEventDto
{
    public long Id { get; set; }
    public string Date { get; set; }
    public double BurnedCalories { get; set; }
    public long WorkoutId { get; set; }
}

