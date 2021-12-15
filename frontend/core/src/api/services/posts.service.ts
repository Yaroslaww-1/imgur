import api from "../api.helper";

import { IPost } from "@models/post.model";

const communitiesEndpoint = "/api/core/communities/";
const postsEndpoint = "/api/core/posts/";

interface IPosts {
  posts: IPost[];
}

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

  static async getPosts(communityId: string): Promise<IPosts> {
    return api.get(communitiesEndpoint + communityId + "/posts");
  }

  static async getPostById(postId: string): Promise<IPost> {
    return api.get(postsEndpoint + postId);
  }
}
