using Adam.Core;
using Adam.Core.Management;
using Adam.Core.Records;
using SoundCloud.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adam.Web.UI;
using Adam.Web.UI.Controls;

namespace SoundCloud.Core
{
    public static class RecordToModelMapper
    {
        //public static SoundViewModel ToSoundModel(this Record record)
        //{
        //    //record.Load(new Guid("{5DBA8B5E-9D8B-4ad0-BDCB-04CEA197761C}"));
        //    // ... of a record 
        //    //string path = record.GetPreview().GetPath();
        //    //// ... of a file 
        //    //path = record.Files.Master.GetPreview().GetPath();
        //    //// ... of a file version 
        //    //path = record.Files.Master.Versions.Latest.GetPreview().GetPath();
        //    //path = record.Files.Master.Versions.Latest.File.GetPreview().GetPath();

        //    string createdBy = "Unnamed";
        //    var user = new User(ApplicationHelper.ApplicationHelper.GetApplication());
        //    if (user.TryLoad(record.Files.Master.Versions.Latest.CreatedBy) == TryLoadResult.Success)
        //        createdBy = user.Name;

        //    return new SoundViewModel()
        //    {
        //        File = RecordWebResource.GetFileVersionUrl(record, record.Files.LatestMaster.Id),
        //        FileImage = RecordWebResource.GetFileImageUrl(record.Files.Master, RecordImageType.Preview, ResourcePrepareMode.AheadOfTime),
        //        CreatedBy = createdBy,
        //    };
        //}
    }
}
