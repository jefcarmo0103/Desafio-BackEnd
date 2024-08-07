using DesafioBackEnd.Application.Abstractions;
using DesafioBackEnd.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Infra.Core.ManagerFileBroker
{
    public class ManagerFileBus : IManagerFileBus
    {
        public async Task<OperationResult<string>> UploadFile(string fileName, string extension, Stream content)
        {
            var path = Environment.GetEnvironmentVariable("LOCALPATH_PHOTOS");
            ArgumentNullException.ThrowIfNull(nameof(path));

            if (string.IsNullOrEmpty(fileName))
                return OperationResult<string>.Fail("Nome do arquivo é obrigatório para upload");

            if (string.IsNullOrEmpty(extension) && extension.Length > 4)
                return OperationResult<string>.Fail("Extensão do arquivo é obrigatório para upload");
                
            var identityFile = Guid.NewGuid();
            var outputPath = $"{path}/{identityFile}.{extension}";
            using (FileStream fs = new(outputPath, FileMode.Create))
                await content.CopyToAsync(fs);
            
            return OperationResult<string>.Ok($"{identityFile}{extension}", "Operação executada com sucesso");
        }
    }
}
