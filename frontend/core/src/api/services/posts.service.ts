import api from "../api.helper";

import { IPost } from "@models/post.model";
import { IComment } from "@models/comment.model";

const communitiesEndpoint = "/api/core/communities/";
const postsEndpoint = "/api/core/posts/";

export class PostsService {
  static async createPost(
    communityId: string,
    name: string,
    content: string,
    image: File | string,
  ): Promise<void> {
    // const formData = new FormData();
    // formData.append("content", content);
    // formData.append("image", image);
    api.post(communitiesEndpoint + communityId + "/posts", { name, content });
  }

  static async getPosts(communityId: string): Promise<IPost[]> {
    return api.get(communitiesEndpoint + communityId + "/posts");
  }

  static async getPostById(postId: string): Promise<IPost> {
    return api.get(postsEndpoint + postId);
  }

  static async togglePostLike(postId: string): Promise<void> {
    return api.post(postsEndpoint + postId + "/reactions/toggleLike", "");
  }

  static async togglePostDislike(postId: string): Promise<void> {
    return api.post(postsEndpoint + postId + "/reactions/toggleDislike", "");
  }

  static async getPostComments(postId: string): Promise<IComment[]> {
    return api.get(postsEndpoint + postId + "/comments");
  }

  static async createPostComment(
    postId: string,
    content: string,
  ): Promise<IComment[]> {
    return api.post(postsEndpoint + postId + "/comments", { content });
  }

  static async toggleCommentLike(
    postId: string,
    commentId: string,
  ): Promise<void> {
    return api.post(
      postsEndpoint + postId + "/comments/" + commentId + "/toggleLike",
      "",
    );
  }

  static async toggleCommentDislike(
    postId: string,
    commentId: string,
  ): Promise<void> {
    return api.post(
      postsEndpoint + postId + "/comments/" + commentId + "/toggleDislike",
      "",
    );
  }
}
