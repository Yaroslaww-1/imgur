import api from "../api.helper";

const endpoint = "/api/core/posts";

export class PostsService {
  static async createPost(
    content: string,
    image: File | string,
  ): Promise<void> {
    // const formData = new FormData();
    // formData.append("content", content);
    // formData.append("image", image);
    api.post(endpoint, { name: "empty", content });
  }
}
