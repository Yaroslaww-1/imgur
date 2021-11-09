﻿using MediaLakeCore.Domain.Users;
using System;
using System.Collections.Generic;

namespace MediaLakeCore.Domain.Posts
{
    public class Post
    {
        public PostId Id { get; private set; }
        public string Name { get; private set; }
        public string Content { get; private set; }
        public User CreatedBy { get; private set; }
        public List<PostComment> Comments { get; private set; }

        private Post()
        {
            // Only for EF
        }

        private Post(PostId chatId, string name, string content, User createdBy, List<PostComment> comments)
        {
            Id = chatId;
            Name = name;
            Content = content;
            CreatedBy = createdBy;
            Comments = comments;
        }

        public static Post CreateNew(string name, string content, User createdBy)
        {
            var chatId = new PostId(Guid.NewGuid());

            return new Post(chatId, name, content, createdBy, new List<PostComment>() { });
        }
    }
}
