// ICellState.cs
public interface ICellState
{
    void Enter(Cell cell);
    void Tick(Cell cell);      // Tur sonunda çağrılacak
    void Exit(Cell cell);
}


[System.Serializable]
public class DryState : ICellState
{
    public void Enter(Cell cell)
    {
        cell.SetSprite(CellSpriteType.Dry);
    }

    public void Tick(Cell cell)
    {
    }

    public void Exit(Cell cell) { }
}


[System.Serializable]
public class MoistState : ICellState
{
    private int _turnsWet = 0;
    private const int _baseWetTurns = 3;

    // Ek süreyi her çağrıda dinamik hesaplar
    private int WetLimit
        => _baseWetTurns +
           (MutationManager.Instance != null &&
            MutationManager.Instance.HasMutation(MutationType.MoistLonger) ? 2 : 0);

    public void Enter(Cell cell)
    {
        _turnsWet = 0;
        cell.SetSprite(CellSpriteType.Moist);
    }

    public void Tick(Cell cell)
    {
        // Önce Kış kontrolü – donarsa ıslaklığı sıfırlamaya gerek yok
        if (SeasonManager.Instance.Current.freezesMoist)
        {
            cell.ChangeState(new FrozenState());
            return;
        }

        _turnsWet++;

        if (_turnsWet >= WetLimit)
            cell.ChangeState(new DryState());
    }

    public void Exit(Cell cell) { }
}

[System.Serializable]
public class FrozenState : ICellState
{
    private int _thawTimer = 0;

    public void Enter(Cell cell)
    {
        cell.SetSprite(CellSpriteType.Frozen);
        _thawTimer = SeasonManager.Instance.Current.durationTurns; // kış bitince çöz
    }

    public void Tick(Cell cell)
    {
        _thawTimer--;
        if (_thawTimer <= 0 && !SeasonManager.Instance.Current.freezesMoist)
            cell.ChangeState(new DryState());
    }

    public void Exit(Cell cell) { }
}
