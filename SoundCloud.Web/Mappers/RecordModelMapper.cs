#region

using Adam.Core;
using Adam.Core.Management;
using Adam.Core.Records;
using SoundCloud.Web.Models;

#endregion

namespace SoundCloud.Web.Mappers
{
    public static class RecordModelMapper
    {
        public static SoundViewModel ToSoundModel(this Record record)
        {
            var createdBy = "Unnamed";
            var user = new User(ApplicationHelper.ApplicationHelper.GetApplication());
            if (user.TryLoad(record.Files.Master.Versions.Latest.CreatedBy) == TryLoadResult.Success)
                createdBy = user.Name;
            var m = new SoundViewModel
            {
                File = record.Files.Master.Versions.Latest.GetPath(new FileVersionPathOptions()),
                FileImage = record.Files.Master.Versions.Latest.GetThumbnail().GetPath(),
                CreatedBy = createdBy
            };
            return m;
        }
    }
}