using Adam.Core;
using Adam.Core.Management;
using Adam.Core.Records;
using Adam.Web.UI;
using Adam.Web.UI.Controls;
using SoundCloud.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoundCloud.WebUI.Mappers
{
    public static class RecordModelMapper
    {
        public static SoundViewModel ToSoundModel(this Record record)
        {
            string createdBy = "Unnamed";
            var user = new User(ApplicationHelper.ApplicationHelper.GetApplication());
            if (user.TryLoad(record.Files.Master.Versions.Latest.CreatedBy) == TryLoadResult.Success)
                createdBy = user.Name;
            var m = new SoundViewModel()
            {
                File = record.Files.Master.Versions.Latest.GetPath(new FileVersionPathOptions()),
                FileImage = record.Files.Master.Versions.Latest.GetThumbnail().GetPath(),
                CreatedBy = createdBy
            };
            return m;
        }
    }
}