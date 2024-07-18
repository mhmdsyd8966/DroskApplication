using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.IServices.IApplicationService;
using Education.System.Presentation.Context;
using Education.System.IServices.IdentityService;
using Education.System.Core.Views;

namespace Education.System.Services.ApplicationService
{
    public class PostService(TheLayerContext context, ITeacherService teacherService) : IPostService
    {

        public async Task AddPost(PostDto post)
        {
            if (string.IsNullOrEmpty(post.Content))
                throw new Exception("Post Content Can't Be Null. Please Try Again");

            var addPost = new Post
            {
                Content = post.Content,
                TeacherId = post.TeacherId,
                CreatedAt = DateTime.Now,
                PostPhoto = post.PostPhoto
            };
            context.Posts.Add(addPost);
            await context.SaveChangesAsync();
            await teacherService.IncrementNumberOfPostsBy(post.TeacherId, 1);
        }

        public async Task DeletePost(Guid id)
        {
            var post = await GetPostById(id);
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            await teacherService.IncrementNumberOfPostsBy(post.TeacherId, -1);
        }

        public async Task EditPost(Guid id, EditPost model)
        {
            var post = await GetPostById(id);
            post.Content = model.Content;
            post.PostPhoto = model.PostImage;
            context.Posts.Update(post);
            await context.SaveChangesAsync();

        }
        public async Task<List<TeacherPost>> GetAllPost()
        {
            var posts = await context.TeacherPosts.OrderByDescending(x => x.CreatedAt).ToListAsync();
            return posts;
        }
        public async Task<List<TeacherPost>> GetAllPostsOfTeacher(string id)
        {
            var posts = await context.TeacherPosts.Where(x => x.TeacherId == id).OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return posts;
        }
        public async Task<Post> GetPostById(Guid id)
        {
            var posts = await context.Posts.FirstOrDefaultAsync(x => x.Id == id) ??
                        throw new Exception("No post with this id");
            return posts;
        }
    }
}
