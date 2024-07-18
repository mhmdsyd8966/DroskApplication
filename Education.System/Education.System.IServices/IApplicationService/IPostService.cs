using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Views;

namespace Education.System.IServices.IApplicationService
{
    public interface IPostService
    {
        Task AddPost(PostDto post);
        Task DeletePost(Guid id);
        Task EditPost(Guid id, EditPost model);
        Task<List<TeacherPost>> GetAllPost();
        Task<List<TeacherPost>> GetAllPostsOfTeacher(string id);
        Task<Post> GetPostById(Guid id);
    }
}
