using MediaPlayerProject.Models;
using MediaPlayerProject.Services.SaveMediaFileTimeSpan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayerProject.Commands
{
    public class SaveMediaFileTimeSpanCommand : AsyncCommandBase
    {
        public SaveMediaFileTimeSpanCommand() { }
        public override async Task ExecuteAsync(object? parameter)
        {
            var mediaFile = parameter as MediaFile;
            var sv = App.GetService<ISaveMediaFileTimeSpan>();
            await sv.Save(mediaFile!);
        }
    }
}
