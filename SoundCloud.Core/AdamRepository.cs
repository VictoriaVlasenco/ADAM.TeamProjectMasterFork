#region

using System;
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
       

        #endregion
    }
}