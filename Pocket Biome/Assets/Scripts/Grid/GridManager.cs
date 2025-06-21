using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private int gridSize = 16;
    [SerializeField] private float cellSpacing = 1f;

    private Cell[,] _cells;

    public Cell[,] AllCells => _cells;

    private void Start()
    {
        GenerateGrid();
        EcosystemManager.Instance.Init(this);

    }
    
    public Cell GetCell(Cell origin, Vector2Int offset)
    {
        for (int x = 0; x < gridSize; x++)
            for (int y = 0; y < gridSize; y++)
            {
                if (_cells[x, y] == origin)
                {
                    int nx = x + offset.x;
                    int ny = y + offset.y;
                    if (nx >= 0 && nx < gridSize && ny >= 0 && ny < gridSize)
                        return _cells[nx, ny];
                }
            }
        return null;
    }

    private void GenerateGrid()
    {
        _cells = new Cell[gridSize, gridSize];
        var offset = (gridSize - 1) * cellSpacing * 0.5f;

        for (int x = 0; x < gridSize; x++)
            for (int y = 0; y < gridSize; y++)
            {
                Vector2 pos = new Vector2(x * cellSpacing - offset,
                                          y * cellSpacing - offset);
                Cell newCell = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                _cells[x, y] = newCell;
            }
    }

    public void AdvanceTurn()
    {
        foreach (var cell in _cells) cell.AdvanceTurn();

        SeasonManager.Instance.TickTurn();

        EcosystemManager.Instance.ProcessPlantGrowth();

        EcosystemManager.Instance.ProcessAnimals();
    }


}
