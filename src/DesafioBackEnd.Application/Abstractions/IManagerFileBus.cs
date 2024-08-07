using DesafioBackEnd.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Abstractions
{
    public interface IManagerFileBus
    {
        Task<OperationResult<string>> UploadFile(string fileName, string extension, Stream content);
    }
}
