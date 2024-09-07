namespace GogoLibrary.Domain.Result;

public class CollectionResult<TEntity> : BaseResult<IEnumerable<TEntity>>
{
    public int Count { get; set; }
}