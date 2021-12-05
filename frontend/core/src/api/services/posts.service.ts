import api from "../api.helper";

import { IPost } from "@models/post.model";

const endpoint = "/api/core/communities/";

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
    api.post(endpoint + communityId + "/posts", { name, content });
  }

  static async getPosts(communityId: string): Promise<IPosts> {
    return api.get(endpoint + communityId + "/posts");
  }

  static async getPostById(
    communityId: string,
    postId: string,
  ): Promise<IPost> {
    return api.get(endpoint + communityId + "/posts/" + postId);
  }
}
