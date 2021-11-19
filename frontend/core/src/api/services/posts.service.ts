import api from "../api.helper";

const endpoint = "";

export class PostsService {
  static async createPost(
    content: string,
    image: File | string,
  ): Promise<void> {
    const formData = new FormData();
    formData.append("content", content);
    formData.append("image", image);
    api.post(endpoint, formData);
  }
}
