using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }


    public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
    {
        return await _tagRepository.GetAllTagsAsync();
    }

    public async Task<int?> CreateNewTagAsync(TagInputModel inputModel)
    {
        var newId = await _tagRepository.CreateNewTagAsync(inputModel);
        return newId;
    }
}