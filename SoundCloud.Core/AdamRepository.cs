#region

using System;
using System.Linq;
using System.Collections.Generic;
using Adam.Core;
using Adam.Core.Classifications;
using Adam.Core.Records;
using Adam.Core.Search;

#endregion

namespace SoundCloud.Core
{
    public class AdamRepository
    {
        private Application app;

        private AdamRepository(Application app)
        {
            this.app = app;
        }

        #region Records

        public static IEnumerable<Record> GetRecordsWithChilds(Application app, string classificationPath)
        {
            var search = new SearchExpression(String.Format("DirectClassification[NamePath={0}]", classificationPath));
            var recordCollection = new RecordCollection(app);
            recordCollection.Load(search);
            return recordCollection;
        }

        public static IEnumerable<Record> GetRecords(Application app, string classificationPath)
        {
            var classificationHelper = new ClassificationHelper(app);
            var id = classificationHelper.GetId(new ClassificationPath(classificationPath));
            if (id != null)
            {
                var search = new SearchExpression(String.Format("Classification[AncestorOrSelf={0}]", id));
                var recordCollection = new RecordCollection(app);
                recordCollection.Load(search);
                return recordCollection;
            }
            return new List<Record>();
        }

        private static List<Record> FindRecords(Application app, SearchExpression searchExpression)
        {
            var recordHelper = new RecordHelper(app);

            var ids = recordHelper.GetIds(searchExpression).ToList();
            List<Record> records = (from recordId in ids let record = new Record(app) where record.TryLoad(recordId) == TryLoadResult.Success select record).ToList();
            return records;
        }

        public static List<Record> FindRecordsSoundCloud(Application app)
        {
            SearchExpression searchExpression = new SearchExpression("Classification.Name=SoundCloud");
            return FindRecords(app, searchExpression);
        }

        #endregion
    }
}