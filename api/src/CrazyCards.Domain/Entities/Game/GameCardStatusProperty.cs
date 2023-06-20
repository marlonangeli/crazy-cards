namespace CrazyCards.Domain.Entities.Game;

public class GameCardStatusProperty
{
    public bool Silenced { get; set; } = false;
    public bool Poisoned { get; set; } = false;
    public bool Stealth { get; set; } = false;
    public bool Frozen { get; set; } = false;
    public bool Sleeping { get; set; } = false;
    public bool Dead { get; set; } = false;
    public bool Shielded { get; set; } = false;
    public bool Taunt { get; set; } = false;
}