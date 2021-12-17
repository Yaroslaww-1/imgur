using MediaLakeCore.Domain.Communities;
using MediaLakeCore.Domain.PostImages;
using MediaLakeCore.Domain.Users;
using System.Collections.Generic;

namespace MediaLakeCore.Domain.Posts
{
    public class PostCreatorDomainService
    {
        public Post CreateNewPost(CommunityId communityId, string name, string content, User createdBy, List<PostImage> draftImages)
        {
            var post = Post.CreateNew(communityId, name, content, createdBy);

            foreach (var draftImage in draftImages)
            {
                draftImage.MarkUsed(post.Id);
            }

            return post;
        }
    }
}
