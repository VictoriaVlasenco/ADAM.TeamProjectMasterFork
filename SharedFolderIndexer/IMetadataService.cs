using System.Collections.Generic;

namespace SharedFolderIndexer
{
    public interface IMetadataService
    {
        List<RecordMetadata> GetMetadataList();
    }
}