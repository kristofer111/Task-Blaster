using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class TagRepository : ITagRepository
{
    public Task CreateNewTagAsync(TagInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TagDto>> GetAllTagsAsync()
    {
        throw new NotImplementedException();
    }
}