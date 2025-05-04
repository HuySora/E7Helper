public enum StatType {
    Null = 0,
    Attack = 1,
    AttackPercent,
    Defense,
    DefensePercent,
    Health,
    HealthPercent,
    Speed,
    EffectivenessPercent,
    EffectResistancePercent,
    CriticalHitChancePercent,
    CriticalHitDamagePercent
}

public class Stat {
    public Stat(StatType type, decimal value, int rollCount) {
        Type = type;
        Value = value;
        RollCount = rollCount;
    }

    public StatType Type { get; }
    public decimal Value { get; }
    public int RollCount { get; private set; }

    public decimal GetGearScore() {
        if (!Constants.StatType2GearScoreMultiplier.TryGetValue(Type, out var mul))
            return 0;
        return Value * mul;
    }
}