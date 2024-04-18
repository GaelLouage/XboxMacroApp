using SharpDX.XInput;
using XboxMacroApp.Models;

namespace XboxMacroApp.Services.Interfaces
{
    public interface IJsonSerivce
    {
        Task<List<ProgramModel>?> GetProgramsAsync();
        Task<(bool IsSuccess, string Message)> AddProgramAsync(ProgramModel program);
        Task<(bool IsSuccess, string Message)> UpdateKeyAsync(ProgramModel program, GamepadButtonFlags xboxKey);
        Task<(bool IsSuccess, string Message)> DeleteProgramAsync(ProgramModel program);
    }
}