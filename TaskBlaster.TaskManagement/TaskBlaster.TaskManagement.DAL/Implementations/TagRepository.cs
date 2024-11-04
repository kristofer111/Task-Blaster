using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Contexts;
using TaskBlaster.TaskManagement.DAL.Entities;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.DAL.Implementations;

public class TagRepository : ITagRepository
{
    private readonly TaskManagementDbContext _taskManagementDbContext;

    public TagRepository(TaskManagementDbContext taskManagementDbContext)
    {
        _taskManagementDbContext = taskManagementDbContext;
    }


    public async Task<int?> CreateNewTagAsync(TagInputModel inputModel)
    {
        bool ans = await _taskManagementDbContext.Tags.AnyAsync(t => t.Name == inputModel.Name);

        if (ans)
        {
            return null;
        }

        var tag = new Tag
        {
            Name = inputModel.Name,
            Description = inputModel.Description
        };

        await _taskManagementDbContext.AddAsync(tag);
        await _taskManagementDbContext.SaveChangesAsync();

        return tag.Id;
    }

    public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
    {
        return await _taskManagementDbContext.Tags.Select(t => new TagDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description
        }).ToListAsync();
    }
}