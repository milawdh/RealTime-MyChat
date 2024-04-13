using Domain.DataLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.File
{
    public interface IFileService
    {
    }
    public class FileService : IFileService
    {
        private readonly Core _core;
        public FileService(Core core)
        {
            this._core = core;
        }


    }
}
