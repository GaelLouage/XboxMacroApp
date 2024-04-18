using Newtonsoft.Json;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XboxMacroApp.Helpers;
using XboxMacroApp.Models;
using XboxMacroApp.Services.Interfaces;


namespace XboxMacroApp.Services.Classes
{
    public class JsonSerivce : IJsonSerivce
    {
        private string _fileName;
        public JsonSerivce(string fileName)
        {
            _fileName = fileName;
            if (!System.IO.File.Exists(_fileName))
            {
                var fs = File.Create(_fileName);
                fs.Close();
            }
        }
        public async Task<List<ProgramModel>?> GetProgramsAsync()
        {
            var file = await File.ReadAllTextAsync(_fileName);
            return JsonConvert.DeserializeObject<List<ProgramModel>>(file);
        }
        public async Task<(bool IsSuccess, string Message)> AddProgramAsync(ProgramModel program)
        {
            List<ProgramModel>? programs = await GetProgramsAsync();
            if (programs is null)
            {
                programs = new List<ProgramModel>();
            }
            var oldCount = programs.Count;
            var programCheckAvailability = programs
                .FirstOrDefault(x => x.FilePath == program.FilePath);
            if (string.IsNullOrEmpty(program.FileName)
             || string.IsNullOrEmpty(program.FilePath))
            {
                return (false, "FileName and FilePath are required.");
            }
            //TODO:validation program  => place in separate method
            if (programCheckAvailability is not null)
            {
                return (false, "List already contains data!");
            }
          
         
            //add the item to the database
            programs.Add(program);
            await FileHelper.WriteListToJsonFileAsync(_fileName, programs);
            var newCount = (await GetProgramsAsync()).Count;
            if (newCount > oldCount)
            {
                return (true, "Successfully updated programs");
            }
            return (false, "Failed to update");
        }
        public async Task<(bool IsSuccess, string Message)> UpdateKeyAsync(ProgramModel program, GamepadButtonFlags xboxKey)
        {
            List<ProgramModel>? programs = await GetProgramsAsync();
            var keyTaken = programs.Any(p => p.AssignedKey == xboxKey);
            if (keyTaken)
            {
                return (false,"Key is not available!");
            }
            // remove the old data
            var programToUpdate = programs.FirstOrDefault(x => x.FilePath == program.FilePath);
            if(programToUpdate is null)
            {
                return (false,"Program not found!");
            }
            program.AssignedKey = xboxKey;
            programs.Remove(programToUpdate);
            // add the new data
            programs.Add(program);
            // update the file
            await FileHelper.WriteListToJsonFileAsync(_fileName,programs);
            return (true,"Updated!");
        }
        public async Task<(bool IsSuccess, string Message)> DeleteProgramAsync(ProgramModel program)
        {
            var programs = await GetProgramsAsync() ?? new List<ProgramModel>();
            var programToDelete = programs
                .FirstOrDefault(x => x.FilePath == program.FilePath);
            var oldCount = 0;
            if(programs.Count > 0)
            {
                 oldCount = programs.Count;
            }
            
            if(programToDelete is null)
            {
                return (false, $"Database does not conctains {program.FileName}");
            }
            programs.Remove(programToDelete);
            await FileHelper.WriteListToJsonFileAsync(_fileName, programs);
            var newCount = (await GetProgramsAsync()).Count;
            var checkIfSuccess = newCount < oldCount;
            if(checkIfSuccess) 
            {
                return (checkIfSuccess, "Deleted program from database!");
            }
            return (checkIfSuccess, "Failed to remove program!");
        }

    }
}
