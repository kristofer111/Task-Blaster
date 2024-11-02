using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class TagService : ITagService
{
    public Task<IEnumerable<TagDto>> GetAllTagsAsync()
    {
        throw new NotImplementedException();
    }

    public Task CreateNewTagAsync(TagInputModel inputModel)
    {
        throw new NotImplementedException();
    }
}