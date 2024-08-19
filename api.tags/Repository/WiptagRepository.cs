using api.tags.ContextBase;
using api.tags.Interface;
using api.tags.Model;
using MongoDB.Driver;

namespace api.tags.Repository
{
    public class WiptagRepository : IWiptagRepository
    {
        private readonly MongoDbContext _context;
        public WiptagRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<WipTagModel>> GetAll(CancellationToken cancellationToken)
        {
            var list = await _context.WipTags.Find(_ => true).ToListAsync(cancellationToken);

            return list;
        }

        public async Task<WipTagModel> Insert(WipTagModel entity, CancellationToken cancellationToken)
        {
            await _context.WipTags.InsertOneAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<WipTagModel>> Insert(IEnumerable<WipTagModel> entity, CancellationToken cancellationToken)
        {
            var itemsModel = entity.Select(x => new InsertOneModel<WipTagModel>(x)).ToArray();
            await _context.WipTags.BulkWriteAsync(itemsModel, new BulkWriteOptions { IsOrdered = false }, cancellationToken);

            return entity;
        }
    }
}
