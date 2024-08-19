using api.tags.Model;

namespace api.tags.Interface
{
    public interface IWiptagRepository
    {
        Task<WipTagModel> Insert(WipTagModel entity, CancellationToken cancellationToken);
        Task<IEnumerable<WipTagModel>> Insert(IEnumerable<WipTagModel> entity, CancellationToken cancellationToken);
        Task<List<WipTagModel>> GetAll(CancellationToken cancellationToken);
    }
}
