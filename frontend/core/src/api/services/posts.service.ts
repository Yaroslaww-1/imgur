import api from "../api.helper";

import { IPost } from "@models/post.model";

const endpoint = "/api/core/posts";

interface IPosts {
  posts: IPost[];
}

export class PostsService {
  static async createPost(
    name: string,
    content: string,
    image: File | string,
  ): Promise<void> {
    // const formData = new FormData();
    // formData.append("content", content);
    // formData.append("image", image);
    api.post(endpoint, { name, content });
  }

  static async getPosts(): Promise<IPosts> {
    return api.get(endpoint);
  }
}
